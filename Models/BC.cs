using SoilParams.SoilEnums;
using SoilParams.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoilParams.Models
{
    class BC : BaseModel
    {
        public BC(SoilModelEnum model)
        {
            Name = model.GetDescription();
        }
    }
}
