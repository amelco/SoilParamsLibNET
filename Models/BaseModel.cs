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
    public abstract class BaseModel
    {
        public SoilModelEnum                                   ModelEnum       { get; set; }
        public string                                          Name            { get; set; }
        public List<string>                                    ParametersNames { get; set; }
        public OneVariableFunctionFitter<TrustRegionMinimizer> Fitter          { get; set; }

        public Func<DoubleVector, double, double>              WrcFunction;    // delegate

        public Dictionary<string, double> CalculateParams(WRCParams model)
        {
            var xValues = new DoubleVector(model.PressureHeads.ToArray());
            var yValues = new DoubleVector(model.MeasuredWaterContents.ToArray());
            var start   = new DoubleVector(model.InitialGuess.ToArray());

            DoubleVector parameters = Fitter.Fit( xValues, yValues, start );

            var soilParameters = new Dictionary<string, double>();
            for (int i=0; i<ParametersNames.Count; i++)
            {
                soilParameters.Add(ParametersNames[i], parameters[i]);
            }

            return soilParameters;
        }
        public List<double> CalculatePredictedWaterContents(List<double> pressureHeads, List<double> parameters)
        {
            var predictedWaterContents = new List<double>();
            foreach (var pressureHead in pressureHeads)
            {
                predictedWaterContents.Add(WrcFunction(new DoubleVector(parameters.ToArray()), (double)pressureHead));
            }
            return predictedWaterContents;
        }
        public Statistics GetStats(WRCParams model)
        {

            Statistics stats = new Statistics();

            var stdDevM = NMathFunctions.StandardDeviation(model.MeasuredWaterContents.ToArray());
            var stdDevP = NMathFunctions.StandardDeviation(model.PredictedWaterContents.ToArray());
            var stdErrM = stdDevM / Math.Sqrt(model.MeasuredWaterContents.Count);
            var stdErrP = stdDevP / Math.Sqrt(model.PredictedWaterContents.Count);
            var corr = NMathFunctions.Correlation(new DoubleVector(model.MeasuredWaterContents.ToArray()), new DoubleVector(model.PredictedWaterContents.ToArray()));
            var rsqd = new GoodnessOfFit(Fitter, new DoubleVector(model.PressureHeads.ToArray()), new DoubleVector(model.MeasuredWaterContents.ToArray()), new DoubleVector(model.Params.Values.ToArray())).RSquared;

            stats.MeasuredStandardDeviation = stdDevM;
            stats.MeasuredStandardError = stdErrM;
            stats.PredictedStandardDeviation = stdDevP;
            stats.PredictedStandardError = stdErrP;
            stats.PearsonCorrelation = corr;
            stats.Rsquared = rsqd;

            return stats;
        }
    }
}
