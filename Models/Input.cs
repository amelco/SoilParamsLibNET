using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoilParams.Models
{
    class Input
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<double> PressureHeads { get; set; }
        public List<double> MeasuredWaterContents { get; set; }
        public List<double> PredictedWaterContents { get; set; }
        public List<string> Models { get; set; }
        public List<double> InitialGuess { get; set; }
    };
}
