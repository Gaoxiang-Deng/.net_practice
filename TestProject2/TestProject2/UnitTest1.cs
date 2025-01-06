using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TestProject2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://gradspace.org";

            try
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string htmlContent = await response.Content.ReadAsStringAsync();
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
                string pageTitle = titleNode != null ? titleNode.InnerText.Trim() : "No title found";

                Console.WriteLine($"Page Title: {pageTitle}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}