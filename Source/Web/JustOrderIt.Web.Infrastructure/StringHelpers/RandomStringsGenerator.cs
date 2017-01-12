namespace JustOrderIt.Web.Infrastructure.StringHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class RandomStringsGenerator
    {
        private static Random random = new Random();

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string LoremIpsum(int minWords, int maxWords, int minSentences, int maxSentences/*, int numParagraphs*/, int minLength, int maxLength)
        {
            var words = new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            var rand = new Random();
            int numSentences = rand.Next(minSentences, maxSentences + 1);
            int numWords = rand.Next(minWords, maxWords + 1);

            StringBuilder result = new StringBuilder();

            for (int s = 0; s < numSentences; s++)
            {
                for (int w = 0; w < numWords; w++)
                {
                    if (w > 0) { result.Append(" "); }
                    result.Append(words[rand.Next(words.Length)]);
                }

                result.Append(". ");
            }

            while (result.Length < minLength)
            {
                result.Append(result);
            }

            if (result.Length > maxLength)
            {
                result.Remove(maxLength - 1, result.Length - maxLength);
            }

            return result.ToString();
        }
    }
}
