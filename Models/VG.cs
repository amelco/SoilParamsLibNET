using SoilParams.SoilEnums;
using SoilParams.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CenterSpace.NMath.Core;

namespace SoilParams.Models
{
    class VG : BaseModel
    {
        public VG(SoilModelEnum model)
        {
            Name = model.GetDescription();

            ParametersNames = new List<string> 
            {
                "ThetaR", "ThetaS", "alpha", "n"
            };

            WrcFunction = delegate( DoubleVector parameters, double h )
            {
                if ( parameters.Length != 4 )
                {
                    throw new InvalidArgumentException( "Incorrect number of function parameters: " + parameters.Length );
                }
                var thetaR = parameters[0];
                var thetaS = parameters[1];
                var alpha  = parameters[2];
                var n      = parameters[3];
                return thetaR + (thetaS - thetaR) / Math.Pow(1 + Math.Pow(alpha * h, n), 1-1/n);
            };

            Fitter = new OneVariableFunctionFitter<TrustRegionMinimizer>(WrcFunction);
        }
    }
}
