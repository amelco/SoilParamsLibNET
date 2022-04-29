namespace SoilParams.Models
{
    public class Statistics
    {
        public double MeasuredStandardDeviation  { get; set; }
        public double MeasuredStandardError      { get; set; }
        public double PredictedStandardDeviation { get; set; }
        public double PredictedStandardError     { get; set; }
        public double PearsonCorrelation         { get; set; }
        public double Rsquared                   { get; set; }

        public override string ToString()
        {
            return 
$@"
Std dev: {MeasuredStandardDeviation.ToString("0.0000"),7} - measured
         {PredictedStandardDeviation.ToString("0.0000"),7} - predicted
Std err: {MeasuredStandardError.ToString("0.0000"),7} - measured
         {PredictedStandardError.ToString("0.0000"),7} - predicted

Person coeff: {PearsonCorrelation.ToString("0.0000"),7}
R-Squared:    {Rsquared.ToString("0.0000"),7}
";
        }
    }
}