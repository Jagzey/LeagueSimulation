using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSimulation
{
    public class GameClock
    {
        private int seconds;
        private int minutes;
        public int Seconds { get; set; }
        public int Minutes { get; set; }

        public GameClock()
        {
            Seconds = 0;
            Minutes = 0;
        }

        public void AddSeconds(int seconds)
        {
            Seconds += seconds;
            if (Seconds > 60)
            {
                Seconds -= 60;
                Minutes++;
            }
        }
    }
}
