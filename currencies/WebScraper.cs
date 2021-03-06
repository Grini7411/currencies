using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace currencies
{
    public static class WebScraper
    {
        public static async Task<List<Currency>> ScrapeUrl(string url)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = await web.LoadFromWebAsync(url);
                HtmlNodeCollection tableRows = doc.DocumentNode.SelectNodes("//table/tbody/tr");
                List<Currency> currencies = new List<Currency>();

                string[] allowedCurrencies = new string[] { "EUR-USD", "EUR-JPY", "EUR-GBP", "USD-ILS" };
                foreach (HtmlNode row in tableRows)
                {
                    string[] split = row.InnerText.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (allowedCurrencies.Contains(split[0]))
                    {
                        Currency currency = new Currency(split[0], float.Parse(split[1]), DateTime.Parse(split[split.Length - 2] + split[split.Length - 1]));
                        currencies.Add(currency);
                        //currency.WriteToFile();
                    }
                }
                return currencies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
