using SoilParams.SoilEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoilParams.Models
{
    public sealed class ModelSimpleFactory
    {
        public static BaseModel CreateModel(SoilModelEnum modelType)
        {
            BaseModel model = modelType switch
            {
                SoilModelEnum.VG => new VG(modelType),
                SoilModelEnum.BC => new BC(modelType),
                _ => throw new ApplicationException("The WRC model is wrong or not exist in the application"),
            };
            return model;
        }
    }
}
