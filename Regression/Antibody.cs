
using System;

namespace Regression.Linear
{
	
	public class Antibody :System.ICloneable //implement clonable interface
	{
		
        private double[] xyvalues;
        public double[] XYvalues
        {
        	get{ return xyvalues;}
        }
        
        private double fitness;
        public double Fitness
        {
        	get{return fitness;}
        }
        private double normalisedFitness;
        
        private double mutationfactor;
        private double lowerboundary;
        private double upperboundary;
        private static Random rand = new Random(123);
        

		public Antibody(double mutation, double lowerlimit, double upperlimit)
		{
			//Constructor
			
			this.mutationfactor = mutation;
            this.lowerboundary = lowerlimit;
            this.upperboundary = upperlimit;
            xyvalues= new double[2];

            //Create random x value
            xyvalues[0] = lowerboundary + (upperboundary - lowerboundary) * rand.NextDouble();
            //Create random y value
            xyvalues[1] = lowerboundary + (upperboundary - lowerboundary) * rand.NextDouble();			
		}
		
		 public Antibody(Antibody copyAntibody)
        {
            this.mutationfactor = copyAntibody.mutationfactor;
            this.lowerboundary = copyAntibody.lowerboundary;
            this.upperboundary = copyAntibody.upperboundary;
            this.xyvalues = copyAntibody.xyvalues;
        }


        // The IClonable interface forces us to write 
        // a Clone() method to clone an antibody

        public object Clone()
        {
            // clone this Antibody
            Antibody myAntibody = this.MemberwiseClone() as Antibody;
            myAntibody.xyvalues = (double[])xyvalues.Clone();

            return myAntibody;
        }  

        public void Mutate()
        {
            
            

            double affinityMutator;
            
            affinityMutator = (1.0 / mutationfactor) * Math.Exp(-1 * normalisedFitness);

            xyvalues[0] = xyvalues[0] + affinityMutator * rand.NextDouble();

            if (xyvalues[0] > upperboundary) xyvalues[0] = upperboundary;
            if (xyvalues[0] < lowerboundary) xyvalues[0] = lowerboundary;

            xyvalues[1] = xyvalues[1] + affinityMutator * rand.NextDouble();
            
            if (xyvalues[1] > upperboundary) xyvalues[1] = upperboundary;
            if (xyvalues[1] < lowerboundary) xyvalues[1] = lowerboundary;
        }

        
        public double findAffinity(Antibody c)
        {
            // Affinity is calculated as the Euclidean distance
            // between two antibodies
            
            double affinity = 0.0;

            double[] cXYvalues = c.XYvalues;
            double dx = Math.Pow((xyvalues[0] - cXYvalues[0]),2);
            double dy = Math.Pow((xyvalues[1] - cXYvalues[1]),2);

            affinity = Math.Sqrt(dx + dy);
            
            return affinity;
        }

        public void calcNormalisedFitness(double lowfit, double highfit)
        {
            //lowfit = lowest fitness value for all antibodies
            //highfit = highest fitness value for all antibodies
            
            normalisedFitness = (fitness - lowfit) / (highfit - lowfit);

        }

        public void evaluate()
        {
            fitness = FitnessFunction.evaluateFunction(xyvalues);
        }	
		
	}
}
