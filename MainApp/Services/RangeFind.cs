

namespace JeeshantPatelAssignment3.Services
{
    public static class RangeFind
    {
        public static bool StartsWith(this string val, params string[] arg)
        {
            return arg.Any(prefix => val.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }
        //public static bool Ifitsstartwith(this string value, params string[] argue)
        //{
        //    return argue.Any(prefix => val.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        //}
    }
}
