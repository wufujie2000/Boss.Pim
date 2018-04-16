using System;
using System.Globalization;

namespace Boss.Pim.EntityFramework.Extensions
{
    public static class StringExtensions
    {
        public static string ToPluralize(this string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            return System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(new CultureInfo("en")).Pluralize(name);
        }
    }
}
