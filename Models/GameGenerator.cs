using System;
using System.Data.SQLite;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace LeagueSimulation.Models
{
    public class GameGenerator
    {
        public int possession; // which team currently has possession
        private int currentSaveState; // which database are we currently accessing
        public string connectionString; // string to connect to database
        private League currentLeague;

        // attributes for both teams
        private int gameId; // used for box score
        public List<string> CommentatorPhrases = new List<string>();
        public List<string> ScoreAfterEachPhrase = new List<string>();
        public List<string> GameTimestamps = new List<string>();
        public readonly List<string> positions = ["PG", "SG", "SF", "PF", "C"];
        private PlayerInGame playerWithBall; // which player has the ball
        private PlayerInGame playerWhoPassed; // player who passed to player with the ball
        private PlayerInGame playerWithBallMatchup; // matchup with player with the ball
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
        public GameClock gameClock;


        public int GameId { get; set; }
        public League CurrentLeague { get; set; }
        public int CurrentSaveState { get; set; }

        private (List<Player>, List<Player>) ExtractPlayersFromTeams(Team team1, Team team2)
        {
            List<Player> teamOnePlayers = new List<Player>();
            List<Player> teamTwoPlayers = new List<Player>();
            // extract the players from team1 and team2 and put them into their respective players lists
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string teamPlayersQuery = $@"
	                SELECT playerId
	                FROM playerOnTeam
	                WHERE ((dayJoined <= {CurrentLeague.CurrentDay} AND yearJoined = {CurrentLeague.CurrentSeason + 2023}) OR (yearJoined < {CurrentLeague.CurrentSeason + 2023}))
	                AND (dayLeft >= {CurrentLeague.CurrentDay} AND yearLeft >= {CurrentLeague.CurrentSeason + 2023})
                    AND (teamId = {team1.TeamId} OR teamId = {team2.TeamId})
                ";
                if (CurrentLeague.Playoffs) teamPlayersQuery = $@"
	                SELECT playerId
	                FROM playerOnTeam
	                WHERE ((dayJoined <= 150 AND yearJoined = {CurrentLeague.CurrentSeason + 2023}) OR (yearJoined < {CurrentLeague.CurrentSeason + 2023}))
	                AND (dayLeft >= 150 AND yearLeft >= {CurrentLeague.CurrentSeason + 2023})
                    AND (teamId = {team1.TeamId} OR teamId = {team2.TeamId})
                ";
                using (var command = new SQLiteCommand(teamPlayersQuery, connection))
                {
                    // this reader, will extract all the players from team1, and put them into the list
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int playerId = reader.GetInt32(reader.GetOrdinal("playerId"));
                            Player player = CurrentLeague.ExtractCurrentPlayerFromPlayerId(playerId); 
                            if (player.TeamId == team1.TeamId)  teamOnePlayers.Add(player);
                            else if (player.TeamId == team2.TeamId) teamTwoPlayers.Add(player);
                        }
                    }
                }

                return (teamOnePlayers, teamTwoPlayers);
            }
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
                        string teamName = team1.teamName;
                        string currentPlayerName = currentStarter.playerStats.playerForename + " " + currentStarter.playerStats.playerSurname;
                        string futurePlayerName = futureStarter.playerStats.playerForename + " " + futureStarter.playerStats.playerSurname;
                        CommentatorPhrases.Add($"{teamName}: {currentPlayerName} substituted for {futurePlayerName}");
                        (int, int) score = CalculateScore();
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                        GameTimestamps.Add(gameClock.PrintTime());
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
                        string teamName = team2.teamName;
                        string currentPlayerName = currentStarter.playerStats.playerForename + " " + currentStarter.playerStats.playerSurname;
                        string futurePlayerName = futureStarter.playerStats.playerForename + " " + futureStarter.playerStats.playerSurname;
                        CommentatorPhrases.Add($"{teamName}: {currentPlayerName} substituted for {futurePlayerName}");
                        (int, int) score = CalculateScore();
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                        GameTimestamps.Add(gameClock.PrintTime());
                    }
                }
            }
            team1StarterStats = team1FutureStarters;
            team2StarterStats = team2FutureStarters;

        }

        public void SetOvertimeRotation()
        {
            List<PlayerInGame> overtimeTeam1Starters = new List<PlayerInGame>()
            {
                team1Stats.Where(x => x.playerStats.position == "PG").OrderByDescending(x => x.MinutesToPlay).ToList()[0],
                team1Stats.Where(x => x.playerStats.position == "SG").OrderByDescending(x => x.MinutesToPlay).ToList()[0],
                team1Stats.Where(x => x.playerStats.position == "SF").OrderByDescending(x => x.MinutesToPlay).ToList()[0],
                team1Stats.Where(x => x.playerStats.position == "PF").OrderByDescending(x => x.MinutesToPlay).ToList()[0],
                team1Stats.Where(x => x.playerStats.position == "C").OrderByDescending(x => x.MinutesToPlay).ToList()[0]
            };

            List<PlayerInGame> overtimeTeam2Starters = new List<PlayerInGame>()
            {
                team2Stats.Where(x => x.playerStats.position == "PG").OrderByDescending(x => x.MinutesToPlay).ToList()[0],
                team2Stats.Where(x => x.playerStats.position == "SG").OrderByDescending(x => x.MinutesToPlay).ToList()[0],
                team2Stats.Where(x => x.playerStats.position == "SF").OrderByDescending(x => x.MinutesToPlay).ToList()[0],
                team2Stats.Where(x => x.playerStats.position == "PF").OrderByDescending(x => x.MinutesToPlay).ToList()[0],
                team2Stats.Where(x => x.playerStats.position == "C").OrderByDescending(x => x.MinutesToPlay).ToList()[0]
            };

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
            int endOfGameTime = 48;
            string oPlayerName = "";
            string oPlayerTeamName = "";
            string dPlayerName = "";
            string dPlayerTeamName = "";

            // we write a nice message to the player about which teams are playing
            // we do this only if the game just started, and it's not overtime
            if (!overtime)
            {
                string team1Record = CurrentLeague.GetTeamRecord(team1.teamName);
                string team2Record = CurrentLeague.GetTeamRecord(team2.teamName);
                if (playoffs)
                {
                    team1Record = CurrentLeague.GetSeriesRecordToDisplay(team1.TeamId.ToString(), team2.TeamId.ToString());
                    team2Record = CurrentLeague.GetSeriesRecordToDisplay(team2.TeamId.ToString(), team1.TeamId.ToString());
                    CommentatorPhrases.Add($"Welcome player! Today, we are watching the {team1.teamName} ({team1Record}) vs. {team2.teamName} ({team2Record}) live in the {CurrentLeague.PlayoffsRound} of the playoffs. Enjoy!");
                }
                else CommentatorPhrases.Add($"Welcome player! Today, we are watching the {team1.teamName} ({team1Record}) vs. {team2.teamName} ({team2Record}) live. Enjoy!");
                ScoreAfterEachPhrase.Add($"0-0");
                GameTimestamps.Add(gameClock.PrintTime());
            }

            // we continue calculating possessions until the number of possessions in a game is reached
            while (gameClock.Minutes <= endOfGameTime && !possessionsComplete)
            {
                


                // this checks if regular time has completed, then check if we need overtime
                if (gameClock.Minutes >= endOfGameTime)
                {
                    possessionsComplete = true;
                    // now we add gameValue to each player
                    foreach (PlayerInGame player in team1Stats) player.GameValue = UpdateGameValue(player);
                    foreach (PlayerInGame player in team2Stats) player.GameValue = UpdateGameValue(player);

                    // here we check if the game ended in a draw, then go into OT if so
                    if (CheckForDraw().Item1)
                    {
                        CommentatorPhrases.Add($"The game {team1.teamName} vs. {team2.teamName} resulted in a draw! We're going into overtime!");
                        CommentatorPhrases.Add($"The score was {CheckForDraw().Item2}");
                        (int, int) score = CalculateScore();
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                        GameTimestamps.Add(gameClock.PrintTime());
                        GameTimestamps.Add(gameClock.PrintTime());

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
                        GameTimestamps.Add(gameClock.PrintTime());

                        // Now we work out the name of the winner of the game, then display it
                        string nameOfWinner = "";
                        if (team1Stats.Sum(x => x.Points) > team2Stats.Sum(x => x.Points)) nameOfWinner = team1.teamName;
                        else nameOfWinner = team2.teamName;
                        CommentatorPhrases.Add($"The game score finished as {score.Item1}-{score.Item2}, as the win goes to the {nameOfWinner}. ");
                        ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                        GameTimestamps.Add(gameClock.PrintTime());
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
                    
                    // we calculate who starts with the ball, if the possession just started
                    if (startingPossession)
                    {
                        // we increment the possession counter
                        possessionCounter++;
                        // this orders the players in the list by ballHandle and in game assists
                        // the higher the player's ball handle, the higher chance they start with the ball
                        offenseStarterStats = offenseStarterStats.OrderByDescending(x => x.playerStats.BallHandle + 2 * x.playerStats.Passing + 3 * x.Assists).ToList();
                        double randomStarterProbability = random.NextDouble();
                        if (randomStarterProbability < 0.46) playerWithBall = offenseStarterStats[0];
                        else if (randomStarterProbability < 0.76) playerWithBall = offenseStarterStats[1];
                        else if (randomStarterProbability < 0.88) playerWithBall = offenseStarterStats[2];
                        else if (randomStarterProbability < 0.94) playerWithBall = offenseStarterStats[3];
                        else playerWithBall = offenseStarterStats[4];
                        // now we set the matchup for playerWithBall
                        playerWithBallMatchup = defenseStarterStats.Where(x => x.playerStats.position == playerWithBall.playerStats.position).ToList()[0];

                        // we set the playerWithBall and his matchup's name, for the commentator phrases
                        oPlayerName = $"{playerWithBall.playerStats.playerForename} {playerWithBall.playerStats.playerSurname}";
                        dPlayerName = $"{playerWithBallMatchup.playerStats.playerForename} {playerWithBallMatchup.playerStats.playerSurname}";

                        if (playerWithBall.playerStats.TeamId == team1.TeamId)
                        {
                            oPlayerTeamName = team1.teamName;
                            dPlayerTeamName = team2.teamName;
                        }
                        else
                        {
                            oPlayerTeamName = team2.teamName;
                            dPlayerTeamName = team1.teamName;
                        }

                        // we say who starts the offense
                        string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
                        CommentatorPhrases.Add($"{playerWithBall.playerStats.playerForename} {playerWithBall.playerStats.playerSurname} starts the offense for the {oPlayerTeamName}");
                        ScoreAfterEachPhrase.Add(currentScore);
                        GameTimestamps.Add(gameClock.PrintTime());
                    }

                    // list of probabilities of events during a single offensive possession for an offensive player
                    bool foulOccurred = false;

                    double prob3PAttempted = 0;
                    // events after a 3 point is attempted
                    double prob3PMade = 0.35;
                    double prob3PBlocked = 0.01;
                    double prob3PMissed = 0.65;

                    double prob2PAttempted = 0;
                    // events after a 2 point is attempted
                    double prob2PMade = 0.38;
                    double prob2PBlocked = 0.006;
                    double prob2PMissed = 0.586;

                    double probLayupAttempted = 0;
                    // events after a layup is attempted
                    double probLayupMade = 0.52;
                    double probLayupBlocked = 0.062;
                    double probLayupMissed = 0.418;

                    double probDunkAttempted = 0;
                    // events after a dunk is attempted
                    double probDunkMade = 0.52;
                    double probDunkBlocked = 0.05;
                    double probDunkMissed = 0.50;

                    double probPassAttempted = 0;
                    //events after a pass is attempted
                    double probPassMade = 0.900;
                    double probPassStolen = 0.100;

                    double passerContribution = 1;
                    // this checks if we aren't at a starting possesion, and the player who passed, isn't the player with the ball
                    // or on a different team
                    if (playerWhoPassed != null && playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                    {
                        // convert pass from (45 - 99) to (0 - 0.27)
                        passerContribution += playerWhoPassed.Assists * 0.002 / 1.176 + 0.24 * (playerWhoPassed.playerStats.Passing - 45) / (99 - 45);
                    }

                    // convert d overallDifference from (-40 to 40) to (-0.05 to 0.05)
                    int overallDifference = playerWithBallMatchup.playerStats.Overall - playerWithBall.playerStats.Overall;
                    double probFoulAfterShot = -0.05 + 0.10 * (overallDifference + 40) / 80;

                    List<double> attemptedEventProbabilites = CalculateEventProbabilities(prob3PAttempted, prob2PAttempted, probLayupAttempted, probDunkAttempted, probPassAttempted, passerContribution);

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
                        playerWithBall.Touches++;
                        if (randomAttemptedProbability < prob3PAttempted)
                        {
                            // this works out the probability a 3 point shot is made
                            double oThreePointStat = playerWithBall.playerStats.ThreePoint;
                            double heightDifference = playerWithBall.playerStats.Height - playerWithBallMatchup.playerStats.Height;
                            double dBlockStat = playerWithBallMatchup.playerStats.Block;
                            double dDefenseStat = playerWithBallMatchup.playerStats.Defense;

                            // convert 3 point stat (40-99) to (-0.12 - 0.07)
                            oThreePointStat = -0.13 + (0.13 + 0.07) * (oThreePointStat - 40) / (99 - 40);
                            if (playerWithBall.ThreePointMade > 10) oThreePointStat -= 0.0007 * (playerWithBall.FieldGoalMade - 10);
                            // convert height difference (-8 - 8) to (-0.07 to 0.07)
                            heightDifference = -0.07 + (0.07 + 0.07) * (heightDifference + 8) / (8 + 8);
                            // convert block stat (40-99) to (-0.009 - 0.045)
                            dBlockStat = -0.009 + (0.045 + 0.009) * (dBlockStat - 40) / (99 - 40);
                            // convert defense stat (40-99) to (-0.10 - 0.10)
                            dDefenseStat = -1 * (-0.11 + (0.11 + 0.13) * (dDefenseStat - 40) / (99 - 40));

                            // this sets the probabilities of the forthcoming events
                            prob3PMade += oThreePointStat + heightDifference + dDefenseStat;
                            prob3PMade *= passerContribution * 0.99;
                            prob3PBlocked += dBlockStat;
                            probFoulAfterShot += 0.02;
                            if (probFoulAfterShot < 0) probFoulAfterShot = 0;
                            prob3PMissed = 1 - (prob3PMade + prob3PBlocked);



                            double randomThreePointProbability = random.NextDouble();
                            // this occurs if the player is fouled on their three
                            if (randomThreePointProbability < probFoulAfterShot)
                            {
                                playerWithBallMatchup.PersonalFouls++;
                                prob3PMade *= 0.085;
                                prob3PBlocked = 0;
                                foulOccurred = true;

                                // this occurs if a 3 point and-1 occurs
                                if (random.NextDouble() < prob3PMade)
                                {
                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.FieldGoalMade++;
                                    playerWithBall.ThreePointAttempted++;
                                    playerWithBall.ThreePointMade++;
                                    playerWithBall.Points += 3;
                                    string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
                                    // set the assist
                                    if (playerWhoPassed != null)
                                    {
                                        if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                        {
                                            int assistEvent = CalculateAssistProbability();
                                            // this is if an assist event
                                            if (assistEvent == 1)
                                            {
                                                CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                                ScoreAfterEachPhrase.Add(currentScore);
                                                GameTimestamps.Add(gameClock.PrintTime());
                                            }
                                        }
                                    }

                                    CommentatorPhrases.Add($"{oPlayerName} made a three after the foul for a four point play for the {oPlayerTeamName}!");
                                    ScoreAfterEachPhrase.Add(currentScore);
                                    GameTimestamps.Add(gameClock.PrintTime());
                                    // now we have a live free throw for the player fouled
                                    double randomFreeThrowProbability = playerWithBall.playerStats.FreeThrow * 0.01;
                                    int reboundEvent = FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, true);
                                    // if the free throw was not made, then a rebound event occurred
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) { startingPossession = false; continue; }
                                }
                                // here we have 3 free throws we need to simulate
                                else
                                {
                                    double randomFreeThrowProbability = playerWithBall.playerStats.FreeThrow * 0.01;

                                    CommentatorPhrases.Add($"{oPlayerName} was fouled on his three for the {oPlayerTeamName} by {dPlayerName} of the {dPlayerTeamName}");
                                    string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
                                    ScoreAfterEachPhrase.Add(currentScore);
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    // here we simulate the first non-live free throw
                                    FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, false);
                                    // here we simulate the second non-live free throw
                                    FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, false);
                                    // now we simulate the live free throw
                                    int reboundEvent = FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, true);
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) {startingPossession = false; continue;}
                                }
                                startingPossession = true;
                            }
                            // if no foul is on the shot, we simulate as normal
                            else
                            {
                                // this occurs if a 3 point shot is made
                                if (randomThreePointProbability < prob3PMade)
                                {
                                    CommentatorPhrases.Add($"{oPlayerName} made a three for the {oPlayerTeamName}.");


                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.FieldGoalMade++;
                                    playerWithBall.ThreePointAttempted++;
                                    playerWithBall.ThreePointMade++;
                                    playerWithBall.Points += 3;
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());
                                    if (playerWhoPassed != null)
                                    {
                                        if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                        {
                                            int assistEvent = CalculateAssistProbability();
                                            // this is if an assist event
                                            if (assistEvent == 1)
                                            {
                                                CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                                GameTimestamps.Add(gameClock.PrintTime());
                                            }
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
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());
                                }
                                // this now occurs if a three point shot is missed
                                else
                                {
                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a three.");
                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.ThreePointAttempted++;

                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    int reboundEvent = ReboundEvent();
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) { startingPossession = false; continue; }
                                }

                            }
                            // work out who has possession if no rebound occurred
                            if (possession == 1 && !reboundOccurred) possession = 2;
                            else if (!reboundOccurred) possession = 1;
                            startingPossession = true;
                        }
                        // work out if a two point will be attempted
                        else if (randomAttemptedProbability < prob2PAttempted + prob3PAttempted)
                        {
                            // this works out the probability a 2 point shot is made
                            double oTwoPointStat = playerWithBall.playerStats.MidRange;
                            double heightDifference = playerWithBall.playerStats.Height - playerWithBallMatchup.playerStats.Height;
                            double dBlockStat = playerWithBallMatchup.playerStats.Block;
                            double dDefenseStat = playerWithBallMatchup.playerStats.Defense;

                            // convert 2 point stat (40-99) to (-0.10 - 0.10)
                            oTwoPointStat = -0.13 + (0.13 + 0.11) * (oTwoPointStat - 40) / (99 - 40);
                            if (playerWithBall.FieldGoalMade > 20) oTwoPointStat -= 0.0007 * (playerWithBall.FieldGoalMade - 20);
                            // convert height difference (-8 - 8) to (-0.05 to 0.05)
                            heightDifference = -0.06 + (0.06 + 0.06) * (heightDifference + 8) / (8 + 8);
                            // convert block stat (40-99) to (-0.009 - 0.029)
                            dBlockStat = -0.009 + (0.029 + 0.009) * (dBlockStat - 40) / (99 - 40);
                            // convert defense stat (40-99) to (-0.13 - 0.13)
                            dDefenseStat = -0.08 + (0.08 + 0.11) * (dDefenseStat - 40) / (99 - 40);
                            dDefenseStat *= -1;

                            // this sets the probabilities of the forthcoming events
                            prob2PMade += oTwoPointStat + heightDifference + dDefenseStat;
                            prob2PMade *= passerContribution;
                            prob2PBlocked += dBlockStat;
                            probFoulAfterShot += 0.05;
                            if (probFoulAfterShot < 0) probFoulAfterShot = 0;
                            prob2PMissed = 1 - (prob2PMade + prob2PBlocked);

                            double randomProbability = random.NextDouble();
                            // this occurs if the player is fouled
                            if (randomProbability < probFoulAfterShot)
                            {
                                playerWithBallMatchup.PersonalFouls++;
                                prob2PMade *= 0.165;
                                prob2PBlocked = 0;
                                foulOccurred = true;

                                // this occurs if a 2 point and-1 occurs
                                if (random.NextDouble() < prob2PMade)
                                {
                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.FieldGoalMade++;
                                    playerWithBall.Points += 2;
                                    string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
                                    // set the assist
                                    if (playerWhoPassed != null)
                                    {
                                        if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                        {
                                            int assistEvent = CalculateAssistProbability();
                                            // this is if an assist event
                                            if (assistEvent == 1)
                                            {
                                                CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                                ScoreAfterEachPhrase.Add(currentScore);
                                                GameTimestamps.Add(gameClock.PrintTime());
                                            }
                                        }
                                    }

                                    CommentatorPhrases.Add($"{oPlayerName} made a mid range after the foul for a three point play for the {oPlayerTeamName}!");
                                    ScoreAfterEachPhrase.Add(currentScore);
                                    // now we simulate the live free throw
                                    double randomFreeThrowProbability = playerWithBall.playerStats.FreeThrow * 0.01;
                                    int reboundEvent = FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, true);
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) { startingPossession = false; continue; }
                                }
                                // here we have 2 free throws we need to simulate
                                else
                                {
                                    double randomFreeThrowProbability = playerWithBall.playerStats.FreeThrow * 0.01;
                                    string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";

                                    CommentatorPhrases.Add($"{oPlayerName} was fouled on his mid range for the {oPlayerTeamName} by {dPlayerName} of the {dPlayerTeamName}");
                                    ScoreAfterEachPhrase.Add(currentScore);
                                    GameTimestamps.Add(gameClock.PrintTime());
                                    // here we simulate the first non-live free throw
                                    FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, false);
                                    // now we simulate the live free throw
                                    int reboundEvent = FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, true);
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) { startingPossession = false; continue; }

                                }
                                startingPossession = true;
                            }

                            // if no foul, simulate shot as normal
                            else
                            {
                                // this occurs if a 2 point shot is made
                                if (randomProbability < prob2PMade)
                                {
                                    if (possessionCounter == 0) { }
                                    CommentatorPhrases.Add($"{oPlayerName} made a mid range shot for the {oPlayerTeamName}.");


                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.FieldGoalMade++;
                                    playerWithBall.Points += 2;
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());
                                    // this validates if the player who passed, exists and is on the same team as player who scored
                                    if (playerWhoPassed == null)
                                    {
                                        // now we change possession
                                        if (possession == 1) possession = 2;
                                        else possession = 1;
                                        startingPossession = true;
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
                                            GameTimestamps.Add(gameClock.PrintTime());
                                        }
                                    }
                                }
                                // this occurs if a 2 point is blocked
                                else if (randomProbability < prob2PMade + prob2PBlocked)
                                {
                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} was blocked by {dPlayerName} of the {dPlayerTeamName} on his mid range shot");
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBallMatchup.Blocks++;
                                }
                                // this now occurs if a 2 shot is missed
                                else
                                {
                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a mid range shot.");
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    playerWithBall.FieldGoalAttempted++;
                                    int reboundEvent = CalculateReboundProbability();
                                    reboundOccurred = true;

                                    // this is if the ball goes out of bounds or defensive rebound
                                    if (reboundEvent == -1 || reboundEvent == 0)
                                    {
                                        if (possession == 1) possession = 2;
                                        else possession = 1;
                                        startingPossession = true;
                                    }
                                    // this is if an offensive rebound occurred
                                    else
                                    {
                                        // possession remains with the offensive team
                                        startingPossession = false;
                                        continue;
                                    }
                                }
                            }

                            if (possession == 1 && !reboundOccurred) possession = 2;
                            else if (!reboundOccurred) possession = 1;
                            startingPossession = true;
                        }
                        // work out if a layup will be attempted
                        else if (randomAttemptedProbability < probLayupAttempted + prob2PAttempted + prob3PAttempted)
                        {
                            // this works out the probability a layup is made
                            double oLayupStat = playerWithBall.playerStats.Layup;
                            if (playerWithBall.playerStats.CloseShot > oLayupStat) oLayupStat = playerWithBall.playerStats.CloseShot;
                            double heightDifference = playerWithBall.playerStats.Height - playerWithBallMatchup.playerStats.Height;
                            double dBlockStat = playerWithBallMatchup.playerStats.Block;
                            double dDefenseStat = playerWithBallMatchup.playerStats.Defense;

                            // convert 2 point stat (40-99) to (-0.11 - 0.11)
                            oLayupStat = -0.16 + (0.16 + 0.11) * (oLayupStat - 40) / (99 - 40);
                            if (playerWithBall.FieldGoalMade > 4 && playerWithBall.GameValue > 13 && playerWithBall.playerStats.SecondaryPlaystyle != "Playmaker" && playerWithBall.playerStats.PrimaryPlaystyle == "Offensive") oLayupStat += 0.022 + 0.0008 * playerWithBall.FieldGoalMade;
                            if (playerWithBall.FieldGoalMade > 20) oLayupStat -= 0.0007 * (playerWithBall.FieldGoalMade - 20);
                            // convert height difference (-8 - 8) to (-0.11 to 0.11)
                            heightDifference = -0.11 + (0.11 + 0.11) * (heightDifference + 8) / (8 + 8);
                            // convert block stat (40-99) to (-0.005 - 0.26)
                            dBlockStat = -0.005 + (0.005 + 0.26) * (dBlockStat - 40) / (99 - 40);
                            // convert defense stat (40-99) to (-0.11 - 0.11)
                            dDefenseStat = -0.12 + (0.12 + 0.15) * (dDefenseStat - 40) / (99 - 40);
                            dDefenseStat *= -1;

                            // this sets the probabilities of the forthcoming events
                            probLayupMade += oLayupStat + heightDifference + dDefenseStat;
                            probLayupMade *= passerContribution * 0.99;
                            probFoulAfterShot += 0.13;
                            if (probFoulAfterShot < 0) probFoulAfterShot = 0;
                            probLayupBlocked += dBlockStat + heightDifference * -0.7;
                            probLayupMissed = 1 - (probLayupMade + probLayupBlocked);

                            // this occurs if a layup is made
                            double randomProbability = random.NextDouble();
                            // this occurs if the player is fouled
                            if (randomProbability < probFoulAfterShot)
                            {
                                playerWithBallMatchup.PersonalFouls++;
                                probLayupMade *= 0.310;
                                probLayupBlocked = 0;
                                foulOccurred = true;

                                // this occurs if a 2 point and-1 occurs
                                if (random.NextDouble() < probLayupMade)
                                {
                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.FieldGoalMade++;
                                    playerWithBall.Points += 2;
                                    string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
                                    // set the assist
                                    if (playerWhoPassed != null)
                                    {
                                        if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                        {
                                            int assistEvent = CalculateAssistProbability();
                                            // this is if an assist event
                                            if (assistEvent == 1)
                                            {
                                                CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                                ScoreAfterEachPhrase.Add(currentScore);
                                                GameTimestamps.Add(gameClock.PrintTime());
                                            }
                                        }
                                    }

                                    CommentatorPhrases.Add($"{oPlayerName} made a layup after the foul for a three point play for the {oPlayerTeamName}!");
                                    ScoreAfterEachPhrase.Add(currentScore);
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    // now we simulate the live free throw
                                    double randomFreeThrowProbability = playerWithBall.playerStats.FreeThrow * 0.01;
                                    int reboundEvent = FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, true);
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) { startingPossession = false; continue; }
                                }
                                // here we have 2 free throws we need to simulate
                                else
                                {
                                    double randomFreeThrowProbability = playerWithBall.playerStats.FreeThrow * 0.01;
                                    string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";

                                    CommentatorPhrases.Add($"{oPlayerName} was fouled on his layup for the {oPlayerTeamName} by {dPlayerName} of the {dPlayerTeamName}");
                                    ScoreAfterEachPhrase.Add(currentScore);
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    // here we simulate the first non-live free throw
                                    FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, false);
                                    // now we simulate the live free throw
                                    int reboundEvent = FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, true);
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) { startingPossession = false; continue; }

                                }
                                startingPossession = true;
                            }
                            // if no foul, we simulate as normal
                            else
                            {
                                if (randomProbability < probLayupMade)
                                {
                                    if (possessionCounter == 0) { }
                                    CommentatorPhrases.Add($"{oPlayerName} made a layup for the {oPlayerTeamName}.");

                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.FieldGoalMade++;
                                    playerWithBall.Points += 2;
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());
                                    // this validates if the player who passed, exists and is on the same team as player who scored
                                    if (playerWhoPassed == null)
                                    {
                                        // now we change possession
                                        if (possession == 1) possession = 2;
                                        else possession = 1;
                                        startingPossession = true;
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
                                            GameTimestamps.Add(gameClock.PrintTime());
                                        }
                                    }
                                }
                                // this occurs if a layup is blocked
                                else if (randomProbability < probLayupMade + probLayupBlocked)
                                {
                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} was blocked by {dPlayerName} of the {dPlayerTeamName} on his layup.");
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBallMatchup.Blocks++;
                                }
                                // this now occurs if a layup is missed
                                else
                                {
                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a layup.");
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    playerWithBall.FieldGoalAttempted++;
                                    int reboundEvent = CalculateReboundProbability();
                                    reboundOccurred = true;
                                    // this is if the ball goes out of bounds or defensive rebound
                                    if (reboundEvent == -1 || reboundEvent == 0)
                                    {
                                        if (possession == 1) possession = 2;
                                        else possession = 1;
                                        startingPossession = true;
                                    }
                                    // this is if an offensive rebound occurred
                                    else
                                    {
                                        // possession remains with the offensive team
                                        startingPossession = false;
                                        continue;
                                    }
                                }
                            }

                            if (possession == 1 && !reboundOccurred) possession = 2;
                            else if (!reboundOccurred) possession = 1;
                            startingPossession = true;
                        }
                        // work out if a dunk will be attempted
                        else if (randomAttemptedProbability < probDunkAttempted + probLayupAttempted + prob2PAttempted + prob3PAttempted)
                        {
                            // this works out the probability a dunk is made
                            double oDunkStat = playerWithBall.playerStats.Dunk;
                            double heightDifference = playerWithBall.playerStats.Height - playerWithBallMatchup.playerStats.Height;
                            double dBlockStat = playerWithBallMatchup.playerStats.Block;
                            double dDefenseStat = playerWithBallMatchup.playerStats.Defense;

                            // convert 2 point stat (40-99) to (-0.11 - 0.11)
                            oDunkStat = -0.14 + (0.14 + 0.12) * (oDunkStat - 40) / (99 - 40);
                            if (playerWithBall.FieldGoalMade > 20) oDunkStat -= 0.0007 * (playerWithBall.FieldGoalMade - 20);
                            // convert height difference (-8 - 8) to (-0.12 to 0.12)
                            heightDifference = -0.12 + (0.12 + 0.12) * (heightDifference + 8) / (8 + 8);
                            // convert block stat (40-99) to (-0.005 - 0.30)
                            dBlockStat = -0.005 + (0.005 + 0.30) * (dBlockStat - 40) / (99 - 40);
                            // convert defense stat (40-99) to (-0.14 - 0.14)
                            dDefenseStat = -0.11 + (0.11 + 0.13) * (dDefenseStat - 40) / (99 - 40);
                            dDefenseStat *= -1;

                            // this sets the probabilities of the forthcoming events
                            probDunkMade += oDunkStat + heightDifference + dDefenseStat;
                            probDunkMade *= passerContribution * 0.97;
                            probDunkBlocked += dBlockStat + heightDifference * -0.7;
                            probFoulAfterShot += 0.23;
                            if (probFoulAfterShot < 0) probFoulAfterShot = 0;
                            probDunkMissed = 1 - (probDunkMade + probDunkBlocked);

                            // this occurs if a dunk is made
                            double randomProbability = random.NextDouble();
                            // this occurs if the player is fouled
                            if (randomProbability < probFoulAfterShot)
                            {
                                playerWithBallMatchup.PersonalFouls++;
                                probDunkMade *= 0.380;
                                probDunkBlocked = 0;
                                foulOccurred = true;

                                // this occurs if a 2 point and-1 occurs
                                if (random.NextDouble() < probDunkMade)
                                {
                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.FieldGoalMade++;
                                    playerWithBall.Points += 2;
                                    string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
                                    CommentatorPhrases.Add($"{oPlayerName} made a dunk after the foul for a three point play for the {oPlayerTeamName}!");
                                    ScoreAfterEachPhrase.Add(currentScore);
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    // set the assist
                                    if (playerWhoPassed != null)
                                    {
                                        if (playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                        {
                                            int assistEvent = CalculateAssistProbability();
                                            // this is if an assist event
                                            if (assistEvent == 1)
                                            {
                                                CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                                ScoreAfterEachPhrase.Add(currentScore);
                                                GameTimestamps.Add(gameClock.PrintTime());
                                            }
                                        }
                                    }

                                    // now we have a live free throw for the player fouled
                                    // now we simulate the live free throw
                                    double randomFreeThrowProbability = playerWithBall.playerStats.FreeThrow * 0.01;
                                    int reboundEvent = FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, true);
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) { startingPossession = false; continue; }
                                }
                                // here we have 2 free throws we need to simulate
                                else
                                {
                                    double randomFreeThrowProbability = playerWithBall.playerStats.FreeThrow * 0.01;
                                    string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";

                                    CommentatorPhrases.Add($"{oPlayerName} was fouled on his dunk for the {oPlayerTeamName} by {dPlayerName} of the {dPlayerTeamName}");
                                    ScoreAfterEachPhrase.Add(currentScore);
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    // here we simulate the first non-live free throw
                                    FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, false);
                                    // now we simulate the live free throw
                                    int reboundEvent = FreeThrowEvent(oPlayerName, oPlayerTeamName, randomFreeThrowProbability, true);
                                    if (reboundEvent != -2) reboundOccurred = true;
                                    if (reboundEvent == 1) { startingPossession = false; continue; }

                                }
                                startingPossession = true;
                            }
                            // if no foul, we simulate as normal
                            else
                            {
                                // if the dunk is made
                                if (randomProbability < probDunkMade)
                                {
                                    if (possessionCounter == 0) { }
                                    CommentatorPhrases.Add($"{oPlayerName} made a dunk for the {oPlayerTeamName}.");

                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBall.FieldGoalMade++;
                                    playerWithBall.Points += 2;
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());
                                    // this validates if the player who passed, exists and is on the same team as player who scored
                                    if (playerWhoPassed != null && playerWhoPassed != playerWithBall && playerWhoPassed.playerStats.TeamId == playerWithBall.playerStats.TeamId)
                                    {
                                        int assistEvent = CalculateAssistProbability();
                                        // this is if an assist event
                                        if (assistEvent == 1)
                                        {
                                            CommentatorPhrases.Add($"Assisted by {playerWhoPassed.playerStats.playerForename} {playerWhoPassed.playerStats.playerSurname}");
                                            score = CalculateScore();
                                            ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                            GameTimestamps.Add(gameClock.PrintTime());
                                        }
                                    }
                                }
                                // this occurs if a dunk is blocked
                                else if (randomProbability < probDunkMade + probDunkBlocked)
                                {
                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} was blocked by {dPlayerName} of the {dPlayerTeamName} on his dunk.");
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    playerWithBall.FieldGoalAttempted++;
                                    playerWithBallMatchup.Blocks++;
                                }
                                // this now occurs if a dunk is missed
                                else
                                {
                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a dunk.");
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    playerWithBall.FieldGoalAttempted++;
                                    int reboundEvent = CalculateReboundProbability();
                                    reboundOccurred = true;
                                    // this is if the ball goes out of bounds or defensive rebound
                                    if (reboundEvent == -1 || reboundEvent == 0)
                                    {
                                        if (possession == 1) possession = 2;
                                        else possession = 1;
                                        startingPossession = true;
                                    }
                                    // this is if an offensive rebound occurred
                                    else
                                    {
                                        // possession remains with the offensive team
                                        startingPossession = false;
                                        continue;
                                    }
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
                            oPassStat = -0.06 + (0.06 + 0.06) * (oPassStat - 40) / (99 - 40);
                            // convert steal stat (40 - 99) to (-0.2 - 0.08)
                            dStealStat = -0.1 + (0.1 + 0.03) * (dStealStat - 40) / (99 - 40);
                            oPassStat *= -1;

                            probPassStolen += oPassStat + dStealStat;
                            probPassMade = 1 - probPassStolen;

                            // this occurs if the pass is stolen
                            if (random.NextDouble() < probPassStolen)
                            {
                                CommentatorPhrases.Add($"{oPlayerName}'s pass of the {oPlayerTeamName} was intercepted by {dPlayerName} of the {dPlayerTeamName}");
                                (int, int) score = CalculateScore();
                                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                GameTimestamps.Add(gameClock.PrintTime());

                                playerWithBall.Turnovers++;
                                playerWithBallMatchup.Steals++;
                                playerWithBall = playerWithBallMatchup;
                                // possession now switches
                                if (possession == 1) possession = 2;
                                else possession = 1;
                                startingPossession = true;
                            }
                            // this occurs if the pass is completed
                            else
                            {
                                // here, we will switch the playerWithBall to someone else, and playerWithBallMatchup
                                bool validPass = false;
                                while (!validPass)
                                {
                                    // we normalise the overalls of the players, so that better players get the ball more
                                    List<PlayerInGame> playersToPass = offenseStarterStats.Where(x => x.playerStats.PlayerId != playerWithBall.playerStats.PlayerId).ToList();
                                    if (playersToPass.Count != 4) { };
                                    double overallsSum = playersToPass.Sum(x => x.playerStats.Overall);
                                    List<double> overallMultipliers = [0, 0, 0, 0];
                                    playersToPass = playersToPass.OrderByDescending(x => x.playerStats.Overall).ToList();

                                    for (int i = 0; i < playersToPass.Count; i++)
                                    {
                                        PlayerInGame player = playersToPass[i];
                                        double currentOverallMultiplier = overallMultipliers[i];
                                        currentOverallMultiplier += player.playerStats.Overall;
                                        if (player.FieldGoalMade > 5) currentOverallMultiplier += 0.20 + player.FieldGoalMade * 0.023;

                                        double gameValue = -5.4 + (5.9 + 5.4) * (playersToPass[i].GameValue + 5) / (5 + 44);
                                        if (gameValue > 5.9) gameValue = 5.9;
                                        overallMultipliers[i] += gameValue;
                                        if (player.playerStats.SecondaryPlaystyle == "Shooter" || player.playerStats.SecondaryPlaystyle == "Finisher")
                                        {
                                            overallMultipliers[i] += 18;
                                        }
                                        else if (player.playerStats.SecondaryPlaystyle == "Playmaker")
                                        {
                                            overallMultipliers[i] += 12;
                                        }
                                        else if (player.playerStats.PrimaryPlaystyle == "Defensive")
                                        {
                                            overallMultipliers[i] += -12;
                                        }
                                    }

                                    // here we check their playstyles and make sure that 
                                    // now we add these gameValues to the overallsSum, so that the divisions are normalised
                                    overallsSum += overallMultipliers.Sum();
                                    double randomProb = random.NextDouble();
                                    PlayerInGame potentialPlayerWithBall = playersToPass[0];
                                    double currentOverallsSum = 0.0;
                                    // this section checks the overalls of the players who could be passed to

                                    for (int i = 0; i < overallMultipliers.Count; i++)
                                    {
                                        PlayerInGame player = playersToPass[i];
                                        if (i == overallMultipliers.Count - 1) { potentialPlayerWithBall = player; break; }
                                        currentOverallsSum += player.playerStats.Overall / overallsSum;
                                        if (randomProb < currentOverallsSum) { potentialPlayerWithBall = player; break; }
                                    }

                                    CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} made a pass to {potentialPlayerWithBall.playerStats.playerForename} {potentialPlayerWithBall.playerStats.playerSurname}");
                                    (int, int) score = CalculateScore();
                                    ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                                    GameTimestamps.Add(gameClock.PrintTime());

                                    playerWhoPassed = playerWithBall;
                                    playerWithBall = potentialPlayerWithBall;
                                    oPlayerName = $"{playerWithBall.playerStats.playerForename} {playerWithBall.playerStats.playerSurname}";
                                    // now we set the matchup for playerWithBall
                                    playerWithBallMatchup = defenseStarterStats.Where(x => x.playerStats.position == playerWithBall.playerStats.position).ToList()[0];
                                    validPass = true;
                                }
                                startingPossession = false;
                                numPasses++;
                            }
                        }
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

                    // here we add the possession time to the game clock
                    if (startingPossession)
                    {
                        gameClock.AddSeconds(CalculatePossessionTime(numPasses));
                        numPasses = 0;
                        playerWithBall = new PlayerInGame(new Player());
                        playerWithBallMatchup = new PlayerInGame(new Player());

                        // this checks whether we need to do any substitutions as we move towards the next rotation slot
                        if (gameClock.Minutes == rotationSlot && gameClock.Minutes != endOfGameTime)
                        {
                            rotationSlot++;
                            if (overtime) SetOvertimeRotation();
                            else CheckForSubstitutions(rotationSlot);
                        }
                    }

                }
            }
        }

        private int FreeThrowEvent(string oPlayerName, string oPlayerTeamName, double randomFreeThrowProbability, bool live)
        {
            Random random = new Random();
            string currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
            // if the free throw is made
            if (random.NextDouble() < randomFreeThrowProbability)
            {
                playerWithBall.FreeThrowAttempted++;
                playerWithBall.FreeThrowMade++;
                playerWithBall.Points++;
                CommentatorPhrases.Add($"{oPlayerName} made a free throw for the {oPlayerTeamName}.");
                currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
                ScoreAfterEachPhrase.Add(currentScore);
                GameTimestamps.Add(gameClock.PrintTime());
            }
            // if the free throw is missed
            else
            {
                CommentatorPhrases.Add($"{oPlayerName} of the {oPlayerTeamName} missed a free throw.");
                playerWithBall.FreeThrowAttempted++;

                currentScore = $"{CalculateScore().Item1}-{CalculateScore().Item2}";
                ScoreAfterEachPhrase.Add(currentScore);
                GameTimestamps.Add(gameClock.PrintTime());

                // this condition is carried out if the free throw is live
                if (live) return ReboundEvent();
            }
            return -2;
        }

        private int ReboundEvent()
        {
            int reboundEvent = CalculateReboundProbability();
            // this is if the ball goes out of bounds or a defensive rebound
            if (reboundEvent == -1 || reboundEvent == 0)
            {
                if (possession == 1) possession = 2;
                else possession = 1;
            }
            return reboundEvent;
        }

        public int CalculatePossessionTime(int numPasses)
        {
            double mean = 14.8 + numPasses * 0.60;
            double stDev = 2.7;
            return (int)Player.GenerateRandomNormalDistribution(mean, stDev, 3, 24);
        }

        public int CalculateReboundProbability()
        {

            List<PlayerInGame> offense;
            List<PlayerInGame> defense;
            string oPlayerTeamName = "";
            string dPlayerTeamName = "";
            if (possession == 1)
            {
                offense = team1StarterStats;
                defense = team2StarterStats;
                oPlayerTeamName = team1.teamName;
                dPlayerTeamName = team2.teamName;
            }
            else
            {
                defense = team1StarterStats;
                offense = team2StarterStats;
                oPlayerTeamName = team2.teamName;
                dPlayerTeamName = team1.teamName;
            }


            double probORebound = 0.29;
            double probDRebound = 0.70;
            double probOutOfBounds = 0.01;
            // we calculate if an offensive rebound occurs
            foreach (PlayerInGame player in offense)
            {
                // find playerMatchup
                PlayerInGame playerMatchup = defense.Where(x => x.playerStats.position == playerWithBall.playerStats.position).ToList()[0];

                // convert rebound number from (45-99) to (-0.01 - 0.01) then add it to offensive rebound probability
                probORebound += -0.01 + (0.01 + 0.01) * (player.playerStats.Rebound - 45) / (99 - 45);
                // convert strengthDifference from (-63 - 63) to (-0.20 to 0.20)
                double strengthDifference = -0.20 + (0.20 + 0.20) * (player.playerStats.Strength - playerMatchup.playerStats.Strength + 63) / (63 + 63);
                probORebound += strengthDifference;

            }
            if (probORebound < 0) probORebound = 0;
            // we calculate if a defensive rebound occurs
            foreach (PlayerInGame player in defense)
            {

                // find playerMatchup
                PlayerInGame playerMatchup = offense.Where(x => x.playerStats.position == playerWithBall.playerStats.position).ToList()[0];
                // convert rebound number from (45-99) to (-0.01 - 0.01) then add it to defensive rebound probability
                probDRebound += -0.01 + (0.01 + 0.01) * (player.playerStats.Rebound - 45) / (99 - 45);
                // convert strengthDifference from (-63 - 63) to (-0.20 to 0.20)
                double strengthDifference = -0.20 + (0.20 + 0.20) * (player.playerStats.Strength - playerMatchup.playerStats.Strength + 63) / (63 + 63);
                probDRebound += strengthDifference;
            }
            if (probDRebound < 0) probDRebound = 0;
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
                PlayerInGame currentPlayer = new PlayerInGame(new Player());
                double pgRebound = pg.playerStats.Rebound / (double)reboundSum - 0.05;
                double sgRebound = sg.playerStats.Rebound / (double)reboundSum - 0.03;
                double sfRebound = sf.playerStats.Rebound / (double)reboundSum + 0.00;
                double pfRebound = pf.playerStats.Rebound / (double)reboundSum + 0.06;
                double cRebound = c.playerStats.Rebound / (double)reboundSum + 0.09;
                if (randomReboundProbability < pgRebound)
                {
                    currentPlayer = pg;
                }
                else if (randomReboundProbability < pgRebound + sgRebound)
                {
                    currentPlayer = sg;
                }
                else if (randomReboundProbability < pgRebound + sgRebound + sfRebound)
                {
                    currentPlayer = sf;
                }
                else if (randomReboundProbability < pgRebound + sgRebound + sfRebound + pfRebound)
                {
                    currentPlayer = pf;
                }
                else
                {
                    currentPlayer = c;
                }
                CommentatorPhrases.Add($"{currentPlayer.playerStats.playerForename} {currentPlayer.playerStats.playerSurname} got the offensive rebound for the {oPlayerTeamName}");
                (int, int) score = CalculateScore();
                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                GameTimestamps.Add(gameClock.PrintTime());

                currentPlayer.Rebounds++;
                playerWithBall = currentPlayer;

                // now we set the matchup for playerWithBall
                playerWithBallMatchup = defense.Where(x => x.playerStats.position == playerWithBall.playerStats.position).ToList()[0];
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
                PlayerInGame currentPlayer = new PlayerInGame(new Player());
                double pgRebound = pg.playerStats.Rebound / (double)reboundSum - 0.10;
                double sgRebound = sg.playerStats.Rebound / (double)reboundSum - 0.07;
                double sfRebound = sf.playerStats.Rebound / (double)reboundSum + 0.01;
                double pfRebound = pf.playerStats.Rebound / (double)reboundSum + 0.07;
                double cRebound = c.playerStats.Rebound / (double)reboundSum + 0.11;
                if (randomReboundProbability < pgRebound)
                {
                    currentPlayer = pg;
                }
                if (randomReboundProbability < sgRebound + pgRebound)
                {
                    currentPlayer = sg;
                }
                else if (randomReboundProbability < sfRebound + sgRebound + pgRebound)
                {
                    currentPlayer = sf;
                }
                else if (randomReboundProbability < pfRebound + sfRebound + sgRebound + pgRebound)
                {
                    currentPlayer = pf;
                }
                else
                {
                    currentPlayer = c;
                }
                CommentatorPhrases.Add($"{currentPlayer.playerStats.playerForename} {currentPlayer.playerStats.playerSurname} got the defensive rebound for the {dPlayerTeamName}");
                (int, int) score = CalculateScore();
                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                GameTimestamps.Add(gameClock.PrintTime());

                currentPlayer.Rebounds++;
                playerWithBall = currentPlayer;

                // now we change possession
                return 0;
            }
            // if no rebound occurs (out of bounds)
            else
            {
                CommentatorPhrases.Add("The shot went out of bounds");
                (int, int) score = CalculateScore();
                ScoreAfterEachPhrase.Add($"{score.Item1}-{score.Item2}");
                GameTimestamps.Add(gameClock.PrintTime());
            }

            return -1;
        }

        public int CalculateAssistProbability()
        {
            double probAssist = 0.8398;
            // convert passing from range (45-99) to (-0.03 - 0.16)
            probAssist += 0.16 * (playerWhoPassed.playerStats.Passing - 45) / (99 - 45);
            // this returns 1 if an assist occurred
            if (new Random().NextDouble() < probAssist) { playerWhoPassed.Assists++; return 1; }
            // returns -1 if no assist
            return -1;
        }

        public double UpdateGameValue(PlayerInGame player)
        {
            double sum = 0;
            // add made threes and twos (midRange, layup, dunk)
            sum += 3.20 * player.ThreePointMade + 2.40 * (player.FieldGoalMade - player.ThreePointMade);

            // add missed threes and twos (midRange, layup, dunk)
            sum += -1.45 * (player.ThreePointAttempted - player.ThreePointMade) + -1.05 * (player.FieldGoalAttempted - player.FieldGoalMade - (player.ThreePointAttempted - player.ThreePointMade));

            // add remaining stats
            sum += 0.55 * player.Rebounds + 2.15 * player.Assists + 2.50 * player.Blocks + 3.40 * player.Steals + -1.45 * player.Turnovers + -3.05 * player.PersonalFouls;
            return sum;
        }

        public void InsertPlayerGameData(int gameId, bool playoffs)
        {
            List<PlayerInGame> team1players = team1Stats;
            List<PlayerInGame> team2players = team2Stats;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    // add this team's stats from the game into the database
                    string fullTransactionQuery = $"BEGIN TRANSACTION; \n";
                    fullTransactionQuery += "INSERT into playerGameStats(seasonId,gameId,playerId,gameValue,isPlayoffs,MP,FGM,FGA,TFGM,TFGA,FTM,FTA,PTS,REB,AST,STL,BLK,TOV,PF)\r\n VALUES";
                    foreach (PlayerInGame player in team1players)
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
                            {player.FreeThrowMade},
                            {player.FreeThrowAttempted},
                            {player.Points},
                            {player.Rebounds},
                            {player.Assists},
                            {player.Steals},
                            {player.Blocks},
                            {player.Turnovers},
                            {player.PersonalFouls}
                            )," + "\n";
                    }

                    foreach (PlayerInGame player in team2players)
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
                            {player.FreeThrowMade},
                            {player.FreeThrowAttempted},
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
                    if (team1Stats.Sum(x => x.Points) > team2Stats.Sum(x => x.Points))
                    {
                        if (!CurrentLeague.Playoffs)
                        {
                            fullTransactionQuery += $"UPDATE teamResults SET wins = wins + 1 WHERE teamId = {team1.TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                            fullTransactionQuery += $"UPDATE teamResults SET losses = losses + 1 WHERE teamId = {team2.TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                        }
                        else
                        {
                            fullTransactionQuery += $"UPDATE playoffResults SET wins = wins + 1 WHERE homeTeamId = {team1Players[0].TeamId} AND awayTeamId = {team2Players[0].TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                        }
                    }
                    else
                    {
                        if (!CurrentLeague.Playoffs)
                        {
                            fullTransactionQuery += $"UPDATE teamResults SET wins = wins + 1 WHERE teamId = {team2.TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                            fullTransactionQuery += $"UPDATE teamResults SET losses = losses + 1 WHERE teamId = {team1.TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                        }
                        else
                        {
                            fullTransactionQuery += $"UPDATE playoffResults SET losses = losses + 1 WHERE homeTeamId = {team1.TeamId} AND awayTeamId = {team2Players[0].TeamId} AND seasonId = {CurrentLeague.CurrentSeason};";
                        }
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
            // work out 3 point attempted probabilities, (50-99) to (0-0.14)
            double threePointStat = playerWithBall.playerStats.ThreePoint;
            threePointAttempted = 0.22 * (threePointStat - 50) / (99 - 50);
            if (threePointAttempted < 0) threePointAttempted = 0;
            threePointAttempted *= Math.Pow(passerContribution, 2.6);
            if (playerWithBall.playerStats.SecondaryPlaystyle == "Playmaker") threePointAttempted *= 0.65;
            attemptedProbablities.Add(threePointAttempted);

            // work out 2 point attempted probabilities, (50-99) to (0-0.09)
            double twoPointStat = playerWithBall.playerStats.MidRange;
            twoPointAttempted = 0.11 * (twoPointStat - 50) / (99 - 50);
            if (twoPointAttempted < 0) twoPointAttempted = 0;
            twoPointAttempted *= Math.Pow(passerContribution, 2.4);
            if (playerWithBall.playerStats.SecondaryPlaystyle == "Playmaker") twoPointAttempted *= 0.65;
            attemptedProbablities.Add(twoPointAttempted);

            // work out layup attempted probabilities, (45-99) to (0.003-0.10)
            double layupStat = playerWithBall.playerStats.Layup;
            if (playerWithBall.playerStats.CloseShot > layupStat) layupStat = playerWithBall.playerStats.CloseShot;
            layupAttempted = 0.003 + 0.16 * (layupStat - 45) / (99 - 45);
            if (layupAttempted < 0) layupAttempted = 0;
            layupAttempted *= Math.Pow(passerContribution, 2.6);
            if (playerWithBall.playerStats.SecondaryPlaystyle == "Playmaker") layupAttempted *= 0.65;
            attemptedProbablities.Add(layupAttempted);

            // work out dunk attempted probabilities, (45-99) to (0.003-0.10)
            double dunkStat = playerWithBall.playerStats.Dunk;
            dunkAttempted = 0.003 + 0.15 * (dunkStat - 45) / (99 - 45);
            if (dunkAttempted < 0) dunkAttempted = 0;
            dunkAttempted *= Math.Pow(passerContribution, 2.5);
            if (playerWithBall.playerStats.SecondaryPlaystyle == "Playmaker") dunkAttempted *= 0.62;
            attemptedProbablities.Add(dunkAttempted);

            // convert overall (60 - 99) to (-0.10 to 0)
            double overallVariable = 0;
            if (playerWithBall.playerStats.Passing < 74)
            {
                overallVariable = -0.10 + 0.10 * (playerWithBall.playerStats.Overall - 60) / (99 - 60);
                overallVariable *= -1;
            }
            // work out pass attempted probablilites
            // passStat from (45-99) to (0-0.38), result is (0.40-0.78)
            double passStat = playerWithBall.playerStats.Passing;
            passAttempted = 0.40 + 0.38 * (passStat - 45) / (99 - 45);
            if (playerWithBall.FieldGoalMade > 4 && playerWithBall.GameValue > 19 && playerWithBall.playerStats.SecondaryPlaystyle != "Playmaker") passAttempted -= 0.024 + 0.003 * (playerWithBall.FieldGoalMade - 4);
            if (passAttempted < 0.14) passAttempted = 0.14;
            passAttempted += overallVariable;
            passAttempted /= Math.Pow(passerContribution, 2.5);
            attemptedProbablities.Add(passAttempted);

            // use this to normalise the probabilites
            double sum = attemptedProbablities.Sum();
            if (sum < 0.999 || sum > 1.001)
            {
                attemptedProbablities[0] /= sum;
                attemptedProbablities[1] /= sum;
                attemptedProbablities[2] /= sum;
                attemptedProbablities[3] /= sum;
                attemptedProbablities[4] /= sum;
            }

            return attemptedProbablities;
        }

        public void LoadMinutesToPlay(int userTeam)
        {
            List<PlayerInGame> userPlayers = new List<PlayerInGame>();
            List<(int, int)> userMinutes = new List<(int, int)>();
            // select all the minutes stored in the database for the user's players
            using (var connection = new SQLiteConnection(CurrentLeague.ConnectionString))
            {
                connection.Open();
                string getMinutesQuery = "SELECT * FROM minutesSelection";
                using (var command = new SQLiteCommand(getMinutesQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int playerId = reader.GetInt32(0);
                            int minutesToPlay = reader.GetInt32(1);
                            
                            userMinutes.Add((playerId, minutesToPlay));
                            
                        }
                    }
                }
            }

            if (userTeam == 1) userPlayers = team1Stats;
            else userPlayers = team2Stats;
            foreach (PlayerInGame player in userPlayers)
            {
                player.MinutesToPlay = userMinutes.Where(x => x.Item1 == player.playerStats.PlayerId).ToList()[0].Item2;
            }

            foreach (string position in positions)
            {
                List<PlayerInGame> teamPosition = userPlayers.Where(x => x.playerStats.position == position).ToList();
                int positionMinsSum = teamPosition.Sum(x => x.MinutesToPlay);
                foreach (PlayerInGame player in teamPosition) player.MinutesToPlay = (int)(player.MinutesToPlay / (positionMinsSum / 48.0));
                positionMinsSum = teamPosition.Sum(x => x.MinutesToPlay);
                if (positionMinsSum != 48)
                {
                    PlayerInGame currentPlayer = teamPosition.OrderBy(x => x.MinutesToPlay).First();
                    currentPlayer.MinutesToPlay -= positionMinsSum - 48;
                }
            }
        }

        public void GenerateMinutesToPlay(bool playoffs)
        {
            Dictionary<int, double> gameValues = GetTeamGameValues(playoffs);
            // now we calculate the minutes for each position in team1
            if (team1.teamName == CurrentLeague.UserTeamName) LoadMinutesToPlay(1);
            else if (team2.teamName == CurrentLeague.UserTeamName) LoadMinutesToPlay(2);
            {
                List<List<PlayerInGame>> teams = [team1Stats, team2Stats];
                for (int i = 0; i < 2; i++)
                {
                    List<PlayerInGame> currentTeam = teams[i];
                    if (currentTeam[0].playerStats.TeamId == CurrentLeague.GetIdFromTeamName(CurrentLeague.UserTeamName)) continue;
                    foreach (string currentPosition in positions)
                    {
                        List<PlayerInGame> teamPosition = currentTeam.Where(x => x.playerStats.position == currentPosition).OrderByDescending(x => x.playerStats.Overall).ToList();
                        foreach (PlayerInGame teamPlayer in teamPosition)
                        {
                            teamPlayer.MinutesToPlay = 16;
                            // convert pg1's overall from (60-99) to (-17 to +17)
                            double overall1Variable = -12.8 + (12.8 + 12.2) * (teamPlayer.playerStats.Overall - 60) / (99 - 60);
                            if (teamPlayer.playerStats.Overall > 79) overall1Variable += 0.31 * (teamPlayer.playerStats.Overall - 79);
                            // convert pg1's averageGameValue from (-5 to 60) to (-2 to 9)
                            double gameValue1Variable = -2 + (2 + 13) * (gameValues[teamPlayer.playerStats.PlayerId] + 5) / (5 + 50);
                            if (gameValue1Variable > 13) gameValue1Variable = 13;
                            teamPlayer.MinutesToPlay += (int)(overall1Variable + gameValue1Variable);
                            if (teamPlayer.MinutesToPlay < 0) teamPlayer.MinutesToPlay = 0;
                        }
                        int currentOverallSum = teamPosition.Sum(x => x.MinutesToPlay);
                        double currentOverallNormalisedSum = currentOverallSum / (double)48;
                        // if all the players at this position are bad, we just assign them 16 minutes each
                        if (currentOverallNormalisedSum <= 0)
                        {
                            currentOverallNormalisedSum = 48;
                            foreach (PlayerInGame teamPlayer in teamPosition) teamPlayer.MinutesToPlay = 16;
                        }
                        foreach (PlayerInGame teamPlayer in teamPosition)
                        {
                            teamPlayer.MinutesToPlay = (int)Math.Round(teamPlayer.MinutesToPlay / currentOverallNormalisedSum);
                        }
                        currentOverallSum = teamPosition.Sum(x => x.MinutesToPlay);
                        if (currentOverallSum > 48 || currentOverallSum < 48)
                        {
                            teamPosition[teamPosition.Count - 1].MinutesToPlay -= currentOverallSum - 48;
                        }
                    }
                }
            }
        }

        public void GeneratePlayerSlots()
        {
            Random random = new Random();
            List<List<PlayerInGame>> teams = [team1Stats, team2Stats];
            for (int i = 0; i < 2; i++)
            {
                List<PlayerInGame> currentTeam = teams[i];
                foreach (string currentPosition in positions)
                {
                    List<int> slots = new List<int>();
                    for (int j = 1; j <= 48; j++) slots.Add(j);
                    List<PlayerInGame> teamPosition = currentTeam.Where(x => x.playerStats.position == currentPosition).OrderByDescending(x => x.playerStats.Overall).ToList();
                    foreach (PlayerInGame teamPlayer in teamPosition)
                    {
                        for (int j = 0; j < teamPlayer.MinutesToPlay; j++)
                        {
                            int slotIndex = random.Next(slots.Count);
                            teamPlayer.SlotsPlaying.Add(slots[slotIndex]);
                            slots.Remove(slots[slotIndex]);
                        }
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
                    AND ((pot.dayJoined <= {CurrentLeague.CurrentDay} AND pot.yearJoined = {CurrentLeague.CurrentSeason} + 2023) OR (pot.yearJoined < {CurrentLeague.CurrentSeason} + 2023))
                    AND pot.dayLeft >= {CurrentLeague.CurrentDay}
                    AND pot.yearLeft >= {CurrentLeague.CurrentSeason} + 2023
                GROUP BY 
                    pot.playerId;
                ";
                if (CurrentLeague.Playoffs) getGameValueQuery = $@"
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
                    AND pot.dayJoined <= 150
                    AND pot.yearJoined <= {CurrentLeague.CurrentSeason} + 2023
                    AND pot.dayLeft >= 150
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

        public GameGenerator(Team team1, Team team2, string connectionString, int currentSaveState, int gameId, bool playoffs, League currentLeague)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.connectionString = connectionString;
            this.gameClock = new GameClock();
            CurrentSaveState = currentSaveState;
            GameId = gameId;
            CurrentLeague = currentLeague;


            // possession set to team 1 or team 2
            possession = new Random().Next(1, 3);
            team1StarterStats = new List<PlayerInGame>();
            team2StarterStats = new List<PlayerInGame>();
            (List<Player>, List<Player>) players = ExtractPlayersFromTeams(team1, team2);
            team1Players = players.Item1;
            team2Players = players.Item2;
            AddPlayersIntoInGame();
            GenerateMinutesToPlay(playoffs);
            GeneratePlayerSlots();
            GetStarters();
            int numPossessions = new Random().Next(196, 205);
            SimulatePossessions(gameId, numPossessions, playoffs);

        }
    }
}
