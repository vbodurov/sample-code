namespace MiscCodeTests.Extensions
{
    internal static class DoubleExtensions
    {
        internal static double Clamp01(this double n)
        {
            if(n < 0) return 0;
            if(n > 1) return 1;
            return n;
        }
    }
}