using HtmlAgilityPack;
using System.Text;

namespace DataDictionaryParser.Shared;

internal static class NHSDataDictionaryHtmlParser
{
    private const string BaseUrl = "https://www.datadictionary.nhs.uk";

    public static async Task<HtmlDocument> FetchDocumentAsync(string url)
    {
        var web = new HtmlWeb
        {
            OverrideEncoding = Encoding.UTF8,
            UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0"
        };

        return await web.LoadFromWebAsync(url);
    }

    public static string BuildUrl(string itemName, string itemTypePlural)
    {
        return $"{BaseUrl}/{itemTypePlural}/{itemName}.html";
    }
}
