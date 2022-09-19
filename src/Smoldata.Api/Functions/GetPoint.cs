using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Bogus;
using Smoldata.ExtensionMethods;

namespace Smoldata.Functions
{
    public static class GetPoint
    {
        [FunctionName(nameof(GetPoint))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "point")] HttpRequest req,
            ILogger log)
        {
            var seed = req.GetSeed();
            var count = req.GetCount();
            var points = GeneratePoints(seed, count);

            return new OkObjectResult(points);
        }

        private static List<Models.Point> GeneratePoints(int seed, int count)
        {
            Randomizer.Seed = new Random(seed);
            var faker = new Faker<Models.Point>()
                .RuleFor(u => u.X, f => f.Random.Double(-1, 1))
                .RuleFor(u => u.Y, f => f.Random.Double(-1, 1));

            return faker.Generate(count);
        }
    }
}
