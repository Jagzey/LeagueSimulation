using System.Data.SQLite;

namespace LeagueSimulation
{
    public class GameGenerator
    {
        // attributes for both teams
        private int gameId; // used for box score
        public int possession; // which team currently has possession
        private int currentSaveState; // which database are we currently accessing
        private string currentUser; // am i working on the project from home or school?
        public string connectionString; // string to connect to database
        public List<string> CommentatorPhrases = new List<string>();
        public List<string> ScoreAfterEachPhrase = new List<string>();
        private PlayerInGame playerWithBall; // which player has the ball
        private PlayerInGame playerWhoPassed; // player who passed to player with the ball
        private PlayerInGame playerWithBallMatchup; // matchup with player with the ball
        private League currentLeague;
        public Team team1;
        public Team team2;
        private List<Player> team1Players = new List<Player>();
        private List<Player> team1Starters = new List<Player>();
        public List<PlayerInGame> team1Stats = new List<PlayerInGame>();
        public List<PlayerInGame> team1StarterStats = new List<PlayerInGame>(); // used for probabilities in possesions
        private List<Player> team2Players = new List<Player>();
        private List<Player> team2Starters = new List<Player>();
        public List<PlayerInGame> team2Stats = new List<PlayerInGame>();
        public List<PlayerInGame> team2StarterStats = new List<PlayerInGame>(); // used for probabilities in possesions


        public int GameId { get; set; }
        public League CurrentLeague { get; set; }
        public string CurrentUser { get; set; }
        public int CurrentSaveState { get; set; }

        private (List<Player>, List<Player>) ExtractPlayersFromTeams(Team team1, Team team2)
        {
            List<Player> teamOnePlayers = new List<Player>();
            List<Player> teamTwoPlayers = new List<Player>();
            Dictionary<int, int> rosterSpots = CalculateRosterSpots(team1.TeamId).Concat(CalculateRosterSpots(team2.TeamId)).ToDictionary(kv => kv.Key, kv => kv.Value);
            // extract the players from team1 and team2 and put them into their respective players lists
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string teamPlayersQuery = $@"
                    WITH currentPlayers AS (
	                    SELECT playerId, teamId
	                    FROM playerOnTeam
	                    WHERE dayJoined <= {CurrentLeague.CurrentDay} AND yearJoined <= {CurrentLeague.CurrentSeason + 2023}
	                    AND dayLeft >= {CurrentLeague.CurrentDay} AND yearLeft >= {CurrentLeague.CurrentSeason + 2023}
                    )
                    SELECT 
                    p.playerId,
                    p.height,
                    p.weight,
                    p.playerForename,
                    p.playerSurname,
                    p.closeShot,
                    p.layup,
                    p.dunk,
                    p.midRange,
                    p.threePoint,
                    p.freeThrow,
                    p.passing,
                    p.ballHandle,
                    p.defense,
                    p.steal,
                    p.block,
                    p.rebound,
                    p.speed,
                    p.strength,
                    p.stamina,
                    p.overall,
                    cp.teamId,
                    pos.positionShort AS positionShort,
                    sp.playstyle AS secondaryPlaystyle,
                    pp.playstyle AS primaryPlaystyle
                    FROM
                        currentPlayers cp
                    JOIN
                        players p ON p.playerId = cp.playerId
                    LEFT JOIN
                        position pos ON p.positionId = pos.positionId
                    LEFT JOIN
                        secondaryPlaystyle sp ON p.secondaryPlaystyleId = sp.secondaryPlaystyleId
                    LEFT JOIN
                        primaryPlaystyle pp ON sp.primaryPlaystyleId = pp.primaryPlaystyleId
                    WHERE
                        cp.teamId = {team1.TeamId}
                        OR cp.teamId = {team2.TeamId};
                ";
                using (var command = new SQLiteCommand(teamPlayersQuery, connection))
                {
                    // this reader, will extract all the players from team1, and put them into the list
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int currentTeamId = reader.GetInt32(reader.GetOrdinal("teamId"));                            // extract data from a player in database, and put into a player class
                            Player player = new Player();
                            player.PlayerId = reader.GetInt32(reader.GetOrdinal("playerId"));
                            player.position = reader.GetString(reader.GetOrdinal("positionShort"));
                            player.PrimaryPlaystyle = reader.GetString(reader.GetOrdinal("primaryPlaystyle"));
                            player.SecondaryPlaystyle = reader.GetString(reader.GetOrdinal("secondaryPlaystyle"));
                            player.RosterSpot = rosterSpots[player.PlayerId];
                            player.Height = reader.GetInt32(reader.GetOrdinal("height")); // height in inches
                            player.Weight = reader.GetInt32(reader.GetOrdinal("weight")); // weight in lbs
                            player.playerForename = reader.GetString(reader.GetOrdinal("playerForename"));
                            player.playerSurname = reader.GetString(reader.GetOrdinal("playerSurname"));
                            player.TeamId = reader.GetInt32(reader.GetOrdinal("teamId"));
                            player.teamName = team1.teamName;
                            player.CloseShot = reader.GetInt32(reader.GetOrdinal("closeShot"));
                            player.Layup = reader.GetInt32(reader.GetOrdinal("layup"));
                            player.Dunk = reader.GetInt32(reader.GetOrdinal("dunk"));
                            player.MidRange = reader.GetInt32(reader.GetOrdinal("midRange"));
                            player.ThreePoint = reader.GetInt32(reader.GetOrdinal("threePoint"));
                            player.FreeThrow = reader.GetInt32(reader.GetOrdinal("freeThrow"));
                            player.Passing = reader.GetInt32(reader.GetOrdinal("passing"));
                            player.BallHandle = reader.GetInt32(reader.GetOrdinal("ballHandle"));
                            player.Defense = reader.GetInt32(reader.GetOrdinal("defense"));
                            player.Steal = reader.GetInt32(reader.GetOrdinal("steal"));
                            player.Block = reader.GetInt32(reader.GetOrdinal("block"));
                            player.Rebound = reader.GetInt32(reader.GetOrdinal("rebound"));
                            player.Speed = reader.GetInt32(reader.GetOrdinal("speed"));
                            player.Strength = reader.GetInt32(reader.GetOrdinal("strength"));
                            player.Stamina = reader.GetInt32(reader.GetOrdinal("stamina"));
                            player.Overall = reader.GetInt32(reader.GetOrdinal("overall"));
                            // add this extracted player to players list
                            if (currentTeamId == team1.TeamId)
                            {
                                player.teamName = team1.teamName;
                                teamOnePlayers.Add(player);
                            }
                            else if (currentTeamId == team2.TeamId)
                            {
                                player.teamName = team2.teamName;
                                teamTwoPlayers.Add(player);
                            }
                        }
                    }
                }

                return (teamOnePlayers, teamTwoPlayers);
            }
        }

        public Dictionary<int, int> CalculateRosterSpots(int teamId)
        {
            Dictionary<int, int> rosterSpots = new Dictionary<int, int>();
            // initially, we get the highest overalls for each position, then sort by rosterSpot
            string getRosterSpotQuery = $@"
                WITH currentPlayers AS (
                SELECT 
                    playerId, 
                    teamId
                FROM 
                    playerOnTeam pot
                WHERE 
                    dayJoined <= {CurrentLeague.CurrentDay} AND yearJoined <= {CurrentLeague.CurrentSeason + 2023}
                    AND dayLeft >= {CurrentLeague.CurrentDay} AND yearLeft >= {CurrentLeague.CurrentSeason + 2023}
            ),
            RankedByPosition AS (
                SELECT
                    p.playerId,
                    p.teamId,
                    p.positionId,
                    p.overall,
                    ROW_NUMBER() OVER (
                        PARTITION BY p.positionId
                        ORDER BY p.overall DESC
                    ) AS positionRank
                FROM
                    players p
                JOIN
                    teams t ON p.teamId = t.teamId
                JOIN
                    currentPlayers cp ON p.playerId = cp.playerId
                WHERE
                    p.teamId = {teamId} -- Filter by the given team
            ),
            TopFive AS (
                SELECT
                    rbp.playerId,
                    rbp.teamId,
                    rbp.positionId,
                    rbp.overall,
                    rbp.positionRank,
                    ROW_NUMBER() OVER (
                        ORDER BY rbp.positionId, rbp.overall DESC
                    ) AS rosterSpot
                FROM
                    RankedByPosition rbp
                WHERE
                    rbp.positionRank = 1 -- Only the top player in each position
                LIMIT 5 -- Ensure exactly 5 players (one per position)
            ),
            RemainingPlayers AS (
                SELECT
                    rbp.playerId,
                    rbp.teamId,
                    rbp.positionId,
                    rbp.overall,
                    rbp.positionRank,
                    ROW_NUMBER() OVER (
                        ORDER BY rbp.positionId, rbp.overall DESC
                    ) + 5 AS rosterSpot
                FROM
                    RankedByPosition rbp
                JOIN
                    players p ON p.playerId = rbp.playerId
                JOIN
                    teams t ON p.teamId = t.teamId
                WHERE 
                    rbp.positionRank > 1
                --LIMIT 10 -- Only include the remaining 10 players
            )
            SELECT
                playerId,
                teamId,
                positionId,
                overall,
                rosterSpot
            FROM (
                SELECT * FROM TopFive
                UNION ALL
                SELECT * FROM RemainingPlayers
            ) rosteredPlayers
            ORDER BY
                rosterSpot;

                ";
            using (var connection = new SQLiteConnection(CurrentLeague.ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getRosterSpotQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) rosterSpots.Add(reader.GetInt32(reader.GetOrdinal("playerId")), reader.GetInt32(reader.GetOrdinal("rosterSpot")));
                    }
                }
            }
            return rosterSpots;
        }

        private void AddPlayersIntoInGame()
        {
            foreach (Player player in team1Players)
            {
                PlayerInGame playerInGame = new PlayerInGame(player);
                team1Stats.Add(playerInGame);
            }

            foreach (Player player in team2Players)
            {
                PlayerInGame playerInGame = new PlayerInGame(player);
                team2Stats.Add(playerInGame);
            }
        }

        private void AddStartersFromInGamePlayers()
        {
            foreach (PlayerInGame player in team1Stats)
            {
                if (player.playerStats.RosterSpot < 6)
                {
                    team1StarterStats.Add(player);
                }

            }
            foreach (PlayerInGame player in team2Stats)
            {
                if (player.playerStats.RosterSpot < 6)
                {
                    team2StarterStats.Add(player);
                }
            }
        }

        private void AddStartersIntoInGame()
        {
            foreach (Player player in team1Starters)
            {
                PlayerInGame playerInGame = new PlayerInGame(player);
                team1StarterStats.Add(playerInGame);
            }

            foreach (Player player in team2Starters)
            {
                PlayerInGame playerInGame = new PlayerInGame(player);
                team2StarterStats.Add(playerInGame);
            }
        }

        public (bool, string) CheckForDraw()
        {
            int score1 = team1Stats.Sum(x => x.Points);
            int score2 = team2Stats.Sum(x => x.Points);
            return (score1 == score2, $"{score1}-{score2}");
        }


        public void CheckForSubstitutions(int rotationSlot)
        {
            // here 'rotationSlot' represents the index of the rotation slot. e.g. 2 represents the 2nd slot
            // we are calculating what substitutions happen for team 1
            List<PlayerInGame> team1FutureStarters = new List<PlayerInGame>();
            team1FutureStarters = team1Stats.Where(x => x.SlotsPlaying.Contains(rotationSlot)).ToList();
            if (team1FutureStarters.Count != 5) { }
            // we cycle through each futureStarter, and make sure we get the commentator phrases right
            foreach (PlayerInGame futureStarter in team1FutureStarters)
            {
                // we cycle through each player on the court
                foreach (PlayerInGame currentStarter in team1StarterStats)
                {
                    if (futureStarter == currentStarter) break;
                    // if the futureStarter and currentStarter are the same person, we don't do anything
                    if (currentStarter.playerStats.position == futureStarter.playerStats.position && futureStarter != currentStarter)
                    {
                        string teamName = currentStarter.playerStats.teamName;
                        string currentPlayerName = currentStarter.playerStats.playerForename + " " + currentStarter.playerStats.playerSurname;
                        string futurePlayerName = futureStarter.playerStats.playerForename + " " + futureStarter.playerStats.playerSurname;
                        CommentatorPhrases.Add($"{teamName}: {currentPlayerName} substituted for {futurePlayerName}");
                        (int, int) score = CalculateScore();
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                    }
                }
            }

            // here 'rotationSlot' represents the index of the rotation slot. e.g. 2 represents the 2nd slot
            // we are calculating what substitutions happen for team 2
            List<PlayerInGame> team2FutureStarters = new List<PlayerInGame>();
            team2FutureStarters = team2Stats.Where(x => x.SlotsPlaying.Contains(rotationSlot)).ToList();
            if (team2FutureStarters.Count != 5) { }
            // we cycle through each futureStarter, and make sure we get the commentator phrases right
            foreach (PlayerInGame futureStarter in team2FutureStarters)
            {
                // we cycle through each player on the court, 
                foreach (PlayerInGame currentStarter in team2StarterStats)
                {
                    // if the futureStarter and currentStarter are the same person, we don't do anything
                    if (currentStarter.playerStats.position == futureStarter.playerStats.position && futureStarter != currentStarter)
                    {
                        string teamName = currentStarter.playerStats.teamName;
                        string currentPlayerName = currentStarter.playerStats.playerForename + " " + currentStarter.playerStats.playerSurname;
                        string futurePlayerName = futureStarter.playerStats.playerForename + " " + futureStarter.playerStats.playerSurname;
                        CommentatorPhrases.Add($"{teamName}: {currentPlayerName} substituted for {futurePlayerName}");
                        (int, int) score = CalculateScore();
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                    }
                }
            }
            team1StarterStats = team1FutureStarters;
            team2StarterStats = team2FutureStarters;

        }

        public void SetOvertimeRotation()
        {
            List<PlayerInGame> overtimeTeam1Starters = new List<PlayerInGame>();
            List<PlayerInGame> team1InRosterOrder = team1Stats.OrderBy(x => x.playerStats.RosterSpot).ToList();
            for (int i = 0; i < 5; i++) overtimeTeam1Starters.Add(team1InRosterOrder[i]);

            List<PlayerInGame> overtimeTeam2Starters = new List<PlayerInGame>();
            List<PlayerInGame> team2InRosterOrder = team2Stats.OrderBy(x => x.playerStats.RosterSpot).ToList();
            for (int i = 0; i < 5; i++) overtimeTeam2Starters.Add(team2InRosterOrder[i]);
            if (overtimeTeam1Starters.Count != 5 || overtimeTeam2Starters.Count != 5) { }

            team1StarterStats = overtimeTeam1Starters;
            team2StarterStats = overtimeTeam2Starters;
        }

        public void SimulatePossessions(int gameId, int numPossessions, bool playoffs)
        {
            CommentatorPhrases = new List<string>();
            // function is used to simulate possessions of a basketball game
            Random random = new Random();
            bool possessionsComplete = false;
            bool startingPossession = true;
            bool overtime = false;
            int possessionCounter = -1;
            int rotationSlot = 1;
            int numPasses = 0;
            GameClock gameClock = new GameClock();
            int endOfGameTime = 48;

            // we write a nice message to the player about which teams are playing
            // we do this only if the game just started, and it's not overtime
            if (!overtime)
            {
                string team1Record = CurrentLeague.GetTeamRecord(team1.teamName);
                string team2Record = CurrentLeague.GetTeamRecord(team2.teamName);
                if (playoffs)
                {
                    team1Record = CurrentLeague.GetSeriesRecordByRound(team1.TeamId.ToString(), team2.TeamId.ToString(), CurrentLeague.GetConferenceIdFromTeamId(team1.TeamId.ToString()));
                    team2Record = CurrentLeague.GetSeriesRecordByRound(team2.TeamId.ToString(), team1.TeamId.ToString(), CurrentLeague.GetConferenceIdFromTeamId(team1.TeamId.ToString()));
                    CommentatorPhrases.Add($"Welcome player! Today, we are watching the {team1.teamName} ({team1Record}) vs. {team2.teamName} ({team2Record}) live in the {CurrentLeague.PlayoffsRound} of the playoffs. Enjoy!");
                }
                else CommentatorPhrases.Add($"Welcome player! Today, we are watching the {team1.teamName} ({team1Record}) vs. {team2.teamName} ({team2Record}) live. Enjoy!");
                ScoreAfterEachPhrase.Add($"0-0");
            }

            // we continue calculating possessions until the number of possessions in a game is reached
            while (gameClock.Minutes <= endOfGameTime && !possessionsComplete)
            {
                // this checks whether we need to do any substitutions as we move towards the next rotation slot
                if (gameClock.Minutes == rotationSlot && gameClock.Minutes != endOfGameTime)
                {
                    rotationSlot++;
                    if (overtime) SetOvertimeRotation();
                    else CheckForSubstitutions(rotationSlot);
                }


                // this checks if regular time has completed, then check if we need overtime
                if (gameClock.Minutes >= endOfGameTime)
                {
                    possessionsComplete = true;
                    // now we add gameValue to each player
                    foreach (PlayerInGame player in team1Stats)
                    {
                        player.GameValue = UpdateGameValue(player);
                    }
                    foreach (PlayerInGame player in team2Stats)
                    {
                        player.GameValue = UpdateGameValue(player);
                    }

                    // here we check if the game ended in a draw, then go into OT if so
                    if (CheckForDraw().Item1)
                    {
                        CommentatorPhrases.Add($"The game {team1Stats[0].playerStats.teamName} vs. {team2Stats[0].playerStats.teamName} resulted in a draw! We're going into overtime!");
                        CommentatorPhrases.Add($"The score was {CheckForDraw().Item2}");
                        (int, int) score = CalculateScore();
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                        possessionsComplete = false;
                        numPossessions += 40;
                        endOfGameTime += 5;
                        overtime = true;
                    }
                    else
                    {
                        // now we add the stats accumulated, into the games table; as a playoff game
                        InsertPlayerGameData(gameId, playoffs);

                        // we add the final commentator phrase and score to the lists
                        CommentatorPhrases.Add($"The game {team1.teamName} vs. {team2.teamName} has come to an end as the clock runs out.");
                        (int, int) score = CalculateScore();
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                        // Now we work out the name of the winner of the game, then display it
                        string nameOfWinner = "";
                        if (team1Stats.Sum(x => x.Points) > team2Stats.Sum(x => x.Points)) nameOfWinner = team1.teamName;
                        else nameOfWinner = team2.teamName;
                        CommentatorPhrases.Add($"The game score finished as {score.Item1}-{score.Item2}, as the win goes to the {nameOfWinner}. ");
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                    }
                }
                // when the game has not finished, we continue simulating possessions
                else
                {
                    // we get the list of offensive and defensive stats
                    List<PlayerInGame> offenseStarterStats = new List<PlayerInGame>();
                    List<PlayerInGame> defenseStarterStats = new List<PlayerInGame>();
                    // team 1 has the ball for this possession
                    if (possession == 1)
                    {
                        offenseStarterStats = team1StarterStats;
                        defenseStarterStats = team2StarterStats;
                    }
                    // team 2 has the ball for this possession
                    else if (possession == 2)
                    {
                        offenseStarterStats = team2StarterStats;
                        defenseStarterStats = team1StarterStats;
                    }
                    if (offenseStarterStats == defenseStarterStats) { }
                    string oPlayerName = "";
                    string oPlayerTeamName = "";
                    string dPlayerName = "";
                    string dPlayerTeamName = "";
                    // we calculate who starts with the ball, if the possession just started
                    if (startingPossession)
                    {
                        // we increment the possession counter
                        possessionCounter++;
                        if (gameClock.Minutes == 40) { };
                        // this orders the players in the list by ballHandle and in game assists
                        // the higher the player's ball handle, the higher chance they start with the ball
                        offenseStarterStats = offenseStarterStats.OrderByDescending(x => x.playerStats.BallHandle + 2 * x.playerStats.Passing + 3 * x.Assists).ToList();
                        double randomStarterProbability = random.NextDouble();
                        if (randomStarterProbability < 0.37) playerWithBall = offenseStarterStats[0];
                        else if (randomStarterProbability < 0.63) playerWithBall = offenseStarterStats[1];
                        else if (randomStarterProbability < 0.78) playerWithBall = offenseStarterStats[2];
                        else if (randomStarterProbability < 0.90) playerWithBall = offenseStarterStats[3];
                        else playerWithBall = offenseStarterStats[4];
                        // now we set the matchup for playerWithBall
                        foreach (PlayerInGame playerInGame2 in defenseStarterStats)
                        {
                            if (playerInGame2.playerStats.position == playerWithBall.playerStats.position)
                            {
                                playerWithBallMatchup = playerInGame2;
                                break;
                            }
                        }
                    }

                    // we set the playerWithBall and his matchup's name, for the commentator phrases
                    oPlayerName = $"{playerWithBall.playerStats.playerForename} {playerWithBall.playerStats.playerSurname}";
                    oPlayerTeamName = $"{playerWithBall.playerStats.teamName}";
                    dPlayerName = $"{playerWithBallMatchup.playerStats.playerForename} {playerWithBallMatchup.playerStats.playerSurname}";
                    dPlayerTeamName = $"{playerWithBallMatchup.playerStats.teamName}";

                    // list of probabilities of events during a single offensive possession for an offensive player

                    double prob3PAttempted = 0;
                    // events after a 3 point is attempted
                    double prob3PMade = 0.34;
                    double prob3PBlocked = 0.01;
                    double prob3PMissed = 0.65;

                    double prob2PAttempted = 0;
                    // events after a 2 point is attempted
                    double prob2PMade = 0.40;
                    double prob2PBlocked = 0.006;
                    double prob2PMissed = 0.586;

                    double probLayupAttempted = 0;
                    // events after a layup is attempted
                    double probLayupMade = 0.55;
                    double probLayupBlocked = 0.062;
                    double probLayupMissed = 0.418;

                    double probDunkAttempted = 0;
                    // events after a dunk is attempted
                    double probDunkMade = 0.54;
                    double probDunkBlocked = 0.05;
                    double probDunkMissed = 0.50;

                    double probPassAttempted = 0;
                    //events after a pass is attempted
                    double probPassMade = 0.87;
                    double probPassStolen = 0.13;

                    double passerContribution = 1;
                    if (playerWhoPassed != null && playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                    {
                        // convert pass from (45 - 99) to (0 - 0.25)
                        passerContribution += 0.25 * (playerWhoPassed.playerStats.Passing - 45) / (99 - 45);
                        passerContribution += (playerWhoPassed.Assists * 0.002) / 1.16;
                    }
                    if (passerContribution > 1.18) { }

                    double probFoulAfterShot = 0;
                    //double probTurnover = 0;
                    if (playerWithBall.playerStats.Overall > 96) { };
                    List<double> attemptedEventProbabilites = new List<double>();
                    attemptedEventProbabilites = CalculateEventProbabilities(prob3PAttempted, prob2PAttempted, probLayupAttempted, probDunkAttempted, probPassAttempted, passerContribution);

                    // set the attempted event probabilities based on function above
                    prob3PAttempted = attemptedEventProbabilites[0];
                    prob2PAttempted = attemptedEventProbabilites[1];
                    probLayupAttempted = attemptedEventProbabilites[2];
                    probDunkAttempted = attemptedEventProbabilites[3];
                    probPassAttempted = attemptedEventProbabilites[4];

                    // if a rebound occurred, we calculate who has possession in a different section
                    bool reboundOccurred = false;

                    // in this section of code, we calculate which event occurs in the possession
                    {
                        // work out if a three point will be attempted, then events after
                        double randomAttemptedProbability = random.NextDouble();
                        if (randomAttemptedProbability < prob3PAttempted)
                        {
                            // this works out the probability a 3 point shot is made
                            double oThreePointStat = playerWithBall.playerStats.ThreePoint;
                            double heightDifference = playerWithBall.playerStats.Height - playerWithBallMatchup.playerStats.Height;
                            double dBlockStat = playerWithBallMatchup.playerStats.Block;
                            double dDefenseStat = playerWithBallMatchup.playerStats.Defense;

                            // convert 3 point stat (40-99) to (-0.12 - 0.12)
                            oThreePointStat = -0.07 + (0.07 + 0.07) * (oThreePointStat - 40) / (99 - 40);
                            if (playerWithBall.ThreePointMade > 3 && playerWithBall.GameValue > 14 && playerWithBall.playerStats.SecondaryPlaystyle != "Playmaker" && playerWithBall.playerStats.PrimaryPlaystyle == "Offensive") oThreePointStat += 0.020 + 0.002 * (playerWithBall.ThreePointMade - 2);
                            if (playerWithBall.ThreePointMade > 10) oThreePointStat -= 0.0007 * (playerWithBall.FieldGoalMade - 10);
                            // convert height difference (-8 - 8) to (-0.07 to 0.07)
                            heightDifference = -0.07 + (0.07 + 0.07) * (heightDifference + 8) / (8 + 8);
                            // convert block stat (40-99) to (-0.009 - 0.045)
                            dBlockStat = -0.009 + (0.045 + 0.009) * (dBlockStat - 40) / (99 - 40);
                            // convert defense stat (40-99) to (-0.10 - 0.10)
                            dDefenseStat = -0.10 + (0.10 + 0.10) * (dDefenseStat - 40) / (99 - 40);
                            dDefenseStat *= -1;

                            // this sets the probabilities of the forthcoming events
                            prob3PMade += (oThreePointStat + heightDifference + dDefenseStat);
                            prob3PMade *= passerContribution;
                            prob3PBlocked += dBlockStat;
                            probFoulAfterShot = 0.01;
                            prob3PMissed = 1 - (prob3PMade + prob3PBlocked);

                            // this occurs if a 3 point shot is made
                            double randomThreePointProbability = random.NextDouble();
                            if (randomThreePointProbability < prob3PMade)
                            {
                                CommentatorPhrases.Add($"{oPlayerName} made a three for the {oPlayerTeamName}.");


                                playerWithBall.FieldGoalAttempted++;
                                playerWithBall.FieldGoalMade++;
                                playerWithBall.ThreePointAttempted++;
                                playerWithBall.ThreePointMade++;
                                playerWithBall.Points += 3;
                                numPasses = 0;
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                // this validates if the player who passed, exists and is on the same team as player who scored
                                if (playerWhoPassed == null)
                                {
                                    // now we change possession
                                    //if (possession == 1) possession = 2;
                                    //else possession = 1;
                                    //continue;
                                }
                                else if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                {
                                    int assistEvent = CalculateAssistProbability();
                                    // this is if an assist event
                                    if (assistEvent == 1)
                                    {
                                        CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");

                                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    }
                                }
                            }
                            // this occurs if a three point is blocked
                            else if (randomThreePointProbability < prob3PMade + prob3PBlocked)
                            {
                                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} was blocked by {dPlayerName} of the {dPlayerTeamName} on his three point shot!");

                                playerWithBall.FieldGoalAttempted++;
                                playerWithBall.ThreePointAttempted++;
                                playerWithBallMatchup.Blocks++;
                                numPasses = 0;
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                            }
                            // this now occurs if a three point shot is missed
                            else
                            {
                                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a three.");

                                playerWithBall.FieldGoalAttempted++;
                                playerWithBall.ThreePointAttempted++;

                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                int reboundEvent = CalculateReboundProbability();
                                reboundOccurred = true;
                                // this is if the ball goes out of bounds or a defensive rebound
                                if (reboundEvent == -1 || reboundEvent == 0)
                                {
                                    if (possession == 1) possession = 2;
                                    else possession = 1;
                                    numPasses = 0;
                                }
                                // this is if an offensive rebound occurred
                                else
                                {
                                    // possession remains with the offensive team
                                    startingPossession = false;
                                    continue;
                                }
                            }
                            // work out who has possession if no rebound occurred
                            if (possession == 1 && !reboundOccurred) possession = 2;
                            else if (!reboundOccurred) possession = 1;
                            startingPossession = true;
                        }
                        // work out if a two point will be attempted
                        else if (randomAttemptedProbability < (prob2PAttempted) + prob3PAttempted)
                        {
                            // this works out the probability a 2 point shot is made
                            double oTwoPointStat = playerWithBall.playerStats.MidRange;
                            double heightDifference = playerWithBall.playerStats.Height - playerWithBallMatchup.playerStats.Height;
                            double dBlockStat = playerWithBallMatchup.playerStats.Block;
                            double dDefenseStat = playerWithBallMatchup.playerStats.Defense;

                            // convert 2 point stat (40-99) to (-0.10 - 0.10)
                            oTwoPointStat = -0.08 + (0.08 + 0.08) * (oTwoPointStat - 40) / (99 - 40);
                            if (playerWithBall.FieldGoalMade > 4 && playerWithBall.GameValue > 13 && playerWithBall.playerStats.SecondaryPlaystyle != "Playmaker" && playerWithBall.playerStats.PrimaryPlaystyle == "Offensive") oTwoPointStat += 0.024 + 0.0006 * (playerWithBall.FieldGoalMade - 4);
                            if (playerWithBall.FieldGoalMade > 20) oTwoPointStat -= 0.0007 * (playerWithBall.FieldGoalMade - 20);
                            // convert height difference (-8 - 8) to (-0.05 to 0.05)
                            heightDifference = -0.05 + (0.05 + 0.05) * (heightDifference + 8) / (8 + 8);
                            // convert block stat (40-99) to (-0.009 - 0.029)
                            dBlockStat = -0.009 + (0.029 + 0.009) * (dBlockStat - 40) / (99 - 40);
                            // convert defense stat (40-99) to (-0.13 - 0.13)
                            dDefenseStat = -0.10 + (0.10 + 0.10) * (dDefenseStat - 40) / (99 - 40);
                            dDefenseStat *= -1;

                            // this sets the probabilities of the forthcoming events
                            prob2PMade += (oTwoPointStat + heightDifference + dDefenseStat);
                            prob2PMade *= passerContribution * 0.98;
                            prob2PBlocked += dBlockStat;
                            prob2PMissed = 1 - (prob2PMade + prob2PBlocked);

                            // this occurs if a 2 point shot is made
                            double randomProbability = random.NextDouble();
                            if (randomProbability < prob2PMade)
                            {
                                if (possessionCounter == 0) { }
                                CommentatorPhrases.Add($"{oPlayerName} made a mid range shot for the {oPlayerTeamName}.");


                                playerWithBall.FieldGoalAttempted++;
                                playerWithBall.FieldGoalMade++;
                                playerWithBall.Points += 2;
                                numPasses = 0;
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                // this validates if the player who passed, exists and is on the same team as player who scored
                                if (playerWhoPassed == null)
                                {
                                    // now we change possession
                                    if (possession == 1) possession = 2;
                                    else possession = 1;
                                    continue;
                                }
                                else if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                {
                                    int assistEvent = CalculateAssistProbability();
                                    // this is if an assist event
                                    if (assistEvent == 1)
                                    {
                                        CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                        score = CalculateScore();
                                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                    }
                                }
                            }
                            // this occurs if a 2 point is blocked
                            else if (randomProbability < prob2PMade + prob2PBlocked)
                            {
                                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} was blocked by {dPlayerName} of the {dPlayerTeamName} on his mid range shot");
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                playerWithBall.FieldGoalAttempted++;
                                playerWithBallMatchup.Blocks++;
                                numPasses = 0;
                            }
                            // this now occurs if a 2 shot is missed
                            else
                            {
                                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a mid range shot.");
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                playerWithBall.FieldGoalAttempted++;
                                int reboundEvent = CalculateReboundProbability();
                                reboundOccurred = true;

                                // this is if the ball goes out of bounds or defensive rebound
                                if (reboundEvent == -1 || reboundEvent == 0)
                                {
                                    if (possession == 1) possession = 2;
                                    else possession = 1;
                                    numPasses = 0;
                                }
                                // this is if an offensive rebound occurred
                                else
                                {
                                    // possession remains with the offensive team
                                    startingPossession = false;
                                    continue;
                                }
                            }
                            if (possession == 1 && !reboundOccurred) possession = 2;
                            else if (!reboundOccurred) possession = 1;
                            startingPossession = true;
                        }
                        // work out if a layup will be attempted
                        else if (randomAttemptedProbability < probLayupAttempted + (prob2PAttempted) + prob3PAttempted)
                        {
                            // this works out the probability a layup is made
                            double oLayupStat = playerWithBall.playerStats.Layup;
                            if (playerWithBall.playerStats.CloseShot > oLayupStat) oLayupStat = playerWithBall.playerStats.CloseShot;
                            double heightDifference = playerWithBall.playerStats.Height - playerWithBallMatchup.playerStats.Height;
                            double dBlockStat = playerWithBallMatchup.playerStats.Block;
                            double dDefenseStat = playerWithBallMatchup.playerStats.Defense;

                            // convert 2 point stat (40-99) to (-0.11 - 0.11)
                            oLayupStat = -0.09 + (0.09 + 0.09) * (oLayupStat - 40) / (99 - 40);
                            if (playerWithBall.FieldGoalMade > 4 && playerWithBall.GameValue > 13 && playerWithBall.playerStats.SecondaryPlaystyle != "Playmaker" && playerWithBall.playerStats.PrimaryPlaystyle == "Offensive") oLayupStat += 0.022 + 0.0008 * playerWithBall.FieldGoalMade;
                            if (playerWithBall.FieldGoalMade > 20) oLayupStat -= 0.0007 * (playerWithBall.FieldGoalMade - 20);
                            // convert height difference (-8 - 8) to (-0.11 to 0.11)
                            heightDifference = -0.11 + (0.11 + 0.11) * (heightDifference + 8) / (8 + 8);
                            // convert block stat (40-99) to (-0.005 - 0.26)
                            dBlockStat = -0.005 + (0.005 + 0.26) * (dBlockStat - 40) / (99 - 40);
                            // convert defense stat (40-99) to (-0.11 - 0.11)
                            dDefenseStat = -0.11 + (0.11 + 0.11) * (dDefenseStat - 40) / (99 - 40);
                            dDefenseStat *= -1;

                            // this sets the probabilities of the forthcoming events
                            probLayupMade += (oLayupStat + heightDifference + dDefenseStat);
                            probLayupMade *= passerContribution * 0.95;
                            probLayupBlocked += dBlockStat + heightDifference * -0.7;
                            probLayupMissed = 1 - (probLayupMade + probLayupBlocked);

                            // this occurs if a layup is made
                            double randomProbability = random.NextDouble();
                            if (randomProbability < probLayupMade)
                            {
                                if (possessionCounter == 0) { }
                                CommentatorPhrases.Add($"{oPlayerName} made a layup for the {oPlayerTeamName}.");

                                playerWithBall.FieldGoalAttempted++;
                                playerWithBall.FieldGoalMade++;
                                playerWithBall.Points += 2;
                                numPasses = 0;
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                // this validates if the player who passed, exists and is on the same team as player who scored
                                if (playerWhoPassed == null)
                                {
                                    // now we change possession
                                    if (possession == 1) possession = 2;
                                    else possession = 1;
                                    continue;
                                }
                                else if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                {
                                    int assistEvent = CalculateAssistProbability();
                                    // this is if an assist event
                                    if (assistEvent == 1)
                                    {
                                        CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                        score = CalculateScore();
                                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                    }
                                }
                            }
                            // this occurs if a layup is blocked
                            else if (randomProbability < probLayupMade + probLayupBlocked)
                            {
                                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} was blocked by {dPlayerName} of the {dPlayerTeamName} on his layup.");
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                playerWithBall.FieldGoalAttempted++;
                                playerWithBallMatchup.Blocks++;
                                numPasses = 0;
                            }
                            // this now occurs if a layup is missed
                            else
                            {
                                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a layup.");
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                playerWithBall.FieldGoalAttempted++;
                                int reboundEvent = CalculateReboundProbability();
                                reboundOccurred = true;
                                // this is if the ball goes out of bounds or defensive rebound
                                if (reboundEvent == -1 || reboundEvent == 0)
                                {
                                    if (possession == 1) possession = 2;
                                    else possession = 1;
                                    numPasses = 0;
                                }
                                // this is if an offensive rebound occurred
                                else
                                {
                                    // possession remains with the offensive team
                                    startingPossession = false;
                                    continue;
                                }
                            }
                            if (possession == 1 && !reboundOccurred) possession = 2;
                            else if (!reboundOccurred) possession = 1;
                            startingPossession = true;
                        }
                        // work out if a dunk will be attempted
                        else if (randomAttemptedProbability < probDunkAttempted + probLayupAttempted + (prob2PAttempted) + prob3PAttempted)
                        {
                            // this works out the probability a dunk is made
                            double oDunkStat = playerWithBall.playerStats.Dunk;
                            double heightDifference = playerWithBall.playerStats.Height - playerWithBallMatchup.playerStats.Height;
                            double dBlockStat = playerWithBallMatchup.playerStats.Block;
                            double dDefenseStat = playerWithBallMatchup.playerStats.Defense;

                            // convert 2 point stat (40-99) to (-0.11 - 0.11)
                            oDunkStat = -0.07 + (0.07 + 0.07) * (oDunkStat - 40) / (99 - 40);
                            if (playerWithBall.FieldGoalMade > 4 && playerWithBall.GameValue > 13 && playerWithBall.playerStats.SecondaryPlaystyle != "Playmaker" && playerWithBall.playerStats.PrimaryPlaystyle == "Offensive") oDunkStat += 0.023 + 0.0010 * playerWithBall.FieldGoalMade;
                            if (playerWithBall.FieldGoalMade > 20) oDunkStat -= 0.0007 * (playerWithBall.FieldGoalMade - 20);
                            // convert height difference (-8 - 8) to (-0.12 to 0.12)
                            heightDifference = -0.12 + (0.12 + 0.12) * (heightDifference + 8) / (8 + 8);
                            // convert block stat (40-99) to (-0.005 - 0.30)
                            dBlockStat = -0.005 + (0.005 + 0.30) * (dBlockStat - 40) / (99 - 40);
                            // convert defense stat (40-99) to (-0.14 - 0.14)
                            dDefenseStat = -0.10 + (0.10 + 0.10) * (dDefenseStat - 40) / (99 - 40);
                            dDefenseStat *= -1;

                            // this sets the probabilities of the forthcoming events
                            probDunkMade += (oDunkStat + heightDifference + dDefenseStat);
                            probDunkMade *= passerContribution * 0.92;
                            probDunkBlocked += dBlockStat + heightDifference * -0.7;
                            probDunkMissed = 1 - (probDunkMade + probDunkBlocked);

                            // this occurs if a dunk is made
                            double randomProbability = random.NextDouble();
                            if (randomProbability < probDunkMade)
                            {
                                if (possessionCounter == 0) { }
                                CommentatorPhrases.Add($"{oPlayerName} made a dunk for the {oPlayerTeamName}.");

                                playerWithBall.FieldGoalAttempted++;
                                playerWithBall.FieldGoalMade++;
                                playerWithBall.Points += 2;
                                numPasses = 0;
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                // this validates if the player who passed, exists and is on the same team as player who scored
                                if (playerWhoPassed == null)
                                {

                                    // now we change possession
                                    if (possession == 1) possession = 2;
                                    else possession = 1;
                                    continue;
                                }
                                else if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                {
                                    int assistEvent = CalculateAssistProbability();
                                    // this is if an assist event
                                    if (assistEvent == 1)
                                    {
                                        CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                        score = CalculateScore();
                                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                    }
                                }
                            }
                            // this occurs if a dunk is blocked
                            else if (randomProbability < probLayupMade + probLayupBlocked)
                            {
                                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} was blocked by {dPlayerName} of the {dPlayerTeamName} on his dunk.");
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                playerWithBall.FieldGoalAttempted++;
                                playerWithBallMatchup.Blocks++;
                                numPasses = 0;
                            }
                            // this now occurs if a dunk is missed
                            else
                            {
                                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a dunk.");
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                playerWithBall.FieldGoalAttempted++;

                                int reboundEvent = CalculateReboundProbability();
                                reboundOccurred = true;
                                // this is if the ball goes out of bounds or defensive rebound
                                if (reboundEvent == -1 || reboundEvent == 0)
                                {
                                    if (possession == 1) possession = 2;
                                    else possession = 1;
                                    numPasses = 0;
                                }
                                // this is if an offensive rebound occurred
                                else
                                {
                                    startingPossession = false;
                                    // possession remains with the offensive team
                                    continue;
                                }
                            }
                            if (possession == 1 && !reboundOccurred) possession = 2;
                            else if (!reboundOccurred) possession = 1;
                            startingPossession = true;
                        }
                        // work out if a pass will be attempted
                        else
                        {
                            double oPassStat = playerWithBall.playerStats.Passing;
                            double dStealStat = playerWithBallMatchup.playerStats.Steal;

                            // convert pass stat (40 - 99) (0.1 - (-0.1))
                            oPassStat = -0.1 + (0.1 + 0.1) * (oPassStat - 40) / (99 - 40);
                            oPassStat *= -1;

                            probPassStolen += oPassStat;
                            probPassMade = 1 - probPassStolen;

                            // this occurs if the pass is stolen
                            if (random.NextDouble() < probPassStolen)
                            {
                                CommentatorPhrases.Add($"{oPlayerName}'s pass of the {oPlayerTeamName} was intercepted by {dPlayerName} of the {dPlayerTeamName}");
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                playerWithBall.Turnovers++;
                                playerWithBallMatchup.Steals++;
                                // possession now switches
                                if (possession == 1) possession = 2;
                                else possession = 1;
                                startingPossession = true;
                                numPasses = 0;
                            }
                            // this occurs if the pass is completed
                            else
                            {
                                // here, we will switch the playerWithBall to someone else, and playerWithBallMatchup
                                bool validPass = false;
                                while (!validPass)
                                {
                                    // we normalise the overalls of the players, so that better players get the ball more
                                    double overallsSum = 0;
                                    List<PlayerInGame> playersToPass = new List<PlayerInGame>();
                                    foreach (PlayerInGame player in offenseStarterStats)
                                    {
                                        // the passer's overall is ignored
                                        if (player == playerWithBall) continue;
                                        overallsSum += player.playerStats.Overall;
                                        playersToPass.Add(player);
                                    }
                                    // convert gameValue from range (-5 - 60) to (-3.6 - 3.9) to add to chance they get the ball
                                    double gameValue1 = -3.6 + (3.9 + 3.6) * (playersToPass[0].GameValue + 5) / (5 + 60);
                                    if (gameValue1 > 5.3) gameValue1 = 5.3;
                                    double gameValue2 = -3.6 + (3.9 + 3.6) * (playersToPass[1].GameValue + 5) / (5 + 60);
                                    if (gameValue2 > 5.3) gameValue1 = 5.3;
                                    double gameValue3 = -3.6 + (3.9 + 3.6) * (playersToPass[2].GameValue + 5) / (5 + 60);
                                    if (gameValue3 > 5.3) gameValue1 = 5.3;
                                    double gameValue4 = -3.6 + (3.9 + 3.6) * (playersToPass[3].GameValue + 5) / (5 + 60);
                                    if (gameValue4 > 5.3) gameValue1 = 5.3;
                                    playersToPass = playersToPass.OrderByDescending(x => x.playerStats.Overall).ToList();
                                    double playstyleVariable1 = 0;
                                    double playstyleVariable2 = 0;
                                    double playstyleVariable3 = 0;
                                    double playstyleVariable4 = 0;
                                    for (int i = 0; i < playersToPass.Count; i++)
                                    {
                                        PlayerInGame player = playersToPass[i];
                                        int currentPlaystyleVariable = 0;
                                        if (player.playerStats.PrimaryPlaystyle == "Offensive")
                                        {
                                            if (player.playerStats.SecondaryPlaystyle == "Shooter" || player.playerStats.SecondaryPlaystyle == "Finisher")
                                            {
                                                currentPlaystyleVariable = 9;
                                            }
                                            else if (player.playerStats.SecondaryPlaystyle == "Playmaker")
                                            {
                                                currentPlaystyleVariable = -10;
                                            }
                                        }
                                        else if (player.playerStats.PrimaryPlaystyle == "Defensive")
                                        {
                                            currentPlaystyleVariable = -15;
                                        }

                                        if (i == 0) playstyleVariable1 = currentPlaystyleVariable;
                                        else if (i == 1) playstyleVariable2 = currentPlaystyleVariable;
                                        else if (i == 2) playstyleVariable3 = currentPlaystyleVariable;
                                        else playstyleVariable4 = currentPlaystyleVariable;
                                    }
                                    gameValue1 += playstyleVariable1;
                                    gameValue2 += playstyleVariable2;
                                    gameValue3 += playstyleVariable3;
                                    gameValue4 += playstyleVariable4;

                                    // here we check their playstyles and make sure that 
                                    // now we add these gameValues to the overallsSum, so that the divisions are normalised
                                    overallsSum += (gameValue1 + gameValue2 + gameValue3 + gameValue4);


                                    // this section checks the overalls of the players who could be passed to
                                    double randomProb = random.NextDouble();
                                    double overall1 = playersToPass[0].playerStats.Overall - 5 + gameValue1;
                                    if (playersToPass[0].FieldGoalMade > 6) overall1 += 0.20 + playersToPass[0].FieldGoalMade * 0.028;
                                    double overall2 = playersToPass[1].playerStats.Overall - 2 + gameValue2;
                                    if (playersToPass[1].FieldGoalMade > 6) overall2 += 0.20 + playersToPass[0].FieldGoalMade * 0.028;
                                    double overall3 = playersToPass[2].playerStats.Overall + 4 + gameValue3;
                                    if (playersToPass[2].FieldGoalMade > 6) overall3 += 0.20 + playersToPass[0].FieldGoalMade * 0.028;
                                    double overall4 = playersToPass[3].playerStats.Overall + 5 + gameValue4;
                                    if (playersToPass[3].FieldGoalMade > 6) overall4 += 0.20 + playersToPass[0].FieldGoalMade * 0.028;
                                    PlayerInGame potentialPlayerWithBall;
                                    if (randomProb < overall1 / overallsSum)
                                    {
                                        potentialPlayerWithBall = playersToPass[0];
                                    }
                                    else if (randomProb < (overall1 / overallsSum) + (overall2 / overallsSum))
                                    {
                                        potentialPlayerWithBall = playersToPass[1];
                                    }
                                    else if (randomProb < (overall1 / overallsSum) + (overall2 / overallsSum) + (overall3 / overallsSum))
                                    {
                                        potentialPlayerWithBall = playersToPass[2];
                                    }
                                    else
                                    {
                                        potentialPlayerWithBall = playersToPass[3];
                                    }

                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} made a pass to {potentialPlayerWithBall.playerStats.playerForename} {potentialPlayerWithBall.playerStats.playerSurname}");
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                                    playerWhoPassed = playerWithBall;
                                    playerWithBall = potentialPlayerWithBall;
                                    // now we set the matchup for playerWithBall
                                    foreach (PlayerInGame playerInGame2 in defenseStarterStats)
                                    {
                                        if (playerInGame2.playerStats.position == playerWithBall.playerStats.position)
                                        {
                                            playerWithBallMatchup = playerInGame2;
                                            break;
                                        }
                                    }
                                    validPass = true;

                                }
                                startingPossession = false;
                                numPasses++;
                            }
                        }
                    }

                    // here we add the possession time to the game clock
                    if (startingPossession)
                    {
                        int possessionTime = CalculatePossessionTime(numPasses);
                        gameClock.AddSeconds(possessionTime);
                    }

                    // once the possession ends, we update the player's game value
                    foreach (PlayerInGame playerInGame in offenseStarterStats)
                    {
                        playerInGame.GameValue = UpdateGameValue(playerInGame);
                    }
                    foreach (PlayerInGame playerInGame in defenseStarterStats)
                    {
                        playerInGame.GameValue = UpdateGameValue(playerInGame);
                    }
                }
            }
            //gameClock.AddSeconds(0);
        }

        public int CalculatePossessionTime(int numPasses)
        {
            double mean = 14.3 + (numPasses * 0.4);
            double stDev = 3.3;
            return (int)Player.GenerateRandomNormalDistribution(mean, stDev);
        }

        public int CalculateReboundProbability()
        {

            List<PlayerInGame> offense;
            List<PlayerInGame> defense;
            if (possession == 1)
            {
                offense = team1StarterStats;
                defense = team2StarterStats;
            }
            else
            {
                defense = team1StarterStats;
                offense = team2StarterStats;
            }
            if (offense.Count != 5 || defense.Count != 5) { }
            bool pfPresent = false;
            foreach (PlayerInGame player in defense) if (player.playerStats.position == "PF") pfPresent = true;
            if (!pfPresent) { }


            double probORebound = 0.27;
            double probDRebound = 0.72;
            double probOutOfBounds = 0.01;
            // we calculate if an offensive rebound occurs
            foreach (PlayerInGame player in offense)
            {
                // find playerMatchup
                PlayerInGame playerMatchup = new PlayerInGame(new Player());
                foreach (PlayerInGame player2 in defense)
                {
                    if (player2.playerStats.position == player.playerStats.position)
                    {
                        playerMatchup = player2;
                        break;
                    }
                }
                // convert rebound number from (45-99) to (-0.01 - 0.01) then add it to offensive rebound probability
                probORebound += -0.01 + (0.01 + 0.01) * (player.playerStats.Rebound - 45) / (99 - 45);
                // convert strengthDifference from (-63 - 63) to (-0.20 to 0.20)
                double strengthDifference = -0.20 + (0.20 + 0.20) * ((player.playerStats.Strength - playerMatchup.playerStats.Strength) + 63) / (63 + 63);
                probORebound += strengthDifference;

            }

            // we calculate if a defensive rebound occurs
            foreach (PlayerInGame player in defense)
            {

                // find playerMatchup
                PlayerInGame playerMatchup = new PlayerInGame(new Player());
                foreach (PlayerInGame player2 in offense)
                {
                    if (player2.playerStats.position == player.playerStats.position)
                    {
                        playerMatchup = player2;
                        break;
                    }
                }
                // convert rebound number from (45-99) to (-0.01 - 0.01) then add it to defensive rebound probability
                probDRebound += -0.01 + (0.01 + 0.01) * (player.playerStats.Rebound - 45) / (99 - 45);
                // convert strengthDifference from (-63 - 63) to (-0.20 to 0.20)
                double strengthDifference = -0.20 + (0.20 + 0.20) * ((player.playerStats.Strength - playerMatchup.playerStats.Strength) + 63) / (63 + 63);
                probDRebound += strengthDifference;
            }
            // normalise the probabilities
            double sum = probDRebound + probORebound + probOutOfBounds;
            probORebound /= sum;
            probDRebound /= sum;
            probOutOfBounds /= sum;

            Random random = new Random();
            double randomProbability = random.NextDouble();
            // if an offensive rebound occurs
            if (randomProbability < probORebound)
            {
                // we calculate which offensive player got the rebound
                int reboundSum = 0;
                foreach (PlayerInGame player in offense) reboundSum += player.playerStats.Rebound;
                double randomReboundProbability = random.NextDouble();
                PlayerInGame pg = offense.Where(x => x.playerStats.position == "PG").ToList()[0];
                PlayerInGame sg = offense.Where(x => x.playerStats.position == "SG").ToList()[0];
                PlayerInGame sf = offense.Where(x => x.playerStats.position == "SF").ToList()[0];
                PlayerInGame pf = offense.Where(x => x.playerStats.position == "PF").ToList()[0];
                PlayerInGame c = offense.Where(x => x.playerStats.position == "C").ToList()[0];
                double pgRebound = (double)pg.playerStats.Rebound / (double)reboundSum - 0.05;
                double sgRebound = (double)sg.playerStats.Rebound / (double)reboundSum - 0.02;
                double sfRebound = (double)sf.playerStats.Rebound / (double)reboundSum + 0.01;
                double pfRebound = (double)pf.playerStats.Rebound / (double)reboundSum + 0.04;
                double cRebound = (double)c.playerStats.Rebound / (double)reboundSum + 0.07;
                if (randomReboundProbability < pgRebound)
                {
                    CommentatorPhrases.Add($"{pg.playerStats.playerForename} {pg.playerStats.playerSurname} got the offensive rebound for the {pg.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                    pg.Rebounds++;
                    playerWithBall = pg;
                }
                else if (randomReboundProbability < pgRebound + sgRebound)
                {
                    CommentatorPhrases.Add($"{sg.playerStats.playerForename} {sg.playerStats.playerSurname} got the offensive rebound for the {sg.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                    sg.Rebounds++;
                    playerWithBall = sg;
                }
                else if (randomReboundProbability < pgRebound + sgRebound + sfRebound)
                {
                    CommentatorPhrases.Add($"{sf.playerStats.playerForename} {sf.playerStats.playerSurname} got the offensive rebound for the {sf.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                    sf.Rebounds++;
                    playerWithBall = sf;
                }
                else if (randomReboundProbability < pgRebound + sgRebound + sfRebound + pfRebound)
                {
                    CommentatorPhrases.Add($"{pf.playerStats.playerForename} {pf.playerStats.playerSurname} got the offensive rebound for the {pf.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                    pf.Rebounds++;
                    playerWithBall = pf;
                }
                else
                {
                    CommentatorPhrases.Add($"{c.playerStats.playerForename} {c.playerStats.playerSurname} got the offensive rebound for the {c.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");

                    c.Rebounds++;
                    playerWithBall = c;
                }
                // now we set the matchup for playerWithBall
                foreach (PlayerInGame playerInGame2 in defense)
                {
                    if (playerInGame2.playerStats.position == playerWithBall.playerStats.position)
                    {
                        playerWithBallMatchup = playerInGame2;
                        break;
                    }
                }
                if (possession == 1)
                {
                    offense = team1StarterStats;
                    defense = team2StarterStats;
                }
                else
                {
                    offense = team2StarterStats;
                    defense = team1StarterStats;
                }
                return 1;
            }
            // if a defensive rebound occurs
            else if (randomProbability < probDRebound + probORebound)
            {
                // we calculate which defensive player got the rebound
                int reboundSum = 0;
                foreach (PlayerInGame player in defense) reboundSum += player.playerStats.Rebound;
                double randomReboundProbability = random.NextDouble();
                PlayerInGame pg = defense.Where(x => x.playerStats.position == "PG").ToList()[0];
                PlayerInGame sg = defense.Where(x => x.playerStats.position == "SG").ToList()[0];
                PlayerInGame sf = defense.Where(x => x.playerStats.position == "SF").ToList()[0];
                PlayerInGame pf = defense.Where(x => x.playerStats.position == "PF").ToList()[0];
                PlayerInGame c = defense.Where(x => x.playerStats.position == "C").ToList()[0];
                double pgRebound = (double)pg.playerStats.Rebound / (double)reboundSum - 0.10;
                double sgRebound = (double)sg.playerStats.Rebound / (double)reboundSum - 0.07;
                double sfRebound = (double)sf.playerStats.Rebound / (double)reboundSum + 0.01;
                double pfRebound = (double)pf.playerStats.Rebound / (double)reboundSum + 0.07;
                double cRebound = (double)c.playerStats.Rebound / (double)reboundSum + 0.11;
                if (randomReboundProbability < pgRebound)
                {
                    CommentatorPhrases.Add($"{pg.playerStats.playerForename} {pg.playerStats.playerSurname} got the defensive rebound for the {pg.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                    pg.Rebounds++;
                }
                else if (randomReboundProbability < sgRebound + pgRebound)
                {
                    CommentatorPhrases.Add($"{sg.playerStats.playerForename} {sg.playerStats.playerSurname} got the defensive rebound for the {sg.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                    sg.Rebounds++;
                }
                else if (randomReboundProbability < sfRebound + sgRebound + pgRebound)
                {
                    CommentatorPhrases.Add($"{sf.playerStats.playerForename} {sf.playerStats.playerSurname} got the defensive rebound for the {sf.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                    sf.Rebounds++;
                }
                else if (randomReboundProbability < pfRebound + sfRebound + sgRebound + pgRebound)
                {
                    CommentatorPhrases.Add($"{pf.playerStats.playerForename} {pf.playerStats.playerSurname} got the defensive rebound for the {pf.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                    pf.Rebounds++;
                }
                else
                {
                    CommentatorPhrases.Add($"{c.playerStats.playerForename} {c.playerStats.playerSurname} got the defensive rebound for the {c.playerStats.teamName}");
                    (int, int) score = CalculateScore();
                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                    c.Rebounds++;
                }

                // now we change possession
                if (possession == 1)
                {
                    offense = team1StarterStats;
                    defense = team2StarterStats;
                }
                else
                {
                    offense = team2StarterStats;
                    defense = team1StarterStats;
                }
                return 0;
            }
            // if no rebound occurs (out of bounds)
            else
            {
                CommentatorPhrases.Add("The shot went out of bounds");
                (int, int) score = CalculateScore();
                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
            }

            return -1;
        }

        public int CalculateAssistProbability()
        {
            Random random = new Random();
            double probAssist = 0.82;
            // convert passing from range (45-99) to (-0.03 - 0.18)
            probAssist += -0.03 + (0.03 + 0.18) * (playerWhoPassed.playerStats.Passing - 45) / (99 - 45);
            // this returns 1 if an assist occurred
            if (random.NextDouble() < probAssist)
            {
                playerWhoPassed.Assists++;
                return 1;
            }
            // returns -1 if no assist
            return -1;
        }

        public double UpdateGameValue(PlayerInGame player)
        {
            double sum = 0;
            // add made threes and twos (midRange, layup, dunk)
            sum += 3.20 * player.ThreePointMade + 2.40 * (player.FieldGoalMade - player.ThreePointMade);

            // add missed threes and twos (midRange, layup, dunk)
            sum += -1.45 * (player.ThreePointAttempted - player.ThreePointMade) + -1.05 * ((player.FieldGoalAttempted - player.FieldGoalMade) - (player.ThreePointAttempted - player.ThreePointMade));

            // add remaining stats
            sum += 0.55 * player.Rebounds + 2.15 * player.Assists + 2.80 * player.Blocks + 3.40 * player.Steals + -1.45 * player.Turnovers + -3.05 * player.PersonalFouls;
            return sum;
        }

        public void InsertPlayerGameData(int gameId, bool playoffs)
        {
            List<PlayerInGame> team1 = team1Stats;
            List<PlayerInGame> team2 = team2Stats;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    // add this team's stats from the game into the database
                    string fullTransactionQuery = $"BEGIN TRANSACTION; \n";
                    fullTransactionQuery += "INSERT into playerGameStats(seasonId,gameId,playerId,gameValue,isPlayoffs,MP,FGM,FGA,TFGM,TFGA,PTS,REB,AST,STL,BLK,TOV,PF)\r\n VALUES";
                    foreach (PlayerInGame player in team1)
                    {
                        if (player.MinutesToPlay < 0) player.MinutesToPlay = 0;
                        fullTransactionQuery += $@"(
                            {CurrentLeague.CurrentSeason},
                            {gameId},
                            {player.playerStats.PlayerId},
                            {Math.Round(player.GameValue, 2)},
                            {playoffs},
                            {player.MinutesToPlay},
                            {player.FieldGoalMade},
                            {player.FieldGoalAttempted},
                            {player.ThreePointMade},
                            {player.ThreePointAttempted},
                            {player.Points},
                            {player.Rebounds},
                            {player.Assists},
                            {player.Steals},
                            {player.Blocks},
                            {player.Turnovers},
                            {player.PersonalFouls}
                            )," + "\n";
                    }

                    foreach (PlayerInGame player in team2)
                    {
                        fullTransactionQuery += $@"(
                            {CurrentLeague.CurrentSeason},
                            {gameId},
                            {player.playerStats.PlayerId},
                            {Math.Round(player.GameValue, 2)},
                            {playoffs},
                            {player.MinutesToPlay},
                            {player.FieldGoalMade},
                            {player.FieldGoalAttempted},
                            {player.ThreePointMade},
                            {player.ThreePointAttempted},
                            {player.Points},
                            {player.Rebounds},
                            {player.Assists},
                            {player.Steals},
                            {player.Blocks},
                            {player.Turnovers},
                            {player.PersonalFouls}
                            )," + "\n";

                        //command.CommandText = addGameStatsQuery;
                        //command.ExecuteNonQuery();
                    }
                    fullTransactionQuery = fullTransactionQuery.Substring(0, fullTransactionQuery.Length - 2) + ";";
                    if (team1Stats.Sum(x => x.Points) > team2Stats.Sum(x => x.Points) && !CurrentLeague.Playoffs)
                    {
                        fullTransactionQuery += $"UPDATE teamResults SET wins = wins + 1 WHERE teamId = {team1Players[0].TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                        fullTransactionQuery += $"UPDATE teamResults SET losses = losses + 1 WHERE teamId = {team2Players[0].TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                    }
                    else if (!CurrentLeague.Playoffs)
                    {
                        fullTransactionQuery += $"UPDATE teamResults SET wins = wins + 1 WHERE teamId = {team2Players[0].TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                        fullTransactionQuery += $"UPDATE teamResults SET losses = losses + 1 WHERE teamId = {team1Players[0].TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                    }
                    fullTransactionQuery += "COMMIT;";
                    command.CommandText = fullTransactionQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<double> CalculateEventProbabilities(double threePointAttempted, double twoPointAttempted, double layupAttempted, double dunkAttempted, double passAttempted, double passerContribution)
        { 

            List<double> attemptedProbablities = new List<double>();
            // work out 3 point attempted probabilities, (50-99) to (0-0.30)
            double threePointStat = playerWithBall.playerStats.ThreePoint;
            threePointAttempted = 0.30 * (threePointStat - 50) / (99 - 50);
            if (threePointAttempted < 0) threePointAttempted = 0;
            threePointAttempted *= passerContribution;
            attemptedProbablities.Add(threePointAttempted);

            // work out 2 point attempted probabilities, (50-99) to (0-0.27)
            double twoPointStat = playerWithBall.playerStats.MidRange;
            twoPointAttempted = 0.27 * (twoPointStat - 50) / (99 - 50);
            if (twoPointAttempted < 0) twoPointAttempted = 0;
            twoPointAttempted *= passerContribution * 1.15;
            attemptedProbablities.Add(twoPointAttempted);

            // work out layup attempted probabilities, (45-99) to (0.003-0.28)
            double layupStat = playerWithBall.playerStats.Layup;
            if (playerWithBall.playerStats.CloseShot > layupStat) layupStat = playerWithBall.playerStats.CloseShot;
            layupAttempted = 0.003 + 0.28 * (layupStat - 45) / (99 - 45);
            if (layupAttempted < 0) layupAttempted = 0;
            layupAttempted *= passerContribution;
            attemptedProbablities.Add(layupAttempted);

            // work out dunk attempted probabilities, (45-99) to (0.003-0.27)
            double dunkStat = playerWithBall.playerStats.Dunk;
            dunkAttempted = 0.003 + 0.30 * (dunkStat - 45) / (99 - 45);
            if (dunkAttempted < 0) dunkAttempted = 0;
            dunkAttempted *= passerContribution * 1.15;
            attemptedProbablities.Add(dunkAttempted);

            // convert overall (60 - 99) to (-0.10 to 0)
            double overallVariable = 0;
            if (playerWithBall.playerStats.Passing < 74)
            {
                overallVariable = -0.10 + (0.10) * (playerWithBall.playerStats.Overall - 60) / (99 - 60);
                overallVariable *= -1;
            }
            // work out pass attempted probablilites
            // passStat from (45-99) to (0-0.36), result is (0.14-0.72)
            double passStat = playerWithBall.playerStats.Passing;
            passAttempted = 0.14 + 0.58 * (passStat - 45) / (99 - 45);
            if (playerWithBall.FieldGoalMade > 4 && playerWithBall.GameValue > 19 && playerWithBall.playerStats.SecondaryPlaystyle != "Playmaker") passAttempted -= 0.024 + 0.003 * (playerWithBall.FieldGoalMade - 4);
            if (passAttempted < 0.10) passAttempted = 0.14;
            passAttempted += overallVariable;
            passAttempted /= Math.Pow(passerContribution, 1.6);
            attemptedProbablities.Add(passAttempted);

            // use this to normalise the probabilites
            double sum = attemptedProbablities.Sum();
            if (sum < 0.98 || sum > 1.02)
            {
                attemptedProbablities[0] /= sum;
                attemptedProbablities[1] /= sum;
                attemptedProbablities[2] /= sum;
                attemptedProbablities[3] /= sum;
                attemptedProbablities[4] /= sum;
            }
            return attemptedProbablities;
        }

        public void GenerateMinutesToPlay(bool playoffs)
        {
            Dictionary<int, double> gameValues = GetTeamGameValues(playoffs);
            // now we calculate the minutes for each position in team1
            {
                // we calculate the minutes to play for each position in team1
                List<PlayerInGame> team1PGs = new List<PlayerInGame>();
                List<PlayerInGame> team1SGs = new List<PlayerInGame>();
                List<PlayerInGame> team1SFs = new List<PlayerInGame>();
                List<PlayerInGame> team1PFs = new List<PlayerInGame>();
                List<PlayerInGame> team1Cs = new List<PlayerInGame>();

                // we get the players into their respective lists
                foreach (PlayerInGame player in team1Stats)
                {
                    if (player.playerStats.position == "PG") team1PGs.Add(player);
                    else if (player.playerStats.position == "SG") team1SGs.Add(player);
                    else if (player.playerStats.position == "SF") team1SFs.Add(player);
                    else if (player.playerStats.position == "PF") team1PFs.Add(player);
                    else if (player.playerStats.position == "C") team1Cs.Add(player);
                }

                // minutes calculated for PGs
                {
                    if (team1PGs.Count == 0) { };
                    team1PGs.OrderByDescending(x => x.playerStats.Overall).ToList();
                    PlayerInGame team1PG1 = team1PGs[0];
                    PlayerInGame team1PG2 = team1PGs[1];
                    PlayerInGame team1PG3 = team1PGs[2];
                    team1PG1.MinutesToPlay = 16;
                    team1PG2.MinutesToPlay = 16;
                    team1PG3.MinutesToPlay = 16;

                    // convert pg1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team1PG1.playerStats.Overall - 60) / (99 - 60);
                    // convert pg1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team1PG1.playerStats.PlayerId] + 5) / (5 + 60);
                    team1PG1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team1PG1.MinutesToPlay < 0) team1PG1.MinutesToPlay = 0;

                    // convert pg2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team1PG2.playerStats.Overall - 60) / (99 - 60);
                    // convert pg2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team1PG2.playerStats.PlayerId] + 5) / (5 + 60);
                    team1PG2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team1PG2.MinutesToPlay < 0) team1PG2.MinutesToPlay = 0;

                    // convert pg3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team1PG3.playerStats.Overall - 60) / (99 - 60);
                    // convert pg3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team1PG3.playerStats.PlayerId] + 5) / (5 + 60);
                    team1PG3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team1PG3.MinutesToPlay < 0) team1PG3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team1PG1.MinutesToPlay + team1PG2.MinutesToPlay + team1PG3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team1PG1.MinutesToPlay = (int)Math.Round(((double)team1PG1.MinutesToPlay / currentOverallNormalisedSum));
                    team1PG2.MinutesToPlay = (int)Math.Round(((double)team1PG2.MinutesToPlay / currentOverallNormalisedSum));
                    team1PG3.MinutesToPlay = (int)Math.Round(((double)team1PG3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team1PG1.MinutesToPlay + team1PG2.MinutesToPlay + team1PG3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team1PG3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

                // minutes calculated for SGs
                {
                    PlayerInGame team1SG1 = team1SGs[0];
                    PlayerInGame team1SG2 = team1SGs[1];
                    PlayerInGame team1SG3 = team1SGs[2];
                    team1SG1.MinutesToPlay = 16;
                    team1SG2.MinutesToPlay = 16;
                    team1SG3.MinutesToPlay = 16;

                    // convert sg1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team1SG1.playerStats.Overall - 60) / (99 - 60);
                    // convert sg1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team1SG1.playerStats.PlayerId] + 5) / (5 + 60);
                    team1SG1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team1SG1.MinutesToPlay < 0) team1SG1.MinutesToPlay = 0;

                    // convert sg2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team1SG2.playerStats.Overall - 60) / (99 - 60);
                    // convert sg2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team1SG2.playerStats.PlayerId] + 5) / (5 + 60);
                    team1SG2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team1SG2.MinutesToPlay < 0) team1SG2.MinutesToPlay = 0;

                    // convert sg3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team1SG3.playerStats.Overall - 60) / (99 - 60);
                    // convert sg3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team1SG3.playerStats.PlayerId] + 5) / (5 + 60);
                    team1SG3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team1SG3.MinutesToPlay < 0) team1SG3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team1SG1.MinutesToPlay + team1SG2.MinutesToPlay + team1SG3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team1SG1.MinutesToPlay = (int)Math.Round(((double)team1SG1.MinutesToPlay / currentOverallNormalisedSum));
                    team1SG2.MinutesToPlay = (int)Math.Round(((double)team1SG2.MinutesToPlay / currentOverallNormalisedSum));
                    team1SG3.MinutesToPlay = (int)Math.Round(((double)team1SG3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team1SG1.MinutesToPlay + team1SG2.MinutesToPlay + team1SG3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team1SG3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

                // minutes calculated for SFs
                {
                    PlayerInGame team1SF1 = team1SFs[0];
                    PlayerInGame team1SF2 = team1SFs[1];
                    PlayerInGame team1SF3 = team1SFs[2];
                    team1SF1.MinutesToPlay = 16;
                    team1SF2.MinutesToPlay = 16;
                    team1SF3.MinutesToPlay = 16;

                    // convert sf1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team1SF1.playerStats.Overall - 60) / (99 - 60);
                    // convert sf1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team1SF1.playerStats.PlayerId] + 5) / (5 + 60);
                    team1SF1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team1SF1.MinutesToPlay < 0) team1SF1.MinutesToPlay = 0;

                    // convert sf2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team1SF2.playerStats.Overall - 60) / (99 - 60);
                    // convert sf2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team1SF2.playerStats.PlayerId] + 5) / (5 + 60);
                    team1SF2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team1SF2.MinutesToPlay < 0) team1SF2.MinutesToPlay = 0;

                    // convert sf3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team1SF3.playerStats.Overall - 60) / (99 - 60);
                    // convert sf3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team1SF3.playerStats.PlayerId] + 5) / (5 + 60);
                    team1SF3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team1SF3.MinutesToPlay < 0) team1SF3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team1SF1.MinutesToPlay + team1SF2.MinutesToPlay + team1SF3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team1SF1.MinutesToPlay = (int)Math.Round(((double)team1SF1.MinutesToPlay / currentOverallNormalisedSum));
                    team1SF2.MinutesToPlay = (int)Math.Round(((double)team1SF2.MinutesToPlay / currentOverallNormalisedSum));
                    team1SF3.MinutesToPlay = (int)Math.Round(((double)team1SF3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team1SF1.MinutesToPlay + team1SF2.MinutesToPlay + team1SF3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team1SF3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

                // minutes calculated for PFs
                {
                    PlayerInGame team1PF1 = team1PFs[0];
                    PlayerInGame team1PF2 = team1PFs[1];
                    PlayerInGame team1PF3 = team1PFs[2];
                    team1PF1.MinutesToPlay = 16;
                    team1PF2.MinutesToPlay = 16;
                    team1PF3.MinutesToPlay = 16;

                    // convert pf1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team1PF1.playerStats.Overall - 60) / (99 - 60);
                    // convert pf1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team1PF1.playerStats.PlayerId] + 5) / (5 + 60);
                    team1PF1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team1PF1.MinutesToPlay < 0) team1PF1.MinutesToPlay = 0;

                    // convert pf2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team1PF2.playerStats.Overall - 60) / (99 - 60);
                    // convert pf2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team1PF2.playerStats.PlayerId] + 5) / (5 + 60);
                    team1PF2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team1PF2.MinutesToPlay < 0) team1PF2.MinutesToPlay = 0;

                    // convert pf3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team1PF3.playerStats.Overall - 60) / (99 - 60);
                    // convert pf3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team1PF3.playerStats.PlayerId] + 5) / (5 + 60);
                    team1PF3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team1PF3.MinutesToPlay < 0) team1PF3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team1PF1.MinutesToPlay + team1PF2.MinutesToPlay + team1PF3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team1PF1.MinutesToPlay = (int)Math.Round(((double)team1PF1.MinutesToPlay / currentOverallNormalisedSum));
                    team1PF2.MinutesToPlay = (int)Math.Round(((double)team1PF2.MinutesToPlay / currentOverallNormalisedSum));
                    team1PF3.MinutesToPlay = (int)Math.Round(((double)team1PF3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team1PF1.MinutesToPlay + team1PF2.MinutesToPlay + team1PF3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team1PF3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

                // minutes calculated for Cs
                {
                    PlayerInGame team1C1 = team1Cs[0];
                    PlayerInGame team1C2 = team1Cs[1];
                    PlayerInGame team1C3 = team1Cs[2];
                    team1C1.MinutesToPlay = 16;
                    team1C2.MinutesToPlay = 16;
                    team1C3.MinutesToPlay = 16;

                    // convert c1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team1C1.playerStats.Overall - 60) / (99 - 60);
                    // convert c1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team1C1.playerStats.PlayerId] + 5) / (5 + 60);
                    team1C1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team1C1.MinutesToPlay < 0) team1C1.MinutesToPlay = 0;

                    // convert c2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team1C2.playerStats.Overall - 60) / (99 - 60);
                    // convert c2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team1C2.playerStats.PlayerId] + 5) / (5 + 60);
                    team1C2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team1C2.MinutesToPlay < 0) team1C2.MinutesToPlay = 0;

                    // convert c3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team1C3.playerStats.Overall - 60) / (99 - 60);
                    // convert c3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team1C3.playerStats.PlayerId] + 5) / (5 + 60);
                    team1C3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team1C3.MinutesToPlay < 0) team1C3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team1C1.MinutesToPlay + team1C2.MinutesToPlay + team1C3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team1C1.MinutesToPlay = (int)Math.Round(((double)team1C1.MinutesToPlay / currentOverallNormalisedSum));
                    team1C2.MinutesToPlay = (int)Math.Round(((double)team1C2.MinutesToPlay / currentOverallNormalisedSum));
                    team1C3.MinutesToPlay = (int)Math.Round(((double)team1C3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team1C1.MinutesToPlay + team1C2.MinutesToPlay + team1C3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team1C3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

            }

            // we calculate the minutes to play for each position in team2
            {
                // we calculate the minutes to play for each position in team1
                List<PlayerInGame> team2PGs = new List<PlayerInGame>();
                List<PlayerInGame> team2SGs = new List<PlayerInGame>();
                List<PlayerInGame> team2SFs = new List<PlayerInGame>();
                List<PlayerInGame> team2PFs = new List<PlayerInGame>();
                List<PlayerInGame> team2Cs = new List<PlayerInGame>();

                // we get the players into their respective lists
                foreach (PlayerInGame player in team2Stats)
                {
                    if (player.playerStats.position == "PG") team2PGs.Add(player);
                    if (player.playerStats.position == "SG") team2SGs.Add(player);
                    if (player.playerStats.position == "SF") team2SFs.Add(player);
                    if (player.playerStats.position == "PF") team2PFs.Add(player);
                    if (player.playerStats.position == "C") team2Cs.Add(player);
                }

                // minutes calculated for PGs
                {
                    PlayerInGame team2PG1 = team2PGs[0];
                    PlayerInGame team2PG2 = team2PGs[1];
                    PlayerInGame team2PG3 = team2PGs[2];
                    team2PG1.MinutesToPlay = 16;
                    team2PG2.MinutesToPlay = 16;
                    team2PG3.MinutesToPlay = 16;

                    // convert pg1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team2PG1.playerStats.Overall - 60) / (99 - 60);
                    // convert pg1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team2PG1.playerStats.PlayerId] + 5) / (5 + 60);
                    team2PG1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team2PG1.MinutesToPlay < 0) team2PG1.MinutesToPlay = 0;

                    // convert pg2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team2PG2.playerStats.Overall - 60) / (99 - 60);
                    // convert pg2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team2PG2.playerStats.PlayerId] + 5) / (5 + 60);
                    team2PG2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team2PG2.MinutesToPlay < 0) team2PG2.MinutesToPlay = 0;

                    // convert pg3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team2PG3.playerStats.Overall - 60) / (99 - 60);
                    // convert pg3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team2PG3.playerStats.PlayerId] + 5) / (5 + 60);
                    team2PG3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team2PG3.MinutesToPlay < 0) team2PG3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team2PG1.MinutesToPlay + team2PG2.MinutesToPlay + team2PG3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team2PG1.MinutesToPlay = (int)Math.Round(((double)team2PG1.MinutesToPlay / currentOverallNormalisedSum));
                    team2PG2.MinutesToPlay = (int)Math.Round(((double)team2PG2.MinutesToPlay / currentOverallNormalisedSum));
                    team2PG3.MinutesToPlay = (int)Math.Round(((double)team2PG3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team2PG1.MinutesToPlay + team2PG2.MinutesToPlay + team2PG3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team2PG3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

                // minutes calculated for SGs
                {
                    PlayerInGame team2SG1 = team2SGs[0];
                    PlayerInGame team2SG2 = team2SGs[1];
                    PlayerInGame team2SG3 = team2SGs[2];
                    team2SG1.MinutesToPlay = 16;
                    team2SG2.MinutesToPlay = 16;
                    team2SG3.MinutesToPlay = 16;

                    // convert sg1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team2SG1.playerStats.Overall - 60) / (99 - 60);
                    // convert sg1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team2SG1.playerStats.PlayerId] + 5) / (5 + 60);
                    team2SG1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team2SG1.MinutesToPlay < 0) team2SG1.MinutesToPlay = 0;

                    // convert sg2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team2SG2.playerStats.Overall - 60) / (99 - 60);
                    // convert sg2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team2SG2.playerStats.PlayerId] + 5) / (5 + 60);
                    team2SG2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team2SG2.MinutesToPlay < 0) team2SG2.MinutesToPlay = 0;

                    // convert sg3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team2SG3.playerStats.Overall - 60) / (99 - 60);
                    // convert sg3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team2SG3.playerStats.PlayerId] + 5) / (5 + 60);
                    team2SG3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team2SG3.MinutesToPlay < 0) team2SG3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team2SG1.MinutesToPlay + team2SG2.MinutesToPlay + team2SG3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team2SG1.MinutesToPlay = (int)Math.Round(((double)team2SG1.MinutesToPlay / currentOverallNormalisedSum));
                    team2SG2.MinutesToPlay = (int)Math.Round(((double)team2SG2.MinutesToPlay / currentOverallNormalisedSum));
                    team2SG3.MinutesToPlay = (int)Math.Round(((double)team2SG3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team2SG1.MinutesToPlay + team2SG2.MinutesToPlay + team2SG3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team2SG3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

                // minutes calculated for SFs
                {
                    PlayerInGame team2SF1 = team2SFs[0];
                    PlayerInGame team2SF2 = team2SFs[1];
                    PlayerInGame team2SF3 = team2SFs[2];
                    team2SF1.MinutesToPlay = 16;
                    team2SF2.MinutesToPlay = 16;
                    team2SF3.MinutesToPlay = 16;

                    // convert sf1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team2SF1.playerStats.Overall - 60) / (99 - 60);
                    // convert sf1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team2SF1.playerStats.PlayerId] + 5) / (5 + 60);
                    team2SF1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team2SF1.MinutesToPlay < 0) team2SF1.MinutesToPlay = 0;

                    // convert sf2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team2SF2.playerStats.Overall - 60) / (99 - 60);
                    // convert sf2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team2SF2.playerStats.PlayerId] + 5) / (5 + 60);
                    team2SF2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team2SF2.MinutesToPlay < 0) team2SF2.MinutesToPlay = 0;

                    // convert sf3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team2SF3.playerStats.Overall - 60) / (99 - 60);
                    // convert sf3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team2SF3.playerStats.PlayerId] + 5) / (5 + 60);
                    team2SF3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team2SF3.MinutesToPlay < 0) team2SF3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team2SF1.MinutesToPlay + team2SF2.MinutesToPlay + team2SF3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team2SF1.MinutesToPlay = (int)Math.Round(((double)team2SF1.MinutesToPlay / currentOverallNormalisedSum));
                    team2SF2.MinutesToPlay = (int)Math.Round(((double)team2SF2.MinutesToPlay / currentOverallNormalisedSum));
                    team2SF3.MinutesToPlay = (int)Math.Round(((double)team2SF3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team2SF1.MinutesToPlay + team2SF2.MinutesToPlay + team2SF3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team2SF3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

                // minutes calculated for PFs
                {
                    PlayerInGame team2PF1 = team2PFs[0];
                    PlayerInGame team2PF2 = team2PFs[1];
                    PlayerInGame team2PF3 = team2PFs[2];
                    team2PF1.MinutesToPlay = 16;
                    team2PF2.MinutesToPlay = 16;
                    team2PF3.MinutesToPlay = 16;

                    // convert pf1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team2PF1.playerStats.Overall - 60) / (99 - 60);
                    // convert pf1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team2PF1.playerStats.PlayerId] + 5) / (5 + 60);
                    team2PF1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team2PF1.MinutesToPlay < 0) team2PF1.MinutesToPlay = 0;

                    // convert pf2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team2PF2.playerStats.Overall - 60) / (99 - 60);
                    // convert pf2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team2PF2.playerStats.PlayerId] + 5) / (5 + 60);
                    team2PF2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team2PF2.MinutesToPlay < 0) team2PF2.MinutesToPlay = 0;

                    // convert pf3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team2PF3.playerStats.Overall - 60) / (99 - 60);
                    // convert pf3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team2PF3.playerStats.PlayerId] + 5) / (5 + 60);
                    team2PF3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team2PF3.MinutesToPlay < 0) team2PF3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team2PF1.MinutesToPlay + team2PF2.MinutesToPlay + team2PF3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team2PF1.MinutesToPlay = (int)Math.Round(((double)team2PF1.MinutesToPlay / currentOverallNormalisedSum));
                    team2PF2.MinutesToPlay = (int)Math.Round(((double)team2PF2.MinutesToPlay / currentOverallNormalisedSum));
                    team2PF3.MinutesToPlay = (int)Math.Round(((double)team2PF3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team2PF1.MinutesToPlay + team2PF2.MinutesToPlay + team2PF3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team2PF3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

                // minutes calculated for Cs
                {
                    PlayerInGame team2C1 = team2Cs[0];
                    PlayerInGame team2C2 = team2Cs[1];
                    PlayerInGame team2C3 = team2Cs[2];
                    team2C1.MinutesToPlay = 16;
                    team2C2.MinutesToPlay = 16;
                    team2C3.MinutesToPlay = 16;

                    // convert c1's overall from (60-99) to (-16 to +16)
                    double overall1Variable = -20 + (20 + 20) * (team2C1.playerStats.Overall - 60) / (99 - 60);
                    // convert c1's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue1Variable = -7 + (7 + 13) * (gameValues[team2C1.playerStats.PlayerId] + 5) / (5 + 60);
                    team2C1.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                    if (team2C1.MinutesToPlay < 0) team2C1.MinutesToPlay = 0;

                    // convert c2's overall from (60-99) to (-16 to +16)
                    double overall2Variable = -20 + (20 + 20) * (team2C2.playerStats.Overall - 60) / (99 - 60);
                    // convert c2's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue2Variable = -7 + (7 + 13) * (gameValues[team2C2.playerStats.PlayerId] + 5) / (5 + 60);
                    team2C2.MinutesToPlay += (int)(overall2Variable + gameValue2Variable);
                    if (team2C2.MinutesToPlay < 0) team2C2.MinutesToPlay = 0;

                    // convert c3's overall from (60-99) to (-16 to +16)
                    double overall3Variable = -20 + (20 + 20) * (team2C3.playerStats.Overall - 60) / (99 - 60);
                    // convert c3's averageGameValue from (-5 to 60) to (-7 to 13)
                    double gameValue3Variable = -7 + (7 + 13) * (gameValues[team2C3.playerStats.PlayerId] + 5) / (5 + 60);
                    team2C3.MinutesToPlay += (int)(overall3Variable + gameValue3Variable);
                    if (team2C3.MinutesToPlay < 0) team2C3.MinutesToPlay = 0;

                    // we use this numbers to normalise the times so that they add up to 48 minutes
                    int currentOverallSum = team2C1.MinutesToPlay + team2C2.MinutesToPlay + team2C3.MinutesToPlay;
                    double currentOverallNormalisedSum = (double)currentOverallSum / (double)48;
                    team2C1.MinutesToPlay = (int)Math.Round(((double)team2C1.MinutesToPlay / currentOverallNormalisedSum));
                    team2C2.MinutesToPlay = (int)Math.Round(((double)team2C2.MinutesToPlay / currentOverallNormalisedSum));
                    team2C3.MinutesToPlay = (int)Math.Round(((double)team2C3.MinutesToPlay / currentOverallNormalisedSum));

                    currentOverallSum = team2C1.MinutesToPlay + team2C2.MinutesToPlay + team2C3.MinutesToPlay;
                    if (currentOverallSum > 48 || currentOverallSum < 48)
                    {
                        team2C3.MinutesToPlay -= (currentOverallSum - 48);
                    }
                }

            }
        }

        public void GeneratePlayerSlots()
        {
            Random random = new Random();
            List<int> slots = new List<int>();
            for (int i = 1; i <= 48; i++) slots.Add(i);

            //we calculate the minute slots for team 1's positions
            List<PlayerInGame> team1PGs = team1Stats.Where(x => x.playerStats.position == "PG").ToList();
            List<PlayerInGame> team1SGs = team1Stats.Where(x => x.playerStats.position == "SG").ToList();
            List<PlayerInGame> team1SFs = team1Stats.Where(x => x.playerStats.position == "SF").ToList();
            List<PlayerInGame> team1PFs = team1Stats.Where(x => x.playerStats.position == "PF").ToList();
            List<PlayerInGame> team1Cs = team1Stats.Where(x => x.playerStats.position == "C").ToList();
            // we set the slots for all the players in  team 1
            {
                // calculate the slots for team 1's PGs
                foreach (PlayerInGame player in team1PGs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
                // calculate the slots for team 1's SGs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team1SGs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
                // calculate the slots for team 1's SFs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team1SFs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
                // calculate the slots for team 1's Pfs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team1PFs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
                // calculate the slots for team 1's Cs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team1Cs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
            }

            //we calculate the minute slots for team 2's positions
            List<PlayerInGame> team2PGs = team2Stats.Where(x => x.playerStats.position == "PG").ToList();
            List<PlayerInGame> team2SGs = team2Stats.Where(x => x.playerStats.position == "SG").ToList();
            List<PlayerInGame> team2SFs = team2Stats.Where(x => x.playerStats.position == "SF").ToList();
            List<PlayerInGame> team2PFs = team2Stats.Where(x => x.playerStats.position == "PF").ToList();
            List<PlayerInGame> team2Cs = team2Stats.Where(x => x.playerStats.position == "C").ToList();
            // we set the slots for all the players in  team 2
            {
                // calculate the slots for team 2's PGs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team2PGs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
                // calculate the slots for team 2's SGs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team2SGs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
                // calculate the slots for team 2's SFs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team2SFs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
                // calculate the slots for team 2's Pfs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team2PFs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
                // calculate the slots for team 2's Cs
                slots = new List<int>();
                for (int i = 1; i <= 48; i++) slots.Add(i);
                foreach (PlayerInGame player in team2Cs)
                {
                    for (int i = 0; i < player.MinutesToPlay; i++)
                    {
                        int slotIndex = random.Next(slots.Count);
                        player.SlotsPlaying.Add(slots[slotIndex]);
                        slots.Remove(slots[slotIndex]);
                    }
                }
            }
        }

        public void GetStarters()
        {
            team1StarterStats = new List<PlayerInGame>();
            team2StarterStats = new List<PlayerInGame>();
            foreach (PlayerInGame player in team1Stats)
            {
                if (player.SlotsPlaying.Contains(1)) team1StarterStats.Add(player);
            }
            foreach (PlayerInGame player in team2Stats)
            {
                if (player.SlotsPlaying.Contains(1)) team2StarterStats.Add(player);
            }
        }

        public double GetPlayerGameValue(PlayerInGame player, bool playoffs)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string getGameValueQuery = $"SELECT AVG(gameValue) FROM playerGameStats WHERE playerId = {player.playerStats.PlayerId} AND isPlayoffs = {playoffs} AND seasonId = {CurrentLeague.CurrentSeason};";
                using (var command = new SQLiteCommand(getGameValueQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0)) return 6;
                            return (double)reader.GetDecimal(0);
                        }
                    }
                }
            }
            return 0;
        }

        public Dictionary<int, double> GetTeamGameValues(bool playoffs)
        {
            Dictionary<int, double> gameValues = new Dictionary<int, double>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string getGameValueQuery = $@"
                SELECT 
                pot.playerId, 
                COALESCE(AVG(pgs.gameValue), 0) as gameValue
                FROM playerOnTeam pot
                LEFT JOIN playerGameStats pgs
                ON pgs.isPlayoffs = {playoffs}
                AND pgs.seasonId = {CurrentLeague.CurrentSeason}
                AND pot.playerId = pgs.playerId
                WHERE 
	                pot.teamId IN ({team1.TeamId}, {team2.TeamId})
                    AND pot.dayJoined <= {CurrentLeague.CurrentDay}
                    AND pot.yearJoined <= {CurrentLeague.CurrentSeason} + 2023
                    AND pot.dayLeft >= {CurrentLeague.CurrentDay}
                    AND pot.yearLeft >= {CurrentLeague.CurrentSeason} + 2023
                GROUP BY 
                    pot.playerId;
                ";
                using (var command = new SQLiteCommand(getGameValueQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameValues.Add(reader.GetInt32(0), reader.GetDouble(1));
                        }
                    }
                }
            }
            return gameValues;
        }

        public (int, int) CalculateScore() => (team1Stats.Sum(x => x.Points), team2Stats.Sum(x => x.Points));

        public GameGenerator(Team team1, Team team2, string connectionString, string currentUser, int currentSaveState, int gameId, bool playoffs, League currentLeague)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.connectionString = connectionString;
            this.CurrentUser = currentUser;
            this.CurrentSaveState = currentSaveState;
            this.GameId = gameId;
            this.CurrentLeague = currentLeague;

            // possession set to team 1 or team 2
            possession = new Random().Next(1, 3);
            team1StarterStats = new List<PlayerInGame>();
            team2StarterStats = new List<PlayerInGame>();
            (List<Player>, List<Player>) players = ExtractPlayersFromTeams(team1, team2);
            team1Players = players.Item1;
            team2Players = players.Item2;
            AddPlayersIntoInGame();
            AddStartersFromInGamePlayers();
            GenerateMinutesToPlay(playoffs);
            GeneratePlayerSlots();
            GetStarters();
            int numPossessions = new Random().Next(196, 205);
            SimulatePossessions(gameId, numPossessions, playoffs);

        }
    }
}
