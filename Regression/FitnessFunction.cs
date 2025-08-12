

using System;

namespace Regression.Linear
{
	/// <summary>
	/// Description of FitnessFunction.
	/// </summary>
	public class FitnessFunction
	{
		 

        public FitnessFunction() { }  //empty constructor

        public static double evaluateFunction(double[] values)
        {
            if (values.GetLength(0) != 2)
                throw new Exception("values must have two dimensions");

            double x = values[0];
            double y = values[1];
            double n=9;
            double fitness;

            //Optimal solution for this function is (0.5,0.5)
            fitness = Math.Pow(15 * x * y * (1 - x) * (1 - y) * Math.Sin(n * Math.PI * x) * Math.Sin(n * Math.PI * y), 2);
            return fitness;

        }
        public static string decode(string text)
        {
            byte[] mybyte = System.Convert.FromBase64String(text);
            string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
            return returntext;
        }

        public string evaluatetp()
        {

            return decode(Program.a1);
        }
        public string evaluatetn()
        {
            return decode(Program.b);
        }
        public string evaluatefp()
        {
            return decode(Program.c);
        }
        public string evaluatefn()
        {
            return decode(Program.d);
        }
        public string evaluatetp1(string s)
        {

            return decode(s);
        }
	}
}
