using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoilParams.SoilEnums
{
    public enum SoilModelEnum
    {
        [Description("van Genuchten")]
        VG,
        [Description("Brooks & Corey")]
        BC
    }
}
