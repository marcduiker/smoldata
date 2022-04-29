using System;
using Microsoft.AspNetCore.Http;

namespace Smoldata.ExtensionMethods
{
    public static class HttpRequestExtensionMethods
    {
        public static int GetSeed(this HttpRequest req)
        {
            string seedString = req.Query["seed"];
            int seed = DateTime.UtcNow.Hour;
            if (int.TryParse(seedString, out int parsedSeed))
            {
                seed = parsedSeed;
            }

            return seed;
        }

        public static int GetCount(this HttpRequest req)
        {
            string countString = req.Query["count"];
            int count = 3;
            if (int.TryParse(countString, out int parsedCount))
            {
                if (count > 0 || count < 11 ) 
                {
                    count = parsedCount;
                }
            }

            return count;
        }
    }
}
