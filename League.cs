using System.Data.Entity.Core.Mapping;
using System.Data.SQLite;
using System.Diagnostics;

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
                        conference TEXT NOT NULL,
                        position INTEGER NOT NULL,
                        WINS INTEGER,
                        LOSSES INTEGER,
                        WINPCT DECIMAL(3,1)
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
                    FOREIGN KEY (gameId) REFERENCES playersGamesStats(gameId),
                    FOREIGN KEY (teamId) REFERENCES teams(teamId),
                    PRIMARY KEY (gameId, teamId)
                    );";

                // create players table
                string createPlayersTableQuery = @"
                        CREATE TABLE IF NOT EXISTS players(
                        playerId INTEGER PRIMARY KEY AUTOINCREMENT,
                        teamId INTEGER NOT NULL,
                        teamName TEXT NOT NULL,
                        rosterSpot INTEGER NOT NULL,
                        playerForename TEXT NOT NULL,
                        playerSurname TEXT NOT NULL,
                        age INTEGER NOT NULL,
                        overall INTEGER NOT NULL,
                        potential INTEGER NOT NULL,
                        position TEXT NOT NULL,
                        primaryPlaystyle TEXT NOT NULL,
                        secondaryPlaystyle TEXT NOT NULL,
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

                string createGamesTableQuery = @"
                        CREATE TABLE playersGamesStats(
                        gameId INTEGER NOT NULL,
                        playerId INTEGER NOT NULL,
                        teamId INTEGER NOT NULL,
                        teamName TEXT NOT NULL,
                        playerForename TEXT NOT NULL,
                        playerSurname TEXT NOT NULL,
                        position TEXT NOT NULL,
                        gameValue DECIMAL (3,1) NOT NULL,
                        MP INTEGER NOT NULL,
                        FG TEXT NOT NULL,
                        TFG TEXT NOT NULL,
                        PTS INTEGER NOT NULL,
                        REB INTEGER NOT NULL,
                        AST INTEGER NOT NULL,
                        STL INTEGER NOT NULL,
                        BLK INTEGER NOT NULL,
                        TOV INTEGER NOT NULL,
                        PF INTEGER NOT NULL,
                        FOREIGN KEY (playerId) REFERENCES players(playerId),
                        PRIMARY KEY (gameId, playerId)
                        );";

                string createScheduleTableQuery = @"
                    CREATE TABLE seasonSchedule(
                    seasonId INTEGER NOT NULL,
                    dayId INTEGER NOT NULL,
                    gameId INTEGER NOT NULL,
                    homeTeam TEXT NOT NULL,
                    awayTeam TEXT NOT NULL,
                    gameCompleted BOOLEAN NOT NULL,
                    PRIMARY KEY (seasonId, dayId, gameId)
                    );";

                string createPlayoffsScheduleTableQuery = @"
                    CREATE TABLE playoffsSchedule(
                    seasonId INTEGER NOT NULL,
                    dayId INTEGER NOT NULL,
                    gameId INTEGER NOT NULL,
                    round TEXT NOT NULL,
                    homeTeam TEXT,
                    awayTeam TEXT,
                    gameCompleted BOOLEAN NOT NULL,
                    PRIMARY KEY (seasonId, dayId, gameId)
                    );";

                string leagueTableQuery = @"
                    CREATE TABLE league(
                    leagueId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    currentDay INTEGER NOT NULL,
                    currentSeason INTEGER NOT NULL,
                    userTeamName TEXT NOT NULL
                    );";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createTeamsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createTeamGameStatsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayersTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createGamesTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createScheduleTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlayoffsScheduleTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = leagueTableQuery;
                    command.ExecuteNonQuery();
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
                            Team team = new Team(teamNames[i], i + 1, 0, 0);
                            string conference = "";
                            if (i < 15) conference = "East";
                            else
                            { 
                                conference = "West";
                                team.Position -= 15;
                            }
                            int teamOverall = random.Next(77, 83);
                            string addTeamQuery = $@"INSERT INTO teams(teamName,city,conference,wins,losses,position)
	                        VALUES(
                            '{team.teamName}',
	                        '{team.city}',
                            '{conference}',
                            {team.W},
                            {team.L},
	                        {i+1 + team.Position}
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

                            // here we try to order the roster by overall,
                            // where the starters have one of each position
                            {
                                List<Player> team1PGs = new List<Player>();
                                List<Player> team1SGs = new List<Player>();
                                List<Player> team1SFs = new List<Player>();
                                List<Player> team1PFs = new List<Player>();
                                List<Player> team1Cs = new List<Player>();
                                List<Player> team1Starters = new List<Player>();
                                List<Player> team1NonStarters = new List<Player>();
                                foreach (Player player1 in team1Players)
                                {
                                    if (player1.position == "PG") team1PGs.Add(player1);
                                    else if (player1.position == "SG") team1SGs.Add(player1);
                                    else if (player1.position == "SF") team1SFs.Add(player1);
                                    else if (player1.position == "PF") team1PFs.Add(player1);
                                    else if (player1.position == "C") team1Cs.Add(player1);
                                }
                                team1PGs = team1PGs.OrderByDescending(x => x.Overall).ToList();
                                team1SGs = team1SGs.OrderByDescending(x => x.Overall).ToList();
                                team1SFs = team1SFs.OrderByDescending(x => x.Overall).ToList();
                                team1PFs = team1PFs.OrderByDescending(x => x.Overall).ToList();
                                team1Cs = team1Cs.OrderByDescending(x => x.Overall).ToList();

                                for (int j = 0; j < 3; j++)
                                {
                                    if (j == 0)
                                    {
                                        team1Starters.Add(team1PGs[j]);
                                        team1Starters.Add(team1SGs[j]);
                                        team1Starters.Add(team1SFs[j]);
                                        team1Starters.Add(team1PFs[j]);
                                        team1Starters.Add(team1Cs[j]);
                                    }
                                    else
                                    {
                                        team1NonStarters.Add(team1PGs[j]);
                                        team1NonStarters.Add(team1SGs[j]);
                                        team1NonStarters.Add(team1SFs[j]);
                                        team1NonStarters.Add(team1PFs[j]);
                                        team1NonStarters.Add(team1Cs[j]);
                                    }
                                }
                                team1Starters = team1Starters.OrderByDescending(x => x.Overall).ToList();
                                team1NonStarters = team1NonStarters.OrderByDescending(x => x.Overall).ToList();

                                // now we add the starters and non-starters back to the original list
                                team1Players = new List<Player>();
                                foreach (Player player in team1Starters)
                                {
                                    team1Players.Add(player);
                                }
                                foreach (Player player in team1NonStarters)
                                {
                                    team1Players.Add(player);
                                }
                            }

                            // now we add the players in this team, to the database
                            for (int j = 0; j < team1Players.Count; j++)
                            {
                                Player player = team1Players[j];
                                string addPlayerQuery = $@"INSERT INTO players(teamId,teamName,rosterSpot,playerForename,playerSurname,position,age,overall,potential,primaryPlaystyle,
                                secondaryPlaystyle,height,weight,closeShot,layup,dunk,midRange,threePoint,freeThrow,passing,ballHandle,defense,
                                steal,block,rebound,speed,strength,stamina,overall)
                                VALUES(
                                {team.TeamId},
                                '{team.teamName}',
                                {j + 1},
                                '{player.playerForename}',
                                '{player.playerSurname}',
                                '{player.position}',
                                {player.Age},
                                {player.Overall},
                                {player.Potential},
                                '{player.PrimaryPlaystyle}',
                                '{player.SecondaryPlaystyle}',
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
        public string GetUserTeamRecord()
        {
            string record = "";
            int wins = 0;
            int losses = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getTeamRecordQuery = $@"SELECT WINS, LOSSES FROM teams WHERE teamName = '{UserTeamName}';";
                using (var command = new SQLiteCommand(getTeamRecordQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // returns the conference of the user's team
                            wins = reader.GetInt32(reader.GetOrdinal("WINS"));
                            losses = reader.GetInt32(reader.GetOrdinal("LOSSES"));
                        }
                    }
                }
            }
            record = $"{wins}-{losses}";
            return record;
        }

        public string GetUserTeamLeaderInStatistic(string stat)
        {
            string fullTeamLeaderString = "";
            List<int> playersOnTeam = new List<int>();
            double maxStat = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getPlayersQuery = $"SELECT playerId FROM playersGamesStats WHERE teamName = '{UserTeamName}';";
                string getTeamLeaderQuery = $"SELECT playerForename, playerSurname, AVG({stat}) FROM playersGamesStats WHERE playerId = ";
                using (var command = new SQLiteCommand(getPlayersQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!playersOnTeam.Contains(reader.GetInt32(0))) playersOnTeam.Add(reader.GetInt32(0));
                        }
                    }
                }
                foreach (int player in playersOnTeam)
                {
                    getTeamLeaderQuery = $"SELECT playerForename, playerSurname, AVG({stat}) FROM playersGamesStats WHERE playerId = ";
                    getTeamLeaderQuery += player.ToString() + ";";
                    using (var command = new SQLiteCommand(getTeamLeaderQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                double currentStat = (double)reader.GetDecimal(2);
                                if (currentStat > maxStat)
                                {
                                    string playerForename = reader.GetString(reader.GetOrdinal("playerForename"));
                                    string playerSurname = reader.GetString(reader.GetOrdinal("playerSurname"));
                                    maxStat = (double)reader.GetDecimal(2);
                                    fullTeamLeaderString = $"{playerForename} {playerSurname}: {Math.Round(maxStat, 1)} {stat.ToLower()}";
                                }
                            }
                        }
                    }
                }
                return fullTeamLeaderString;
            }

        }
        public string GetTeamRecord(string teamName)
        {
            int wins = 0;
            int losses = 0;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getRecord = $"SELECT WINS, LOSSES FROM teams WHERE teamName = '{teamName}'";
                using (var command = new SQLiteCommand(getRecord, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wins = reader.GetInt32(reader.GetOrdinal("WINS"));
                            losses = reader.GetInt32(reader.GetOrdinal("LOSSES"));
                        }
                    }
                }
            }
            return $"{wins}-{losses}";
        }
        public string GetUserConference()
        {
            string conference = "";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getConferenceQuery = $@"SELECT conference from teams WHERE teamName = '{UserTeamName}';";
                using (var command = new SQLiteCommand(getConferenceQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // returns the conference of the user's team
                            conference = reader.GetString(0);
                        }
                    }
                }
            }
            return conference;
        }
        public int GetLeaguePosition()
        {
            int position = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getConferenceQuery = $@"SELECT position from teams WHERE teamName = '{UserTeamName}';";
                using (var command = new SQLiteCommand(getConferenceQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // returns the conference of the user's team
                            position = reader.GetInt32(0);
                        }
                    }
                }
            }
            return position;
        }
        public List<string> GetConferenceTeams(string userConference)
        {
            List<string> conferenceTeams = new List<string>();
            string currentTeamName = "";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string getConferenceTeamsQuery = $@"
                    SELECT teamName 
                    FROM teams 
                    WHERE conference = '{userConference}'
                    ORDER BY position ASC;";
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
                    if (gameSplit[0] == UserTeamName)
                    {
                        teamUpcomingGames.Add($"{Team.GetCityFromTeamName(gameSplit[0])} vs {Team.GetCityFromTeamName(gameSplit[1])}");
                    }
                    else if (gameSplit[1] == UserTeamName)
                    {
                        teamUpcomingGames.Add($"{Team.GetCityFromTeamName(gameSplit[1])} @ {Team.GetCityFromTeamName(gameSplit[0])}");
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
                            INSERT into seasonSchedule(seasonId,dayId,gameId,homeTeam,awayTeam,gameCompleted)
                                VALUES({CurrentSeason},{i+1},
                                {gameId},
                                '{teamsInScheduleGame[0]}',
                                '{teamsInScheduleGame[1]}',
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
            foreach (string game in CurrentSchedule[currentDayOfGames-1])
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
                            string team1 = reader.GetString(reader.GetOrdinal("homeTeam"));
                            string team2 = reader.GetString(reader.GetOrdinal("awayTeam"));
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
                                if (game.Contains(teamsPlaying[0]) || game.Contains(teamsPlaying[1]))
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
                                if (tempDay[j].Contains(teamsPlaying[0]) || tempDay[j].Contains(teamsPlaying[1]))
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

                return dailySchedule;
            }
            return new List<List<string>>();
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

                using (var command = new SQLiteCommand(getEasternConferenceTeamsInPlayoffsQuery,connection))
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
            Team teamForTeamName = new Team(teamName, 0, 0, 0);
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
                            int w = reader.GetInt32(reader.GetOrdinal("WINS"));
                            int l = reader.GetInt32(reader.GetOrdinal("LOSSES"));
                            Team team = new Team(teamName, teamId, w, l);
                            return team;
                        }
                    }
                }
            }
            return new Team("", 0, 0, 0);
        }

        public bool CheckIfGameCompleted(int currentDay, string homeTeam, string awayTeam)
        {
            bool gameCompleted = false;
            ConnectionString = $"Data Source={LeagueFileName};Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string checkGameCompletedQuery = $@"
                    SELECT gameCompleted
                    FROM seasonSchedule
                    WHERE homeTeam = '{homeTeam}' AND awayTeam = '{awayTeam}' AND dayId = '{currentDay}'
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
            return gameCompleted;
        }
        public void SetGameToComplete(int currentDay, string homeTeam, string awayTeam)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string updateGameComplete = $"UPDATE seasonSchedule SET gameCompleted = {true} WHERE dayId = '{currentDay}' AND homeTeam = '{homeTeam}' AND awayTeam = '{awayTeam}';";
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = updateGameComplete;
                    command.ExecuteNonQuery();
                }
            }
        }
        public static bool CheckIfTeamNameExists(string teamName, string currentUser)
        {
            string teamFilePath = $"C:\\Users\\{currentUser}\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Names Files\\basketball_team_names_list.txt";
            string[] teamNames = File.ReadAllLines(teamFilePath);
            if (teamNames.Contains(teamName)) return true;
            return false;
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

        public void SimulateDay(List<string> games, int currentDaySimulated)
        {
            // simulate games within the day
            foreach (string game in games)
            {
                string[] teamsPlaying = game.Split(',');
                if (!CheckIfGameCompleted(currentDaySimulated, teamsPlaying[0], teamsPlaying[1]))
                {
                    SimulateGame(game, currentDaySimulated);
                }
                
            }
            // update windows form
            UpdateLeagueDataIfNeeded(currentDaySimulated);
        }

        public void SimulateGame(string game, int currentDayOfGame)
        {
            // simulate game specified by user
            GamesPlayed++;
            string[] teamsPlaying = game.Split(",");
            if (!CheckIfGameCompleted(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]))
            {
                Team team1 = ExtractTeamFromTeamName(teamsPlaying[0]);
                Team team2 = ExtractTeamFromTeamName(teamsPlaying[1]);
                int gameId = GetGameId(game, currentDayOfGame);
                GameGenerator simulatedGame = new GameGenerator(team1, team2, ConnectionString, CurrentUser, CurrentSaveState, gameId, Playoffs);
                SetGameToComplete(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
            }

            // we check if all games are complete in the day, then increment currentDay if so
            bool allGamesComplete = true;
            // we check everyday up to the current game being simulated, where i is the current day
            // if all games are complete, then we increase the season currentDay
            for (int i = 0; i < currentDayOfGame; i++)
            {
                for (int j = 0; j < CurrentSchedule[currentDayOfGame].Count; j++)
                {
                    string currentGame = CurrentSchedule[i][j];
                    string[] currentTeamsPlaying = currentGame.Split(",");
                    if (!CheckIfGameCompleted(i+1, currentTeamsPlaying[0], currentTeamsPlaying[1]))
                    {
                        allGamesComplete = false; break;
                    }
                }
                if (!allGamesComplete) break;
            }
            if (allGamesComplete) CurrentDay++;
        }

        public (List<string>, List<string>) WatchGame(string game, int currentDayOfGame)
        {
            // simulate game specified by user
            GamesPlayed++;
            string[] teamsPlaying = game.Split(",");
            if (!CheckIfGameCompleted(GamesPlayed, teamsPlaying[0], teamsPlaying[1]))
            {
                Team team1 = ExtractTeamFromTeamName(teamsPlaying[0]);
                Team team2 = ExtractTeamFromTeamName(teamsPlaying[1]);
                int gameId = GetGameId(game, currentDayOfGame);
                GameGenerator simulatedGame = new GameGenerator(team1, team2, ConnectionString, CurrentUser, CurrentSaveState, gameId, Playoffs);
                SetGameToComplete(currentDayOfGame, teamsPlaying[0], teamsPlaying[1]);
                return (simulatedGame.CommentatorPhrases, simulatedGame.ScoreAfterEachPhrase);
            }
            return (new List<string>(), new List<string>());
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
                    AND homeTeam = '{teamsPlaying[0]}'
                    AND awayTeam = '{teamsPlaying[1]}'
                    ;";
                using (var command = new SQLiteCommand(getGameIdQuery, connection))
                {
                    using (var reader =  command.ExecuteReader())
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
