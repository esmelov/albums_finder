using System;
using System.Runtime.InteropServices;

namespace ITunes.Contracts.Extensions
{
    public static class EnumExtension
    {
        public static string WithFirstLowChar<T>(this T value)
            where T : struct, Enum
        {
            var valueStr = value.ToString();

            if (char.IsLetter(valueStr[0]) && char.IsUpper(valueStr[0]))
            {
                var span = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(valueStr.AsSpan()), valueStr.Length);
                span[0] = char.ToLower(span[0]);
            }

            return valueStr;
        }
    }
}
