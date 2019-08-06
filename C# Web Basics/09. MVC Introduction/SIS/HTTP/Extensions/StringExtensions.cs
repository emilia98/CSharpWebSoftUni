namespace HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }
    }
}
