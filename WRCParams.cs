using SoilParams.SoilEnums;
using SoilParams.Extensions;
using SoilParams.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoilParams
{
    public class WRCParams
    {
        public BaseModel                  WRCModel               { get; set; }
        public List<double>               PressureHeads          { get; private set; }
        public List<double>               MeasuredWaterContents  { get; private set; } = new();
        public List<double>               PredictedWaterContents { get; private set; } = new();
        public Dictionary<string, double> Params                 { get; private set; } = new();
        public List<double>               InitialGuess           { get; private set; }
        public Statistics                 Stats                  { get; set; }


        public WRCParams(string inputString)
        {
            WRCModel = ModelSimpleFactory.CreateModel(SoilModelEnum.VG);
            ReadInputString(inputString);
        }

        public WRCParams(SoilModelEnum model, string inputString)
        {
            WRCModel = ModelSimpleFactory.CreateModel(model);
            ReadInputString(inputString);
        }
        
        private void ReadInputString(string input)
        {
            try
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<Input>(input);
                PressureHeads = result.PressureHeads;
                MeasuredWaterContents = result.MeasuredWaterContents;
                InitialGuess = result.InitialGuess;
            }
            catch (Exception e)
            {
                throw new Exception("Invalid input file. Check the manual to generte a correct input file.\nError: " + e.Message);
            }
        }

        public string GetModelName()
        {
            return WRCModel.Name;
        }

        public void CalculateParams()
        {
            if (WRCModel == null)
            {
                throw new AppDomainUnloadedException("A model must be selected first");
            }

            Params = WRCModel.CalculateParams(this);

        }

        public void CalculateWaterContents()
        {
            if (Params.Count == 0)
            {
                throw new AppDomainUnloadedException("Model parameters does not exist. Try to run CalculateParams function first");
            }
            PredictedWaterContents = WRCModel.CalculatePredictedWaterContents(PressureHeads, Params.Values.ToList());
        }

        public void CalculateStatistics()
        {
            Stats = WRCModel.GetStats(this);

        }
    }
}
