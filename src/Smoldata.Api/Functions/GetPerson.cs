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
    public static class GetPerson
    {
        [FunctionName(nameof(GetPerson))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "person")] HttpRequest req,
            ILogger log)
        {
            var seed = req.GetSeed();
            var count = req.GetCount();
            var persons = GeneratePersons(seed, count);

            return new OkObjectResult(persons);
        }

        private static List<Models.Person> GeneratePersons(int seed, int count)
        {
            Randomizer.Seed = new Random(seed);
            var faker = new Faker<Models.Person>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Age, f => f.Random.Number(99));

            return faker.Generate(count);
        }
    }
}
