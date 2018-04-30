using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Pim.Extensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// 调用 Math.Round 方法后，再转为int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int RoundToInt(this float val)
        {
            var obj = Math.Round(val);
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 确保小数位（四舍五入）。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float RoundDigits(this float val, int digits = 6)
        {
            return Convert.ToSingle(Math.Round(val, digits, MidpointRounding.AwayFromZero));
        }
    }
}
