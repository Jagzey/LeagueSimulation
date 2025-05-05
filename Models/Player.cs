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
            if (value < 55) primaryPlaystyle = playstyles[0];
            else if (value < 75) primaryPlaystyle = playstyles[1];
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
            List<string> offensivePlaystyles = new List<string>()
            {
                "Shooter",
                "Playmaker",
                "Finisher",
            };
            List<string> defensivePlaystyles = new List<string>()
            {
                "Rim Protector",
                "Lockdown",
                "Ripper"
            };

            Dictionary<string, Dictionary<string, double>> playstyleMaxValues = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, double> pointGuardValues = new Dictionary<string, double>()
            {
                {"offensiveFirst", 26 },
                {"offensiveSecond", 90 },
                {"defensiveFirst",  47},
                {"heightMean", 75.5 }
            };
            Dictionary<string, double> shootingGuardValues = new Dictionary<string, double>()
            {
                {"offensiveFirst", 42 },
                {"offensiveSecond", 71 },
                {"defensiveFirst",  60},
                {"heightMean", 77.5 }
            };
            Dictionary<string, double> smallForwardValues = new Dictionary<string, double>()
            {
                {"offensiveFirst", 44 },
                {"offensiveSecond", 62 },
                {"defensiveFirst",  66},
                {"heightMean", 79 }
            };
            Dictionary<string, double> powerForwardValues = new Dictionary<string, double>()
            {
                {"offensiveFirst", 32 },
                {"offensiveSecond", 40 },
                {"defensiveFirst",  48},
                {"heightMean", 81.5 }
            };
            Dictionary<string, double> centerValues = new Dictionary<string, double>()
            {
                {"offensiveFirst", 26 },
                {"offensiveSecond", 38 },
                {"defensiveFirst",  55},
                {"heightMean", 83 }
            };
            playstyleMaxValues = new Dictionary<string, Dictionary<string, double>>()
            {
                {"PG", pointGuardValues },
                {"SG", shootingGuardValues },
                {"SF", smallForwardValues },
                {"PF", powerForwardValues },
                {"C", centerValues}
            };


            Random random = new Random();
            double heightMean = playstyleMaxValues[position]["heightMean"];
            if (primaryPlaystyle == "Offensive")
            {
                double randomValue = random.Next(0, 100);
                double value1 = playstyleMaxValues[position]["offensiveFirst"];
                double value2 = playstyleMaxValues[position]["offensiveSecond"];
                if (height - heightMean < -2)
                {
                    value1 += 1;
                    value2 += 2;
                }
                else if (height - heightMean >= 1)
                {
                    value1 += -1;
                    value2 += -2;
                }
                if (randomValue < value1) secondaryPlaystyle = offensivePlaystyles[0];
                else if (randomValue < value2) secondaryPlaystyle = offensivePlaystyles[1];
                else secondaryPlaystyle = offensivePlaystyles[2];
            }
            else if (primaryPlaystyle == "Defensive")
            {
                double randomValue = random.Next(0, 100);
                if (position == "PF" || position == "C") defensivePlaystyles.Remove("Ripper");
                else defensivePlaystyles.Remove("Rim Protector");
                double value1 = playstyleMaxValues[position]["defensiveFirst"];
                if (height - heightMean < -2) value1 += -7;
                else if (height - heightMean >= 1) value1 += 5;
                if (randomValue < value1) secondaryPlaystyle = defensivePlaystyles[0];
                else secondaryPlaystyle = defensivePlaystyles[1];
            }
            else if (primaryPlaystyle == "2-Way")
            {
                secondaryPlaystyle = "2-Way Player";
                SecondaryPlaystyle = secondaryPlaystyle;
            }
            SecondaryPlaystyle = secondaryPlaystyle;
            if (secondaryPlaystyle == null) { }
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
            if (File.Exists($@"Player Data\player_stats_config.json"))
            {
                json = File.ReadAllText($@"Player Data\player_stats_config.json");
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

            // converting 60-99 into 0-35
            int multipler2 = (int)((overall - 50) * 0.66);
            double multipler = overall / 60.0;
            if (multipler < 1) multipler = 1;
            closeShot = (int)(GenerateRandomNormalDistribution(closeShot, 1.4, 25, 99 / multipler) * multipler);
            layup = (int)(GenerateRandomNormalDistribution(layup, 1.4, 25, 99 / multipler) * multipler);
            dunk = (int)(GenerateRandomNormalDistribution(dunk, 1.4, 25, 99 / multipler) * multipler);
            midRange = (int)(GenerateRandomNormalDistribution(midRange, 1.4, 25, 99 / multipler) * multipler);
            threePoint = (int)(GenerateRandomNormalDistribution(threePoint, 1.4, 25, 99 / multipler) * multipler);
            freeThrow = (int)(GenerateRandomNormalDistribution(freeThrow, 1.4, 25, 99 / multipler) * multipler);
            passing = (int)(GenerateRandomNormalDistribution(passing, 1.4, 25, 99 / multipler) * multipler);
            ballHandle = (int)(GenerateRandomNormalDistribution(ballHandle, 1.4, 25, 99 / multipler) * multipler);
            defense = (int)(GenerateRandomNormalDistribution(defense, 1.4, 25, 99 / multipler) * multipler);
            steal = (int)(GenerateRandomNormalDistribution(steal, 1.4, 25, 99 / multipler) * multipler);
            block = (int)(GenerateRandomNormalDistribution(block, 1.4, 25, 99 / multipler) * multipler);
            rebound = (int)(GenerateRandomNormalDistribution(rebound, 1.4, 25, 99 / multipler) * multipler);
            speed = (int)(GenerateRandomNormalDistribution(speed, 1.4, 25, 99 / multipler) * multipler);
            strength = (int)(GenerateRandomNormalDistribution(strength, 1.4, 25, 99 / multipler) * multipler);
            stamina = (int)(GenerateRandomNormalDistribution(stamina, 1.4, 25, 99 / multipler) * multipler);

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
            double ageMean = 26.8;
            double ageStDev = 4.1;
            int ageMax = 40;
            int ageMin = 18;
            if (isRookie)
            {
                ageMean = 20;
                ageStDev = 0.5;
                ageMax = 22;
            }
            int age = (int)GenerateRandomNormalDistribution(ageMean, ageStDev, ageMin, ageMax);
            Age = age;

            // we make sure players that are older than 33 and younger than 21, are not very good
            // compared to the rest of the league
            double overallStDev = 8.0;
            if (age < 28)
            {
                overallMean -= 2.45 * (28 - age);
                overallStDev -= 0.73 * (28 - age);
                if (overallMean < 62) overallMean = 62;
                if (overallStDev < 1.5) overallStDev = 1.5;
            }
            else if (age > 33)
            {
                overallMean -= 3.5 + (age - 33);
                overallStDev -= 0.4 * (age - 33);
            }

            // now we generate the overall of the player
            int ovrMax = 99;
            int ovrMin = 60;
            if (isRookie) ovrMax = 70 ;
            Overall = (int)GenerateRandomNormalDistribution(overallMean, overallStDev, ovrMin, ovrMax);

            // we calculate the potential of the player
            if (age < 28)
            {
                // this gets the age difference, and an additional overallDifference is added
                // this makes sure players with really low overall, can have really high potential
                double ageDifference = (28 - age) * 0.4 + (80 - Overall) * 0.40;
                int potentialMean = (int)(ageDifference);
                int potentialStDev = 4;
                int potential = (int)GenerateRandomNormalDistribution(potentialMean, potentialStDev);
                Potential = potential + Overall;

            }
            else Potential = age;
            if (Potential < Overall) Potential = Overall;
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
