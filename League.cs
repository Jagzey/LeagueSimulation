using System.Data.SQLite;

namespace LeagueSimulation
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
        public List<List<string>> CurrentSchedule { get; set; }
        public List<List<string>> CurrentPlayoffsSchedule { get; set; }

        public void InitialiseDatabase(string leagueFileName)
        {
            // Creates a connection to leaguetest database
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

                // create team game stats table
                string createTeamGameStatsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS teamGameStats(
                    gameId INTEGER NOT NULL,
                    teamId INTEGER NOT NULL,
                    teamName TEXT NOT NULL,
                    opponentName TEXT NOT NULL,
                    location TEXT NOT NULL,
                    result TEXT NOT NULL,
                    score TEXT NOT NULL,
                    FGPCT REAL NOT NULL,
                    TFGPCT REAL NOT NULL,
                    PTS INTEGER NOT NULL,
                    REB INTEGER NOT NULL,
                    AST INTEGER NOT NULL,
                    STL INTEGER NOT NULL,
                    BLK INTEGER NOT NULL,
                    TOV INTEGER NOT NULL,
                    FOREIGN KEY (gameId) REFERENCES playerGameStats(gameId),
                    FOREIGN KEY (teamId) REFERENCES teams(teamId),
                    PRIMARY KEY (gameId, teamId)
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
                        rosterSpot INTEGER,
                        dateJoined TEXT NOT NULL,
                        dateLeft TEXT NOT NULL
                        );";

                string createPlayerGameStatsQuery = @"
                        CREATE TABLE playerGameStats(
                        playerGameStatsId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        gameId INTEGER NOT NULL,
                        playerId INTEGER NOT NULL,
                        gameValue DECIMAL (3,1) NOT NULL,
                        MP INTEGER NOT NULL,
                        FGM INTEGER NOT NULL,
                        FGA INTEGER NOT NULL,
                        TFGM INTEGER NOT NULL,
                        TFGA INTEGER NOT NULL,
                        PTS INTEGER NOT NULL,
                        REB INTEGER NOT NULL,
                        AST INTEGER NOT NULL,
                        STL INTEGER NOT NULL,
                        BLK INTEGER NOT NULL,
                        TOV INTEGER NOT NULL,
                        PF INTEGER NOT NULL,
                        FOREIGN KEY (playerId) REFERENCES players(playerId)
                        );";

                string createPlayerSeasonTableQuery = @"
                        CREATE TABLE playersSeasonStats(
                        seasonId INTEGER NOT NULL,
                        playerId INTEGER NOT NULL,
                        teamId INTEGER NOT NULL,
                        teamName TEXT NOT NULL,
                        playerForename TEXT NOT NULL,
                        playerSurname TEXT NOT NULL,
                        position TEXT NOT NULL,
                        gameValue DECIMAL (3,1) NOT NULL,
                        MP DECIMAL (3,1) NOT NULL,
                        FGPCT DECIMAL (3,1) NOT NULL,
                        TFGPCT DECIMAL (3,1) NOT NULL,
                        PTS DECIMAL (3,1) NOT NULL,
                        REB DECIMAL (3,1) NOT NULL,
                        AST DECIMAL (3,1) NOT NULL,
                        STL DECIMAL (3,1) NOT NULL,
                        BLK DECIMAL (3,1) NOT NULL,
                        TOV DECIMAL (3,1) NOT NULL,
                        PF DECIMAL (3,1) NOT NULL,
                        FOREIGN KEY (seasonId) REFERENCES seasonSchedule(seasonId),
                        FOREIGN KEY (teamId) REFERENCES teams(teamId),
                        FOREIGN KEY (playerId) REFERENCES players(playerId),
                        PRIMARY KEY (seasonId, playerId, teamId)
                        );";

                string createScheduleTableQuery = @"
                    CREATE TABLE seasonSchedule(
                    gameId INTEGER NOT NULL,
                    seasonId INTEGER NOT NULL,
                    dayId INTEGER NOT NULL,
                    homeTeamId INTEGER NOT NULL,
                    awayTeamId INTEGER NOT NULL,
                    gameCompleted BOOLEAN NOT NULL,
                    PRIMARY KEY (seasonId, dayId, gameId)
                    );";

                string createPlayoffsScheduleTableQuery = @"
                    CREATE TABLE playoffsSchedule(
                    playoffsGameId INTEGER NOT NULL,
                    seasonId INTEGER NOT NULL,
                    dayId INTEGER NOT NULL,
                    round TEXT NOT NULL,
                    homeTeamId INTEGER,
                    awayTeamId INTEGER,
                    gameCompleted BOOLEAN NOT NULL,
                    PRIMARY KEY (seasonId, dayId, playoffsGameId)
                    );";

                string leagueTableQuery = @"
                    CREATE TABLE league(
                    leagueId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    currentDay INTEGER NOT NULL,
                    currentSeason INTEGER NOT NULL,
                    userTeamName TEXT NOT NULL
                    );";

                string primaryPlaystyleQuery = @"
                    CREATE TABLE primaryPlaystyle(
                    primaryPlaystyleId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    playstyle TEXT NOT NULL,
                    description TEXT NOT NULL
                    );";

                string secondaryPlaystyleQuery = @"
                    CREATE TABLE secondaryPlaystyle(
                    secondaryPlaystyleId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    primaryPlaystyleId INTEGER NOT NULL,
                    playstyle TEXT NOT NULL,
                    description TEXT NOT NULL
                    );";

                string positionQuery = $@"
                    CREATE TABLE position(
                    positionId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    positionShort TEXT NOT NULL,
                    positionName TEXT NOT NULL,
                    description TEXT NOT NULL
                    );";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createTeamsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createConferenceQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createTeamGameStatsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayersTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayerOnTeamTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayerSeasonTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayerGameStatsQuery;
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
                        ("Shooter", "A player who can and will choose to shoot the ball"),
                        ("Playmaker", "A player who likes to make plays and lead passes to his teammates"),
                        ("Finisher", "A player who can play fast and loves to attack the rim")
                    };
                    List<(string, string)> defensiveValues = new List<(string, string)>()
                    {
                        ("Ripper", "A player who likes to steal the ball from his opponent"),
                        ("Lockdown", "A player who likes to play great defense to stop the opponents from scoring"),
                        ("Rim Protector", "A player who likes to prevent buckets in and around the rim")
                    };

                    (string, string) twoWayValue = ("2-Way Player", "A player who can play on both sides of the ball");

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
            string teamFilePath = $@"C:\Users\{CurrentUser}\OneDrive - The Kings School Chester\A-Level\Computer Science\NEA Project\Project Files\LeagueGenerator\Names Files\basketball_team_names_list.txt";
            string[] teamNames = File.ReadAllLines(teamFilePath);
            string playerFilePath = $@"C:\Users\{CurrentUser}\OneDrive - The Kings School Chester\A-Level\Computer Science\NEA Project\Project Files\LeagueGenerator\Names Files\male_names_list.txt";
            string[] playerNames = File.ReadAllLines(playerFilePath);

            // Creates a connection to leagueX database
            string connectionString = $@"Data Source={leagueFileName};Version=3;";
            if (File.Exists(leagueFileName))
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    // This makes sure we go through 450 names
                    int nameCounter = -1;
                    using (var command = new SQLiteCommand(connection))
                    {
                        // generate 30 teams
                        for (int i = 0; i < 30; i++)
                        {
                            Random random = new Random();
                            Team team = new Team(teamNames[i], i + 1);
                            int conference = 0;
                            if (i < 15) conference = 1;
                            else
                            {
                                conference = 2;
                                team.Position -= 15;
                            }
                            int teamOverall = random.Next(77, 83);
                            string addTeamQuery = $@"INSERT INTO teams(teamName,city,conferenceId)
	                        VALUES(
                            '{team.teamName}',
	                        '{team.city}',
                            {conference}
                            );";
                            command.CommandText = addTeamQuery;
                            command.ExecuteNonQuery();
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
                                nameCounter++;
                                string[] playerName = playerNames[nameCounter].Split(' ');

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
                                string addPlayerOnTeamQuery = $@"INSERT INTO playerOnTeam(playerId,teamId,dateJoined,dateLeft)
                                VALUES(
                                {player.PlayerId + (i * 15)},
                                {team.TeamId},
                                '1/2024',
                                '999/9999'
                                );";

                                command.CommandText = addPlayerOnTeamQuery;
                                command.ExecuteNonQuery();

                                int playerOnTeamId = 0;
                                string getPlayerOnTeamIdQuery = @$"
                                    SELECT playerOnTeam.playerOnTeamId 
                                    FROM playerOnTeam
                                    WHERE playerOnTeam.playerId = {player.PlayerId + (i * 15)}";

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


                                string addPlayerQuery = $@"INSERT INTO players(playerOnTeamId, teamId,playerForename,playerSurname,positionId,dateOfBirth,overall,potential,
                                secondaryPlaystyleId,height,weight,closeShot,layup,dunk,midRange,threePoint,freeThrow,passing,ballHandle,defense,
                                steal,block,rebound,speed,strength,stamina,overall)
                                VALUES(
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
                                );";


                                command.CommandText = addPlayerQuery;
                                command.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }
        }

        public static int CalculatePlayerRosterSpot(int playerId, string connectionString)
        {
            // initially, we get the highest overalls for each position, then sort by rosterSpot
            string getRosterSpotQuery = $@"
                WITH RankedByPosition AS (
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
                    WHERE
                        p.teamId = (SELECT p.teamId FROM players p, teams t WHERE p.teamId = t.teamId AND p.playerId = {playerId}) -- Filter by the given team
                ),
                TopFive AS (
                    SELECT
                        rbp.playerId,
                        rbp.teamId,
                        rbp.positionId,
                        rbp.overall,
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
                        p.playerId,
                        p.teamId,
                        p.positionId,
                        p.overall,
                        ROW_NUMBER() OVER (
                            ORDER BY p.overall DESC
                        ) + 5 AS rosterSpot -- Start from 6
                    FROM
                        players p
                    JOIN
                        teams t ON p.teamId = t.teamId
                    WHERE
                        p.teamId = p.teamId = (SELECT p.teamId FROM players p, teams t WHERE p.teamId = t.teamId AND p.playerId = {playerId}) -- Filter by the given team -- Filter by the given team
                        AND p.playerId NOT IN (SELECT playerId FROM TopFive) -- Exclude top 5 players
                    LIMIT 10 -- Only include the remaining 10 players
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
                WHERE playerId = {playerId} -- enter playerId for the player you want to query
                ORDER BY
                    rosterSpot;
                ";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getRosterSpotQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) return reader.GetInt32(reader.GetOrdinal("rosterSpot"));
                    }
                }
            }
            return 0;
        }

        public string GetUserTeamLeaderInStatistic(string stat)
        {
            string fullTeamLeaderString = "";
            List<int> playersOnTeam = new List<int>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string teamLeaderQuery = $@"
                    WITH PlayerAverage{stat} AS (
                        SELECT
                            p.playerId,
                            p.playerForename,
                            p.playerSurname,
                            AVG(pgs.{stat}) AS avg{stat}
                        FROM
                            league l
                        JOIN
                            seasonSchedule sg ON sg.seasonId = l.currentSeason
                        JOIN
                            playerGameStats pgs ON sg.gameId = pgs.gameId
                        JOIN
                            players p ON p.playerId = pgs.playerId
                        WHERE
                            sg.gameCompleted = 1 -- Only include completed games
                            AND p.teamId = {GetIdFromTeamName(UserTeamName)} -- Replace with the given teamId
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

        public string GetTeamStatistic(string stat)
        {
            /* using (var connection = new SQLiteConnection(ConnectionString))
             {
                 connection.Open();
                 string getTeamStatQuery = $"SELECT AVG({stat}) FROM teamGameStats WHERE teamName = '{this.UserTeamName}';";
                 using (var command = new SQLiteCommand(getTeamStatQuery, connection))
                 {
                     using (var reader = command.ExecuteReader())
                     {
                         while (reader.Read())
                         {
                             if (reader.IsDBNull(0)) return "";
                             return $"{Math.Round(reader.GetDouble(0), 1)}";
                         }
                     }
                 }

             }*/
            string teamLeaderQuery = $@"
                    WITH TeamAverage{stat} AS (
                        SELECT
                            p.teamId,
                            sg.gameId,
                            sg.seasonId,
                            SUM(pgs.{stat}) AS total{stat}
                        FROM
                            seasonSchedule sg
                        JOIN
                            league l ON l.currentSeason = sg.seasonId
                        JOIN
                            playerGameStats pgs ON sg.gameId = pgs.gameId
                        JOIN
                            players p ON p.playerId = pgs.playerId
                        WHERE
                            sg.gameCompleted = 1 -- Only include completed games
                            AND p.teamId = {GetIdFromTeamName(UserTeamName)} -- Replace with the given teamId
                        GROUP BY
                            p.teamId, sg.gameId, sg.seasonId
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
        public string GetTeamRecord(string teamName)
        {
            int wins = 0;
            int losses = 0;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getWinsLossesQuery = $@"
                    WITH TeamGameScores AS (
                        SELECT
                            sg.seasonId,
                            sg.gameId,
                            sg.homeTeamId AS homeTeamId,
                            sg.awayTeamId AS awayTeamId,
                            CASE 
                                WHEN sg.homeTeamId = p.teamId THEN sg.homeTeamId
                                WHEN sg.awayTeamId = p.teamId THEN sg.awayTeamId
                            END AS teamId,
                            SUM(pgs.PTS) AS teamScore
                        FROM
                            seasonSchedule sg
                        JOIN
                            playerGameStats pgs ON sg.gameId = pgs.gameId
                        JOIN
                            players p ON p.playerId = pgs.playerId
                        WHERE
                            sg.gameCompleted = 1
                        GROUP BY
                            sg.seasonId, sg.gameId, teamId
                    ),
                    Wins AS (
                        SELECT
                            tgs.seasonId,
                            tgs.teamId,
                            COUNT(*) AS wins
                        FROM
                            TeamGameScores tgs
                        JOIN
                            TeamGameScores opp ON tgs.gameId = opp.gameId
                                AND tgs.teamId != opp.teamId
                        WHERE
                            tgs.teamScore > opp.teamScore
                        GROUP BY
                            tgs.seasonId, tgs.teamId
                    ),
                    Losses AS (
                        SELECT
                            tgs.seasonId,
                            tgs.teamId,
                            COUNT(*) AS losses
                        FROM
                            TeamGameScores tgs
                        JOIN
                            TeamGameScores opp ON tgs.gameId = opp.gameId
                                AND tgs.teamId != opp.teamId
                        WHERE
                            tgs.teamScore < opp.teamScore
                        GROUP BY
                            tgs.seasonId, tgs.teamId
                    ),
                    CombinedResults AS (
                        SELECT 
                            tgs.seasonId,
                            tgs.teamId,
                            COALESCE(w.wins, 0) AS wins,
                            COALESCE(l.losses, 0) AS losses
                        FROM
                            (SELECT DISTINCT seasonId, teamId FROM TeamGameScores) tgs
                        LEFT JOIN Wins w ON tgs.seasonId = w.seasonId AND tgs.teamId = w.teamId
                        LEFT JOIN Losses l ON tgs.seasonId = l.seasonId AND tgs.teamId = l.teamId
                    )
                    SELECT 
                        cr.seasonId,
                        cr.teamId,
                        t.teamName,
                        cr.wins,
                        cr.losses
                    FROM 
                        CombinedResults cr
                    JOIN
                        teams t ON cr.teamId = t.teamId
                    WHERE
                        cr.seasonId = {CurrentSeason} -- seasonId
	                    AND cr.teamId = {GetIdFromTeamName(teamName)}; -- teamId

                    ";
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

        public string GetWinPercentageFromRecord(string teamRecord)
        {
            string[] splitRecord = teamRecord.Split('-');
            double wins = int.Parse(splitRecord[0]);
            double losses = int.Parse(splitRecord[1]);
            // we check if wins are zero, just return a 0 win pct
            if (wins == 0) return "0.0";
            // we check if losses are zero, but the team has one at least a game
            // to avoid a division by zero error
            else if (losses == 0 && wins > 0) return "100.0";
            else return $"{wins / (wins + losses)}";
        }
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
        public string GetUserConference()
        {
            int conferenceId = 0;
            string conference = "";
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
                            conferenceId = reader.GetInt32(0);
                            if (conferenceId == 1) conference = "East";
                            else conference = "West";
                        }
                    }
                }
            }
            return conference;
        }
        public int GetLeaguePosition()
        {
            string conference = GetUserConference();
            int conferenceId = 0;
            if (conference.Contains("E")) conferenceId = 1;
            else conferenceId = 2;
            List<string> teamNamesInUserConference = new List<string>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getConferenceQuery = $@"SELECT teamName from teams WHERE conferenceId = {conferenceId};";
                using (var command = new SQLiteCommand(getConferenceQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // returns the conference of the user's team
                            teamNamesInUserConference.Add(reader.GetString(0));
                        }
                    }
                }
            }
            // now we need to order the teams based on win percentage
            {
                for (int i = 0; i < teamNamesInUserConference.Count; i++)
                {
                    teamNamesInUserConference[i] += ":" + GetWinPercentageFromRecord(GetTeamRecord(teamNamesInUserConference[i])).ToString();
                }
                
                teamNamesInUserConference = teamNamesInUserConference.OrderByDescending(x => x.Split(":")[1]).ToList();
                // here, we make sure the remove the record from the string of names
                for (int i = 0; i < teamNamesInUserConference.Count; i++) teamNamesInUserConference[i] = teamNamesInUserConference[i].Split(":")[0];
            }
            return teamNamesInUserConference.IndexOf(UserTeamName) + 1;
        }
        public List<string> GetConferenceTeams(string conference)
        {
            int conferenceId = 0;
            if (conference == "East") conferenceId = 1;
            else conferenceId = 2;
            List<string> conferenceTeams = new List<string>();
            string currentTeamName = "";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getConferenceTeamsQuery = $@"
                    SELECT teamName 
                    FROM teams 
                    WHERE conferenceId = {conferenceId}
                    ;";
                using (var command = new SQLiteCommand(getConferenceTeamsQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // returns the conference of the user's team
                            currentTeamName = reader.GetString(0);
                            conferenceTeams.Add(currentTeamName);
                        }
                    }
                }
            }
            // now we need to order the teams based on win percentage
            for (int i = 0; i < conferenceTeams.Count; i++)
            {
                conferenceTeams[i] += ":" + GetWinPercentageFromRecord(GetTeamRecord(conferenceTeams[i])).ToString();
            }
            conferenceTeams = conferenceTeams.OrderByDescending(x => x.Split(":")[1]).ToList();
            // here, we make sure the remove the record from the string of names
            for (int i = 0; i < conferenceTeams.Count; i++) conferenceTeams[i] = conferenceTeams[i].Split(":")[0];
            return conferenceTeams;
        }
        public List<string> GetTeamUpcomingGames()
        {
            List<string> teamUpcomingGames = new List<string>();
            for (int i = CurrentDay; i < CurrentSchedule.Count; i++)
            {
                List<string> currentScheduleDay = CurrentSchedule[i];
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
                    else if (GetTeamNameFromId(gameSplit[0]) == UserTeamName)
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

        public void CreateLeague()
        {
            // we initialise the database and teams of the league
            InitialiseDatabase(LeagueFileName);
            GenerateTeams(LeagueFileName);
            // now we create a league schedule
            this.CurrentSchedule = GenerateSchedule();
            InsertLeagueData();
            InsertLeagueSchedule(CurrentSchedule);
        }

        public void LoadLeague(int saveState)
        {
            // we get the schedule from the database
            CurrentSchedule = LoadLeagueSchedule();
            LoadLeagueData();
        }

        public void InsertLeagueSchedule(List<List<string>> schedule)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertGameIntoScheduleQuery = "";
                int gameId = 0;
                for (int i = 0; i < schedule.Count; i++)
                {
                    List<string> scheduleDay = schedule[i];

                    for (int j = 0; j < scheduleDay.Count; j++)
                    {
                        gameId++;
                        string scheduleGame = scheduleDay[j];
                        string[] teamsInScheduleGame = scheduleGame.Split(',');
                        insertGameIntoScheduleQuery = $@"
                            INSERT into seasonSchedule(seasonId,dayId,gameId,homeTeamId,awayTeamId,gameCompleted)
                                VALUES({CurrentSeason},{i + 1},
                                {gameId},
                                {int.Parse(teamsInScheduleGame[0])},
                                {int.Parse(teamsInScheduleGame[1])},
                                FALSE)
                            ;";

                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = insertGameIntoScheduleQuery;
                            command.ExecuteNonQuery();
                        }
                    }

                }
            }
        }

        public static string CheckTeamMatchesSaveState(int saveState, string currentUser)
        {
            string connectionString = $"Data Source={GetStaticLeagueFileName(saveState, currentUser)};Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string getTeamNameFromSaveState = "SELECT userTeamName FROM league";
                using (var command = new SQLiteCommand(getTeamNameFromSaveState, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }
            return "";
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
        }

        public void UpdateLeagueDataIfNeeded(int currentDayOfGames)
        {
            bool allGamesComplete = true;
            foreach (string game in CurrentSchedule[currentDayOfGames - 1])
            {
                string[] teamsPlaying = game.Split(",");
                if (!CheckIfGameCompleted(currentDayOfGames, teamsPlaying[0], teamsPlaying[1]))
                {
                    allGamesComplete = false; break;
                }
            }
            if (allGamesComplete)
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
                    }
                }
            }
            return scheduleToPull;
        }

        // this function generates a schedule if the user creates a league
        public List<List<string>> GenerateSchedule()
        {
            // we get the team file names
            Random random = new Random();
            string fileTeamNamesPath = @$"C:\Users\{CurrentUser}\OneDrive - The Kings School Chester\A-Level\Computer Science\NEA Project\Project Files\LeagueSimulation\Names Files\basketball_team_names_list.txt";
            string[] teamNames = File.ReadAllLines(fileTeamNamesPath);
            int[] gamesPlayed = new int[teamNames.Length];
            List<string> generatedGames = new List<string>();

            bool validSchedule = false;
            List<List<string>> dailySchedule = new List<List<string>>();
            double homeOrAway = random.NextDouble();
            while (!validSchedule)
            {
                // all 1230 games are generated, now we split them into days
                bool validGames = false;
                while (!validGames)
                {
                    gamesPlayed = new int[teamNames.Length];
                    generatedGames = new List<string>();
                    for (int j = 0; j < teamNames.Length; j++)
                    {
                        // if we've reached the last team and it has played 82 games, we quit
                        if (gamesPlayed[j] >= 82 && j == teamNames.Length - 1) { break; }
                        // if team1 has played 82 games, we just skip that team as their season is finished
                        if (gamesPlayed[j] >= 82) continue;
                        if (j == 26) { };

                        string teamName1 = teamNames[j];

                        // in this condition, we make sure every team plays each other team thrice (58 games)
                        int teamName2Counter = 0;
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
                            while (!validTeamName)
                            {
                                int currentGameCounter = 0;
                                foreach (string game in generatedGames)
                                {
                                    string tempTeamName2 = teamNames[randomTeam];
                                    if (game == $"{teamName1},{tempTeamName2}" || game == $"{tempTeamName2},{teamName1}") currentGameCounter++;
                                    if (currentGameCounter > 3) break;
                                }
                                if (currentGameCounter > 4)
                                {
                                    randomTeam = random.Next(j + 1, teamNames.Length);
                                }
                                // here we make sure team2 is not team1 & team2has played less than 82 games
                                else if (randomTeam == j || gamesPlayed[randomTeam] >= 82)
                                {
                                    randomTeam = random.Next(j + 1, teamNames.Length);
                                }
                                else validTeamName = true;
                            }
                            string teamName2 = teamNames[randomTeam];
                            // once the teams have been picked, we increase their games played
                            gamesPlayed[j]++; gamesPlayed[randomTeam]++;
                            // now we add this game to the schedule list
                            homeOrAway = random.NextDouble();
                            if (homeOrAway < 0.5) generatedGames.Add($"{teamName1},{teamName2}");
                            else generatedGames.Add($"{teamName2},{teamName1}");

                        }
                    }
                    validGames = false;
                    if (generatedGames.Count == 1230)
                    {
                        foreach (int team in gamesPlayed)
                        {
                            if (team == 82) validGames = true;
                            else continue;
                        }
                    }
                }

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
                        string currentGame = generatedGames[random.Next(generatedGames.Count)];
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
                                    if (tryGameCounter > 100) { validGame = true; break; }
                                    // we set validGame to false, so we go back around and check if this next game is valid
                                    validGame = false;
                                    // we change the game to something different from the list
                                    currentGame = generatedGames[random.Next(generatedGames.Count)]; break;
                                }
                                //
                                else
                                {
                                    validGame = true; continue;
                                }
                            }
                        }
                        if (tryGameCounter <= 100)
                        {
                            generatedGames.Remove(currentGame);

                            currentDay.Add(currentGame);
                        }
                        if (tryGameCounter > 100) break;
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
                                if (i == j) { continue; }
                                if (lastDay[j].Split(",").Contains(teamsPlaying[0]) || lastDay[j].Split(",").Contains(teamsPlaying[1]))
                                {
                                    daysFilled = false;
                                    tempDay.Add(currentDay);
                                    lastDay.Remove(currentDay);
                                }
                            }
                        }
                        remainingDays.Add(lastDay);
                        bool tempDayDuplicates = false;
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
                            }
                        }
                    }
                    // we set this to true because the schedule is going to be set after the 'daysFilled' while loop
                    validSchedule = true;
                    foreach (List<string> remainingGameDay in remainingDays) dailySchedule.Add(remainingGameDay);
                }


                return ConvertScheduleToTeamIds(dailySchedule, teamNames);
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
                    teamsPlaying[0] = GetIdFromTeamName(teamsPlaying[0]).ToString();
                    teamsPlaying[1] = GetIdFromTeamName(teamsPlaying[1]).ToString();
                    teamNameSchedule[i][j] = $"{teamsPlaying[0]},{teamsPlaying[1]}";
                }
            }
            return teamNameSchedule;
        }


        public List<List<string>> GeneratePlayoffsSchedule()
        {
            // we get the top 8 seeds from both conferences, to generate the schedule
            List<string> eastPlayoffTeams = new List<string>();
            List<string> westPlayoffTeams = new List<string>();
            List<List<string>> games = new List<List<string>>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getEastPlayoffTeams = $"SELECT teamName FROM teams WHERE position < 9 AND conference = 'East' ORDER BY position";
                string getWestPlayoffTeams = $"SELECT teamName FROM teams WHERE position < 9 AND conference = 'West' ORDER BY position";
                using (var command = new SQLiteCommand(getEastPlayoffTeams, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) eastPlayoffTeams.Add(reader.GetString(0));
                }
                using (var command = new SQLiteCommand(getWestPlayoffTeams, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) westPlayoffTeams.Add(reader.GetString(0));
                }
            }

            // here we generate the first 4 games for the east and west coast for the 1st round of the playoffs
            for (int i = 0; i < 8; i++)
            {
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
                    games.Add(currentDay);
                }
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
                    games.Add(currentDay);
                }
            }
            return games;
        }

        public List<List<string>> GetTeamsInPlayoffs()
        {
            List<List<string>> playoffTeams = new List<List<string>>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getEasternConferenceTeamsInPlayoffsQuery = $@"
                    SELECT teamName
                    FROM teams
                    WHERE conference = 'East' 
                    AND position > 9
                    ORDER BY WINPCT DESC
                    ;";

                using (var command = new SQLiteCommand(getEasternConferenceTeamsInPlayoffsQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playoffTeams[0].Add(reader.GetString(0));
                        }
                    }
                }

                string getWesternConferenceTeamsInPlayoffsQuery = $@"
                    SELECT teamName
                    FROM teams
                    WHERE conference = 'East' 
                    AND position > 9
                    ORDER BY WINPCT DESC
                    ;";

                using (var command = new SQLiteCommand(getWesternConferenceTeamsInPlayoffsQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playoffTeams[1].Add(reader.GetString(0));
                        }
                    }
                }
            }
            return playoffTeams;
        }

        public void CreatePlayoffsScheduleRecords(List<List<string>> teamsInPlayoffs)
        {
            List<string> eastPlayoffTeams = teamsInPlayoffs[0];
            List<string> westPlayoffTeams = teamsInPlayoffs[1];
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertGameIntoPlayoffsQuery = "";
                // here we set the first games for the first round of the east playoffs
                int gameId = 0;
                for (int i = 0; i < eastPlayoffTeams.Count / 2; i++)
                {
                    gameId++;
                    string homeTeam = eastPlayoffTeams[i];
                    string awayTeam = eastPlayoffTeams[(eastPlayoffTeams.Count - 1) - i];
                    insertGameIntoPlayoffsQuery = $@"
                            INSERT into seasonSchedule(seasonId,dayId,gameId,round,homeTeam,awayTeam,gameCompleted)
                                VALUES({CurrentSeason},{1},
                                {gameId},
                                'First Round',
                                '{homeTeam}',
                                '{awayTeam}',
                                FALSE)
                            ;";
                }
            }
        }
        public Team ExtractTeamFromTeamName(string teamName)
        {
            // use this pseudo-team class to get the team names in the correct format
            Team teamForTeamName = new Team(teamName, 0);
            string uncompressedTeamName = teamForTeamName.teamName;

            ConnectionString = $"Data Source={LeagueFileName};Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string extractTeamQuery = $@"
                    SELECT *
                    FROM teams
                    WHERE teamName LIKE '%{uncompressedTeamName}%'
                ";

                using (SQLiteCommand command = new SQLiteCommand(extractTeamQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int teamId = reader.GetInt32(reader.GetOrdinal("teamId"));
                            Team team = new Team(teamName, teamId);
                            return team;
                        }
                    }
                }
            }
            return new Team("", 0);
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
                    WHERE homeTeamId = {homeTeamId} AND awayTeamId = {awayTeamId} AND dayId = '{currentDay}'
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
        public void SetGameToComplete(int currentDay, string homeTeam, string awayTeam)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string updateGameComplete = $"UPDATE seasonSchedule SET gameCompleted = {true} WHERE dayId = '{currentDay}' AND homeTeamId = {homeTeam} AND awayTeamId = {awayTeam};";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = updateGameComplete;
                    command.ExecuteNonQuery();
                }
            }
        }
        public static bool CheckIfLeagueExists(int saveState, string currentUser)
        {
            // this is the string of the file name
            string variableFileName = $"C:\\Users\\{currentUser}\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Databases\\League{saveState}.db";
            // if the file doesn't exist, we initialise the database
            if (!File.Exists(variableFileName)) return false;
            return true;
        }
        public string GetLeagueFileName(int saveState)
        {
            // this is the string of the file name
            return $"C:\\Users\\{CurrentUser}\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Databases\\League{saveState}.db";
        }

        public static string GetStaticLeagueFileName(int saveState, string currentUser)
        {
            // this is the string of the file name
            string variableFileName = $@"C:\Users\{currentUser}\OneDrive - The Kings School Chester\A-Level\Computer Science\NEA Project\Project Files\LeagueSimulation\Databases\League{saveState}.db";
            return variableFileName;
        }

        public void SimulateDay(List<string> games, int currentDaySimulated, bool updateStats)
        {
            // simulate games within the day
            foreach (string game in games)
            {
                string[] teamsPlaying = game.Split(',');
                if (!CheckIfGameCompleted(currentDaySimulated, teamsPlaying[0], teamsPlaying[1]))
                {
                    SimulateGame(game, currentDaySimulated, updateStats);
                }

            }
            // update windows form
            UpdateLeagueDataIfNeeded(currentDaySimulated);
        }

        public void SimulateGame(string game, int currentDayOfGame, bool updateStats)
        {
            // simulate game specified by user
            GamesPlayed++;
            string[] teamsPlaying = game.Split(",");
            if (!CheckIfGameCompleted(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]))
            {
                Team team1 = ExtractTeamFromTeamName(GetTeamNameFromId(teamsPlaying[0]));
                Team team2 = ExtractTeamFromTeamName(GetTeamNameFromId(teamsPlaying[1]));
                int gameId = GetGameId(game, currentDayOfGame);
                GameGenerator simulatedGame = new GameGenerator(team1, team2, ConnectionString, CurrentUser, CurrentSaveState, gameId, Playoffs, this);
                SetGameToComplete(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
            }

            // we check if all games are complete in the day, then increment currentDay if so
            bool allGamesComplete = true;
            // we check everyday up to the current game being simulated, where i is the current day
            // if all games are complete, then we increase the season currentDay

            for (int j = 0; j < CurrentSchedule[currentDayOfGame - 1].Count; j++)
            {
                string currentGame = CurrentSchedule[currentDayOfGame - 1][j];
                string[] currentTeamsPlaying = currentGame.Split(",");
                if (!CheckIfGameCompleted(currentDayOfGame, currentTeamsPlaying[0], currentTeamsPlaying[1]))
                {
                    allGamesComplete = false; break;
                }
            }

            if (allGamesComplete)
            {
                CurrentDay++;
                if (updateStats) { }; //SetPlayerAverageStats();
            }
        }

        public (List<string>, List<string>) WatchGame(string game, int currentDayOfGame)
        {
            // simulate game specified by user
            GamesPlayed++;
            string[] teamsPlaying = game.Split(",");
            if (!CheckIfGameCompleted(GamesPlayed, teamsPlaying[0], teamsPlaying[1]))
            {
                Team team1 = ExtractTeamFromTeamName(GetTeamNameFromId(teamsPlaying[0]));
                Team team2 = ExtractTeamFromTeamName(GetTeamNameFromId(teamsPlaying[1]));
                int gameId = GetGameId(game, currentDayOfGame);
                GameGenerator simulatedGame = new GameGenerator(team1, team2, ConnectionString, CurrentUser, CurrentSaveState, gameId, Playoffs, this);
                SetGameToComplete(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
                //SetPlayerAverageStats();
                return (simulatedGame.CommentatorPhrases, simulatedGame.ScoreAfterEachPhrase);
            }
            return (new List<string>(), new List<string>());
        }

        public Player GetPlayerFromId(int playerId)
        {
            Player playerTeam1 = new Player();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string team1PlayersQuery = $@"
                    SELECT *
                    FROM players
                    WHERE playerId = {playerId};
                ";
                using (var command = new SQLiteCommand(team1PlayersQuery, connection))
                {
                    // this reader, will extract a player, and put them into a Player
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // extract data from a player in database, and put into a player class
                            playerTeam1.PlayerId = reader.GetInt32(reader.GetOrdinal("playerId"));
                            playerTeam1.position = reader.GetString(reader.GetOrdinal("position"));
                            playerTeam1.PrimaryPlaystyle = reader.GetString(reader.GetOrdinal("primaryPlaystyle"));
                            playerTeam1.SecondaryPlaystyle = reader.GetString(reader.GetOrdinal("secondaryPlaystyle"));
                            playerTeam1.RosterSpot = reader.GetInt32(reader.GetOrdinal("rosterSpot"));
                            playerTeam1.Height = reader.GetInt32(reader.GetOrdinal("height")); // height in inches
                            playerTeam1.Weight = reader.GetInt32(reader.GetOrdinal("weight")); // weight in lbs
                            playerTeam1.playerForename = reader.GetString(reader.GetOrdinal("playerForename"));
                            playerTeam1.playerSurname = reader.GetString(reader.GetOrdinal("playerSurname"));
                            playerTeam1.TeamId = reader.GetInt32(reader.GetOrdinal("teamId"));
                            playerTeam1.teamName = reader.GetString(reader.GetOrdinal("teamName"));
                            playerTeam1.CloseShot = reader.GetInt32(reader.GetOrdinal("closeShot"));
                            playerTeam1.Layup = reader.GetInt32(reader.GetOrdinal("layup"));
                            playerTeam1.Dunk = reader.GetInt32(reader.GetOrdinal("dunk"));
                            playerTeam1.MidRange = reader.GetInt32(reader.GetOrdinal("midRange"));
                            playerTeam1.ThreePoint = reader.GetInt32(reader.GetOrdinal("threePoint"));
                            playerTeam1.FreeThrow = reader.GetInt32(reader.GetOrdinal("freeThrow"));
                            playerTeam1.Passing = reader.GetInt32(reader.GetOrdinal("passing"));
                            playerTeam1.BallHandle = reader.GetInt32(reader.GetOrdinal("ballHandle"));
                            playerTeam1.Defense = reader.GetInt32(reader.GetOrdinal("defense"));
                            playerTeam1.Steal = reader.GetInt32(reader.GetOrdinal("steal"));
                            playerTeam1.Block = reader.GetInt32(reader.GetOrdinal("block"));
                            playerTeam1.Rebound = reader.GetInt32(reader.GetOrdinal("rebound"));
                            playerTeam1.Speed = reader.GetInt32(reader.GetOrdinal("speed"));
                            playerTeam1.Strength = reader.GetInt32(reader.GetOrdinal("strength"));
                            playerTeam1.Stamina = reader.GetInt32(reader.GetOrdinal("stamina"));
                            playerTeam1.Overall = reader.GetInt32(reader.GetOrdinal("overall"));

                        }
                    }
                }
            }
            return playerTeam1;
        }

        public void SetPlayerAverageStats()
        {
            int playersCount = 450;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                // set player's average stats
                for (int i = 1; i <= playersCount; i++)
                {
                    Player player = GetPlayerFromId(i);
                    int playerId = player.PlayerId;
                    string getAverageStatsQuery = $@"
                    SELECT AVG(gameValue), AVG(MP), AVG(PTS), AVG(REB), 
                    AVG(AST), AVG(STL), AVG(BLK), AVG(TOV), AVG(PF)
                    FROM playerGameStats
                    WHERE playerId = {playerId}
                    AND MP > 0
                    ;";
                    double avgPTS = 0;
                    double avgMP = 0;
                    double avgGameValue = 0;
                    double avgREB = 0;
                    double avgAST = 0;
                    double avgSTL = 0;
                    double avgBLK = 0;
                    double avgTOV = 0;
                    double avgPF = 0;
                    double avgFG = GameGenerator.CalculatePlayerFieldGoal(playerId, false, ConnectionString);
                    double avgTFG = GameGenerator.CalculatePlayerFieldGoal(playerId, true, ConnectionString);
                    avgFG *= 100;
                    avgTFG *= 100;
                    avgFG = Math.Round(avgFG, 1);
                    avgTFG = Math.Round(avgTFG, 1);

                    bool validPlayer = true;

                    // here we get the average stats from the playerGameStats table
                    using (var command = new SQLiteCommand(getAverageStatsQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(0)) { validPlayer = false; break; }
                                avgPTS = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(PTS)")), 1);
                                avgMP = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(MP)")), 1);
                                avgGameValue = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(gameValue)")), 1);
                                avgREB = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(REB)")), 1);
                                avgAST = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(AST)")), 1);
                                avgSTL = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(STL)")), 1);
                                avgBLK = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(BLK)")), 1);
                                avgTOV = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(TOV)")), 1);
                                avgPF = Math.Round(reader.GetDouble(reader.GetOrdinal("AVG(PF)")), 1);
                            }
                        }
                    }

                    // here we set the average stats of the player in the playersSeasonStats table
                    string dropCurrentStatsQuery = $@"DELETE FROM playersSeasonStats WHERE seasonId = {this.CurrentSeason} AND playerId = {player.PlayerId} AND teamId = {player.TeamId}";
                    string setAverageStatsQuery = $@"
                        INSERT INTO playersSeasonStats(seasonId,playerId,teamId,teamName,playerForename,playerSurname,position,gameValue,
                        MP,FGPCT,TFGPCT,PTS,REB,AST,STL,BLK,TOV,PF)
                        VALUES(
                        {this.CurrentSeason},
                        {player.PlayerId},
                        {player.TeamId},
                        '{player.teamName}',
                        '{player.playerForename}',
                        '{player.playerSurname}',
                        '{player.position}',
                        {avgGameValue},
                        {avgMP},
                        {avgFG},
                        {avgTFG},
                        {avgPTS},
                        {avgREB},
                        {avgAST},
                        {avgSTL},
                        {avgBLK},
                        {avgTOV},
                        {avgPF}
                        );";
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = dropCurrentStatsQuery;
                        if (validPlayer) command.ExecuteNonQuery();
                        command.CommandText = setAverageStatsQuery;
                        if (validPlayer) command.ExecuteNonQuery();
                    }
                }

            }
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
                string getCompletedGamesQuery = "SELECT COUNT(*) FROM seasonSchedule WHERE gameCompleted = 1;";
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

        public League(string currentUser, int saveState, bool createLeague, string userTeamName)
        {
            this.CurrentUser = currentUser;
            this.CurrentDay = 1;
            this.CurrentSeason = 1;
            LeagueFileName = GetLeagueFileName(saveState);
            ConnectionString = $"Data Source={LeagueFileName};Version=3;";
            CurrentSaveState = saveState;
            UserTeamName = userTeamName;
            if (createLeague) CreateLeague();
            else LoadLeague(saveState);
            this.CurrentDay = GetLeagueCurrentDay();

            this.GamesPlayed = GetRegSeasonGamesPlayed();


        }


    }
}
