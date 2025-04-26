using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Text.Json;

namespace LeagueSimulation.Models
{
    public class Player
    {
        private int playerId;
        public string position;
        private string primaryPlaystyle = "";
        private string secondaryPlaystyle = "";
        private int height = 0; // height in inches
        private int weight = 0; // weight in lbs
        public string playerForename = "";
        public string playerSurname = "";
        private int rosterSpot = 0;


        // team information
        private int teamId = 0;
        public string teamName = "";

        // all attributes are between 25-99
        private int closeShot = 0;
        private int layup = 0;
        private int dunk = 0;
        private int midRange = 0;
        private int threePoint = 0;
        private int freeThrow = 0;
        private int passing = 0;
        private int ballHandle = 0;
        private int defense = 0;
        private int steal = 0;
        private int block = 0;
        private int rebound = 0;
        private int speed = 0;
        private int strength = 0;
        private int stamina = 0;
        private int overall = 0;
        private int age = 0;
        private int potential = 0;
        private double gameValue = 0;
        private double minutesPlayed = 0;

        // getters and setters for player's attribtues
        public int Age { get; set; }
        public string PrimaryPlaystyle { get; set; }
        public string SecondaryPlaystyle { get; set; }
        public int PlayerId { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int TeamId { get; set; }
        public int CloseShot { get; set; }
        public int Layup { get; set; }
        public int Dunk { get; set; }
        public int MidRange { get; set; }
        public int ThreePoint { get; set; }
        public int FreeThrow { get; set; }
        public int Passing { get; set; }
        public int BallHandle { get; set; }
        public int Defense { get; set; }
        public int Steal { get; set; }
        public int Block { get; set; }
        public int Rebound { get; set; }
        public int Speed { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Overall { get; set; }
        public int Potential { get; set; }
        public double MinutesPlayed { get; set; }
        public double GameValue { get; set; }



        public static double GenerateRandomNormalDistribution(double mean, double standardDeviation, double minimum = 0, double maximum = 0)
        {
            Random random = new Random();
            double u1 = 1.0 - random.NextDouble(); // Uniform random number from 0 to 1
            double u2 = 1.0 - random.NextDouble();
            double z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2); // Box-Muller transform
            double result = mean + standardDeviation * z0;
            if (minimum != 0 && maximum != 0)
            {
                if (result < minimum) return minimum;
                if (result > maximum) return maximum;
            }
            return result;
        }

        public void RandomHeight(string position)
        {
            double mean = 72;
            double stdev = 2;
            Dictionary<string, (double, double, double, double)> positionToHeightMulti = new Dictionary<string, (double, double, double, double)>()
            {
                {"PG", (3.5, -0.1, 69, 76)},
                {"SG", (5, -0.5, 74, 78)},
                {"SF", (7, 0.4, 76, 81)},
                {"PF", (9.2, 0.5, 78, 83)},
                {"C", (11, -0.2, 81, 87)}
            };
            mean += positionToHeightMulti[position].Item1;
            stdev += positionToHeightMulti[position].Item2;
            double min = positionToHeightMulti[position].Item3;
            double max = positionToHeightMulti[position].Item4;
            double doubleHeight = GenerateRandomNormalDistribution(mean, stdev, min, max);
            

            int height = (int)doubleHeight;
            string realHeight = $"{height / 12}'{height % 12}";
            Height = height;
        }

        public void RandomWeight(string position, int height)
        {
            double doubleWeight = 0;
            double mean = 0;
            double stdev = 0;
            double min = 0;
            double max = 0;
            double multiplier = 0;

            Dictionary<string, (double, double, double, double)> positionToWeightMulti = new Dictionary<string, (double, double, double, double)>()
            {
                {"PG", (191, 12, 150, 220)},
                {"SG", (195, 14.8, 170, 235)},
                {"SF", (230, 14, 190, 255)},
                {"PF", (252, 20, 220, 280)},
                {"C", (250, 26, 225, 310)}
            };
            Dictionary<string, (double, double)> heightToWeightMulti = new Dictionary<string, (double, double)>()
            {
                {"PG", (75, 200)},
                {"SG", (77, 210)},
                {"SF", (80, 180)},
                {"PF", (82, 200)},
                {"C", (85, 216)}
            };

            multiplier = (height - heightToWeightMulti[position].Item1) / heightToWeightMulti[position].Item1 * heightToWeightMulti[position].Item2;
            mean += positionToWeightMulti[position].Item1 + multiplier;
            stdev += positionToWeightMulti[position].Item2;
            min = positionToWeightMulti[position].Item3;
            max = positionToWeightMulti[position].Item4;
            doubleWeight = GenerateRandomNormalDistribution(mean, stdev, min, max);
            Weight = (int)doubleWeight;
        }

        public void GeneratePrimaryPlaystyle()
        {
            string primaryPlaystyle = "";
            Random random = new Random();
            List<string> playstyles = new List<string>() { "Offensive", "Defensive", "2-Way" };
            int value = random.Next(1, 100);
            if (value < 46) primaryPlaystyle = playstyles[0];
            else if (value < 69) primaryPlaystyle = playstyles[1];
            else primaryPlaystyle = playstyles[2];
            PrimaryPlaystyle = primaryPlaystyle;
        }

        public void GenerateSecondaryPlaystyle(int height, string primaryPlaystyle, string secondaryPlaystyle)
        {
            int value = 0;
            List<string> playstyles = new List<string>()
            {
                "Shooter",
                "Playmaker",
                "Inside-Scorer",
                "Finisher",
                "Rim Protector",
                "Lockdown",
                "Ripper"
            };
            Random random = new Random();
            if (position == "PG")
            {
                if (primaryPlaystyle == "Offensive")
                {
                    playstyles = new List<string> { "Shooter", "Playmaker", "Finisher" };
                    if (height - 75.5 < -2)
                    {
                        value = random.Next(0, 100);
                        if (value < 27) secondaryPlaystyle = playstyles[0];
                        else if (value < 93) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }
                    else if (height - 75.5 < 1)
                    {
                        value = random.Next(0, 100);
                        if (value < 29) secondaryPlaystyle = playstyles[0];
                        else if (value < 90) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }
                    else
                    {
                        value = random.Next(0, 100);
                        if (value < 26) secondaryPlaystyle = playstyles[0];
                        else if (value < 87) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }

                }

                else if (primaryPlaystyle == "Defensive")
                {
                    playstyles = new List<string> { "Lockdown", "Ripper" };
                    if (height - 75.5 < -2)
                    {
                        value = random.Next(0, 100);
                        if (value < 38) secondaryPlaystyle = playstyles[0];
                        else if (value <= 100) secondaryPlaystyle = playstyles[1];
                    }
                    else if (height - 75.5 < 1)
                    {
                        value = random.Next(0, 100);
                        if (value < 50) secondaryPlaystyle = playstyles[0];
                        else if (value <= 100) secondaryPlaystyle = playstyles[1];
                    }
                    else
                    {
                        value = random.Next(0, 100);
                        if (value < 60) secondaryPlaystyle = playstyles[0];
                        else if (value <= 100) secondaryPlaystyle = playstyles[1];
                    }
                }

                else if (primaryPlaystyle == "2-Way") secondaryPlaystyle = "2-Way Player";
            }
            if (position == "SG" || position == "SF")
            {
                if (primaryPlaystyle == "Offensive")
                {
                    playstyles = new List<string> { "Shooter", "Playmaker", "Inside-Scorer", "Finisher" };
                    if (height - 79 < -2)
                    {
                        value = random.Next(0, 100);
                        if (value < 45) secondaryPlaystyle = playstyles[0];
                        else if (value < 73) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[3];
                    }
                    else if (height - 79 < 1)
                    {
                        value = random.Next(0, 100);
                        if (value < 48) secondaryPlaystyle = playstyles[0];
                        else if (value < 71) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[3];
                    }
                    else
                    {
                        value = random.Next(0, 100);
                        if (value < 40) secondaryPlaystyle = playstyles[0];
                        else if (value < 69) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[3];
                    }

                }

                else if (primaryPlaystyle == "Defensive")
                {
                    playstyles = new List<string> { "Rim Protector", "Lockdown", "Ripper" };
                    if (height - 79 < -2)
                    {
                        value = random.Next(0, 100);
                        if (value < 51) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }
                    else if (height - 79 < 1)
                    {
                        value = random.Next(0, 100);
                        if (value < 60) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }
                    else
                    {
                        value = random.Next(0, 100);
                        if (value < 71) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }
                }

                else if (primaryPlaystyle == "2-Way")
                {
                    secondaryPlaystyle = "2-Way Player";
                }
            }
            if (position == "PF" || position == "C")
            {
                if (primaryPlaystyle == "Offensive")
                {
                    playstyles = new List<string> { "Shooter", "Playmaker", "Finisher" };
                    if (height - 83 < -2)
                    {
                        value = random.Next(0, 100);
                        if (value < 35) secondaryPlaystyle = playstyles[0];
                        else if (value < 48) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }
                    else if (height - 83 < 1)
                    {
                        value = random.Next(0, 100);
                        if (value < 31) secondaryPlaystyle = playstyles[0];
                        else if (value < 40) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }
                    else
                    {
                        value = random.Next(0, 100);
                        if (value < 28) secondaryPlaystyle = playstyles[0];
                        else if (value < 32) secondaryPlaystyle = playstyles[1];
                        else if (value <= 100) secondaryPlaystyle = playstyles[2];
                    }

                }

                if (primaryPlaystyle == "Defensive")
                {
                    playstyles = new List<string> { "Rim Protector", "Lockdown" };
                    if (height - 83 < -2)
                    {
                        value = random.Next(0, 100);
                        if (value < 42) secondaryPlaystyle = playstyles[0];
                        else if (value <= 100) secondaryPlaystyle = playstyles[1];
                    }
                    else if (height - 83 < 1)
                    {
                        value = random.Next(0, 100);
                        if (value < 54) secondaryPlaystyle = playstyles[0];
                        else if (value <= 100) secondaryPlaystyle = playstyles[1];

                    }
                    else
                    {
                        value = random.Next(0, 100);
                        if (value < 68) secondaryPlaystyle = playstyles[0];
                        if (value <= 100) secondaryPlaystyle = playstyles[1];
                    }


                }

                if (primaryPlaystyle == "2-Way")
                {
                    secondaryPlaystyle = "2-Way Player";
                }

            }
            if (secondaryPlaystyle == "") { }
            SecondaryPlaystyle = secondaryPlaystyle;
        }

        public void GenerateStats(string position, string primaryPlaystyle, string secondaryPlaystyle)
        {
            int closeShot = 0;
            int layup = 0;
            int dunk = 0;
            int midRange = 0;
            int threePoint = 0;
            int freeThrow = 0;
            int passing = 0;
            int ballHandle = 0;
            int defense = 0;
            int steal = 0;
            int block = 0;
            int rebound = 0;
            int speed = 0;
            int strength = 0;
            int stamina = 0;
            int overall = Overall;
            List<string> playstyles = new List<string>()
            {
                "Shooter",
                "Playmaker",
                "Inside-Scorer",
                "Finisher",
                "Rim Protector",
                "Lockdown",
                "Ripper"
            };

            string json = "";
            if (File.Exists($"\"C:\\Users\\nzuobm\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Names Files\\player_stats_config.json\""))
            {
                json = File.ReadAllText($"\"C:\\Users\\nzuobm\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Names Files\\player_stats_config.json\"");
            }
            else if (File.Exists($"\"C:\\Users\\FiercePC\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Names Files\\player_stats_config.json\""))
            {
                json = File.ReadAllText($"\"C:\\Users\\FiercePC\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Names Files\\player_stats_config.json\"");
            }

            var stats = JsonSerializer.Deserialize<Dictionary<string,Dictionary<string,Dictionary<string,Dictionary<string, int>>>>>(json);
            if (stats != null)
            {
                closeShot = stats[position][primaryPlaystyle][secondaryPlaystyle]["closeShot"];
                layup = stats[position][primaryPlaystyle][secondaryPlaystyle]["layup"];
                dunk = stats[position][primaryPlaystyle][secondaryPlaystyle]["dunk"];
                midRange = stats[position][primaryPlaystyle][secondaryPlaystyle]["midRange"];
                threePoint = stats[position][primaryPlaystyle][secondaryPlaystyle]["threePoint"];
                freeThrow = stats[position][primaryPlaystyle][secondaryPlaystyle]["freeThrow"];
                passing = stats[position][primaryPlaystyle][secondaryPlaystyle]["passing"];
                ballHandle = stats[position][primaryPlaystyle][secondaryPlaystyle]["ballHandle"];
                defense = stats[position][primaryPlaystyle][secondaryPlaystyle]["defense"];
                steal = stats[position][primaryPlaystyle][secondaryPlaystyle]["steal"];
                block = stats[position][primaryPlaystyle][secondaryPlaystyle]["block"];
                rebound = stats[position][primaryPlaystyle][secondaryPlaystyle]["rebound"];
                speed = stats[position][primaryPlaystyle][secondaryPlaystyle]["speed"];
                strength = stats[position][primaryPlaystyle][secondaryPlaystyle]["strength"];
                stamina = stats[position][primaryPlaystyle][secondaryPlaystyle]["stamina"];
            }
            


            double statsMean = 0;
            double statsStdev = 1.4;
            closeShot += (int)GenerateRandomNormalDistribution(0, 1.4);
            layup += (int)GenerateRandomNormalDistribution(0, 1.4);
            dunk += (int)GenerateRandomNormalDistribution(0, 1.4);
            midRange += (int)GenerateRandomNormalDistribution(0, 1.4);
            threePoint += (int)GenerateRandomNormalDistribution(0, 1.4);
            freeThrow += (int)GenerateRandomNormalDistribution(0, 1.4);
            passing += (int)GenerateRandomNormalDistribution(0, 1.4);
            ballHandle += (int)GenerateRandomNormalDistribution(0, 1.4);
            defense += (int)GenerateRandomNormalDistribution(0, 1.4);
            steal += (int)GenerateRandomNormalDistribution(0, 1.4);
            block += (int)GenerateRandomNormalDistribution(0, 1.4);
            rebound += (int)GenerateRandomNormalDistribution(0, 1.4);
            speed += (int)GenerateRandomNormalDistribution(0, 1.4);
            strength += (int)GenerateRandomNormalDistribution(0, 1.4);
            stamina += (int)GenerateRandomNormalDistribution(0, 1.4);

            // converting 60-99 into 0-30
            int multipler = (int)((overall - 50) * 0.6);

            if (position == "PG")
            {
                // calculate offensive stats; if offensive
                if (primaryPlaystyle == "Offensive")
                {
                    if (SecondaryPlaystyle == playstyles[0]) // shooting offensive player
                    {
                        // calculate closeShot
                        statsMean = 33;
                        statsStdev = 1;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 36;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;

                        // calculate dunk
                        statsMean = 27;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 63;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 65;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 62;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 63;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 63;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;

                        // calculate defense
                        statsMean = 33;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 39;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 25;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 33;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 62;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 32;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 64;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }

                    else if (SecondaryPlaystyle == playstyles[1]) // playmaking offensive player
                    {
                        // calculate closeShot
                        statsMean = 29;
                        statsStdev = 1;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 43;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;

                        // calculate dunk
                        statsMean = 42;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 54;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 57;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 59;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 63;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;
                        if (passing > 99) passing = 99;

                        // calculate ballHandle
                        statsMean = 65;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 38;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 36;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 25;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 33;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 66;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 30;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 64;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }

                    else if (SecondaryPlaystyle == playstyles[3]) // finishing offensive player
                    {
                        // calculate closeShot
                        statsMean = 56;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 61;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (layup > 99) layup = 99;

                        // calculate dunk
                        statsMean = 62;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 47;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 55;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 60;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 62;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 65;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 53;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 39;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 30;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 33;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 60;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 44;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 61;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }
                }
                // calculate defensive stats; if defensive
                else if (primaryPlaystyle == "Defensive")
                {
                    if (SecondaryPlaystyle == playstyles[5])
                    {
                        // calculate closeShot
                        statsMean = 28;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 37;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate dunk
                        statsMean = 32;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 36;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 51;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 58;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 56;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 53;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 74;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;
                        if (defense > 99) defense = 99;

                        // calculate steal
                        statsMean = 70;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;
                        if (steal > 99) steal = 99;

                        // calculate block
                        statsMean = 44;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 50;
                        statsStdev = 2.5;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 62;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 51;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 69;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;
                    } // lockdown playstyle

                    else if (SecondaryPlaystyle == playstyles[6]) // ripper playstyle
                    {
                        // calculate closeShot
                        statsMean = 31;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 43;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate dunk
                        statsMean = 32;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 38;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 48;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 53;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 54;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 55;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 69;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;
                        if (defense > 99) defense = 99;

                        // calculate steal
                        statsMean = 70;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;
                        if (steal > 99) steal = 99;


                        // calculate block
                        statsMean = 41;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 53;
                        statsStdev = 2.5;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 61;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 42;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 68;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;
                    }
                }
                // calculate 2-way stats;
                else if (primaryPlaystyle == "2-Way")
                {
                    // calculate closeShot
                    statsMean = 40;
                    statsStdev = 1.4;
                    closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    closeShot += multipler;

                    // calculate layup
                    statsMean = 53;
                    statsStdev = 1.4;
                    layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    layup += multipler;
                    if (dunk > 99) dunk = 99;

                    // calculate dunk
                    statsMean = 48;
                    statsStdev = 1.4;
                    dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    dunk += multipler;

                    // calculate midRange
                    statsMean = 44;
                    statsStdev = 1.3;
                    midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    midRange += multipler;
                    if (midRange > 99) midRange = 99;

                    // calculate 3
                    statsMean = 55;
                    statsStdev = 1.4;
                    threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    threePoint += multipler;
                    if (threePoint > 99) threePoint = 99;

                    // calculate freeThrow
                    statsMean = 58;
                    statsStdev = 1.4;
                    freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    freeThrow += multipler;
                    if (freeThrow > 99) freeThrow = 99;

                    // calculate passing
                    statsMean = 62;
                    statsStdev = 1.4;
                    passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    passing += multipler;

                    // calculate ballHandle
                    statsMean = 63;
                    statsStdev = 1.4;
                    ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    ballHandle += multipler;
                    if (ballHandle > 99) ballHandle = 99;

                    // calculate defense
                    statsMean = 57;
                    statsStdev = 1.4;
                    defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    defense += multipler;
                    if (defense > 99) defense = 99;

                    // calculate steal
                    statsMean = 51;
                    statsStdev = 1.4;
                    steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    steal += multipler;

                    // calculate block
                    statsMean = 30;
                    statsStdev = 1.4;
                    block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    block += multipler;

                    // calculate rebound
                    statsMean = 39;
                    statsStdev = 2.5;
                    rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    rebound += multipler;

                    // calculate speed
                    statsMean = 60;
                    statsStdev = 1.4;
                    speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    speed += multipler;
                    if (speed > 99) speed = 99;

                    // calculate strength
                    statsMean = 45;
                    statsStdev = 1.4;
                    strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    strength += multipler;

                    // calculate stamina
                    statsMean = 57;
                    statsStdev = 1.4;
                    stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    stamina += multipler;
                    if (stamina > 99) stamina = 99;
                }

            }
            else if (position == "SG" || position == "SF")
            {
                // calculate offensive stats; if offensive
                if (primaryPlaystyle == "Offensive")
                {
                    if (SecondaryPlaystyle == playstyles[0]) // shooting offensive player
                    {
                        // calculate closeShot
                        statsMean = 34;
                        statsStdev = 1.3;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 45;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;

                        // calculate dunk
                        statsMean = 42;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate midRange
                        statsMean = 61;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 64;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 62;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 50;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 54;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;

                        // calculate defense
                        statsMean = 48;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 38;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 39;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 50;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 52;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 46;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 55;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }

                    else if (SecondaryPlaystyle == playstyles[1]) // playmaking offensive player
                    {
                        // calculate closeShot
                        statsMean = 36;
                        statsStdev = 1.3;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 44;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (layup > 99) layup = 99;

                        // calculate dunk
                        statsMean = 46;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 49;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 51;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 54;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 63;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;
                        if (passing > 99) passing = 99;

                        // calculate ballHandle
                        statsMean = 62;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 50;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 33;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 39;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 41;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 52;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 48;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 54;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }

                    else if (SecondaryPlaystyle == playstyles[3]) // finishing offensive player
                    {
                        // calculate closeShot
                        statsMean = 60;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 61;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (layup > 99) layup = 99;

                        // calculate dunk
                        statsMean = 62;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate midRange
                        statsMean = 43;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 48;
                        statsStdev = 4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 51;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 52;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 54;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 45;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 38;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 41;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 47;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 60;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 46;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 57;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }
                }
                // calculate defensive stats; if defensive
                else if (primaryPlaystyle == "Defensive")
                {
                    if (SecondaryPlaystyle == playstyles[5])
                    {
                        // calculate closeShot
                        statsMean = 27;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 42;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate dunk
                        statsMean = 28;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 32;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 42;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 50;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 36;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 42;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 72;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;
                        if (defense > 99) defense = 99;

                        // calculate steal
                        statsMean = 70;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;
                        if (steal > 99) steal = 99;

                        // calculate block
                        statsMean = 41;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 48;
                        statsStdev = 2.5;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 70;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 62;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 71;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;
                    } // lockdown playstyle

                    else if (SecondaryPlaystyle == playstyles[6]) // ripper playstyle
                    {
                        // calculate closeShot
                        statsMean = 34;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 47;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate dunk
                        statsMean = 34;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 37;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 47;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 50;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 42;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 44;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 65;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;
                        if (defense > 99) defense = 99;

                        // calculate steal
                        statsMean = 69;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;
                        if (steal > 99) steal = 99;


                        // calculate block
                        statsMean = 37;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 47;
                        statsStdev = 2.5;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 68;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 51;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 60;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;
                    }
                }
                // calculate 2-way stats;
                else if (primaryPlaystyle == "2-Way")
                {
                    // calculate closeShot
                    statsMean = 45;
                    statsStdev = 1.4;
                    closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    closeShot += multipler;

                    // calculate layup
                    statsMean = 56;
                    statsStdev = 1.4;
                    layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    layup += multipler;
                    if (dunk > 99) dunk = 99;

                    // calculate dunk
                    statsMean = 47;
                    statsStdev = 1.4;
                    dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    dunk += multipler;

                    // calculate midRange
                    statsMean = 42;
                    statsStdev = 1.3;
                    midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    midRange += multipler;
                    if (midRange > 99) midRange = 99;

                    // calculate 3
                    statsMean = 52;
                    statsStdev = 1.4;
                    threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    threePoint += multipler;
                    if (threePoint > 99) threePoint = 99;

                    // calculate freeThrow
                    statsMean = 55;
                    statsStdev = 1.4;
                    freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    freeThrow += multipler;
                    if (freeThrow > 99) freeThrow = 99;

                    // calculate passing
                    statsMean = 53;
                    statsStdev = 1.4;
                    passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    passing += multipler;

                    // calculate ballHandle
                    statsMean = 59;
                    statsStdev = 1.4;
                    ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    ballHandle += multipler;
                    if (ballHandle > 99) ballHandle = 99;

                    // calculate defense
                    statsMean = 61;
                    statsStdev = 1.4;
                    defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    defense += multipler;
                    if (defense > 99) defense = 99;

                    // calculate steal
                    statsMean = 51;
                    statsStdev = 1.4;
                    steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    steal += multipler;

                    // calculate block
                    statsMean = 40;
                    statsStdev = 1.4;
                    block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    block += multipler;

                    // calculate rebound
                    statsMean = 34;
                    statsStdev = 2.5;
                    rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    rebound += multipler;

                    // calculate speed
                    statsMean = 60;
                    statsStdev = 1.4;
                    speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    speed += multipler;
                    if (speed > 99) speed = 99;

                    // calculate strength
                    statsMean = 45;
                    statsStdev = 1.4;
                    strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    strength += multipler;

                    // calculate stamina
                    statsMean = 57;
                    statsStdev = 1.4;
                    stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    stamina += multipler;
                    if (stamina > 99) stamina = 99;
                }
            }
            else if (position == "PF")
            {
                // calculate offensive stats; if offensive
                if (primaryPlaystyle == "Offensive")
                {
                    if (SecondaryPlaystyle == playstyles[0]) // shooting offensive player
                    {
                        // calculate closeShot
                        statsMean = 37;
                        statsStdev = 1.3;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 39;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;

                        // calculate dunk
                        statsMean = 48;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate midRange
                        statsMean = 64;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 62;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 61;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 42;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 42;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;

                        // calculate defense
                        statsMean = 57;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 41;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 54;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 56;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 50;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 61;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 61;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }

                    else if (SecondaryPlaystyle == playstyles[1]) // playmaking offensive player
                    {
                        // calculate closeShot
                        statsMean = 52;
                        statsStdev = 1.3;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 52;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;

                        // calculate dunk
                        statsMean = 53;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 49;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 51;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 53;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 64;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;
                        if (passing > 99) passing = 99;

                        // calculate ballHandle
                        statsMean = 62;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;

                        // calculate defense
                        statsMean = 51;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 36;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 49;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 51;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 45;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 60;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 60;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }

                    else if (SecondaryPlaystyle == playstyles[3]) // finishing offensive player
                    {
                        // calculate closeShot
                        statsMean = 60;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 61;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (layup > 99) layup = 99;

                        // calculate dunk
                        statsMean = 62;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate midRange
                        statsMean = 39;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 46;
                        statsStdev = 4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 51;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 44;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 46;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 48;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 36;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 52;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 55;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 57;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 65;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 64;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }
                }
                // calculate defensive stats; if defensive
                else if (primaryPlaystyle == "Defensive")
                {
                    if (SecondaryPlaystyle == playstyles[5])
                    {
                        // calculate closeShot
                        statsMean = 34;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 42;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (layup > 99) layup = 99;

                        // calculate dunk
                        statsMean = 47;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 38;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 46;
                        statsStdev = 4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 49;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 44;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 32;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 75;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;
                        if (defense > 99) defense = 99;

                        // calculate steal
                        statsMean = 63;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;
                        if (steal > 99) steal = 99;

                        // calculate block
                        statsMean = 65;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;
                        if (block > 99) block = 99;

                        // calculate rebound
                        statsMean = 64;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;
                        if (rebound > 99) rebound = 99;

                        // calculate speed
                        statsMean = 59;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 69;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 63;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;
                    } // lockdown playstyle

                    else if (SecondaryPlaystyle == playstyles[4]) // rim protection playstyle
                    {
                        // calculate closeShot
                        statsMean = 37;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 42;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate dunk
                        statsMean = 45;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 39;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 40;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 50;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 46;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 36;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 70;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;
                        if (defense > 99) defense = 99;

                        // calculate steal
                        statsMean = 60;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;
                        if (steal > 99) steal = 99;


                        // calculate block
                        statsMean = 66;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;
                        if (block > 99) block = 99;

                        // calculate rebound
                        statsMean = 64;
                        statsStdev = 2.5;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;
                        if (rebound > 99) rebound = 99;

                        // calculate speed
                        statsMean = 48;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 63;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 68;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;
                    }// rim protection playstyle
                }
                // calculate 2-way stats;
                else if (primaryPlaystyle == "2-Way")
                {
                    // calculate closeShot
                    statsMean = 52;
                    statsStdev = 1.4;
                    closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    closeShot += multipler;

                    // calculate layup
                    statsMean = 35;
                    statsStdev = 1.4;
                    layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    layup += multipler;
                    if (dunk > 99) dunk = 99;

                    // calculate dunk
                    statsMean = 55;
                    statsStdev = 1.4;
                    dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    dunk += multipler;

                    // calculate midRange
                    statsMean = 41;
                    statsStdev = 1.3;
                    midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    midRange += multipler;
                    if (midRange > 99) midRange = 99;

                    // calculate 3
                    statsMean = 47;
                    statsStdev = 1.4;
                    threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    threePoint += multipler;
                    if (threePoint > 99) threePoint = 99;

                    // calculate freeThrow
                    statsMean = 52;
                    statsStdev = 1.4;
                    freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    freeThrow += multipler;
                    if (freeThrow > 99) freeThrow = 99;

                    // calculate passing
                    statsMean = 46;
                    statsStdev = 1.4;
                    passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    passing += multipler;

                    // calculate ballHandle
                    statsMean = 44;
                    statsStdev = 1.4;
                    ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    ballHandle += multipler;
                    if (ballHandle > 99) ballHandle = 99;

                    // calculate defense
                    statsMean = 55;
                    statsStdev = 1.4;
                    defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    defense += multipler;
                    if (defense > 99) defense = 99;

                    // calculate steal
                    statsMean = 54;
                    statsStdev = 1.4;
                    steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    steal += multipler;

                    // calculate block
                    statsMean = 60;
                    statsStdev = 1.4;
                    block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    block += multipler;

                    // calculate rebound
                    statsMean = 61;
                    statsStdev = 2.5;
                    rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    rebound += multipler;
                    if (rebound > 99) rebound = 99;

                    // calculate speed
                    statsMean = 49;
                    statsStdev = 1.4;
                    speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    speed += multipler;
                    if (speed > 99) speed = 99;

                    // calculate strength
                    statsMean = 64;
                    statsStdev = 2.2;
                    strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    strength += multipler;
                    if (strength > 99) strength = 99;

                    // calculate stamina
                    statsMean = 59;
                    statsStdev = 1.4;
                    stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    stamina += multipler;
                    if (stamina > 99) stamina = 99;
                }
            }
            else if (position == "C")
            {
                // calculate offensive stats; if offensive
                if (primaryPlaystyle == "Offensive")
                {
                    if (SecondaryPlaystyle == playstyles[0]) // shooting offensive player
                    {
                        // calculate closeShot
                        statsMean = 36;
                        statsStdev = 1.3;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 31;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;

                        // calculate dunk
                        statsMean = 35;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 61;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 59;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 62;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 42;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 42;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;

                        // calculate defense
                        statsMean = 47;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 33;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 49;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 52;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;
                        if (rebound > 99) rebound = 99;

                        // calculate speed
                        statsMean = 46;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 58;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 61;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }

                    else if (SecondaryPlaystyle == playstyles[1]) // playmaking offensive player
                    {
                        // calculate closeShot
                        statsMean = 52;
                        statsStdev = 1.3;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 48;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;

                        // calculate dunk
                        statsMean = 53;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 36;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 43;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 41;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 64;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;
                        if (passing > 99) passing = 99;

                        // calculate ballHandle
                        statsMean = 60;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;

                        // calculate defense
                        statsMean = 46;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 31;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 57;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 60;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 54;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 63;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 58;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }

                    else if (SecondaryPlaystyle == playstyles[3]) // finishing offensive player
                    {
                        // calculate closeShot
                        statsMean = 62;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 60;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (layup > 99) layup = 99;

                        // calculate dunk
                        statsMean = 61;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate midRange
                        statsMean = 35;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 43;
                        statsStdev = 4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 49;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 49;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 52;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 45;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;

                        // calculate steal
                        statsMean = 36;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;

                        // calculate block
                        statsMean = 59;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;

                        // calculate rebound
                        statsMean = 62;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;

                        // calculate speed
                        statsMean = 64;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 60;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;

                        // calculate stamina
                        statsMean = 60;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;

                    }
                }
                // calculate defensive stats; if defensive
                else if (primaryPlaystyle == "Defensive")
                {
                    if (SecondaryPlaystyle == playstyles[5])
                    {
                        // calculate closeShot
                        statsMean = 31;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;
                        if (closeShot > 99) closeShot = 99;

                        // calculate layup
                        statsMean = 28;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (layup > 99) layup = 99;

                        // calculate dunk
                        statsMean = 35;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 32;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 34;
                        statsStdev = 4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 48;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 40;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 34;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 71;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;
                        if (defense > 99) defense = 99;

                        // calculate steal
                        statsMean = 63;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;
                        if (steal > 99) steal = 99;

                        // calculate block
                        statsMean = 71;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;
                        if (block > 99) block = 99;

                        // calculate rebound
                        statsMean = 70;
                        statsStdev = 1.4;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;
                        if (rebound > 99) rebound = 99;

                        // calculate speed
                        statsMean = 59;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 65;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 62;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;
                    } // lockdown playstyle

                    else if (SecondaryPlaystyle == playstyles[4]) // rim protection playstyle
                    {
                        // calculate closeShot
                        statsMean = 37;
                        statsStdev = 1.4;
                        closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        closeShot += multipler;

                        // calculate layup
                        statsMean = 33;
                        statsStdev = 1.4;
                        layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        layup += multipler;
                        if (dunk > 99) dunk = 99;

                        // calculate dunk
                        statsMean = 34;
                        statsStdev = 1.4;
                        dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        dunk += multipler;

                        // calculate midRange
                        statsMean = 26;
                        statsStdev = 1.3;
                        midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        midRange += multipler;
                        if (midRange > 99) midRange = 99;

                        // calculate 3
                        statsMean = 32;
                        statsStdev = 1.4;
                        threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        threePoint += multipler;
                        if (threePoint > 99) threePoint = 99;

                        // calculate freeThrow
                        statsMean = 48;
                        statsStdev = 1.4;
                        freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        freeThrow += multipler;
                        if (freeThrow > 99) freeThrow = 99;

                        // calculate passing
                        statsMean = 42;
                        statsStdev = 1.4;
                        passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        passing += multipler;

                        // calculate ballHandle
                        statsMean = 29;
                        statsStdev = 1.4;
                        ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        ballHandle += multipler;
                        if (ballHandle > 99) ballHandle = 99;

                        // calculate defense
                        statsMean = 67;
                        statsStdev = 1.4;
                        defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        defense += multipler;
                        if (defense > 99) defense = 99;

                        // calculate steal
                        statsMean = 62;
                        statsStdev = 1.4;
                        steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        steal += multipler;
                        if (steal > 99) steal = 99;


                        // calculate block
                        statsMean = 75;
                        statsStdev = 1.4;
                        block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        block += multipler;
                        if (block > 99) block = 99;

                        // calculate rebound
                        statsMean = 67;
                        statsStdev = 2.5;
                        rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        rebound += multipler;
                        if (rebound > 99) rebound = 99;

                        // calculate speed
                        statsMean = 47;
                        statsStdev = 1.4;
                        speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        speed += multipler;
                        if (speed > 99) speed = 99;

                        // calculate strength
                        statsMean = 67;
                        statsStdev = 1.4;
                        strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        strength += multipler;
                        if (strength > 99) strength = 99;

                        // calculate stamina
                        statsMean = 58;
                        statsStdev = 1.4;
                        stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                        stamina += multipler;
                        if (stamina > 99) stamina = 99;
                    }// rim protection playstyle
                }
                // calculate 2-way stats;
                else if (primaryPlaystyle == "2-Way")
                {
                    // calculate closeShot
                    statsMean = 57;
                    statsStdev = 1.4;
                    closeShot = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    closeShot += multipler;

                    // calculate layup
                    statsMean = 40;
                    statsStdev = 1.4;
                    layup = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    layup += multipler;
                    if (dunk > 99) dunk = 99;

                    // calculate dunk
                    statsMean = 54;
                    statsStdev = 1.4;
                    dunk = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    dunk += multipler;

                    // calculate midRange
                    statsMean = 42;
                    statsStdev = 1.3;
                    midRange = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    midRange += multipler;
                    if (midRange > 99) midRange = 99;

                    // calculate 3
                    statsMean = 49;
                    statsStdev = 1.4;
                    threePoint = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    threePoint += multipler;
                    if (threePoint > 99) threePoint = 99;

                    // calculate freeThrow
                    statsMean = 52;
                    statsStdev = 1.4;
                    freeThrow = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    freeThrow += multipler;
                    if (freeThrow > 99) freeThrow = 99;

                    // calculate passing
                    statsMean = 53;
                    statsStdev = 1.4;
                    passing = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    passing += multipler;

                    // calculate ballHandle
                    statsMean = 38;
                    statsStdev = 1.4;
                    ballHandle = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    ballHandle += multipler;
                    if (ballHandle > 99) ballHandle = 99;

                    // calculate defense
                    statsMean = 57;
                    statsStdev = 1.4;
                    defense = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    defense += multipler;
                    if (defense > 99) defense = 99;

                    // calculate steal
                    statsMean = 56;
                    statsStdev = 1.4;
                    steal = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    steal += multipler;

                    // calculate block
                    statsMean = 62;
                    statsStdev = 1.4;
                    block = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    block += multipler;

                    // calculate rebound
                    statsMean = 64;
                    statsStdev = 2.5;
                    rebound = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    rebound += multipler;
                    if (rebound > 99) rebound = 99;

                    // calculate speed
                    statsMean = 51;
                    statsStdev = 1.4;
                    speed = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    speed += multipler;
                    if (speed > 99) speed = 99;

                    // calculate strength
                    statsMean = 64;
                    statsStdev = 2.2;
                    strength = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    strength += multipler;
                    if (strength > 99) strength = 99;

                    // calculate stamina
                    statsMean = 59;
                    statsStdev = 1.4;
                    stamina = (int)GenerateRandomNormalDistribution(statsMean, statsStdev);
                    stamina += multipler;
                    if (stamina > 99) stamina = 99;
                }
            }

            // use weight scalar to scale strength (150-300) to (-11 to +11)
            decimal scalar = 22 * ((Weight - 150m) / 150m);
            strength += -11 + (int)scalar;
            if (strength > 99) strength = 99;
            CloseShot = closeShot;
            Layup = layup;
            if (CloseShot > Layup) Layup = CloseShot;
            Dunk = dunk;
            MidRange = midRange;
            ThreePoint = threePoint;
            FreeThrow = freeThrow;
            Passing = passing;
            BallHandle = ballHandle;
            Defense = defense;
            Steal = steal;
            Block = block;
            Rebound = rebound;
            Speed = speed;
            Strength = strength;
            Stamina = stamina;
        }


        public void GenerateAgeOvrAndPotential(double overallMean, bool isRookie)
        {
            // we generate the age of the current player
            double ageMean = 26.5;
            double ageStDev = 4.5;
            if (isRookie)
            {
                ageMean = 20;
                ageStDev = 0.5;
            }
            int age = (int)GenerateRandomNormalDistribution(ageMean, ageStDev);
            if (age < 18) age = 18;
            if (age > 40) age = 40;
            Age = age;

            // we make sure players that are older than 33 and younger than 21, are not very good
            // compared to the rest of the league
            if (age < 22)
            {
                overallMean -= 3.8 + (22 - age);
            }
            else if (age > 33)
            {
                overallMean -= 4.5 + (age - 33);
            }

            // now we generate the overall of the player
            double overallStDev = 7.6;
            int overall = (int)GenerateRandomNormalDistribution(overallMean, overallStDev);
            if (overall < 60) overall = 60;
            else if (overall > 99) overall = 99;
            else if (!isRookie && overall > 98) overall = 98;
            Overall = overall;

            // we calculate the potential of the player
            if (age < 28)
            {
                // this gets the age difference, and an additional overallDifference is added
                // this makes sure players with really low overall, can have really high potential
                double ageDifference = (27 - age) * 0.7 + (80 - overall) * 0.6;
                int potentialMean = (int)(3 + ageDifference);
                int potentialStDev = 2;
                int potential = (int)GenerateRandomNormalDistribution(potentialMean, potentialStDev);
                Potential = potential + overall;

            }
            else Potential = age;
            if (Potential < overall) Potential = overall;
        }

        public void GeneratePlayer(string pos, int teamId, string teamName, string forename, string surname, int meanOverall)
        {
            // add team information to player
            TeamId = teamId;
            this.teamName = teamName;

            // add name to player
            playerForename = forename;
            playerSurname = surname;
            // height and weight of player
            position = pos;
            // generates height and weight for the player
            RandomHeight(position);
            RandomWeight(position, Height);

            // generate the playstyles of the player
            GeneratePrimaryPlaystyle();
            GenerateSecondaryPlaystyle(Height, PrimaryPlaystyle, SecondaryPlaystyle);

            // generate stats, age, overall and the position of the player
            GenerateAgeOvrAndPotential(meanOverall, false);
            GenerateStats(position, PrimaryPlaystyle, SecondaryPlaystyle);
        }

        public void GenerateRookie(string pos, int teamId, string teamName, string forename, string surname, int meanOverall)
        {
            // add team information to player
            TeamId = teamId;
            this.teamName = teamName;

            // add name to player
            playerForename = forename;
            playerSurname = surname;
            // height and weight of player
            position = pos;
            // generates height and weight for the player
            RandomHeight(position);
            RandomWeight(position, Height);

            // generate the playstyles of the player
            GeneratePrimaryPlaystyle();
            GenerateSecondaryPlaystyle(Height, PrimaryPlaystyle, SecondaryPlaystyle);

            // generate stats, age, overall and the position of the player
            GenerateAgeOvrAndPotential(meanOverall, true);
            GenerateStats(position, PrimaryPlaystyle, SecondaryPlaystyle);
        }

    }
}
