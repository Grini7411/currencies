using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace currencies
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
             * I'v chose to use file to store all my data, because the number of
             * results is relatively small, so I don't need index and queries in order
             * to grab my results. 
             * Also, openning connections to db and closing them, costs time that saved due to read
             * and write to a locally saved file.
             */

            FileService.RemoveTextFromFile();

            string url = "https://www.bloomberg.com/markets/currencies";
            string urlForUSDToILS = "https://www.bloomberg.com/markets/currencies/europe-africa-middle-east";

            List<Currency> list1 = await WebScraper.ScrapeUrl(url);
            List<Currency> list2 = await WebScraper.ScrapeUrl(urlForUSDToILS);
            List<Currency> allCurrencies = list1.Concat(list2).Distinct(new CurrencyComparer()).ToList();
            FileService.WriteToFile(allCurrencies);
            Console.WriteLine("saved to file");
            FileService.ReadFromFile();
        }

        class CurrencyComparer : IEqualityComparer<Currency>
        {
            public bool Equals([AllowNull] Currency x, [AllowNull] Currency y)
            {
                if (Object.ReferenceEquals(x, y)) return true;
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;
                return x.Name == y.Name;
            }

            public int GetHashCode([DisallowNull] Currency obj)
            {
                if(Object.ReferenceEquals(obj, null)) return 0;
                int hashProductName = obj.Name == null ? 0 : obj.Name.GetHashCode();
                return hashProductName;
            }
        }


    }
}
