using System.Data.SQLite;
using System.Runtime.CompilerServices;

namespace LeagueSimulation.Models
{
    public class League
    {
        // these are attributes to access the file
        public string currentUser = "";
        private string leagueFileName = "";
        private string connectionString = "";
        private int currentSaveState = 0;

        // these are attributes based on user team
        private int currentDay = 0;
        private int currentSeason = 0;
        private int gamesPlayed = 0;
        private bool playoffs = false;
        private string playoffsRound = "";
        private string userTeamName = "";

        private List<List<string>> currentSchedule = new List<List<string>>();
        private List<List<string>> currentPlayoffsSchedule = new List<List<string>>();
        public string CurrentUser { get; set; }
        public string LeagueFileName { get; set; }
        public string ConnectionString { get; set; }
        public int CurrentSaveState { get; set; }
        //
        public int CurrentDay { get; set; }
        public int CurrentSeason { get; set; }
        public int GamesPlayed { get; set; }
        public string UserTeamName { get; set; }
        public bool Playoffs { get; set; }
        public string PlayoffsRound { get; set; }
        public List<List<string>> CurrentSchedule { get; set; }
        public List<List<string>> CurrentPlayoffsSchedule { get; set; }

        public void InitialiseDatabase(string leagueFileName)
        {
            // Creates a connection to league database
            string connectionString = $@"Data Source={leagueFileName};Version=3;";
            SQLiteConnection.CreateFile(leagueFileName);

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //create teams table
                string createTeamsTableQuery = @"
                        CREATE TABLE IF NOT EXISTS teams(
                        teamId INTEGER PRIMARY KEY AUTOINCREMENT,
                        teamName TEXT NOT NULL,
                        city TEXT NOT NULL,
                        conferenceId INTEGER NOT NULL
                        );";

                //create conference table
                string createConferenceQuery = @"
                        CREATE TABLE IF NOT EXISTS conferences(
                        conferenceId INTEGER PRIMARY KEY AUTOINCREMENT,
                        conferenceName TEXT NOT NULL
                        );";

                // create players table
                string createPlayersTableQuery = @"
                        CREATE TABLE IF NOT EXISTS players(
                        playerId INTEGER PRIMARY KEY AUTOINCREMENT,
                        playerOnTeamId INTEGER NOT NULL,
                        teamId INTEGER NOT NULL,
                        playerForename TEXT NOT NULL,
                        playerSurname TEXT NOT NULL,
                        dateOfBirth INTEGER NOT NULL,
                        overall INTEGER NOT NULL,
                        potential INTEGER NOT NULL,
                        positionId INTEGER NOT NULL,
                        secondaryPlaystyleId INTEGER NOT NULL,
                        height INTEGER NOT NULL,
                        weight INTEGER NOT NULL,
                        closeShot INTEGER NOT NULL,
                        layup INTEGER NOT NULL,
                        dunk INTEGER NOT NULL,
                        midRange INTEGER NOT NULL,
                        threePoint INTEGER NOT NULL,
                        freeThrow INTEGER NOT NULL,
                        passing INTEGER NOT NULL,
                        ballHandle INTEGER NOT NULL,
                        defense INTEGER NOT NULL,
                        steal INTEGER NOT NULL,
                        block INTEGER NOT NULL,
                        rebound INTEGER NOT NULL,
                        speed INTEGER NOT NULL,
                        stamina INTEGER NOT NULL,
                        strength INTEGER NOT NULL 
                        );";

                // create playerOnTeam table
                string createPlayerOnTeamTableQuery = @"
                        CREATE TABLE IF NOT EXISTS playerOnTeam(
                        playerOnTeamId INTEGER PRIMARY KEY AUTOINCREMENT,
                        playerId INTEGER NOT NULL,
                        teamId INTEGER NOT NULL,
                        dayJoined INTEGER NOT NULL,
                        yearJoined INTEGER NOT NULL,
                        dayLeft INTEGER NOT NULL,
                        yearLeft INTEGER NOT NULL
                        );";

                string createPlayerGameStatsQuery = @"
                        CREATE TABLE IF NOT EXISTS playerGameStats(
                        playerGameStatsId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        gameId INTEGER NOT NULL,
                        playerId INTEGER NOT NULL,
                        seasonId INTEGER NOT NULL,
                        isPlayoffs BOOLEAN NOT NULL,
                        gameValue DECIMAL (3,1) NOT NULL,
                        MP INTEGER NOT NULL,
                        FGM INTEGER NOT NULL,
                        FGA INTEGER NOT NULL,
                        TFGM INTEGER NOT NULL,
                        TFGA INTEGER NOT NULL,
                        FTM INTEGER NOT NULL,
                        FTA INTEGER NOT NULL,
                        PTS INTEGER NOT NULL,
                        REB INTEGER NOT NULL,
                        AST INTEGER NOT NULL,
                        STL INTEGER NOT NULL,
                        BLK INTEGER NOT NULL,
                        TOV INTEGER NOT NULL,
                        PF INTEGER NOT NULL,
                        FOREIGN KEY (playerId) REFERENCES players(playerId)
                        );";

                string createScheduleTableQuery = @"
                    CREATE TABLE IF NOT EXISTS seasonSchedule(
                    gameId INTEGER NOT NULL,
                    seasonId INTEGER NOT NULL,
                    dayId INTEGER NOT NULL,
                    homeTeamId INTEGER NOT NULL,
                    awayTeamId INTEGER NOT NULL,
                    gameCompleted BOOLEAN NOT NULL,
                    PRIMARY KEY (seasonId, dayId, gameId)
                    );";

                string createPlayoffsScheduleTableQuery = @"
                    CREATE TABLE IF NOT EXISTS playoffsSchedule(
                    playoffsGameId INTEGER NOT NULL,
                    seasonId INTEGER NOT NULL,
                    dayId INTEGER NOT NULL,
                    conferenceId INTEGER NOT NULL,
                    seed INTEGER NOT NULL,
                    playoffsRound TEXT NOT NULL,
                    homeTeamId INTEGER NOT NULL,
                    awayTeamId INTEGER NOT NULL,
                    gameCompleted BOOLEAN NOT NULL,
                    PRIMARY KEY (playoffsGameId AUTOINCREMENT)
                    );";

                string leagueTableQuery = @"
                    CREATE TABLE IF NOT EXISTS league(
                    leagueId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    currentDay INTEGER NOT NULL,
                    currentSeason INTEGER NOT NULL,
                    userTeamName TEXT NOT NULL
                    );";

                string primaryPlaystyleQuery = @"
                    CREATE TABLE IF NOT EXISTS primaryPlaystyle(
                    primaryPlaystyleId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    playstyle TEXT NOT NULL,
                    description TEXT NOT NULL
                    );";

                string secondaryPlaystyleQuery = @"
                    CREATE TABLE IF NOT EXISTS secondaryPlaystyle(
                    secondaryPlaystyleId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    primaryPlaystyleId INTEGER NOT NULL,
                    playstyle TEXT NOT NULL,
                    description TEXT NOT NULL
                    );";

                string positionQuery = $@"
                    CREATE TABLE IF NOT EXISTS position(
                    positionId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    positionShort TEXT NOT NULL,
                    positionName TEXT NOT NULL,
                    description TEXT NOT NULL
                    );";

                string progressionQuery = $@"
                    CREATE TABLE IF NOT EXISTS playerProgression(
                    progressionId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    playerId INTEGER NOT NULL,
                    seasonId INTEGER NOT NULL,
                    progression INTEGER NOT NULL
                    );";

                string awardsTeamQuery = $@"
                    CREATE TABLE IF NOT EXISTS seasonTeamAwards(
                    seasonId INTEGER NOT NULL,
                    positionId INTEGER NOT NULL,
                    AllNBAOne INTEGER NOT NULL,
                    AllNBATwo INTEGER NOT NULL,
                    AllNBAThree INTEGER NOT NULL,
                    AllDefenseOne INTEGER NOT NULL,
                    AllDefenseTwo INTEGER NOT NULL,
                    AllDefenseThree INTEGER NOT NULL,
                    PRIMARY KEY (seasonId, positionId)
                    );";

                string awardsNonTeamQuery = $@"
                    CREATE TABLE IF NOT EXISTS seasonNonTeamAwards(
                    seasonId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    MVP INTEGER NOT NULL,
                    DPOY INTEGER NOT NULL,
                    Champion INTEGER NOT NULL,
                    FMVP INTEGER NOT NULL,
                    EFMVP INTEGER NOT NULL,
                    WFMVP INTEGER NOT NULL,
                    ROY INTEGER NOT NULL,
                    MPLeader INTEGER NOT NULL,
                    PTSLeader INTEGER NOT NULL,
                    REBLeader INTEGER NOT NULL,
                    ASTLeader INTEGER NOT NULL,
                    STLLeader INTEGER NOT NULL,
                    BLKLeader INTEGER NOT NULL,
                    TOVLeader INTEGER NOT NULL,
                    FGMLeader INTEGER NOT NULL,
                    FGALeader INTEGER NOT NULL,
                    FGPCTLeader INTEGER NOT NULL,
                    TFGMLeader INTEGER NOT NULL,
                    TFGALeader INTEGER NOT NULL,
                    TFGPCTLeader INTEGER NOT NULL
                    );";

                string teamResultQuery = $@"
                        CREATE TABLE IF NOT EXISTS teamResults(
                        teamId INTEGER NOT NULL,
                        seasonId INTEGER NOT NULL,
                        wins INTEGER NOT NULL,
                        losses INTEGER NOT NULL,
                        PRIMARY KEY (teamId, seasonId)
                        );";

                string playoffResultsQuery = $@"
                        CREATE TABLE IF NOT EXISTS playoffResults(
                        homeTeamId INTEGER NOT NULL,
                        awayTeamId INTEGER NOT NULL,
                        seasonId INTEGER NOT NULL,
                        playoffsRound TEXT NOT NULL,
                        conferenceId INTEGER NOT NULL,
                        seed INTEGER NOT NULL,
                        wins INTEGER NOT NULL,
                        losses INTEGER NOT NULL,
                        PRIMARY KEY (homeTeamId, awayTeamId, seasonId)
                        );";

                string minutesSelectionQuery = $@"
                        CREATE TABLE IF NOT EXISTS minutesSelection(
                        playerId INTEGER NOT NULL,
                        minutesToPlay INTEGER NOT NULL,
                        PRIMARY KEY (playerId, minutesToPlay)
                        );";

                string createPlayerGameStatsIndexQuery = $@"
                CREATE INDEX IF NOT EXISTS playerGameStatsOnlySeasonIdIndex ON playerGameStats(seasonId);
                CREATE INDEX playerGameStatsPlayerIdSeasonIdIndex ON playerGameStats(playerId, seasonId);";

                string createSeasonScheduleIndexQuery = $"CREATE INDEX seasonScheduleIndex ON seasonSchedule(seasonId);";

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createTeamsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createConferenceQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayersTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayerOnTeamTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayerGameStatsQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayerGameStatsIndexQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createScheduleTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayoffsScheduleTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = leagueTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = primaryPlaystyleQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = secondaryPlaystyleQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = positionQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = progressionQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = awardsNonTeamQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = awardsTeamQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = teamResultQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = playoffResultsQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createSeasonScheduleIndexQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = minutesSelectionQuery;
                    command.ExecuteNonQuery();
                }
            }

            // in this section, we insert needed data directly into the database
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // insert primary playstyles
                {
                    // insert data into playstyle tables
                    // we store the list of playstyles and their descriptions
                    List<(string, string)> values = new List<(string, string)>()
                {
                    ("Offensive", "A player who takes pride in scoring the ball"),
                    ("Defensive", "A player who takes pride in defending the ball"),
                    ("2-Way", "A player who can play effectively on both sides of the ball")
                };

                    string primaryPlaystyleQuery = $@"
                    INSERT INTO primaryPlaystyle(playstyle,description)
                    VALUES(x,y)";

                    foreach ((string, string) value in values)
                    {
                        primaryPlaystyleQuery = $@"
                        INSERT INTO primaryPlaystyle(playstyle,description)
                        VALUES(x,y)";
                        primaryPlaystyleQuery = primaryPlaystyleQuery.Replace("x,y", $"'{value.Item1}','{value.Item2}'");
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = primaryPlaystyleQuery;
                            command.ExecuteNonQuery();
                        }
                    }
                }
                // insert secondary playstyles
                {
                    // insert data into playstyle tables
                    // we store the list of playstyles and their descriptions
                    List<(string, string)> offensiveValues = new List<(string, string)>()
                    {
                        ("Shooter", "a player known for their ability to score points by consistently making accurate shots from various distances, especially beyond the arc"),
                        ("Playmaker", "a player who excels at creating scoring opportunities for teammates through excellent passing, court vision, and decision-making"),
                        ("Finisher", "a player who excels at converting scoring opportunities close to the basket, often through layups, dunks, or contested shots")
                    };
                    List<(string, string)> defensiveValues = new List<(string, string)>()
                    {
                        ("Ripper", "A player who likes to steal the ball from his opponent"),
                        ("Lockdown", "A player who likes to play great defense to stop the opponents from scoring"),
                        ("Rim Protector", "A player who likes to prevent buckets in and around the rim")
                    };

                    (string, string) twoWayValue = ("2-Way Player", "A player who can play effectively on both sides of the ball");

                    string secondaryPlaystyleQuery = $@"
                    INSERT INTO secondaryPlaystyle(playstyle,primaryPlaystyleId,description)
                    VALUES(x,y,z)";

                    foreach ((string, string) value in offensiveValues)
                    {
                        secondaryPlaystyleQuery = $@"
                        INSERT INTO secondaryPlaystyle(playstyle,primaryPlaystyleId,description)
                        VALUES(x,y,z)";
                        secondaryPlaystyleQuery = secondaryPlaystyleQuery.Replace("x,y,z", $"'{value.Item1}',1,'{value.Item2}'");
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = secondaryPlaystyleQuery;
                            command.ExecuteNonQuery();
                        }
                    }

                    foreach ((string, string) value in defensiveValues)
                    {
                        secondaryPlaystyleQuery = $@"
                        INSERT INTO secondaryPlaystyle(playstyle,primaryPlaystyleId,description)
                        VALUES(x,y,z)";
                        secondaryPlaystyleQuery = secondaryPlaystyleQuery.Replace("x,y,z", $"'{value.Item1}',2,'{value.Item2}'");
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = secondaryPlaystyleQuery;
                            command.ExecuteNonQuery();
                        }
                    }

                    string twoWayPlaystyleQuery = $@"INSERT INTO secondaryPlaystyle(playstyle,primaryPlaystyleId,description)
                        VALUES('{twoWayValue.Item1}',3,'{twoWayValue.Item2}')";

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = twoWayPlaystyleQuery;
                        command.ExecuteNonQuery();
                    }
                }
                // insert position
                {
                    string pgQuery = $@"
                        INSERT INTO position(positionShort,positionName,description)
                        VALUES('PG','Point Guard', 'player who organises the offense')
                        ";

                    string sgQuery = $@"
                        INSERT INTO position(positionShort,positionName,description)
                        VALUES('SG','Shooting Guard', 'scorer and defender, specializing in perimeter play')
                        ";

                    string sfQuery = $@"
                        INSERT INTO position(positionShort,positionName,description)
                        VALUES('SF','Small Forward', 'player who scores, defends, and facilitates from the wing')
                        ";

                    string pfQuery = $@"
                        INSERT INTO position(positionShort,positionName,description)
                        VALUES('PF','Power Forward', 'a strong rebounder')
                        ";

                    string cQuery = $@"
                        INSERT INTO position(positionShort,positionName,description)
                        VALUES('C','Center', 'the tallest player, focused on rim protection, rebounding, and inside scoring')
                        ";

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = pgQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = sgQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = sfQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = pfQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = cQuery;
                        command.ExecuteNonQuery();
                    }
                }

                // insert conference
                {
                    string eastQuery = $@"
                        INSERT INTO conferences(conferenceName)
                        VALUES('East')
                        ";

                    string westQuery = $@"
                        INSERT INTO conferences(conferenceName)
                        VALUES('West')
                        ";

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = eastQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = westQuery;
                        command.ExecuteNonQuery();
                    }
                }

            }


        }
        public void GenerateTeams(string leagueFileName)
        {
            string teamFilePath = $@"C:\Users\{CurrentUser}\OneDrive - The Kings School Chester\A-Level\Computer Science\NEA Project\Project Files\LeagueSimulation\Player Data\basketball_team_names_list.txt";
            string[] teamNames = File.ReadAllLines(teamFilePath);
            string playerFilePath = $@"C:\Users\{CurrentUser}\OneDrive - The Kings School Chester\A-Level\Computer Science\NEA Project\Project Files\LeagueSimulation\Player Data\male_names_list_NEW.txt";
            List<string> playerNames = File.ReadAllLines(playerFilePath).ToList();

            // Creates a connection to leagueX database
            string connectionString = $@"Data Source={leagueFileName};Version=3;";
            if (File.Exists(leagueFileName))
            {
                List<string> positions = new List<string>() { "PG", "SG", "SF", "PF", "C" };
                List<string> playstyles = new List<string>()
                {
                    "Shooter",
                    "Playmaker",
                    "Finisher",
                    "Ripper",
                    "Lockdown",
                    "Rim Protector",
                    "2-Way Player"
                };
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    // This makes sure we go through 450 names
                    int nameCounter = -1;
                    using (var command = new SQLiteCommand(connection))
                    {
                        string fullQuery = "BEGIN TRANSACTION; \n";
                        string fullTeamQuery = "INSERT INTO teams(teamName,city,conferenceId) \n VALUES";
                        string fullPlayerQuery = @"INSERT INTO players(playerOnTeamId, teamId,playerForename,playerSurname,positionId,dateOfBirth,overall,potential,
                            secondaryPlaystyleId,height,weight,closeShot,layup,dunk,midRange,threePoint,freeThrow,passing,ballHandle,defense,
                            steal,block,rebound,speed,strength,stamina,overall) VALUES";

                        string fullPlayerOnTeamQuery = "INSERT INTO playerOnTeam(playerId,teamId,dayJoined,yearJoined,dayLeft,yearLeft) \n VALUES";
                        // generate 30 teams
                        for (int i = 0; i < 30; i++)
                        {
                            Random random = new Random();
                            Team team = new Team(teamNames[i], i + 1, CurrentUser);
                            int conference = 0;
                            if (i < 15) conference = 1;
                            else
                            {
                                conference = 2;
                                team.Position -= 15;
                            }
                            int teamOverall = random.Next(77, 82);
                            fullTeamQuery += $@"
	                        (
                            '{team.teamName}',
	                        '{team.city}',
                            {conference}
                            )," + "\n";
                            List<Player> team1Players = new List<Player>();
                            // generate 15 players for this current team
                            for (int j = 0; j < 15; j++)
                            {
                                // this calculates the position for the current player
                                // this will be used to generate players for a specific team
                                string position = "";
                                if (j < 3) position = "PG";
                                else if (j < 6) position = "SG";
                                else if (j < 9) position = "SF";
                                else if (j < 12) position = "PF";
                                else position = "C";

                                // generate player name
                                nameCounter = random.Next(playerNames.Count());
                                string[] playerName = playerNames[nameCounter].Split(' ');
                                playerNames.Remove(playerNames[nameCounter]);

                                // a player is created, then added to the database
                                Player player = new Player();
                                player.GeneratePlayer(position, i + 1, team.teamName, playerName[0], playerName[1], teamOverall);
                                team1Players.Add(player);
                            }

                            // now we add the players in this team, to the database
                            for (int j = 0; j < team1Players.Count; j++)
                            {
                                Player player = team1Players[j];
                                player.PlayerId = j + 1;
                                fullPlayerOnTeamQuery += $@"
                                (
                                {player.PlayerId + i * 15},
                                {team.TeamId},
                                1,
                                2024,
                                999,
                                9999
                                )," + "\n";

                                int playerOnTeamId = player.PlayerId + i * 15;
                                int positionId = positions.IndexOf(player.position) + 1;
                                int playstyleId = playstyles.IndexOf(player.SecondaryPlaystyle) + 1;


                                fullPlayerQuery += $@"
                                (
                                {playerOnTeamId},
                                {team.TeamId},
                                '{player.playerForename}',
                                '{player.playerSurname}',
                                {positionId},
                                {2024 - player.Age},
                                {player.Overall},
                                {player.Potential},
                                {playstyleId},
                                {player.Height},
                                {player.Weight},
                                {player.CloseShot},
                                {player.Layup},
                                {player.Dunk},
                                {player.MidRange},
                                {player.ThreePoint},
                                {player.FreeThrow},
                                {player.Passing},
                                {player.BallHandle},
                                {player.Defense},
                                {player.Steal},
                                {player.Block},
                                {player.Rebound},
                                {player.Speed},
                                {player.Strength},
                                {player.Stamina},
                                {player.Overall}
                                )," + "\n";

                            }
                        }

                        fullTeamQuery = fullTeamQuery.Substring(0, fullTeamQuery.Length - 2) + ";";
                        fullPlayerQuery = fullPlayerQuery.Substring(0, fullPlayerQuery.Length - 2) + ";";
                        fullPlayerOnTeamQuery = fullPlayerOnTeamQuery.Substring(0, fullPlayerOnTeamQuery.Length - 2) + ";";
                        fullQuery += fullTeamQuery + fullPlayerQuery + fullPlayerOnTeamQuery + "COMMIT;";

                        command.CommandText = fullQuery;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        // gets the user's team leader in points, rebounds and assists to display to user in the dashboard
        public string GetUserTeamLeaderInStatistic(string stat)
        {
            string fullTeamLeaderString = "";
            List<int> playersOnTeam = new List<int>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string teamLeaderQuery = $@"
                    WITH currentPlayers AS (
                    SELECT playerId, teamId
                    FROM playerOnTeam pot
                    WHERE ((dayJoined <= {CurrentDay} AND yearJoined = {CurrentSeason + 2023}) OR (yearJoined < {CurrentSeason + 2023}))
                    AND dayLeft >= {CurrentDay} AND yearLeft >= {CurrentSeason + 2023}
                    AND pot.teamId = {GetIdFromTeamName(UserTeamName)} -- Replace with the given teamId
                    ),
                    PlayerAverage{stat} AS (
                        SELECT
                            p.playerId,
                            p.playerForename,
                            p.playerSurname,
                            AVG(pgs.{stat}) AS avg{stat}
                        FROM
                            currentPlayers cp
                        JOIN
                            playerGameStats pgs ON pgs.playerId = cp.playerId
                            AND pgs.seasonId = {CurrentSeason}
                        JOIN
                            players p ON p.playerId = cp.playerId
                            
                        GROUP BY
                            p.playerId, p.playerForename, p.playerSurname
                    )
                    SELECT
                        playerForename,
                        playerSurname,
                        avg{stat}
                    FROM
                        PlayerAverage{stat}
                    ORDER BY
                        avg{stat} DESC
                    LIMIT 1;
                    ";
                if (Playoffs) teamLeaderQuery = teamLeaderQuery.Replace($"= {CurrentDay}", "= 150");
                using (var command = new SQLiteCommand(teamLeaderQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // we add the player's name to the string
                            string playerForename = reader.GetString(reader.GetOrdinal("playerForename"));
                            string playerSurname = reader.GetString(reader.GetOrdinal("playerSurname"));
                            string playerStat = Math.Round(reader.GetDouble(reader.GetOrdinal($"avg{stat}")), 1).ToString();
                            fullTeamLeaderString = $"{playerForename} {playerSurname}: {playerStat} {stat}";
                        }
                    }
                }

                return fullTeamLeaderString;
            }

        }

        // this returns the user's average team stats e.g. average total points per game
        public string GetTeamStatistic(string stat)
        {
            string teamLeaderQuery = $@"
                    WITH currentPlayers AS (
                    SELECT playerId, teamId
                    FROM playerOnTeam pot
                    WHERE ((dayJoined <= {CurrentDay} AND yearJoined = {CurrentSeason + 2023}) OR (yearJoined < {CurrentSeason + 2023}))
					AND dayLeft >= {CurrentDay} AND yearLeft >= {CurrentSeason + 2023}
                    AND pot.teamId = {GetIdFromTeamName(UserTeamName)} -- Replace with the given teamId
                    ),
                    TeamAverage{stat} AS (
                        SELECT
                            cp.teamId,
                            pgs.gameId,
                            pgs.seasonId,
                            SUM(pgs.{stat}) AS total{stat}
                        FROM
                            currentPlayers cp
                        JOIN 
							playerGameStats pgs 
						ON cp.playerId = pgs.playerId
                        AND pgs.seasonId = {CurrentSeason}
                        GROUP BY
                            cp.teamId, pgs.gameId, pgs.seasonId
                    )
                    SELECT
                        tav.teamId,
                        t.teamName,
                        AVG(tav.total{stat}) AS avgTeamPoints
                    FROM
                        TeamAverage{stat} tav
                    JOIN
                        teams t ON tav.teamId = t.teamId
                    GROUP BY
                        tav.teamId, t.teamName;
                    ";
            if (Playoffs) teamLeaderQuery = teamLeaderQuery.Replace($"= {CurrentDay}", "= 150");
            string fullTeamAverageStr = "";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(teamLeaderQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fullTeamAverageStr += Math.Round(reader.GetDouble(reader.GetOrdinal("avgTeamPoints")), 1).ToString();
                        }
                    }
                }
            }
            return fullTeamAverageStr;
        }

        // returns a team's record, when their team name is entered
        public string GetTeamRecord(string teamName)
        {
            int wins = 0;
            int losses = 0;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string getWinsLossesQuery = $@"
                SELECT tr.wins, tr.losses 
                FROM teamResults tr 
                JOIN teams t ON tr.teamId = t.teamId 
                WHERE t.teamName = '{teamName}' 
                AND tr.seasonId = {CurrentSeason}";
                using (var command = new SQLiteCommand(getWinsLossesQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wins = reader.GetInt32(reader.GetOrdinal("wins"));
                            losses = reader.GetInt32(reader.GetOrdinal("losses"));
                        }
                    }
                }
            }
            return $"{wins}-{losses}";
        }

        // this returns the teams in a given conference, in winning percentage order
        public List<string> GetConferenceTeamsByWinPct(int conferenceId)
        {
            List<string> teams = new List<string>();
            string orderTeamsQuery = $@"
                SELECT 
                t.teamId, 
                t.teamName,
                CASE WHEN tr.losses = 0 THEN tr.wins 
                ELSE CAST(tr.wins AS DOUBLE) / (tr.wins + tr.losses) 
                END AS winPct
                FROM teams t
                LEFT JOIN teamResults tr ON tr.teamId = t.teamId
                AND t.conferenceId = {conferenceId}
                WHERE tr.seasonId = {CurrentSeason}
                ORDER BY winPct DESC
                ";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(orderTeamsQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) teams.Add(reader.GetString(reader.GetOrdinal("teamName")));
                    }
                }
            }

            return teams;
        }

        public List<string> GetPlayoffGamesByRound(string round)
        {
            List<string> games = new List<string>();
            string getGamesQuery = $@"
                SELECT homeTeamId, awayTeamId, conferenceId
                FROM playoffsSchedule ps
                WHERE playoffsRound = '{round}' AND seasonId = {CurrentSeason}
                ;";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getGamesQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            games.Add($"{reader.GetInt32(0)},{reader.GetInt32(1)},{reader.GetInt32(2)}");
                        }
                    }
                }
            }
            return games;
        }

        public string GetSeriesRecordToDisplay(string team1Id, string team2Id)
        {
            string getShortRecordQuery = $@"
                SELECT
                homeTeamId,
                awayTeamId,
                wins,
                losses
                FROM playoffResults pr
                WHERE
                (pr.homeTeamId = {team1Id}
                AND pr.awayTeamId = {team2Id}
                AND pr.seasonId = {CurrentSeason})
                OR (pr.homeTeamId = {team2Id}
                AND pr.awayTeamId = {team1Id}
                AND pr.seasonId = {CurrentSeason})
            ";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getShortRecordQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string homeTeamId = reader.GetInt32(reader.GetOrdinal("homeTeamId")).ToString();
                            if (homeTeamId == team1Id) return $"{reader.GetInt32(reader.GetOrdinal("wins"))}-{reader.GetInt32(reader.GetOrdinal("losses"))}";
                            else return $"{reader.GetInt32(reader.GetOrdinal("losses"))}-{reader.GetInt32(reader.GetOrdinal("wins"))}";

                        }
                    }
                }
            }
            return "0-0";
        }
        // start copying from here
        public string GetTeamNameFromId(string id)
        {
            string getIdQuery = $"SELECT teamName FROM teams WHERE teamId = {id}";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getIdQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) return reader.GetString(0);
                    }
                }
            }
            return "";
        }

        public string GetCityNameFromId(string id)
        {
            string getIdQuery = $"SELECT city FROM teams WHERE teamId = {id}";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getIdQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) return reader.GetString(0);
                    }
                }
            }
            return "";
        }
        public int GetUserConferenceId()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getConferenceQuery = $@"SELECT conferenceId from teams WHERE teamName = '{UserTeamName}';";
                using (var command = new SQLiteCommand(getConferenceQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // returns the conference of the user's team
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return 0;
        }

        public int GetConferenceIdFromTeamId(string teamId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getConferenceQuery = $@"SELECT conferenceId from teams WHERE teamId = {teamId};";
                using (var command = new SQLiteCommand(getConferenceQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // returns the conference of the user's team
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return 0;
        }
        public int GetLeaguePositionInConf(int conferenceId, string teamName) => GetConferenceTeamsByWinPct(conferenceId).IndexOf(teamName) + 1;
        public List<string> GetConferenceTeams(int conferenceId) => GetConferenceTeamsByWinPct(conferenceId);
        public List<string> GetTeamUpcomingGames()
        {
            List<string> teamUpcomingGames = new List<string>();
            List<List<string>> schedule = new List<List<string>>();
            if (Playoffs) schedule = CurrentPlayoffsSchedule;
            else schedule = CurrentSchedule;
            for (int i = CurrentDay; i < schedule.Count; i++)
            {
                List<string> currentScheduleDay = schedule[i];
                foreach (string game in currentScheduleDay)
                {
                    string[] gameSplit = game.Split(',');
                    string userTeam = "";
                    string otherTeam = "";
                    if (GetTeamNameFromId(gameSplit[0]) == UserTeamName)
                    {
                        userTeam = GetTeamNameFromId(gameSplit[0]);
                        otherTeam = GetTeamNameFromId(gameSplit[1]);
                        teamUpcomingGames.Add($"{Team.GetCityFromTeamName(userTeam)} vs {Team.GetCityFromTeamName(otherTeam)}");
                    }
                    else if (GetTeamNameFromId(gameSplit[1]) == UserTeamName)
                    {
                        userTeam = GetTeamNameFromId(gameSplit[1]);
                        otherTeam = GetTeamNameFromId(gameSplit[0]);
                        teamUpcomingGames.Add($"{Team.GetCityFromTeamName(userTeam)} @ {Team.GetCityFromTeamName(otherTeam)}");
                    }

                    if (teamUpcomingGames.Count > 2) break;
                }
                if (teamUpcomingGames.Count > 2) break;
            }
            return teamUpcomingGames;
        }
        public List<string> GetGamesForDay(int day) => CurrentSchedule[day - 1];

        public List<string> GetPlayoffGamesForDay(int day) => CurrentPlayoffsSchedule[day - 1];

        public void CreateLeague()
        {
            // we initialise the database and teams of the league
            InitialiseDatabase(LeagueFileName);
            GenerateTeams(LeagueFileName);
            // now we create a league schedule
            CurrentSchedule = GenerateSchedule();
            InsertLeagueData();
            InsertLeagueSchedule(CurrentSchedule);
        }

        public void LoadLeague(int saveState)
        {
            // we get the schedule from the database
            LoadLeagueData();
            CurrentSchedule = LoadLeagueSchedule();
            if (Playoffs) CurrentPlayoffsSchedule = LoadPlayoffsSchedule();
        }

        public void InsertLeagueSchedule(List<List<string>> schedule)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertGameIntoScheduleQuery = "BEGIN TRANSACTION; \n";
                insertGameIntoScheduleQuery += "INSERT into seasonSchedule(seasonId,dayId,gameId,homeTeamId,awayTeamId,gameCompleted) \r\n VALUES";
                int gameId = 0;
                for (int i = 0; i < schedule.Count; i++)
                {
                    List<string> scheduleDay = schedule[i];

                    for (int j = 0; j < scheduleDay.Count; j++)
                    {
                        gameId++;
                        string scheduleGame = scheduleDay[j];
                        string[] teamsInScheduleGame = scheduleGame.Split(',');
                        insertGameIntoScheduleQuery += $@"
                            ({CurrentSeason},{i + 1},
                                {gameId},
                                {int.Parse(teamsInScheduleGame[0])},
                                {int.Parse(teamsInScheduleGame[1])},
                                FALSE)
                            ," + "\n";
                    }
                }
                insertGameIntoScheduleQuery = insertGameIntoScheduleQuery.Substring(0, insertGameIntoScheduleQuery.Length - 2) + ";";
                insertGameIntoScheduleQuery += "COMMIT;";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = insertGameIntoScheduleQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertLeagueData()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertLeagueQuery = $@"
                    INSERT into league(currentDay,currentSeason,userTeamName)
                    VALUES({CurrentDay},
                    {CurrentSeason},
                    '{UserTeamName}')
                    ;";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = insertLeagueQuery;
                    command.ExecuteNonQuery();
                }
            }

            string fullQuery = "BEGIN TRANSACTION; \n INSERT INTO teamResults(teamId, seasonId, wins, losses) \n VALUES";
            for (int i = 0; i < 30; i++)
            {
                fullQuery += $"({i + 1}, {CurrentSeason}, 0, 0)";
                if (i == 29) fullQuery += ";" + "\n";
                else fullQuery += "," + "\n";
            }
            fullQuery += "COMMIT;";
            

            string minutesToPlayQuery = $@"WITH currentPlayers AS (
                SELECT playerId, teamId
                FROM playerOnTeam, league
                WHERE ((dayJoined <= league.CurrentDay AND yearJoined = league.CurrentSeason + 2023) OR (yearJoined < league.CurrentSeason + 2023))
                AND dayLeft >= league.CurrentDay AND yearLeft >= league.CurrentSeason + 2023 AND teamId = {GetIdFromTeamName(UserTeamName)}
                )
                INSERT INTO minutesSelection(playerId, minutesToPlay)
                SELECT playerId, 16 FROM currentPlayers;";

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = fullQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = minutesToPlayQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLeagueDataIfNeeded(int currentDayOfGames)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string updateLeagueQuery = $@"
                    UPDATE league
                    SET currentDay = {CurrentDay},
                    currentSeason = {CurrentSeason}
                    ;";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = updateLeagueQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void LoadLeagueData()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getScheduleQuery = $@"
                    SELECT *
                    FROM league
                    ;";

                using (var command = new SQLiteCommand(getScheduleQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int currentDay = reader.GetInt32(reader.GetOrdinal("currentDay"));
                            int currentSeason = reader.GetInt32(reader.GetOrdinal("currentSeason"));
                            string userTeamName = reader.GetString(reader.GetOrdinal("userTeamName"));
                            CurrentDay = currentDay;
                            CurrentSeason = currentSeason;
                            UserTeamName = userTeamName;
                            Playoffs = CheckIfPlayoffs();
                            if (Playoffs) PlayoffsRound = GetPlayoffsRound();
                        }
                    }
                }
            }
        }

        public List<List<string>> LoadLeagueSchedule()
        {
            List<List<string>> scheduleToPull = new List<List<string>>();
            int gameCounter = 0;
            int dayCounter = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getScheduleQuery = $@"
                    SELECT *
                    FROM seasonSchedule
                    WHERE seasonId = {CurrentSeason}
                    ;";

                using (var command = new SQLiteCommand(getScheduleQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<string> scheduleDayToPull = new List<string>();
                        while (reader.Read())
                        {
                            if (reader.GetInt32(reader.GetOrdinal("seasonId")) != CurrentSeason) continue;
                            if (reader.GetInt32(reader.GetOrdinal("gameId")) > gameCounter)
                            {
                                gameCounter = reader.GetInt32(reader.GetOrdinal("gameId"));
                            }
                            string team1 = reader.GetInt32(reader.GetOrdinal("homeTeamId")).ToString();
                            string team2 = reader.GetInt32(reader.GetOrdinal("awayTeamId")).ToString();
                            string scheduleGameToPull = $"{team1},{team2}";
                            if (reader.GetInt32(reader.GetOrdinal("dayId")) > dayCounter)
                            {
                                if (scheduleDayToPull.Count > 0) scheduleToPull.Add(scheduleDayToPull);
                                scheduleDayToPull = new List<string>();
                                dayCounter = reader.GetInt32(reader.GetOrdinal("dayId"));

                            }
                            scheduleDayToPull.Add(scheduleGameToPull);
                        }
                        if (scheduleDayToPull.Count > 0) scheduleToPull.Add(scheduleDayToPull);
                    }
                }
            }
            return scheduleToPull;
        }

        public List<List<string>> LoadPlayoffsSchedule()
        {
            List<List<string>> scheduleToPull = new List<List<string>>();
            int gameCounter = 0;
            int dayCounter = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getScheduleQuery = $@"
                    SELECT *
                    FROM playoffsSchedule
                    WHERE seasonId = {CurrentSeason}
                    ORDER BY dayId
                    ;";

                using (var command = new SQLiteCommand(getScheduleQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<string> scheduleDayToPull = new List<string>();
                        while (reader.Read())
                        {
                            if (reader.GetInt32(reader.GetOrdinal("seasonId")) != CurrentSeason) continue;
                            if (reader.GetInt32(reader.GetOrdinal("playoffsGameId")) > gameCounter)
                            {
                                gameCounter = reader.GetInt32(reader.GetOrdinal("playoffsGameId"));
                            }
                            string team1 = reader.GetInt32(reader.GetOrdinal("homeTeamId")).ToString();
                            string team2 = reader.GetInt32(reader.GetOrdinal("awayTeamId")).ToString();
                            string scheduleGameToPull = $"{team1},{team2}";
                            if (reader.GetInt32(reader.GetOrdinal("dayId")) > dayCounter)
                            {
                                if (scheduleDayToPull.Count > 0) scheduleToPull.Add(scheduleDayToPull);
                                scheduleDayToPull = new List<string>();
                                dayCounter = reader.GetInt32(reader.GetOrdinal("dayId"));

                            }
                            scheduleDayToPull.Add(scheduleGameToPull);
                        }
                        if (scheduleDayToPull.Count > 0) scheduleToPull.Add(scheduleDayToPull);
                    }
                }
            }
            return scheduleToPull;
        }

        public bool CheckIfPlayoffs()
        {
            string checkForPlayoffsQuery = $"SELECT COUNT(*) FROM playoffsSchedule WHERE seasonId = {CurrentSeason}";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(checkForPlayoffsQuery, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) return reader.GetInt32(0) > 0;
                }
            }
            return false;
        }

        public string GetPlayoffsRound()
        {
            string getRoundQuery = $@"
                SELECT ps.playoffsRound
                FROM playoffsSchedule ps
                WHERE seasonId = {CurrentSeason}
                ORDER BY playoffsGameId DESC
                LIMIT 1
            ";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getRoundQuery, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) return reader.GetString(0);
                }
            }
            return "";
        }

        // this function generates a schedule if the user creates a league
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public List<List<string>> GenerateSchedule()
        {
            // we get the team file names
            Random random = new Random();
            string fileTeamNamesPath = @$"C:\Users\{CurrentUser}\OneDrive - The Kings School Chester\A-Level\Computer Science\NEA Project\Project Files\LeagueSimulation\Player Data\basketball_team_names_list.txt";
            string[] teamNames = File.ReadAllLines(fileTeamNamesPath);
            int[] gamesPlayed = new int[teamNames.Length];
            List<string> generatedGames = new List<string>();

            bool validSchedule = false;
            List<List<string>> dailySchedule = new List<List<string>>();
            double homeOrAway = random.NextDouble();
            while (!validSchedule)
            {
                dailySchedule = new List<List<string>>();
                // all 1230 games are generated, now we split them into days
                bool validGames = false;
                int numGameTries = 0;
                while (!validGames)
                {
                    numGameTries++;
                    gamesPlayed = new int[teamNames.Length];
                    generatedGames = new List<string>();
                    for (int j = 0; j < teamNames.Length; j++)
                    {
                        // if we've reached the last team and it has played 82 games, we quit
                        if (gamesPlayed[j] >= 82 && j == teamNames.Length - 1) { break; }
                        // if team1 has played 82 games, we just skip that team as their schedule is finished
                        if (gamesPlayed[j] >= 82) continue;
                        if (j == 26) { };

                        string teamName1 = teamNames[j];

                        // in this condition, we make sure every team plays each other team twice (58 games)
                        int teamName2Counter = j + 1;
                        string initialTeamName2 = "";
                        while (teamName2Counter < 59)
                        {
                            if (29 == teamName2Counter) { };
                            if (teamName2Counter > 29)
                            {
                                initialTeamName2 = teamNames[teamName2Counter - 29];
                            }
                            else
                            {
                                initialTeamName2 = teamNames[teamName2Counter];
                            }
                            // now we check if team2 == team1 or they've already played 82 games
                            if (teamName2Counter > 29 && (teamName2Counter == j || gamesPlayed[teamName2Counter - 29] >= 82 || teamName2Counter - 29 == j))
                            {
                                teamName2Counter++;
                                continue;
                            }
                            else if (teamName2Counter <= 29 && (teamName2Counter == j || gamesPlayed[teamName2Counter] >= 82))
                            {
                                teamName2Counter++;
                                continue;
                            }
                            // this occurs if teamName2 is valid for the first 58 games for this team
                            else
                            {
                                if (teamName2Counter > 29) gamesPlayed[teamName2Counter - 29]++;
                                // once the teams have been picked, we increase their games played
                                else gamesPlayed[teamName2Counter]++;
                                gamesPlayed[j]++;

                                // now we add this game to the schedule list
                                homeOrAway = random.NextDouble();
                                if (homeOrAway < 0.5) generatedGames.Add($"{teamName1},{initialTeamName2}");
                                else generatedGames.Add($"{initialTeamName2},{teamName1}");
                                teamName2Counter++;
                            }
                        }

                        // we check if every team after this team has played 82 games
                        // if so, we need to regenerate the schedule as this makes it invalid
                        validGames = true;
                        {
                            for (int k = j + 1; k < teamNames.Length; k++)
                            {
                                int currentTeamGamesPlayed = gamesPlayed[k];
                                if (currentTeamGamesPlayed >= 82) { validGames = false; break; }
                            }
                        }

                        // now we randomise which remaining teams they play (24 games)
                        for (int i = 0; i < 24; i++)
                        {
                            // if team1 has played 82 games, we just skip that team as their season is finished
                            if (gamesPlayed[j] >= 82) break;

                            // we check if every team after this team has played 82 games
                            // if so, we need to regenerate the schedule
                            {
                                List<int> remainingTeamGamesPlayed = new List<int>();
                                for (int k = j + 1; k < teamNames.Length; k++)
                                {
                                    int currentTeamGamesPlayed = gamesPlayed[k];
                                    if (currentTeamGamesPlayed < 82) remainingTeamGamesPlayed.Add(currentTeamGamesPlayed);
                                    if (remainingTeamGamesPlayed.Count > 1) break;
                                }
                                if (remainingTeamGamesPlayed.Count == 0 && gamesPlayed[j] != 82)
                                {
                                    validGames = false;
                                }
                                else if (remainingTeamGamesPlayed.Count == 1 && gamesPlayed[j] != 82 && gamesPlayed[j] != remainingTeamGamesPlayed[0])
                                {
                                    validGames = false;
                                }
                                else validGames = true;
                                if (validGames == false) break;
                            }

                            // if team1 has played 82 games, we break and stop generating teams for them to play
                            if (gamesPlayed[j] >= 82) break;

                            bool validTeamName = false;
                            int randomTeam = random.Next(j + 1, teamNames.Length);
                            if (j == randomTeam) { };
                            int tryCounter = 0;
                            while (!validTeamName)
                            {
                                int currentGameCounter = 0;
                                List<string> gamesWeNeed = generatedGames.Where(x => x.Contains(teamName1) && x.Contains(teamNames[randomTeam])).ToList();
                                // Check how many times the team has already played against the selected team
                                if (gamesWeNeed != null)
                                {
                                    foreach (string game in gamesWeNeed)
                                    {
                                        // Increment counter if this game matches the condition
                                        currentGameCounter++;
                                        if (currentGameCounter > 3) break; // Exit early if the limit is exceeded

                                    }
                                }
                                // Optimize checks with combined conditions
                                if (currentGameCounter > 3 || randomTeam == j || gamesPlayed[randomTeam] >= 82)
                                {
                                    // Pick a new team if current team is invalid
                                    //randomTeam = random.Next(j + 1, teamNames.Length);
                                    randomTeam = Array.IndexOf(gamesPlayed, gamesPlayed.Min());
                                    tryCounter++;
                                    if (tryCounter > 25)
                                    {
                                        validGames = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    // Valid team found
                                    validTeamName = true;
                                }
                            }
                            if (validGames == false) break;
                            string teamName2 = teamNames[randomTeam];
                            // once the teams have been picked, we increase their games played
                            gamesPlayed[j]++; gamesPlayed[randomTeam]++;
                            // now we add this game to the schedule list
                            homeOrAway = random.NextDouble();
                            if (homeOrAway < 0.5) generatedGames.Add($"{teamName1},{teamName2}");
                            else generatedGames.Add($"{teamName2},{teamName1}");

                        }

                        if (validGames == false) { generatedGames = new List<string>(); break; }
                    }
                    validGames = false;
                    if (generatedGames.Count == 1230)
                    {
                        validGames = true;
                        for (int i = 0; i < teamNames.Length; i++)
                        {
                            string currentTeam = teamNames[i];
                            int gamesPlayedCounter = 0;
                            for (int j = 0; j < generatedGames.Count; j++)
                            {
                                string currentGame = generatedGames[j];
                                if (currentGame.Contains(currentTeam)) gamesPlayedCounter++;
                            }
                            if (gamesPlayedCounter != 82) { validGames = false; break; }
                        }
                    }
                }

                // now we set the 1320 games into days
                for (int i = 0; i < 123; i++)
                {
                    List<string> currentDay = new List<string>();
                    if (i == 122) { };
                    // now we select 10 games for this day
                    int gamesInDay = 10;
                    for (int j = 0; j < gamesInDay; j++)
                    {
                        // once we've used all the games, we break and use that schedule
                        if (generatedGames.Count == 0) break;
                        int gameIndex = random.Next(generatedGames.Count);
                        string currentGame = generatedGames[gameIndex];
                        bool validGame = false;
                        int tryGameCounter = 0;
                        while (!validGame)
                        {
                            // any games left over from the previous day, carry over to the next day.
                            if (i != 0)
                            {
                                gamesInDay = 10 + (10 - dailySchedule[i - 1].Count);
                            }

                            if (gamesInDay == 11) { };
                            // we make sure that no team in this game, has already played today.
                            string[] teamsPlaying = currentGame.Split(',');
                            // if there are no games in the day, we add the game immediately
                            if (currentDay.Count == 0) { validGame = true; continue; }
                            foreach (string game in currentDay)
                            {
                                // this returns true if either team has already played today.
                                if (game.Split(",").Contains(teamsPlaying[0]) || game.Split(",").Contains(teamsPlaying[1]))
                                {
                                    tryGameCounter++;
                                    if (tryGameCounter > 65) { validGame = true; break; }
                                    // we set validGame to false, so we go back around and check if this next game is valid
                                    validGame = false;
                                    // we change the game to something different from the list
                                    gameIndex = random.Next(generatedGames.Count);
                                    currentGame = generatedGames[gameIndex]; break;
                                }
                                //
                                else
                                {
                                    validGame = true; continue;
                                }
                            }
                        }
                        if (tryGameCounter <= 65)
                        {
                            currentDay.Add(currentGame);
                            generatedGames.RemoveAt(gameIndex);
                        }
                        if (tryGameCounter > 65) break;
                    }
                    dailySchedule.Add(currentDay);
                }

                // we add any extra remaining days to the schedule
                {
                    List<List<string>> remainingDays = new List<List<string>>();
                    List<string> lastDay = new List<string>();
                    List<string> tempDay = new List<string>();
                    foreach (string currentGame in generatedGames)
                    {
                        lastDay.Add(currentGame);

                    }
                    foreach (string game in lastDay)
                    {
                        generatedGames.Remove(game);
                    }
                    bool daysFilled = false;
                    while (!daysFilled)
                    {
                        // we check if the last day has any duplicates, if so, we move it into the tempDay
                        for (int i = 0; i < lastDay.Count; i++)
                        {
                            string currentDay = lastDay[i];
                            string[] teamsPlaying = currentDay.Split(',');
                            for (int j = 0; j < lastDay.Count; j++)
                            {
                                if (i == j) continue;
                                if (lastDay[j].Contains(teamsPlaying[0]) || lastDay[j].Contains(teamsPlaying[1]))
                                {
                                    daysFilled = false;
                                    tempDay.Add(currentDay);
                                    lastDay.Remove(currentDay);
                                }
                            }
                        }
                        remainingDays.Add(lastDay);
                        bool tempDayDuplicates = false;
                        lastDay = new List<string>();
                        if (tempDay.Count == 0) daysFilled = true;
                        // we check if tempDay has duplicates, lastDay becomes tempDay then we go round again
                        else
                        {
                            for (int i = 0; i < tempDay.Count; i++)
                            {
                                string currentDay = tempDay[i];
                                string[] teamsPlaying = currentDay.Split(',');
                                for (int j = 0; j < tempDay.Count; j++)
                                {
                                    if (i == j) { continue; }
                                    if (tempDay[j].Split(",").Contains(teamsPlaying[0]) || tempDay[j].Split(",").Contains(teamsPlaying[1]))
                                    {
                                        tempDayDuplicates = true;
                                        break;
                                    }
                                }
                            }
                            if (tempDayDuplicates)
                            {
                                lastDay = tempDay;
                                tempDay = new List<string>();
                            }
                            else
                            {
                                daysFilled = true;
                                remainingDays.Add(tempDay);
                                break;
                            }
                        }
                    }

                    // now we check for any additional games in the schedule; if there are any games, we need to try and remove them
                    foreach (List<string> remainingGameDay in remainingDays) dailySchedule.Add(remainingGameDay);
                    int numGames = 0;
                    for (int i = 0; i < dailySchedule.Count; i++) numGames += dailySchedule[i].Count;
                    // we set this to true because the schedule is going to be set after the 'daysFilled' while loop
                    if (numGames == 1230) validSchedule = true;
                }

                // this line converts the team's names into their corresponding teamIds
                if (validSchedule) return ConvertScheduleToTeamIds(dailySchedule, teamNames);
            }
            return new List<List<string>>();
        }

        public List<List<string>> ConvertScheduleToTeamIds(List<List<string>> teamNameSchedule, string[] teamNamesFromFile)
        {
            Dictionary<string, int> idForTeamName = new Dictionary<string, int>();
            for (int i = 0; i < teamNamesFromFile.Length; i++)
            {
                string teamName = teamNamesFromFile[i];
                idForTeamName.Add(teamName, i + 1);
            }
            for (int i = 0; i < teamNameSchedule.Count; i++)
            {
                List<string> gamesInDay = teamNameSchedule[i];
                for (int j = 0; j < gamesInDay.Count; j++)
                {
                    string currentGame = gamesInDay[j];
                    string[] teamsPlaying = currentGame.Split(',');
                    teamsPlaying[0] = idForTeamName[teamsPlaying[0]].ToString();
                    teamsPlaying[1] = idForTeamName[teamsPlaying[1]].ToString();
                    teamNameSchedule[i][j] = $"{teamsPlaying[0]},{teamsPlaying[1]}";
                }
            }
            return teamNameSchedule;
        }


        public void GeneratePlayoffsFirstRound()
        {
            // we get the top 8 seeds from both conferences, to generate the schedule
            List<string> eastPlayoffTeams = GetConferenceTeamsByWinPct(1);
            List<string> westPlayoffTeams = GetConferenceTeamsByWinPct(2);
            List<List<string>> eastPlayoffGames = new List<List<string>>();
            List<List<string>> westPlayoffGames = new List<List<string>>();
            // here, we get rid of all the teams which aren't top 8
            for (int i = 14; i > 7; i--)
            {
                eastPlayoffTeams.RemoveAt(i);
                westPlayoffTeams.RemoveAt(i);
            }

            // here we generate the first 4 games for the east and west coast for the 1st round of the playoffs
            for (int i = 0; i < 8; i++)
            {
                // odd days (day 1, day 3, ...) are eastConference games
                // 4 games are generated each, per series
                if (i % 2 == 0)
                {
                    List<string> currentDay = new List<string>();
                    // we generate the games for the east coast
                    for (int j = 0; j < 4; j++)
                    {
                        string initialHomeTeam = eastPlayoffTeams[j];
                        string initialAwayTeam = eastPlayoffTeams[7 - j];
                        currentDay.Add($"{initialHomeTeam},{initialAwayTeam}");
                    }
                    eastPlayoffGames.Add(currentDay);
                }
                // even days (day 2, day 4, ...) are westConference games
                // 4 games are generated each, per series
                else
                {
                    List<string> currentDay = new List<string>();
                    // we generate the games for the west coast
                    for (int j = 0; j < 4; j++)
                    {
                        string initialHomeTeam = westPlayoffTeams[j];
                        string initialAwayTeam = westPlayoffTeams[7 - j];
                        currentDay.Add($"{initialHomeTeam},{initialAwayTeam}");
                    }
                    westPlayoffGames.Add(currentDay);
                }
            }

            for (int i = 0; i < eastPlayoffGames.Count; i++)
            {
                List<string> currentDay = eastPlayoffGames[i];
                for (int j = 0; j < currentDay.Count; j++)
                {
                    string currentGame = currentDay[j];
                    string[] teamsPlaying = currentGame.Split(',');
                    string insertGameQuery = $@"
                    INSERT INTO playoffsSchedule(seasonId,dayId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,gameCompleted)
                    VALUES(
                    {CurrentSeason},
                    {CurrentDay + 1 + i},
                    {1},
                    '{PlayoffsRound}',
                    {j + 1},
                    {GetIdFromTeamName(teamsPlaying[0])},
                    {GetIdFromTeamName(teamsPlaying[1])},
                    FALSE
                    );";

                    string insertPlayoffResultQuery = $@"
                    INSERT INTO playoffResults(seasonId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,wins,losses)
                    VALUES(
                    {CurrentSeason},
                    {1},
                    '{PlayoffsRound}',
                    {j + 1},
                    {GetIdFromTeamName(teamsPlaying[0])},
                    {GetIdFromTeamName(teamsPlaying[1])},
                    0,
                    0
                    );";

                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = insertGameQuery;
                            command.ExecuteNonQuery();

                            command.CommandText = insertPlayoffResultQuery;
                            if (i == 0) command.ExecuteNonQuery();
                        }
                    }
                }

            }

            for (int i = 0; i < westPlayoffGames.Count; i++)
            {
                List<string> currentDay = westPlayoffGames[i];
                for (int j = 0; j < currentDay.Count; j++)
                {
                    string currentGame = currentDay[j];
                    string[] teamsPlaying = currentGame.Split(',');
                    string insertGameQuery = $@"
                    INSERT INTO playoffsSchedule(seasonId,dayId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,gameCompleted)
                    VALUES(
                    {CurrentSeason},
                    {CurrentDay + 1 + i},
                    {2},
                    '{PlayoffsRound}',
                    {j + 1},
                    {GetIdFromTeamName(teamsPlaying[0])},
                    {GetIdFromTeamName(teamsPlaying[1])},
                    FALSE
                    );";

                    string insertPlayoffResultQuery = $@"
                    INSERT INTO playoffResults(seasonId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,wins,losses)
                    VALUES(
                    {CurrentSeason},
                    {2},
                    '{PlayoffsRound}',
                    {j + 1},
                    {GetIdFromTeamName(teamsPlaying[0])},
                    {GetIdFromTeamName(teamsPlaying[1])},
                    0,
                    0
                    );";

                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = insertGameQuery;
                            command.ExecuteNonQuery();

                            command.CommandText = insertPlayoffResultQuery;
                            if (i == 0) command.ExecuteNonQuery();
                        }
                    }
                }

            }
        }

        public void GeneratePlayoffsSecondRound()
        {
            // we need to get the winners of all the eastern conference series in the 1st round
            string getWinnersNewQuery = $@"
                SELECT 
                CASE
                   WHEN losses = 4 THEN awayTeamId
                   WHEN wins = 4 THEN homeTeamId
                ELSE NULL
                END AS winner,
                conferenceId,
                seed
                FROM playoffResults
                WHERE seasonId = {CurrentSeason}
                AND playoffsRound = 'First Round'
                ";
            List<int> eastTeamIds = new List<int>();
            List<int> eastSeeds = new List<int>();
            List<int> westTeamIds = new List<int>();
            List<int> westSeeds = new List<int>();
            List<List<string>> eastPlayoffGames = new List<List<string>>();
            List<List<string>> westPlayoffGames = new List<List<string>>();
            // this connection gets all the winners of all the series in the 1st round
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getWinnersNewQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int conferenceId = reader.GetInt32(reader.GetOrdinal("conferenceId"));
                            // handle east teams
                            if (conferenceId == 1)
                            {
                                eastTeamIds.Add(reader.GetInt32(reader.GetOrdinal("winner")));
                                eastSeeds.Add(reader.GetInt32(reader.GetOrdinal("seed")));
                            }
                            else if (conferenceId == 2)
                            {
                                westTeamIds.Add(reader.GetInt32(reader.GetOrdinal("winner")));
                                westSeeds.Add(reader.GetInt32(reader.GetOrdinal("seed")));
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    List<string> currentDay = new List<string>()
                    {
                        // we add the seed 1-4 game, then the 2-3 game
                        $"{eastTeamIds[0]},{eastTeamIds[3]}",
                        $"{eastTeamIds[1]},{eastTeamIds[2]}"
                    };
                    eastPlayoffGames.Add(currentDay);
                }
                else
                {
                    List<string> currentDay = new List<string>()
                    {
                        $"{westTeamIds[0]},{westTeamIds[3]}",
                        $"{westTeamIds[1]},{westTeamIds[2]}"
                    };

                    westPlayoffGames.Add(currentDay);
                }
            }

            for (int i = 0; i < eastPlayoffGames.Count; i++)
            {
                List<string> currentDay = eastPlayoffGames[i];
                for (int j = 0; j < currentDay.Count; j++)
                {
                    string currentGame = currentDay[j];
                    string[] teamsPlaying = currentGame.Split(',');
                    string insertGameQuery = $@"
                    INSERT INTO playoffsSchedule(seasonId,dayId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,gameCompleted)
                    VALUES(
                    {CurrentSeason},
                    {CurrentDay + 1 + i},
                    {1},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    FALSE
                    );";

                    string insertPlayoffResultQuery = $@"
                    INSERT INTO playoffResults(seasonId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,wins,losses)
                    VALUES(
                    {CurrentSeason},
                    {1},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    0,
                    0
                    );";

                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = insertGameQuery;
                            command.ExecuteNonQuery();

                            command.CommandText = insertPlayoffResultQuery;
                            if (i == 0) command.ExecuteNonQuery();
                        }
                    }
                }

            }

            for (int i = 0; i < westPlayoffGames.Count; i++)
            {
                List<string> currentDay = westPlayoffGames[i];
                for (int j = 0; j < currentDay.Count; j++)
                {
                    string currentGame = currentDay[j];
                    string[] teamsPlaying = currentGame.Split(',');
                    string insertGameQuery = $@"
                    INSERT INTO playoffsSchedule(seasonId,dayId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,gameCompleted)
                    VALUES(
                    {CurrentSeason},
                    {CurrentDay + 1 + i},
                    {2},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    FALSE
                    );";

                    string insertPlayoffResultQuery = $@"
                    INSERT INTO playoffResults(seasonId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,wins,losses)
                    VALUES(
                    {CurrentSeason},
                    {2},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    0,
                    0
                    );";

                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = insertGameQuery;
                            command.ExecuteNonQuery();

                            command.CommandText = insertPlayoffResultQuery;
                            if (i == 0) command.ExecuteNonQuery();
                        }
                    }
                }

            }

        }

        public void GeneratePlayoffsConferenceFinals()
        {
            // we need to get the winners of all the eastern conference series in the 2nd round
            // we make the winners play the ECF series; then same for the western conference
            string getWinnersNewQuery = $@"
                SELECT 
                CASE
                   WHEN losses = 4 THEN awayTeamId
                   WHEN wins = 4 THEN homeTeamId
                ELSE NULL
                END AS winner,
                conferenceId,
                seed
                FROM playoffResults
                WHERE seasonId = {CurrentSeason}
                AND playoffsRound = 'Second Round'
                ";
            List<int> eastTeamIds = new List<int>();
            List<int> eastSeeds = new List<int>();
            List<int> westTeamIds = new List<int>();
            List<int> westSeeds = new List<int>();
            List<List<string>> eastPlayoffGames = new List<List<string>>();
            List<List<string>> westPlayoffGames = new List<List<string>>();
            // this connection gets all the winners of all the series in the 1st round
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getWinnersNewQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int conferenceId = reader.GetInt32(reader.GetOrdinal("conferenceId"));
                            // handle east teams
                            if (conferenceId == 1)
                            {
                                eastTeamIds.Add(reader.GetInt32(reader.GetOrdinal("winner")));
                                eastSeeds.Add(reader.GetInt32(reader.GetOrdinal("seed")));
                            }
                            else if (conferenceId == 2)
                            {
                                westTeamIds.Add(reader.GetInt32(reader.GetOrdinal("winner")));
                                westSeeds.Add(reader.GetInt32(reader.GetOrdinal("seed")));
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    List<string> currentDay = new List<string>()
                    {
                        // we add the ECF game
                        $"{eastTeamIds[0]},{eastTeamIds[1]}"
                    };
                    eastPlayoffGames.Add(currentDay);
                }
                else
                {
                    List<string> currentDay = new List<string>()
                    {
                        // we add the WCF game
                        $"{westTeamIds[0]},{westTeamIds[1]}"
                    };

                    westPlayoffGames.Add(currentDay);
                }
            }

            for (int i = 0; i < eastPlayoffGames.Count; i++)
            {
                List<string> currentDay = eastPlayoffGames[i];
                for (int j = 0; j < currentDay.Count; j++)
                {
                    string currentGame = currentDay[j];
                    string[] teamsPlaying = currentGame.Split(',');
                    string insertGameQuery = $@"
                    INSERT INTO playoffsSchedule(seasonId,dayId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,gameCompleted)
                    VALUES(
                    {CurrentSeason},
                    {CurrentDay + 1 + i},
                    {1},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    FALSE
                    );";

                    string insertPlayoffResultQuery = $@"
                    INSERT INTO playoffResults(seasonId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,wins,losses)
                    VALUES(
                    {CurrentSeason},
                    {1},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    0,
                    0
                    );";

                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = insertGameQuery;
                            command.ExecuteNonQuery();

                            command.CommandText = insertPlayoffResultQuery;
                            if (i == 0) command.ExecuteNonQuery();
                        }
                    }
                }

            }

            for (int i = 0; i < westPlayoffGames.Count; i++)
            {
                List<string> currentDay = westPlayoffGames[i];
                for (int j = 0; j < currentDay.Count; j++)
                {
                    string currentGame = currentDay[j];
                    string[] teamsPlaying = currentGame.Split(',');
                    string insertGameQuery = $@"
                    INSERT INTO playoffsSchedule(seasonId,dayId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,gameCompleted)
                    VALUES(
                    {CurrentSeason},
                    {CurrentDay + 1 + i},
                    {2},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    FALSE
                    );";

                    string insertPlayoffResultQuery = $@"
                    INSERT INTO playoffResults(seasonId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,wins,losses)
                    VALUES(
                    {CurrentSeason},
                    {2},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    0,
                    0
                    );";

                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = insertGameQuery;
                            command.ExecuteNonQuery();

                            command.CommandText = insertPlayoffResultQuery;
                            if (i == 0) command.ExecuteNonQuery();
                        }
                    }
                }

            }

        }

        public void GeneratePlayoffsFinals()
        {
            // we need to get the winners of the ECF and WCF, then put them into a head-to-head Finals!
            string getWinnersNewQuery = $@"
                SELECT 
                CASE
                   WHEN losses = 4 THEN awayTeamId
                   WHEN wins = 4 THEN homeTeamId
                ELSE NULL
                END AS winner,
                conferenceId
                FROM playoffResults
                WHERE seasonId = {CurrentSeason}
                AND playoffsRound = 'Conference Finals'
                ";
            int eastTeamId = 0;
            int westTeamId = 0;
            List<List<string>> finalsPlayoffGames = new List<List<string>>();
            // this connection gets all the winners of all the series in the 1st round
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getWinnersNewQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int conferenceId = reader.GetInt32(reader.GetOrdinal("conferenceId"));
                            // handle east teams
                            if (conferenceId == 1)
                            {
                                eastTeamId = reader.GetInt32(reader.GetOrdinal("winner"));
                            }
                            else if (conferenceId == 2)
                            {
                                westTeamId = reader.GetInt32(reader.GetOrdinal("winner"));
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                List<string> currentDay = new List<string>()
                    {
                        // we add the finals game
                        $"{eastTeamId},{westTeamId}"
                    };
                finalsPlayoffGames.Add(currentDay);
            }

            for (int i = 0; i < finalsPlayoffGames.Count; i++)
            {
                List<string> currentDay = finalsPlayoffGames[i];
                for (int j = 0; j < currentDay.Count; j++)
                {
                    string currentGame = currentDay[j];
                    string[] teamsPlaying = currentGame.Split(',');
                    string insertGameQuery = $@"
                    INSERT INTO playoffsSchedule(seasonId,dayId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,gameCompleted)
                    VALUES(
                    {CurrentSeason},
                    {CurrentDay + 1 + i},
                    {0},
                    '{PlayoffsRound}',
                    {1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    FALSE
                    );";

                    string insertPlayoffResultQuery = $@"
                    INSERT INTO playoffResults(seasonId,conferenceId,playoffsRound,seed,homeTeamId,awayTeamId,wins,losses)
                    VALUES(
                    {CurrentSeason},
                    {0},
                    '{PlayoffsRound}',
                    {j + 1},
                    {teamsPlaying[0]},
                    {teamsPlaying[1]},
                    0,
                    0
                    );";

                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = insertGameQuery;
                            command.ExecuteNonQuery();

                            command.CommandText = insertPlayoffResultQuery;
                            if (i == 0) command.ExecuteNonQuery();

                        }
                    }
                }

            }

        }
        // start from here
        public string GetGameScore(int gameId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string tablePicker = "season";
                string gamePicker = "g";
                if (Playoffs) { tablePicker = "playoffs"; gamePicker = "playoffsG"; }
                string gameScoreQuery = $@"
                    WITH homeTeamPlayers AS (
                    SELECT playerId, teamId
                    FROM playerOnTeam
                    WHERE ((dayJoined <= {CurrentDay} AND yearJoined = {CurrentSeason + 2023}) OR (yearJoined < {CurrentSeason + 2023}))
                    AND dayLeft >= {CurrentDay} AND yearLeft >= {CurrentSeason + 2023}
	                AND teamId = (SELECT homeTeamId FROM {tablePicker}Schedule ss WHERE ss.{gamePicker}ameId = {gameId} AND ss.seasonId = {CurrentSeason})
                ),
                awayTeamPlayers AS (
                    SELECT playerId, teamId
                    FROM playerOnTeam
                    WHERE ((dayJoined <= {CurrentDay} AND yearJoined = {CurrentSeason + 2023}) OR (yearJoined < {CurrentSeason + 2023}))
                    AND dayLeft >= {CurrentDay} AND yearLeft >= {CurrentSeason + 2023}
	                AND teamId = (SELECT awayTeamId FROM {tablePicker}Schedule ss WHERE ss.{gamePicker}ameId = {gameId} AND ss.seasonId = {CurrentSeason})
                ),
                    homeTeamPoints AS (
                    SELECT SUM(pgs.PTS) as teamPoints
                    FROM playerGameStats pgs
                    JOIN homeTeamPlayers htp ON htp.playerId = pgs.playerId
                    WHERE pgs.gameId = {gameId}
                    AND pgs.seasonId = {CurrentSeason}
                    AND pgs.isPlayoffs = {Playoffs}
                ),
                    awayTeamPoints AS (
                    SELECT SUM(pgs.PTS) as teamPoints
                    FROM playerGameStats pgs
                    JOIN awayTeamPlayers atp ON atp.playerId = pgs.playerId
                    WHERE pgs.gameId = {gameId}
                    AND pgs.seasonId = {CurrentSeason}
                    AND pgs.isPlayoffs = {Playoffs}
                )
                    SELECT *
                    FROM homeTeamPoints, awayTeamPoints
                ;";
                if (Playoffs) gameScoreQuery = $@"
                    WITH homeTeamPlayers AS (
                    SELECT playerId, teamId
                    FROM playerOnTeam
                    WHERE ((dayJoined <= 150 AND yearJoined = {CurrentSeason + 2023}) OR (yearJoined < {CurrentSeason + 2023}))
                    AND dayLeft >= 150 AND yearLeft >= {CurrentSeason + 2023}
	                AND teamId = (SELECT homeTeamId FROM {tablePicker}Schedule ss WHERE ss.{gamePicker}ameId = {gameId} AND ss.seasonId = {CurrentSeason})
                ),
                awayTeamPlayers AS (
                    SELECT playerId, teamId
                    FROM playerOnTeam
                    WHERE ((dayJoined <= 150 AND yearJoined = {CurrentSeason + 2023}) OR (yearJoined < {CurrentSeason + 2023}))
                    AND dayLeft >= 150 AND yearLeft >= {CurrentSeason + 2023}
	                AND teamId = (SELECT awayTeamId FROM {tablePicker}Schedule ss WHERE ss.{gamePicker}ameId = {gameId} AND ss.seasonId = {CurrentSeason})
                ),
                    homeTeamPoints AS (
                    SELECT SUM(pgs.PTS) as teamPoints
                    FROM playerGameStats pgs
                    JOIN homeTeamPlayers htp ON htp.playerId = pgs.playerId
                    WHERE pgs.gameId = {gameId}
                    AND pgs.seasonId = {CurrentSeason}
                    AND pgs.isPlayoffs = {Playoffs}
                ),
                    awayTeamPoints AS (
                    SELECT SUM(pgs.PTS) as teamPoints
                    FROM playerGameStats pgs
                    JOIN awayTeamPlayers atp ON atp.playerId = pgs.playerId
                    WHERE pgs.gameId = {gameId}
                    AND pgs.seasonId = {CurrentSeason}
                    AND pgs.isPlayoffs = {Playoffs}
                )
                    SELECT *
                    FROM homeTeamPoints, awayTeamPoints
                ;";

                using (var command = new SQLiteCommand(gameScoreQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return $"{reader.GetInt32(0)}-{reader.GetInt32(1)}";
                        }
                    }
                }
            }
            return "";
        }

        public int GetIdFromTeamName(string teamName)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getTeamIdQuery = $@"SELECT teamId FROM teams WHERE teamName = '{teamName}'";
                using (var command = new SQLiteCommand(getTeamIdQuery, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) return reader.GetInt32(0);
                }
            }
            return 0;
        }

        public bool CheckIfGameCompleted(int currentDay, string homeTeamId, string awayTeamId)
        {
            ConnectionString = $"Data Source={LeagueFileName};Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string checkGameCompletedQuery = $@"
                    SELECT gameCompleted
                    FROM seasonSchedule
                    WHERE homeTeamId = {homeTeamId} AND awayTeamId = {awayTeamId} AND dayId = {currentDay} AND seasonId = {CurrentSeason}
                ;";

                using (SQLiteCommand command = new SQLiteCommand(checkGameCompletedQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetBoolean(0);
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckIfPlayoffGameCompleted(int currentDay, string homeTeamId, string awayTeamId)
        {
            ConnectionString = $"Data Source={LeagueFileName};Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string checkGameCompletedQuery = $@"
                    SELECT gameCompleted
                    FROM playoffsSchedule
                    WHERE homeTeamId = {homeTeamId} AND awayTeamId = {awayTeamId} AND dayId = {currentDay} AND seasonId = {CurrentSeason}
                ;";

                using (SQLiteCommand command = new SQLiteCommand(checkGameCompletedQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetBoolean(0);
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckIfDayOfGameCompleted(int currentDay)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string checkGameCompletedQuery = $@"
                    SELECT COUNT(*) = 0 as gamesLeft
                    FROM seasonSchedule
                    WHERE dayId = {currentDay} AND seasonId = {CurrentSeason}
                    AND gameCompleted = FALSE
                ;";
                if (Playoffs) checkGameCompletedQuery = checkGameCompletedQuery.Replace("seasonS", "playoffsS");
                using (SQLiteCommand command = new SQLiteCommand(checkGameCompletedQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetBoolean(0);
                        }
                    }
                }
            }
            return false;
        }
        public void SetGameToComplete(int currentDay, string homeTeam, string awayTeam)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string updateGameComplete = $"UPDATE seasonSchedule SET gameCompleted = {true} WHERE dayId = {currentDay} AND homeTeamId = {homeTeam} AND awayTeamId = {awayTeam} AND seasonId = {CurrentSeason};";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = updateGameComplete;
                    command.ExecuteNonQuery();
                }
            }

        }

        public void SetPlayoffGameToComplete(int currentDay, string homeTeam, string awayTeam)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string updateGameComplete = $"UPDATE playoffsSchedule SET gameCompleted = {true} WHERE dayId = {currentDay} AND homeTeamId = {homeTeam} AND awayTeamId = {awayTeam} AND seasonId = {CurrentSeason};";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = updateGameComplete;
                    command.ExecuteNonQuery();
                }
            }

        }
        public string GetLeagueFileName(int saveState)
        {
            // this is the string of the file name
            return $"C:\\Users\\{CurrentUser}\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Databases\\League{saveState}.db";
        }

        public void SimulateDay(List<string> games, int currentDaySimulated, bool playoffs)
        {
            // simulate games within the day
            foreach (string game in games) SimulateGame(game, currentDaySimulated, playoffs);
        }

        public void SimulateGame(string game, int currentDayOfGame, bool playoffs)
        {
            // simulate game specified by user
            string[] teamsPlaying = game.Split(",");
            bool gameComplete = false;
            if (Playoffs)
            {
                gameComplete = CheckIfPlayoffGameCompleted(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
            }
            else
            {
                GamesPlayed++;
                gameComplete = CheckIfGameCompleted(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
            }
            if (!gameComplete)
            {
                Team team1 = new Team(GetTeamNameFromId(teamsPlaying[0]), Convert.ToInt32(teamsPlaying[0]), CurrentUser);
                Team team2 = new Team(GetTeamNameFromId(teamsPlaying[1]), Convert.ToInt32(teamsPlaying[1]), CurrentUser);

                int gameId = 0;
                if (Playoffs) gameId = GetPlayoffGameId(game, currentDayOfGame);
                else gameId = GetGameId(game, currentDayOfGame);
                GameGenerator simulatedGame = new GameGenerator(team1, team2, ConnectionString, CurrentUser, CurrentSaveState, gameId, Playoffs, this);
                if (Playoffs) SetPlayoffGameToComplete(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
                else SetGameToComplete(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
            }

            // we check if all games are complete in the day, then increment currentDay if so
            bool allGamesComplete = CheckIfDayOfGameCompleted(currentDayOfGame);

            // we check everyday up to the current game being simulated, where i is the current day
            // if all games are complete, then we increase the season currentDay

            // here we check if we need to add any additional games
            bool seasonComplete = false;
            if (Playoffs)
            {
                // if all series are complete; we advance to the next round

                if (CheckIfFullPlayoffRoundGamesComplete())
                {
                    MoveToNextPlayoffRound();
                    if (PlayoffsRound == "Second Round") GeneratePlayoffsSecondRound();
                    else if (PlayoffsRound == "Conference Finals") GeneratePlayoffsConferenceFinals();
                    else if (PlayoffsRound == "Finals") GeneratePlayoffsFinals();
                    else if (PlayoffsRound == "") seasonComplete = true;
                    if (Playoffs && !seasonComplete) CurrentPlayoffsSchedule = LoadPlayoffsSchedule();
                }
                // if all game 4s are complete, game 5s are complete etc. we add any initial games
                // for any uncomplete series
                if (CheckIfInitialPlayoffRoundGamesComplete() && !seasonComplete)
                {
                    AddAnyNeededPlayoffGames();
                    CurrentPlayoffsSchedule = LoadPlayoffsSchedule();
                }
            }
            if (GamesPlayed % 1230 == 0 && !seasonComplete && !Playoffs)
            {
                // here, we move the league into playoffs mode
                Playoffs = true;
                PlayoffsRound = "First Round";
                CurrentDay = 0;
                SetCurrentDayForPlayoffs();
                GeneratePlayoffsFirstRound();
                CurrentPlayoffsSchedule = new List<List<string>>();
                CurrentPlayoffsSchedule = LoadPlayoffsSchedule();
            }
            if (allGamesComplete && !seasonComplete)
            {
                CurrentDay++;
                UpdateLeagueDataIfNeeded(CurrentDay - 1);
            }
            if (seasonComplete)
            {
                AdvanceToNextSeason();
            }
        }

        public Player ExtractCurrentPlayerFromPlayerId(int playerId)
        {
            Player player = new Player();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string teamPlayersQuery = $@"
                    WITH currentPlayers AS (
	                    SELECT playerId, teamId
	                    FROM playerOnTeam
	                    WHERE ((dayJoined <= {CurrentDay} AND yearJoined = {CurrentSeason + 2023}) OR (yearJoined < {CurrentSeason + 2023}))
	                    AND dayLeft >= {CurrentDay} AND yearLeft >= {CurrentSeason + 2023}
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
                    p.potential,
                    cp.teamId,
                    league.currentSeason + 2023 - p.dateOfBirth as age,
                    pos.positionShort AS positionShort,
                    sp.playstyle AS secondaryPlaystyle,
                    pp.playstyle AS primaryPlaystyle
                    FROM
                        currentPlayers cp, league
                    JOIN
                        players p ON p.playerId = cp.playerId
                    LEFT JOIN
                        position pos ON p.positionId = pos.positionId
                    LEFT JOIN
                        secondaryPlaystyle sp ON p.secondaryPlaystyleId = sp.secondaryPlaystyleId
                    LEFT JOIN
                        primaryPlaystyle pp ON sp.primaryPlaystyleId = pp.primaryPlaystyleId
                    WHERE cp.playerId = {playerId};
                ";
                if (Playoffs) teamPlayersQuery = $@"
                    WITH currentPlayers AS (
	                    SELECT playerId, teamId
	                    FROM playerOnTeam
	                    WHERE ((dayJoined <= 150 AND yearJoined = {CurrentSeason + 2023}) OR (yearJoined < {CurrentSeason + 2023}))
	                    AND (dayLeft >= 150 AND yearLeft >= {CurrentSeason + 2023})
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
                    p.potential,
                    cp.teamId,
                    league.currentSeason + 2023 - p.dateOfBirth as age,
                    pos.positionShort AS positionShort,
                    sp.playstyle AS secondaryPlaystyle,
                    pp.playstyle AS primaryPlaystyle
                    FROM
                        currentPlayers cp, league
                    JOIN
                        players p ON p.playerId = cp.playerId
                    LEFT JOIN
                        position pos ON p.positionId = pos.positionId
                    LEFT JOIN
                        secondaryPlaystyle sp ON p.secondaryPlaystyleId = sp.secondaryPlaystyleId
                    LEFT JOIN
                        primaryPlaystyle pp ON sp.primaryPlaystyleId = pp.primaryPlaystyleId
                    WHERE cp.playerId = {playerId};
                ";
                using (var command = new SQLiteCommand(teamPlayersQuery, connection))
                {
                    // this reader, will extract all the players from team1, and put them into the list
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // extract data from a player in database, and put into a player class
                            
                            player.PlayerId = reader.GetInt32(reader.GetOrdinal("playerId"));
                            player.position = reader.GetString(reader.GetOrdinal("positionShort"));
                            player.PrimaryPlaystyle = reader.GetString(reader.GetOrdinal("primaryPlaystyle"));
                            player.SecondaryPlaystyle = reader.GetString(reader.GetOrdinal("secondaryPlaystyle"));
                            player.Height = reader.GetInt32(reader.GetOrdinal("height")); // height in inches
                            player.Weight = reader.GetInt32(reader.GetOrdinal("weight")); // weight in lbs
                            player.playerForename = reader.GetString(reader.GetOrdinal("playerForename"));
                            player.playerSurname = reader.GetString(reader.GetOrdinal("playerSurname"));
                            player.TeamId = reader.GetInt32(reader.GetOrdinal("teamId"));
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
                            player.Potential = reader.GetInt32(reader.GetOrdinal("potential"));
                            player.Age = reader.GetInt32(reader.GetOrdinal("age"));
                        }
                    }
                }

                return player;
            }
        }

        public double GetPlayerCurrentGameValue(int playerId)
        {
            string getPlayerGameValueQuery = $"SELECT ROUND(AVG(pgs.gameValue), 1) as gameValue\r\nFROM players p, league\r\nJOIN playerGameStats pgs ON pgs.seasonId = currentSeason\r\nAND pgs.playerId = p.playerId\r\nWHERE p.playerId = {playerId};";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getPlayerGameValueQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDouble(0);
                        }
                    }
                }
            }
            return 0.0;
        }

        public void MoveToNextPlayoffRound()
        {
            if (PlayoffsRound == "First Round") PlayoffsRound = "Second Round";
            else if (PlayoffsRound == "Second Round") PlayoffsRound = "Conference Finals";
            else if (PlayoffsRound == "Conference Finals") PlayoffsRound = "Finals";
            else if (PlayoffsRound == "Finals") PlayoffsRound = "";
        }

        public void AdvanceToNextSeason()
        {
            // hand out awards
            {
                // start with getting the non-team award winners
                int MVP = 0;
                int DPOY = 0;
                int FMVP = 0;
                int EFMVP = 0;
                int WFMVP = 0;
                int ROY = 0;
                int Champion = 0;
                string setMVPQuery = $@"
                SELECT 
                p.playerId,
                AVG(pgs.gameValue) as gameValue


                FROM players p, league l
                JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                AND pgs.isPlayoffs = 0
                AND pgs.seasonId = {CurrentSeason}
                GROUP BY p.playerId
                ORDER BY gameValue DESC
                LIMIT 1
                ";
                string setDPOYQuery = $@"WITH playerData AS (SELECT
                p.playerId,
                ROUND(AVG(pgs.gameValue), 1) as gameValue,
                ROUND(AVG(pgs.REB), 1) as REB,
                ROUND(AVG(pgs.STL), 1) as STL,
                ROUND(AVG(pgs.BLK), 1) as BLK

                FROM players p, league l
                JOIN playerGameStats pgs ON pgs.playerId = p.playerId AND pgs.isPlayoffs = 0
                AND pgs.seasonId = {CurrentSeason}
                GROUP BY p.playerId
                )
				SELECT pd.*, 1.3 * BLK + STL + 0.04 * gameValue + 0.3 * REB AS defenseValue
				FROM playerData pd
				ORDER BY defenseValue DESC
                LIMIT 1
                ";
                string setFMVPQuery = $@"
                SELECT 
                p.playerId,
                ROUND(AVG(pgs.gameValue), 1) as gameValue

                FROM players p, league l
                JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                AND pgs.isPlayoffs = 1
                JOIN playoffsSchedule ps ON ps.playoffsGameId = pgs.gameId
                AND ps.playoffsRound = 'Finals'
                AND ps.seasonId = {CurrentSeason}
                GROUP BY p.playerId
                ORDER BY gameValue DESC
                LIMIT 1	
                ";
                string setEFMVPQuery = $@"
                SELECT 
                p.playerId,
                ROUND(AVG(pgs.gameValue), 1) as gameValue


                FROM players p, league l
                JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                AND pgs.isPlayoffs = 1
                JOIN playoffsSchedule ps ON ps.playoffsGameId = pgs.gameId
                AND ps.playoffsRound = 'Conference Finals'
                AND ps.conferenceId = 1
                AND ps.seasonId = {CurrentSeason}
                GROUP BY p.playerId
                ORDER BY gameValue DESC
                LIMIT 1		
                ";
                string setWFMVPQuery = $@"
                SELECT 
                p.playerId,
                ROUND(AVG(pgs.gameValue), 1) as gameValue


                FROM players p, league l
                JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                AND pgs.isPlayoffs = 1
                JOIN playoffsSchedule ps ON ps.playoffsGameId = pgs.gameId
                AND ps.playoffsRound = 'Conference Finals'
                AND ps.conferenceId = 2
                AND ps.seasonId = {CurrentSeason}
                GROUP BY p.playerId
                ORDER BY gameValue DESC
                LIMIT 1		
                ";
                string setROYQuery = $@"
                WITH playerData AS (SELECT
                p.playerId,
                ROUND(AVG(pgs.gameValue), 1) as gameValue


                FROM players p, league l
                JOIN playerGameStats pgs ON pgs.playerId = p.playerId AND pgs.isPlayoffs = 0
                JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                JOIN position pos ON pos.positionId = p.positionId
                WHERE p.teamId != 0
                GROUP BY p.playerId, pgs.seasonId
                )
                SELECT pd.*, 
                COUNT(*) as seasonsPlayed
                FROM playerData pd
                GROUP BY playerId
                HAVING seasonsPlayed = 1
                ORDER BY gameValue DESC
                LIMIT 1
                
                ";
                string setChampionNewQuery = $@"
                    SELECT pr.homeTeamId
                    FROM playoffResults pr, league
                    WHERE pr.seasonId = league.CurrentSeason
                    AND pr.playoffsRound = 'Finals'
                    AND (pr.wins = 4)
                    UNION
                    SELECT pr.awayTeamId
                    FROM playoffResults pr, league
                    WHERE pr.seasonId = league.CurrentSeason
                    AND pr.playoffsRound = 'Finals'
                    AND (pr.losses = 4)
                ";
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(setMVPQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MVP = reader.GetInt32(reader.GetOrdinal("playerId"));
                            }
                        }
                    }

                    using (var command = new SQLiteCommand(setDPOYQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DPOY = reader.GetInt32(reader.GetOrdinal("playerId"));
                            }
                        }
                    }

                    using (var command = new SQLiteCommand(setFMVPQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FMVP = reader.GetInt32(reader.GetOrdinal("playerId"));
                            }
                        }
                    }

                    using (var command = new SQLiteCommand(setEFMVPQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EFMVP = reader.GetInt32(reader.GetOrdinal("playerId"));
                            }
                        }
                    }

                    using (var command = new SQLiteCommand(setWFMVPQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WFMVP = reader.GetInt32(reader.GetOrdinal("playerId"));
                            }
                        }
                    }

                    using (var command = new SQLiteCommand(setROYQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ROY = reader.GetInt32(reader.GetOrdinal("playerId"));
                            }
                        }
                    }

                    using (var command = new SQLiteCommand(setChampionNewQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Champion = reader.GetInt32(0);
                            }
                        }
                    }
                }

                // this gets the leaders for all the stats
                Dictionary<string, int> statsLeaders = new Dictionary<string, int>()
                {
                  {"MP", 0},
                  { "PTS", 0},
                  { "REB", 0},
                  { "AST", 0},
                  { "STL", 0},
                  { "BLK", 0},
                  { "TOV", 0},
                  { "FGM", 0},
                  { "FGA", 0},
                  { "FGPCT", 0},
                  { "TFGM", 0},
                  { "TFGA", 0},
                  { "TFGPCT", 0},
                };
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    foreach (string stat in statsLeaders.Keys)
                    {
                        string setStatQuery = $@"
                        SELECT 
                        p.playerId,
                        AVG(pgs.{stat}) as {stat}


                        FROM players p, league l
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                        AND pgs.isPlayoffs = 0
                        AND pgs.seasonId = {CurrentSeason}
                        GROUP BY p.playerId, pgs.seasonId
                        ORDER BY AVG(pgs.{stat}) DESC
                        LIMIT 1
                        ";
                        if (stat.Contains("PCT"))
                        {
                            string shortenedStat = stat.Replace("PCT", "");
                            setStatQuery = $@"
                        SELECT 
                        p.playerId,
                        COALESCE(SUM(pgs.{shortenedStat}M) * 100.0 / SUM(pgs.{shortenedStat}A), 0.0) as {stat}


                        FROM players p, league l
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                        AND pgs.seasonId = {CurrentSeason}
                        AND pgs.isPlayoffs = 0
                        GROUP BY p.playerId, pgs.seasonId
                        HAVING SUM(pgs.{shortenedStat}M) > 70
                        ORDER BY {stat} DESC
                        LIMIT 1
                        ";
                        }
                        using (var command = new SQLiteCommand(setStatQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    statsLeaders[stat] = reader.GetInt32(reader.GetOrdinal("playerId"));
                                }
                            }
                        }
                    }
                }


                // set the team awards
                List<string> positions = new List<string>()
                {"PG", "SG", "SF", "PF", "C" };
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    // now we finally set the nonTeamAwards in the database
                    string setAwardsQuery = $@"
                    INSERT INTO seasonNonTeamAwards(MVP, DPOY, FMVP, EFMVP, WFMVP, ROY, Champion, MPLeader,
                    PTSLeader, REBLeader, ASTLeader, STLLeader, BLKLeader, TOVLeader, FGMLeader, 
                    FGALeader, FGPCTLeader, TFGMLeader, TFGALeader, TFGPCTLeader)
                    VALUES(
                    {MVP},
                    {DPOY},
                    {FMVP},
                    {EFMVP},
                    {WFMVP},
                    {ROY},
                    {Champion},
                    {statsLeaders["MP"]},
                    {statsLeaders["PTS"]},
                    {statsLeaders["REB"]},
                    {statsLeaders["AST"]},
                    {statsLeaders["STL"]},
                    {statsLeaders["BLK"]},
                    {statsLeaders["TOV"]},
                    {statsLeaders["FGM"]},
                    {statsLeaders["FGA"]},
                    {statsLeaders["FGPCT"]},
                    {statsLeaders["TFGM"]},
                    {statsLeaders["TFGA"]},
                    {statsLeaders["TFGPCT"]}
                    );";
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = setAwardsQuery;
                        command.ExecuteNonQuery();
                    }

                    // this section sets the All-League and All-Defense (teamAwards) leagues
                    for (int i = 1; i <= 5; i++)
                    {
                        string position = positions[i - 1];
                        string setAllNBAQuery = $@"
                        SELECT 
                        p.playerId,
                        p.playerForename || ' ' || p.playerSurname as name,
                        ((l.currentSeason + 2023) - p.dateOfBirth) AS age,
                        pos.positionShort as playerPosition,
                        AVG(pgs.gameValue) as gameValue


                        FROM players p, league l
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId AND pgs.isPlayoffs = 0
                        AND pgs.seasonId = {CurrentSeason}
                        JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                        JOIN position pos ON pos.positionId = p.positionId
                        WHERE playerPosition = '{position}'
                        GROUP BY p.playerId, pgs.seasonId
                        ORDER BY gameValue DESC
                        LIMIT 3
                        ";
                        List<int> allNBAPos = new List<int>();
                        using (var command = new SQLiteCommand(setAllNBAQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    allNBAPos.Add(reader.GetInt32(reader.GetOrdinal("playerId")));
                                }
                            }
                        }
                        string setAllDefQuery = $@"
                        WITH playerData AS (SELECT 
                        p.playerId,
                        p.playerForename || ' ' || p.playerSurname as name,
                        ((l.currentSeason + 2023) - p.dateOfBirth) AS age,
                        pos.positionShort as playerPosition,
                        AVG(pgs.REB) as REB,
                        AVG(pgs.STL) as STL,
                        AVG(pgs.BLK) as BLK,
                        AVG(pgs.gameValue) as gameValue


                        FROM players p, league l
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId AND pgs.isPlayoffs = 0
                        AND pgs.seasonId = {CurrentSeason}
                        JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                        JOIN position pos ON pos.positionId = p.positionId
                        WHERE playerPosition = '{position}'
                        GROUP BY p.playerId, pgs.seasonId
                        )
                        SELECT 
                        pd.*,
                        1.3 * BLK + STL + 0.04 * gameValue + 0.3 * REB AS defenseValue
                        FROM playerData pd
                        ORDER BY defenseValue DESC
                        LIMIT 3
                        ";
                        List<int> allDefPos = new List<int>();
                        using (var command = new SQLiteCommand(setAllDefQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    allDefPos.Add(reader.GetInt32(reader.GetOrdinal("playerId")));
                                }
                            }
                        }



                        // now we finally set the nonTeamAwards in the database
                        string setTeamAwardsQuery = $@"
                        INSERT INTO seasonTeamAwards(seasonId, positionId, AllNBAOne, AllNBATwo, AllNBAThree,
                        AllDefenseOne, AllDefenseTwo, AllDefenseThree)
                        VALUES(
                        {CurrentSeason},
                        {i},
                        {allNBAPos[0]},
                        {allNBAPos[1]},
                        {allNBAPos[2]},
                        {allDefPos[0]},
                        {allDefPos[1]},
                        {allDefPos[2]}
                        );";

                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = setTeamAwardsQuery;
                            command.ExecuteNonQuery();
                        }
                    }
                }

            }

            //carry out player retirement and give out rookies
            List<Player> players = new List<Player>();
            {
                // extract all the players in the current league
                string getPlayersQuery = $@"
                    SELECT 
                    p.playerId

                    FROM players p, league l
                    JOIN playerGameStats pgs ON pgs.playerId = p.playerId
                    AND pgs.isPlayoffs = FALSE
                    JOIN playerOnTeam pot ON yearJoined <= {2023 + CurrentSeason}
                        AND yearLeft > {2023 + CurrentSeason}
                        AND pot.playerId = p.playerId
                    JOIN
                            teams t ON t.teamId = pot.teamId
                    AND t.teamId != 0
                    JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                    JOIN primaryPlaystyle pp ON sp.primaryPlaystyleId = pp.primaryPlaystyleId
                    JOIN position pos ON pos.positionId = p.positionId
                    GROUP BY p.playerId
                    ";
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(getPlayersQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                int playerId = reader.GetInt32(reader.GetOrdinal("playerId"));
                                Player player = ExtractCurrentPlayerFromPlayerId(playerId);
                                player.GameValue = GetPlayerCurrentGameValue(player.PlayerId);
                                players.Add(player);
                            }
                        }
                    }
                }

                for (int i = 0; i < players.Count; i++)
                {
                    Player player = players[i];
                    if (player.Age < 32) continue;
                    double playerGameValue = player.GameValue;
                    double avgGameValue = -1;
                    string averageGameValueQuery = $@"
                    SELECT ROUND(AVG(pgs.gameValue), 1) as gameValue
                    FROM players p
                    JOIN playerGameStats pgs ON pgs.playerId = p.playerId AND pgs.isPlayoffs = 0
                    WHERE overall < {player.Overall + 6} AND overall > {player.Overall - 6}
                    ORDER BY ROUND(AVG(pgs.gameValue), 1) DESC

                    ";
                    string playerMinutesPlayedQuery = $@"
                    SELECT COALESCE(ROUND(AVG(pgs.MP), 1), 0) as minutesPlayed
                    FROM players p
                    JOIN playerGameStats pgs ON pgs.playerId = p.playerId
                    WHERE p.playerId = {player.PlayerId}";
                    double minutesPlayed = 0;
                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(averageGameValueQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsDBNull(0)) avgGameValue = 3;
                                    else avgGameValue = reader.GetDouble(0);
                                }
                            }
                        }
                        using (var command = new SQLiteCommand(averageGameValueQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    minutesPlayed = reader.GetDouble(0);
                                }
                            }
                        }
                    }
                    double playerGameValueDiff = playerGameValue - avgGameValue;
                    double retireProbability = 0;
                    // convert range of gameValueDiff (-9 to 15) to (0.3 to -0.70)
                    playerGameValueDiff = (-0.3 + (0.3 + 0.62) * (playerGameValueDiff + 9) / (15 + 9)) * -1;
                    // convert player age from (31-40) to (0.09-0.74)
                    double ageVariable = 0.10 + (0.65 - 0.10) * 0.82 * (player.Age - 31) / (40 - 31);
                    if (player.Age > 40) ageVariable = 0.99;
                    retireProbability = playerGameValueDiff + ageVariable;

                    // inititate retirement (playerOnTeam and teamId goes to zero)
                    if (new Random().NextDouble() < retireProbability)
                    {
                        // remove the player from the current 'players' list
                        players.Remove(player);
                        int currentTeamId = player.TeamId;
                        string setRetirementQuery = $@"
                            UPDATE playerOnTeam
                            SET dayLeft = (SELECT MAX(dayId) + 1 FROM seasonSchedule ss WHERE ss.seasonId = {CurrentSeason}),
                            yearLeft = {CurrentSeason + 2023}
                            WHERE playerId = {player.PlayerId}
                        ";
                        string setTeamIdToZeroQuery = $@"
                            UPDATE players
                            SET teamId = 0
                            WHERE playerId = {player.PlayerId}
                        ";

                        // remove this retired player from the user's minutes selection table if needed
                        if (currentTeamId == GetIdFromTeamName(UserTeamName))
                        {
                            // remove player to minutesSelection table
                            string setMinutesQuery = $@"
                            DELETE FROM minutesSelection WHERE playerId = {player.PlayerId}
                            ";
                            using (var connection = new SQLiteConnection(ConnectionString))
                            {
                                connection.Open();
                                using (var command = new SQLiteCommand(connection))
                                {
                                    command.CommandText = setMinutesQuery;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        using (var connection = new SQLiteConnection(ConnectionString))
                        {
                            connection.Open();
                            using (var command = new SQLiteCommand(connection))
                            {
                                command.CommandText = setRetirementQuery;
                                command.ExecuteNonQuery();

                                command.CommandText = setTeamIdToZeroQuery;
                                command.ExecuteNonQuery();
                            }
                        }

                        // now we add a rookie (his son) in his place
                        Player rookiePlayer = new Player();
                        int playerId = 0;
                        // here we need to get the surname for this rookie
                        string currentSuffix = "";
                        string finalSuffix = "";
                        if (player.playerSurname.Contains(" ")) currentSuffix = player.playerSurname.Split(" ")[1];
                        List<string> newSuffixes = GeneratePlayerSuffix(currentSuffix);
                        currentSuffix = newSuffixes[0];
                        finalSuffix = newSuffixes[1];
                        // update the dad's surname to add 'Sr.'
                        if (currentSuffix == "Sr.")
                        {
                            using (var connection = new SQLiteConnection(ConnectionString))
                            {
                                connection.Open();
                                using (var command = new SQLiteCommand(connection))
                                {
                                    string setSurnameQuery = $"UPDATE players SET playerSurname = '{player.playerSurname + " " + currentSuffix}' WHERE playerId = {player.PlayerId}";
                                    command.CommandText = setSurnameQuery;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        // now we generate the rookie's stats
                        rookiePlayer.GenerateRookie(player.position, player.TeamId, player.teamName, player.playerForename, player.playerSurname.Split(" ")[0] + " " + finalSuffix, 67);


                        // here we get the position, playstyle and playerOnTeamId for this specific player; to add
                        int playerOnTeamId = 0;
                        string getPlayerOnTeamIdQuery = @$"
                                    SELECT playerOnTeam.playerOnTeamId 
                                    FROM playerOnTeam
                                    WHERE playerOnTeam.playerId = {player.PlayerId}";

                        int positionId = 0;
                        string getPositionIdQuery = @$"
                                    SELECT positionId 
                                    FROM position 
                                    WHERE position.positionShort = '{player.position}'
                                    ";

                        int playstyleId = 0;
                        string getPlaystyleIdQuery = @$"
                                    SELECT secondaryPlaystyleId
                                    FROM secondaryPlaystyle
                                    WHERE secondaryPlaystyle.playstyle = '{player.SecondaryPlaystyle}'
                                    ";

                        // this connection sets the player's data in the database
                        using (var connection = new SQLiteConnection(ConnectionString))
                        {
                            connection.Open();
                            using (var command = new SQLiteCommand(connection))
                            {
                                string addPlayerOnTeamQuery = $@"INSERT INTO playerOnTeam(playerId,teamId,dayJoined,yearJoined,dayLeft,yearLeft)
                                VALUES(
                                {0},
                                {player.TeamId},
                                1,
                                {CurrentSeason + 2023 + 1},
                                999,
                                9999
                                );";
                                command.CommandText = addPlayerOnTeamQuery;
                                command.ExecuteNonQuery();

                                string setPlayerOnTeamIdQuery = $@"
                                UPDATE playerOnTeam SET playerId = (SELECT MAX(playerId) + 1 FROM players) WHERE playerId = {playerId}
                                ";
                                command.CommandText = setPlayerOnTeamIdQuery;
                                command.ExecuteNonQuery();
                            }

                            // this gets the playerId of the rookie
                            string getPlayerId = $@"SELECT MAX(pot.playerId) FROM playerOnTeam pot";
                            using (var command2 = new SQLiteCommand(getPlayerId, connection))
                            {
                                using (var reader = command2.ExecuteReader())
                                {
                                    while (reader.Read()) playerId = reader.GetInt32(0);

                                }
                            }

                            // get the positionId, playstyleId and playerOnTeamId so that we can add the player at the end
                            getPlayerOnTeamIdQuery = @$"
                                    SELECT playerOnTeam.playerOnTeamId 
                                    FROM playerOnTeam
                                    WHERE playerOnTeam.playerId = {playerId}";
                            using (var command2 = new SQLiteCommand(getPlayerOnTeamIdQuery, connection))
                            {
                                using (var reader = command2.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        playerOnTeamId = reader.GetInt32(0);
                                    }
                                }
                            }
                            getPositionIdQuery = @$"
                                    SELECT positionId 
                                    FROM position 
                                    WHERE position.positionShort = '{player.position}'
                                    ";
                            using (var command2 = new SQLiteCommand(getPositionIdQuery, connection))
                            {
                                using (var reader = command2.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        positionId = reader.GetInt32(0);
                                    }
                                }
                            }
                            getPlaystyleIdQuery = @$"
                                    SELECT secondaryPlaystyleId
                                    FROM secondaryPlaystyle
                                    WHERE secondaryPlaystyle.playstyle = '{player.SecondaryPlaystyle}'
                                    ";
                            using (var command2 = new SQLiteCommand(getPlaystyleIdQuery, connection))
                            {
                                using (var reader = command2.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        playstyleId = reader.GetInt32(0);
                                    }
                                }
                            }

                            using (var command = new SQLiteCommand(connection))
                            {
                                string addPlayerQuery = $@"INSERT INTO players(playerOnTeamId, teamId,playerForename,playerSurname,positionId,dateOfBirth,overall,potential,
                                secondaryPlaystyleId,height,weight,closeShot,layup,dunk,midRange,threePoint,freeThrow,passing,ballHandle,defense,
                                steal,block,rebound,speed,strength,stamina)
                                VALUES(
                                {playerOnTeamId},
                                {rookiePlayer.TeamId},
                                '{rookiePlayer.playerForename}',
                                '{rookiePlayer.playerSurname}',
                                {positionId},
                                {2024 + CurrentSeason - rookiePlayer.Age},
                                {rookiePlayer.Overall},
                                {rookiePlayer.Potential},
                                {playstyleId},
                                {rookiePlayer.Height},
                                {rookiePlayer.Weight},
                                {rookiePlayer.CloseShot},
                                {rookiePlayer.Layup},
                                {rookiePlayer.Dunk},
                                {rookiePlayer.MidRange},
                                {rookiePlayer.ThreePoint},
                                {rookiePlayer.FreeThrow},
                                {rookiePlayer.Passing},
                                {rookiePlayer.BallHandle},
                                {rookiePlayer.Defense},
                                {rookiePlayer.Steal},
                                {rookiePlayer.Block},
                                {rookiePlayer.Rebound},
                                {rookiePlayer.Speed},
                                {rookiePlayer.Strength},
                                {rookiePlayer.Stamina}
                                );";

                                // if this is the user's team, we add this player to the minutesSELECTION query
                                if (player.TeamId == GetIdFromTeamName(UserTeamName))
                                {
                                    string setMinutesQuery = $@"INSERT INTO minutesSelection VALUES ({playerId}, 16)";
                                    command.CommandText = setMinutesQuery;
                                    command.ExecuteNonQuery();

                                }


                                command.CommandText = addPlayerQuery;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            // carry out player progression
            {

                // find the average gameValue from players with overall, x - 5 < x < x + 5
                // where x is the current player's overall e.g. 85 < x < 95 where overall is 90
                // then we compare the current player's gameValue with average to calculate a new overall
                // then apply attribute boosts
                string fullQuery = "BEGIN TRANSACTION; \n";
                string fullPlayerUpdateQuery = "";
                string fullProgressionQuery = "INSERT INTO playerProgression(playerId, seasonId, progression)\r\n  VALUES";
                foreach (Player player in players)
                {
                    double playerGameValue = player.GameValue;
                    double avgGameValue = -1;
                    string averageGameValueQuery = $@"
                    SELECT ROUND(AVG(pgs.gameValue), 1) as gameValue
                    FROM players p
                    JOIN playerGameStats pgs ON pgs.playerId = p.playerId AND pgs.isPlayoffs = 0
                    WHERE overall < {player.Overall + 6} AND overall > {player.Overall - 6}
                    ORDER BY ROUND(AVG(pgs.gameValue), 1) DESC

                    ";
                    string playerMinutesPlayed = $@"
                    SELECT COALESCE(ROUND(AVG(pgs.MP), 1), 0) as minutesPlayed
                    FROM players p
                    JOIN playerGameStats pgs ON pgs.playerId = p.playerId
                    WHERE p.playerId = {player.PlayerId}";
                    double minutesPlayed = 0;
                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(averageGameValueQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsDBNull(0)) avgGameValue = 5;
                                    else avgGameValue = reader.GetDouble(0);
                                }
                            }
                        }
                        using (var command = new SQLiteCommand(averageGameValueQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    minutesPlayed = reader.GetDouble(0);
                                }
                            }
                        }
                    }

                    // now we set the boost the player gets
                    double finalBoost = 0;
                    double playerGameValueDiff = playerGameValue - avgGameValue;
                    // convert range of gameValueDiff (-20 to 25) to (-1.1 to 1.1)
                    playerGameValueDiff = -1 + (1 + 1) * (playerGameValueDiff + 20) / (25 + 20);
                    playerGameValueDiff += minutesPlayed / 48;
                    if (player.Age < 24) playerGameValueDiff += (24 - player.Age) * 0.12;
                    if (playerGameValueDiff > 1.1) playerGameValueDiff = 1.1;
                    if (playerGameValueDiff > 0.84 && player.Age < 26) playerGameValueDiff *= 2;
                    else if (playerGameValueDiff < -1.1) playerGameValueDiff = -1.1;

                    // convert range of age (18 to 38) to (2.5 to -3.0)
                    double ageVariable = 2.5 + (- 3.0 - 2.5) * (player.Age - 18) / (38 - 18);
                    if (player.Age >= 30) ageVariable -= 2.0 + 0.3 * (player.Age - 30);
                    finalBoost += playerGameValueDiff + ageVariable;

                    // slightly randomises the boost, so there could be a slight increase or decrease in performance
                    double finalBoostMultiplier = Player.GenerateRandomNormalDistribution(0, 1.0);
                    finalBoost += finalBoostMultiplier;

                    // add team position boost to player, the worse their team finished, the bigger the boost they get
                    if (player.Age < 35) finalBoost += GetTeamPositionPlayerBoost(player.TeamId);

                    // add extra boost for high potential players, and make sure any extreme boosts don't occur
                    if (player.Potential > player.Overall)
                    {
                        finalBoost += (player.Potential - player.Overall) / 7.2;
                        if (player.Age < 23) finalBoost *= 1 + 0.02 * (22 - player.Age);
                    }
                    if (finalBoost > 7 && player.Age > 19) { finalBoost = 7 + Player.GenerateRandomNormalDistribution(-1.1, 0.4); }
                    else if (finalBoost < -7) { finalBoost = -7 + Player.GenerateRandomNormalDistribution(1.1, 0.4); }
                    finalBoost = Math.Round(finalBoost);

                    // reduce the potential of older players
                    if (player.Age > 30) player.Potential -= 36 - player.Age;
                    if (player.Overall + finalBoost < 60 || player.Potential + finalBoost < 60) finalBoost = 0;
                    else if (player.Overall + finalBoost > 99) finalBoost = 99 - player.Overall;
                    else if (player.Potential + finalBoost > 99) finalBoost = 99 - player.Potential;
                    if (finalBoost < 0) player.Potential -= (int)finalBoost;
                    
                    // once a player hits 33, they have no potential
                    if (player.Age > 32) player.Potential = player.Overall;


                    double overallMultiplier = (player.Overall + finalBoost) / player.Overall;
                    // now we set the attributes
                    {
                        player.Dunk = (int)(player.Dunk * overallMultiplier);
                        player.MidRange = (int)(player.MidRange * overallMultiplier);
                        player.ThreePoint = (int)(player.ThreePoint * overallMultiplier);
                        player.FreeThrow = (int)(player.FreeThrow * overallMultiplier);
                        player.Passing = (int)(player.Passing * overallMultiplier);
                        player.BallHandle = (int)(player.BallHandle * overallMultiplier);
                        player.Defense = (int)(player.Defense * overallMultiplier);
                        player.Steal = (int)(player.Steal * overallMultiplier);
                        player.Block = (int)(player.Block * overallMultiplier);
                        player.Rebound = (int)(player.Rebound * overallMultiplier);
                        player.Speed = (int)(player.Speed * overallMultiplier);
                        player.Stamina = (int)(player.Stamina * overallMultiplier);
                        player.Strength = (int)(player.Strength * overallMultiplier);
                        player.Layup = (int)(player.Layup * overallMultiplier);

                        if (player.Layup > 99) player.Layup = 99;
                        else if (player.Layup < 25) player.Layup = 25;
                        if (player.Dunk > 99) player.Dunk = 99;
                        else if (player.Dunk < 25) player.Dunk = 25;
                        if (player.MidRange > 99) player.MidRange = 99;
                        else if (player.MidRange < 25) player.MidRange = 25;
                        if (player.ThreePoint > 99) player.ThreePoint = 99;
                        else if (player.ThreePoint < 25) player.ThreePoint = 25;
                        if (player.FreeThrow > 99) player.FreeThrow = 99;
                        else if (player.FreeThrow < 25) player.FreeThrow = 25;
                        if (player.Passing > 99) player.Passing = 99;
                        else if (player.Passing < 25) player.Passing = 25;
                        if (player.BallHandle > 99) player.BallHandle = 99;
                        else if (player.BallHandle < 25) player.BallHandle = 25;
                        if (player.Defense > 99) player.Defense = 99;
                        else if (player.Defense < 25) player.Defense = 25;
                        if (player.Steal > 99) player.Steal = 99;
                        else if (player.Steal < 25) player.Steal = 25;
                        if (player.Block > 99) player.Block = 99;
                        else if (player.Block < 25) player.Block = 25;
                        if (player.Rebound > 99) player.Rebound = 99;
                        else if (player.Rebound < 25) player.Rebound = 25;
                        if (player.Speed > 99) player.Speed = 99;
                        else if (player.Speed < 25) player.Speed = 25;
                        if (player.Stamina > 99) player.Stamina = 99;
                        else if (player.Stamina < 25) player.Stamina = 25;
                        if (player.Strength > 99) player.Strength = 99;
                        else if (player.Strength < 25) player.Strength = 25;
                    }
                    int finalPotBoost = (int)(finalBoost * 0.5);
                    if (finalPotBoost < -4) player.Potential += 4 + finalPotBoost;
                    else if (finalPotBoost < 0) finalPotBoost = 0; 

                    player.Overall += (int)finalBoost;
                    player.Potential += finalPotBoost;
                    if (player.Overall > player.Potential && finalPotBoost <= 0) { player.Potential = player.Overall; }
                    else if (player.Overall > player.Potential) player.Potential = player.Overall + finalPotBoost;
                    if (player.Potential > 99) player.Potential = 99;

                    //update player's stats in database
                    fullPlayerUpdateQuery += $@"
                    UPDATE players 
                    SET layup = {player.Layup},
                    dunk = {player.Dunk},
                    midRange = {player.MidRange},
                    threePoint = {player.ThreePoint},
                    freeThrow = {player.FreeThrow},
                    passing = {player.Passing},
                    ballHandle = {player.BallHandle},
                    defense = {player.Defense},
                    steal = {player.Steal},
                    block = {player.Block},
                    rebound = {player.Rebound},
                    speed = {player.Speed},
                    stamina = {player.Stamina},
                    overall = {player.Overall},
                    potential = {player.Potential},
                    strength = {player.Strength}
                    WHERE playerId = {player.PlayerId}; 
                    " + $"\n";
                    //now we add the player's progression to the database
                    fullProgressionQuery += $@"({player.PlayerId}, {CurrentSeason}, {finalBoost}),";
                }
                fullProgressionQuery = fullProgressionQuery.Substring(0, fullProgressionQuery.Length - 1) + ";";
                fullQuery += fullProgressionQuery + fullPlayerUpdateQuery + "COMMIT;";
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = fullQuery;
                        command.ExecuteNonQuery();
                    }
                }

            }

            // move league into next season
            {
                Playoffs = false;
                PlayoffsRound = "";
                CurrentDay = 1;
                CurrentSeason++;

                // we generate a schedule for season 2
                CurrentSchedule = new List<List<string>>();
                CurrentSchedule = GenerateSchedule();
                CurrentPlayoffsSchedule = new List<List<string>>();
                InsertLeagueSchedule(CurrentSchedule);

                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string updateLeagueQuery = $@"
                    REINDEX playerGameStatsOnlySeasonIdIndex;
                    REINDEX playerGameStatsPlayerIdSeasonIdIndex;
                    UPDATE league
                    SET currentDay = {CurrentDay},
                    currentSeason = {CurrentSeason};
                    DELETE FROM minutesSelection
                    WHERE playerId NOT IN (
                    SELECT p.playerId
                    FROM players p
                    JOIN teams t ON t.teamId = p.teamId
                    AND t.teamName = '{UserTeamName}');
                    ;";
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = updateLeagueQuery;
                        command.ExecuteNonQuery();
                    }
                }

                string fullQuery = "BEGIN TRANSACTION; \n INSERT INTO teamResults(teamId, seasonId, wins, losses) \n VALUES";
                for (int i = 0; i < 30; i++)
                {
                    fullQuery += $"({i + 1}, {CurrentSeason}, 0, 0)";
                    if (i == 29) fullQuery += ";" + "\n";
                    else fullQuery += "," + "\n";
                }
                fullQuery += "COMMIT;";
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = fullQuery;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public double GetTeamPositionPlayerBoost(int teamId)
        {
            string checkTeamBoostQuery = $@"
                WITH teamBoosts AS (SELECT 
                t.teamId, 
                t.teamName,
                tr.seasonId + 2023 as year,
                CASE WHEN tr.losses = 0 THEN tr.wins 
                ELSE CAST(tr.wins AS DOUBLE) / (tr.wins + tr.losses) 
                END AS winPct,
                ROW_NUMBER () OVER (ORDER BY tr.wins * 1.0 / (tr.wins + tr.losses) DESC) * 0.19 - 0.57 as teamBoost
                FROM teams t
                LEFT JOIN teamResults tr ON tr.teamId = t.teamId
                AND t.conferenceId = {GetConferenceIdFromTeamId(teamId.ToString())}
                AND tr.seasonId  = {CurrentSeason}
                GROUP BY tr.seasonId, tr.teamId
                ORDER BY winPct DESC
                )
                SELECT teamBoost
                FROM teamBoosts 
                WHERE teamId = {teamId}
            ;";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(checkTeamBoostQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDouble(0);
                        }
                    }
                }
            }
            return 0.0;
        }

        public List<string> GeneratePlayerSuffix(string currentSuffix)
        {
            string finalSuffix = "";
            if (currentSuffix == "") { finalSuffix = "Jr."; currentSuffix = "Sr."; }
            else if (currentSuffix == "Jr.") { finalSuffix = "III"; }
            else if (currentSuffix == "III") { finalSuffix = "IV"; }
            else if (currentSuffix == "IV") { finalSuffix = "V"; }
            else if (currentSuffix == "V") { finalSuffix = "VI"; }
            else if (currentSuffix == "VI") { finalSuffix = "VII"; }
            else if (currentSuffix == "VII") { finalSuffix = "VIII"; }
            else if (currentSuffix == "VIII") { finalSuffix = "IX"; }
            else if (currentSuffix == "IX") { finalSuffix = "X"; }

            return new List<string>() { currentSuffix, finalSuffix };
        }

        public bool CheckIfInitialPlayoffRoundGamesComplete()
        {
            string checkSeriesCompleteQuery = $@"
                SELECT COUNT(*)
                FROM playoffsSchedule
                WHERE playoffsRound = '{PlayoffsRound}'
                AND gameCompleted = 0
            ";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(checkSeriesCompleteQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0) return true;
                            else return false;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckIfFullPlayoffRoundGamesComplete()
        {
            string checkFullRoundGamesCompleteNewQuery = $@"
                SELECT COUNT(*) = 0
                FROM playoffResults pr
                WHERE pr.seasonId = {CurrentSeason}
                AND pr.playoffsRound = '{PlayoffsRound}'
                AND (wins < 4 AND losses < 4)
                ";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(checkFullRoundGamesCompleteNewQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetBoolean(0);
                        }
                    }
                }
            }
            return true;
        }

        public void AddAnyNeededPlayoffGames()
        {
            string getUncompleteSeriesNewQuery = $@"
                SELECT pr.seed, pr.conferenceId, pr.homeTeamId, pr.awayTeamId
                FROM playoffResults pr
                WHERE pr.wins < 4 AND pr.losses < 4
                AND pr.playoffsRound = '{PlayoffsRound}'
                AND pr.seasonId = {CurrentSeason}
                ";
            List<int> seeds = new List<int>();
            List<int> conferenceIds = new List<int>();
            List<int> homeTeamIds = new List<int>();
            List<int> awayTeamIds = new List<int>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getUncompleteSeriesNewQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            seeds.Add(reader.GetInt32(reader.GetOrdinal("seed")));
                            conferenceIds.Add(reader.GetInt32(reader.GetOrdinal("conferenceId")));
                            homeTeamIds.Add(reader.GetInt32(reader.GetOrdinal("homeTeamId")));
                            awayTeamIds.Add(reader.GetInt32(reader.GetOrdinal("awayTeamId")));
                        }
                    }
                }
            }
            // add any extra games to the next round and don't move on to the next game
            for (int i = 0; i < seeds.Count; i++)
            {
                int currentSeed = seeds[i];
                int currentConferenceId = conferenceIds[i];
                int currentHomeTeamId = homeTeamIds[i];
                int curentAwayTeamId = awayTeamIds[i];
                // now we insert this game into the database
                string insertPlayoffGameQuery = $@"
                    INSERT INTO playoffsSchedule(seasonId,dayId,conferenceId,seed,playoffsRound,homeTeamId,awayTeamId,gameCompleted)
                    VALUES(
                    {CurrentSeason},
                    {CurrentDay + 1},
                    {currentConferenceId},
                    {currentSeed},
                    '{PlayoffsRound}',
                    {currentHomeTeamId},
                    {curentAwayTeamId},
                    {false}
                    );
                    ";
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = insertPlayoffGameQuery;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void SetCurrentDayForPlayoffs()
        {
            string setDayToOneQuery = "UPDATE league SET currentDay = 1";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = setDayToOneQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

        public (List<string>, List<string>, List<string>) WatchGame(string game, int currentDayOfGame)
        {
            // simulate game specified by user
            (List<string>, List<string>, List<string>) gameData = (new List<string>(), new List<string>(), new List<string>());
            GamesPlayed++;
            string[] teamsPlaying = game.Split(",");
            bool gameComplete = false;
            if (Playoffs) gameComplete = CheckIfPlayoffGameCompleted(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
            else gameComplete = CheckIfGameCompleted(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
            if (!gameComplete)
            {
                Team team1 = new Team(GetTeamNameFromId(teamsPlaying[0]), Convert.ToInt32(teamsPlaying[0]), CurrentUser);
                Team team2 = new Team(GetTeamNameFromId(teamsPlaying[1]), Convert.ToInt32(teamsPlaying[1]), CurrentUser);
                int gameId = 0;
                if (Playoffs) gameId = GetPlayoffGameId(game, currentDayOfGame);
                else gameId = GetGameId(game, currentDayOfGame);
                GameGenerator simulatedGame = new GameGenerator(team1, team2, ConnectionString, CurrentUser, CurrentSaveState, gameId, Playoffs, this);
                gameData = (simulatedGame.CommentatorPhrases, simulatedGame.ScoreAfterEachPhrase, simulatedGame.GameTimestamps);
                if (Playoffs) SetPlayoffGameToComplete(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
                else SetGameToComplete(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
            }

            // we check if all games are complete in the day, then increment currentDay if so
            bool allGamesComplete = CheckIfDayOfGameCompleted(currentDayOfGame);

            // here we check if we need to add any additional games
            bool seasonComplete = false;
            if (Playoffs)
            {
                // if all series are complete; we advance to the next round

                if (CheckIfFullPlayoffRoundGamesComplete())
                {
                    MoveToNextPlayoffRound();
                    if (PlayoffsRound == "Second Round") GeneratePlayoffsSecondRound();
                    else if (PlayoffsRound == "Conference Finals") GeneratePlayoffsConferenceFinals();
                    else if (PlayoffsRound == "Finals") GeneratePlayoffsFinals();
                    else if (PlayoffsRound == "") seasonComplete = true;
                    if (Playoffs && !seasonComplete) CurrentPlayoffsSchedule = LoadPlayoffsSchedule();
                }
                // if all game 4s are complete, game 5s are complete etc. we add any initial games
                // for any uncomplete series
                if (CheckIfInitialPlayoffRoundGamesComplete() && !seasonComplete)
                {
                    AddAnyNeededPlayoffGames();
                    CurrentPlayoffsSchedule = LoadPlayoffsSchedule();
                }
            }
            if (GamesPlayed % 1230 == 0 && !seasonComplete && !Playoffs)
            {
                // here, we move the league into playoffs mode
                Playoffs = true;
                PlayoffsRound = "First Round";
                CurrentDay = 0;
                SetCurrentDayForPlayoffs();
                GeneratePlayoffsFirstRound();
                CurrentPlayoffsSchedule = new List<List<string>>();
                CurrentPlayoffsSchedule = LoadPlayoffsSchedule();
            }
            if (allGamesComplete && !seasonComplete)
            {
                CurrentDay++;
                UpdateLeagueDataIfNeeded(CurrentDay - 1);
            }
            if (seasonComplete)
            {
                AdvanceToNextSeason();
            }

            return (gameData.Item1, gameData.Item2, gameData.Item3);
        }

        public int GetPlayerIdFromName(string firstname, string surname)
        {
            string getPlayerIdQuery = $"SELECT p.playerId FROM players p WHERE p.playerForename = '{firstname}' AND p.playerSurname = '{surname}';";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getPlayerIdQuery, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) return reader.GetInt32(0);
                }
            }
            return 1;
        }

        public int GetGameId(string game, int currentDayOfGame)
        {
            string[] teamsPlaying = game.Split(",");
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getGameIdQuery = $@"
                    SELECT gameId
                    FROM seasonSchedule
                    WHERE dayId = {currentDayOfGame}
                    AND homeTeamId = {teamsPlaying[0]}
                    AND awayTeamId = {teamsPlaying[1]}
                    AND seasonId = {CurrentSeason}
                    ;";
                using (var command = new SQLiteCommand(getGameIdQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return -1;
        }

        public int GetPlayoffGameId(string game, int currentDayOfGame)
        {
            string[] teamsPlaying = game.Split(",");
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getGameIdQuery = $@"
                    SELECT playoffsGameId
                    FROM playoffsSchedule
                    WHERE dayId = {currentDayOfGame}
                    AND homeTeamId = {teamsPlaying[0]}
                    AND awayTeamId = {teamsPlaying[1]}
                    AND seasonId = {CurrentSeason}
                    ;";
                using (var command = new SQLiteCommand(getGameIdQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return -1;
        }

        public int GetRegSeasonGamesPlayed()
        {
            int gamesPlayed = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getCompletedGamesQuery = $"SELECT COUNT(*) FROM seasonSchedule WHERE gameCompleted = 1 AND seasonId = {CurrentSeason};";
                using (var command = new SQLiteCommand(getCompletedGamesQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) gamesPlayed = reader.GetInt32(0);

                    }
                }
            }
            return gamesPlayed;
        }

        public int GetLeagueCurrentDay()
        {
            int currentDay = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getCurrentDayQuery = "SELECT currentDay FROM league;";
                using (var command = new SQLiteCommand(getCurrentDayQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) currentDay = reader.GetInt32(0);

                    }
                }
            }
            return currentDay;
        }

        public League(string currentUser, int saveState, bool createLeague, string userTeamName, string leagueFileName)
        {
            CurrentUser = currentUser;
            CurrentDay = 1;
            CurrentSeason = 1;
            LeagueFileName = leagueFileName;
            ConnectionString = $"Data Source={LeagueFileName};Version=3;";
            CurrentSaveState = saveState;
            UserTeamName = userTeamName;
            if (createLeague) CreateLeague();
            else LoadLeague(saveState);
            CurrentDay = GetLeagueCurrentDay();
            GamesPlayed = GetRegSeasonGamesPlayed();

        }
    }
}
