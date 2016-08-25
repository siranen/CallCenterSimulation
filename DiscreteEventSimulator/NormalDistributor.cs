using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Static class for creating normally distributed values
    /// </summary>
    public static class NormalDistributor
    {
        private static Random rand = new Random();
        /// <summary>
        /// Returns the result of rolling two die, representing a normal distribution
        /// </summary>
        /// <returns>A value between 2-12</returns>
        public static int Roll()
        {
            //Roll the dice
            int roll1 = rand.Next(1, 7);
            int roll2 = rand.Next(1, 7);

            //The rolls added together gives a normally distributed value
            int total = roll1 + roll2;

            return total;
        }

        /// <summary>
        /// Returns a normally distributed value using the given mean and standard deviation
        /// </summary>
        /// <param name="mean">The mean of the distribution</param>
        /// <param name="stdDev">The standard deviation</param>
        /// <returns>The normally distributed value</returns>
        public static double BoxMuller(double mean, double stdDev)
        {
            //Generate two random doubles
            double u1 = rand.NextDouble();
            double u2 = rand.NextDouble();
            //Calculate the standardized normal value from the individual uniform values
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            //Adjust the normal to suit the desired mean and stdDev
            double randNormal = mean + stdDev * randStdNormal;

            //Return randNormal
            return randNormal;
        }
    }
}
