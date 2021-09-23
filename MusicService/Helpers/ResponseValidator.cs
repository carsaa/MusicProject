namespace MusicService.Helpers
{
    public static class ResponseValidator
    {
        public static bool IsMatch(string searchString, string responseString)
        {
            return FormatString(searchString) == FormatString(responseString);
        }

        private static string FormatString(string stringToFormat)
        {
            stringToFormat = stringToFormat.Replace(".", string.Empty);
            stringToFormat = stringToFormat.Replace(" ", string.Empty);
            return stringToFormat.ToLower();
        }
    }
}
