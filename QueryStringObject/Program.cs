using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace QueryStringObject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            dynamic ds = new ExpandoObject();
            ds.countryId = "US";
            ds.orgUnitId = "1316464";
            ds.expiryDate = "*";

            var queryManager = new QueryManager();
            var queryString = await queryManager.ToQueryStringAsync(ds);

            Console.WriteLine("string");
            Console.WriteLine(queryString);
        }
    }
}
