using SoilParams.SoilEnums;
using System.ComponentModel;
using System.Linq;

namespace SoilParams.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this SoilModelEnum model )
        {
            var attribs = model.GetType().GetMember(model.ToString()).FirstOrDefault().GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attribs == null)
            {
                return model.ToString();
            }
            return ((DescriptionAttribute)attribs.ElementAt(0)).Description;
        }
    }
}
