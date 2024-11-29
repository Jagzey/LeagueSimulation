using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSimulation
{
    public class Team
    {
        public string teamName = "";
        public string city = "";
        private int position = 0;
        private string conference = "";
        private int w = 0;
        private int l = 0;
        private double winPct = 0;
        private int teamId = 0;
        //public int revenue = 200000000;
        public int Position { get; set; }
        public string? Conference { get; set; } 
        public int W { get; set; }
        public int L { get; set; }
        public double WinPct { get; set; }
        public int TeamId { get { return teamId; } }

        //public int Revenue { get; set; }

        public static string GetCityFromTeamName(string unorganisedName)
        {
            return GetFullNameAndCityName(unorganisedName).Item1;
        }

        public string GetConference()
        {
            string conference = "";
            string teamNamesFilePath = $@"C:\Users\FiercePC\OneDrive - The Kings School Chester\A-Level\Computer Science\NEA Project\Project Files\LeagueSimulation\Names Files\basketball_team_names_list.txt";
            string[] teamNames = File.ReadAllLines(teamNamesFilePath);
            for (int i = 0; i < teamNames.Length; i++)
            {
                if (teamNames[i].Contains(teamName))
                {
                    if (i < 15) return "East";
                    else return "West";
                }
            }
            return conference;
        }

        public static (string, string) GetFullNameAndCityName(string unorganisedName)
        {
            // work out number of capital letters in team name
            int numCapitals = 0;
            foreach (char c in unorganisedName) if (Char.IsUpper(c)) numCapitals++;
            int currentCapital = 0;
            string firstPart = "";
            string secondPart = "";
            for (int i = 0; i < unorganisedName.Length; i++)
            {
                char c = unorganisedName[i];
                if (Char.IsUpper(c)) currentCapital++;
                if (i == 0)
                {
                    firstPart += c;
                }
                else if (c == ' ') continue;
                else if (Char.IsUpper(c) && (currentCapital < numCapitals))
                {
                    firstPart += ' ';
                    firstPart += c;
                }
                else if (Char.IsUpper(c) && (currentCapital == numCapitals))
                {
                    secondPart += c;
                }
                else if (currentCapital == numCapitals)
                {
                    secondPart += c;
                }
                else
                {
                    firstPart += c;
                }
            }
            // find the second part of team name


            return (firstPart, secondPart);
        }

        public Team(string unorganisedName, int teamId, int w, int l)
        {
            if (unorganisedName != null)
            {
                (string, string) twoPartName = GetFullNameAndCityName(unorganisedName);
                city = twoPartName.Item1;
                teamName = $"{twoPartName.Item1} {twoPartName.Item2}";
                this.teamId = teamId;
                position = teamId;
                Conference = GetConference();
                this.w = w;
                this.l = l;
            }
        }

    }
}
