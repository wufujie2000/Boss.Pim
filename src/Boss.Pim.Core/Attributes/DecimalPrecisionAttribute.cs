using System;

namespace Boss.Pim.Attributes
{
    /// <summary>
    /// <para>自定义Decimal类型的精度属性</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {
        public DecimalPrecisionAttribute(byte scale) : this(18, scale)
        {

        }

        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }
        public byte Precision { get; set; }
        public byte Scale { get; set; }
    }
}