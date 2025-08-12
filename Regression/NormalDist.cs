
using System;

namespace CenterSpace.Free
{
 
  public class NormalDist
  {
    #region Static Variables ------------------------------------------------

    private static readonly double MACHINE_EPSILON = 1.0e-12;
    
    private readonly static double[] a = 
    {
      2.2352520354606839287e00,1.6102823106855587881e02,1.0676894854603709582e03,
      1.8154981253343561249e04,6.5682337918207449113e-2
    };

    private readonly static double[] b = 
    {
      4.7202581904688241870e01,9.7609855173777669322e02,1.0260932208618978205e04,
      4.5507789335026729956e04
    };
    private static readonly double[] c = 
    {
      3.9894151208813466764e-1,8.8831497943883759412e00,9.3506656132177855979e01,
      5.9727027639480026226e02,2.4945375852903726711e03,6.8481904505362823326e03,
      1.1602651437647350124e04,9.8427148383839780218e03,1.0765576773720192317e-8
    };

    private static readonly double[] d = 
    {
      2.2266688044328115691e01,2.3538790178262499861e02,1.5193775994075548050e03,
      6.4855582982667607550e03,1.8615571640885098091e04,3.4900952721145977266e04,
      3.8912003286093271411e04,1.9685429676859990727e04
    };
    private static readonly double half = 0.5e0;

    private static readonly double[] p = 
    {
      2.1589853405795699e-1,1.274011611602473639e-1,2.2235277870649807e-2,
      1.421619193227893466e-3,2.9112874951168792e-5,2.307344176494017303e-2
    };

    private static readonly double one = 1.0e0;

    private static readonly double[] q = 
    {
      1.28426009614491121e00,4.68238212480865118e-1,6.59881378689285515e-2,
      3.78239633202758244e-3,7.29751555083966205e-5
    };

    private static readonly double sixten = 1.60e0;
    private static readonly double sqrpi = 3.9894228040143267794e-1;
    private static readonly double thrsh = 0.66291e0;
    private static readonly double root32 = 5.656854248e0;
    private static readonly double zero = 0.0e0;

    private static readonly double OneOverRoot2Pi = 1.0/Math.Sqrt(2*Math.PI);

    #endregion Static Variables 


    #region Instance Variables ----------------------------------------------

    private double mean_;
    private double sigma_;
    // Once we have the mean and variance, the following are constants
    // that are needed to evaluate the pdf.
    private double oneOverSigma_;
    private double oneOverSigmaSqr_;
    // c_ is just oneOverSigma_*OneOverRoot2Pi
    private double c_;

    #endregion Instance Variables 


    #region Constructors ----------------------------------------------------

    
    public NormalDist( double mean, double var )
    {
      mean_ = mean;
      Variance = var;
    }
    public NormalDist()
    {
      
    }
    #endregion Constructors 


    #region Properties ------------------------------------------------------

   
    public double Mean
    {
      get
      {
        return mean_;
      }

      set
      {
        mean_ = value;
      }
    }

   
    public double Variance
    {
      get
      {
        return( sigma_ * sigma_ );
      }

      set
      {
        if ( value <= 0.0 )
        {
          string msg = string.Format( "Expected variance > 0 in NormalDistribution. Found variance = {0}", value );
          throw new Exception( msg );
        }
        sigma_ = Math.Sqrt( value );
        oneOverSigma_ = 1.0 / sigma_;
        oneOverSigmaSqr_ = oneOverSigma_ * oneOverSigma_;
        c_ = oneOverSigma_*OneOverRoot2Pi;
      }
    }

    
    public double Skewness
    {
      get
      {
        return 0.0;
      }
    }

  
    public double Kurtosis
    {
      get
      {
        return 0.0;
      }
    }

    #endregion Properties


    #region Member Functions ------------------------------------------------

    
    public double PDF( double x )
    {
      double y = (x - mean_);
      double xMinusMuSqr = y*y;
      
      // c_ is a constant equal to one over sigma times one over square root of 2 PI
      return c_*Math.Exp( -0.5*xMinusMuSqr*oneOverSigmaSqr_ );
    }

  
    public void CDF()
    {
        int i;
        double x = 100;
        double del, temp, z, xden, xnum, y, xsq, min;
        double result, ccum;
        double arg = (x - mean_) / sigma_;

        min = Double.Epsilon;
        z = arg;
        y = Math.Abs(z);
        if (y <= thrsh)
        {
            //
            // Evaluate  anorm  for  |X| <= 0.66291
            //
            xsq = zero;
            if (y > MACHINE_EPSILON) xsq = z * z;
            xnum = a[4] * xsq;
            xden = xsq;
            for (i = 0; i < 3; i++)
            {
                xnum = (xnum + a[i]) * xsq;
                xden = (xden + b[i]) * xsq;
            }
            result = z * (xnum + a[3]) / (xden + b[3]);
            temp = result;
            result = half + temp;
            ccum = half - temp;
        }

        //
        // Evaluate  anorm  for 0.66291 <= |X| <= sqrt(32)
        //
        else if (y <= root32)
        {
            xnum = c[8] * y;
            xden = y;
            for (i = 0; i < 7; i++)
            {
                xnum = (xnum + c[i]) * y;
                xden = (xden + d[i]) * y;
            }
            result = (xnum + c[7]) / (xden + d[7]);
            xsq = Math.Floor(y * sixten) / sixten;
            del = (y - xsq) * (y + xsq);
            result = Math.Exp(-(xsq * xsq * half)) * Math.Exp(-(del * half)) * result;
            ccum = one - result;
            if (z > zero)
            {
                temp = result;
                result = ccum;
                ccum = temp;
            }
        }

        //
        // Evaluate  anorm  for |X| > sqrt(32)
        //
        else
        {
            result = zero;
            xsq = one / (z * z);
            xnum = p[5] * xsq;
            xden = xsq;
            for (i = 0; i < 4; i++)
            {
                xnum = (xnum + p[i]) * xsq;
                xden = (xden + q[i]) * xsq;
            }
            result = xsq * (xnum + p[4]) / (xden + q[4]);
            result = (sqrpi - result) / y;
            xsq = Math.Floor(z * sixten) / sixten;
            del = (z - xsq) * (z + xsq);
            result = Math.Exp(-(xsq * xsq * half)) * Math.Exp(-(del * half)) * result;
            ccum = one - result;
            if (z > zero)
            {
                temp = result;
                result = ccum;
                ccum = temp;
            }
        }

        if (result < min) result = 0.0e0;
        //
        // Fix up for negative argument, erf, etc.
        //
        if (ccum < min) ccum = 0.0e0;

        
    }
  
    public double CDF( double x )
    {
     
      int i;
      double del,temp,z,xden,xnum,y,xsq,min;
      double result, ccum;
      double arg = (x - mean_) / sigma_;

      min = Double.Epsilon;
      z = arg;
      y = Math.Abs(z);
      if(y <= thrsh) 
      {
        //
        // Evaluate  anorm  for  |X| <= 0.66291
        //
        xsq = zero;
        if(y > MACHINE_EPSILON) xsq = z*z;
        xnum = a[4]*xsq;
        xden = xsq;
        for(i=0; i<3; i++) 
        {
          xnum = (xnum+a[i])*xsq;
          xden = (xden+b[i])*xsq;
        }
        result = z*(xnum+a[3])/(xden+b[3]);
        temp = result;
        result = half+temp;
        ccum = half-temp;
      }

      //
      // Evaluate  anorm  for 0.66291 <= |X| <= sqrt(32)
      //
      else if(y <= root32) 
      {
        xnum = c[8]*y;
        xden = y;
        for(i=0; i<7; i++) 
        {
          xnum = (xnum+c[i])*y;
          xden = (xden+d[i])*y;
        }
        result = (xnum+c[7])/(xden+d[7]);
        xsq = Math.Floor(y*sixten)/sixten;
        del = (y-xsq)*(y+xsq);
        result = Math.Exp(-(xsq*xsq*half))*Math.Exp(-(del*half))*result;
        ccum = one-result;
        if(z > zero) 
        {
          temp = result;
          result = ccum;
          ccum = temp;
        }
      }

      //
      // Evaluate  anorm  for |X| > sqrt(32)
      //
      else  
      {
        result = zero;
        xsq = one/(z*z);
        xnum = p[5]*xsq;
        xden = xsq;
        for(i=0; i<4; i++) 
        {
          xnum = (xnum+p[i])*xsq;
          xden = (xden+q[i])*xsq;
        }
        result = xsq*(xnum+p[4])/(xden+q[4]);
        result = (sqrpi-result)/y;
        xsq = Math.Floor(z*sixten)/sixten;
        del = (z-xsq)*(z+xsq);
        result = Math.Exp(-(xsq*xsq*half))*Math.Exp(-(del*half))*result;
        ccum = one-result;
        if(z > zero) 
        {
          temp = result;
          result = ccum;
          ccum = temp;
        }
      }

      if(result < min) result = 0.0e0;
      //
      // Fix up for negative argument, erf, etc.
      //
      if(ccum < min) ccum = 0.0e0;

      return result;
    }

    #endregion Member Functions

  }
}
