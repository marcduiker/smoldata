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
    public static class GetTransaction
    {
        [FunctionName(nameof(GetTransaction))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "transaction")] HttpRequest req,
            ILogger log)
        {
            var seed = req.GetSeed();
            var count = req.GetCount();
            var transactions = GenerateTransactions(seed, count);

            return new OkObjectResult(transactions);
        }

        private static List<Models.Transaction> GenerateTransactions(int seed, int count)
        {
            Randomizer.Seed = new Random(seed);
            var faker = new Faker<Models.Transaction>()
                .RuleFor(u => u.BuyerAccount, f => f.Finance.Account())
                .RuleFor(u => u.SellerAccount, f => f.Finance.Account())
                .RuleFor(u => u.Currency, f => f.Finance.Currency().Code)
                .RuleFor(u => u.Amount, f => f.Finance.Amount());

            return faker.Generate(count);
        }
    }
}
