using System.Text.RegularExpressions;

namespace MessageLibrary
{
    public class Quarantine : Urls
    {
        // This method used to search a method to see it contains urls. For example, www.bbcnews.co.uk becomes <URL Quarantined>
        public string quarantine(string message)
        {
            // Regular expression used to check the message for urls
            Regex regx = new Regex("((http://|www\\.)([A-Z0-9.-:]{1,})\\.[0-9A-Z?;~&#=\\-_\\./]{2,})", RegexOptions.IgnoreCase);

            // Checks the message for urls 
            MatchCollection matches = regx.Matches(message);

            // Replaces any urls found with <URL Quarantined>
            foreach (Match match in matches)
            {
                message = message.Replace(match.Value, "<URL Quarantined>");
            }
            // Returns the message
            return message;
        }
    }
}
