using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSimulation
{
    public class PlayerInGame
    {
        // holds stats for the current player
        public Player playerStats;

        // holds in-game stats
        private int fieldGoalMade = 0;
        private int fieldGoalAttempted = 0;
        private int threePointMade = 0;
        private int threePointAttempted = 0;
        private int freeThrowMade = 0;
        private int freeThrowAttempted = 0;
        private int points = 0;
        private int rebounds = 0;
        private int assists = 0;
        private int steals = 0;
        private int blocks = 0;
        private int turnovers = 0;
        private int personalFouls = 0;
        private double gameValue = 0;
        private int minutesToPlay = 0;
        private List<int> slotsPlaying = new List<int>();

        public int FieldGoalMade { get; set; }
        public int FieldGoalAttempted { get; set; }
        public int ThreePointMade { get; set; }
        public int ThreePointAttempted { get; set; }
        public int FreeThrowMade { get ; set; }
        public int FreeThrowAttempted { get; set; }
        public int Points { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }
        public int PersonalFouls { get; set; }
        public double GameValue { get; set; }
        public int MinutesToPlay { get; set; }
        public List<int> SlotsPlaying { get; set; }

        public PlayerInGame(Player player)
        {
            this.playerStats = player;
            this.SlotsPlaying = new List<int>();
        }
    }
}
