using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSimulation.Models
{
    public class Team
    {
        public string teamName = "";
        public string city = "";
        private int position = 0;
        private string conference = "";
        private int teamId = 0;
        private string currentUser = "";
        //public int revenue = 200000000;
        public int Position { get; set; }
        public string? Conference { get; set; }
        public int TeamId { get { return teamId; } }
        public string CurrentUser { get; set; }

        //public int Revenue { get; set; }

        public static string GetCityFromTeamName(string unorganisedName) => GetFullNameAndCityName(unorganisedName).Item1;

        public static (string, string) GetFullNameAndCityName(string teamName)
        {
            // work out number of capital letters in team name
            int numCapitals = 0;
            foreach (char c in teamName) if (char.IsUpper(c)) numCapitals++;
            int currentCapital = 0;
            string firstPart = "";
            string secondPart = "";
            for (int i = 0; i < teamName.Length; i++)
            {
                char c = teamName[i];
                if (char.IsUpper(c)) currentCapital++;
                if (i == 0)
                {
                    firstPart += c;
                }
                else if (c == ' ') continue;
                else if (char.IsUpper(c) && currentCapital < numCapitals)
                {
                    firstPart += ' ';
                    firstPart += c;
                }
                else if (char.IsUpper(c) && currentCapital == numCapitals)
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

        public Team(string teamName, int teamId, string currentUser)
        {
            if (teamName != null)
            {
                (string, string) twoPartName = GetFullNameAndCityName(teamName);
                city = twoPartName.Item1;
                this.teamName = teamName;
                this.teamId = teamId;
                if (teamId < 16) Conference = "East";
                else Conference = "West";
            }

            CurrentUser = currentUser;
        }

    }
}
