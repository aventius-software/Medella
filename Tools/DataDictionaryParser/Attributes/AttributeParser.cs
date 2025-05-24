using DataDictionaryParser.Classes;
using DataDictionaryParser.Shared;

namespace DataDictionaryParser.Attributes;

internal class AttributeParser
{    
    //public static List<ClassAttribute> Parse(string className)
    //{
    //    // Build the URL to the page
    //    var hyperLink = NHSDataDictionaryHtmlParser.BuildUrl(className, "classes");

    //    // Load the html
    //    var document = NHSDataDictionaryHtmlParser.FetchDocument(hyperLink);

    //    // Parse the html to find the attributes for this class
    //    var data = new List<ClassAttribute>();
    //    var query = "//*[substring(@id, string-length(@id) - string-length('.attributes') + 1) = '.attributes']//tr";
    //    var rows = document.DocumentNode.SelectNodes(query);

    //    if (rows is not null)
    //    {
    //        foreach (var row in rows)
    //        {
    //            var cells = row.SelectNodes("td");

    //            if (cells is not null)
    //            {
    //                var rowData = new ClassAttribute
    //                {
    //                    KeyValue = cells[0].InnerText.Trim(),
    //                    AttributeName = cells[1].InnerText.Trim(),
    //                    HyperLink = cells[1].SelectSingleNode("a").Attributes["href"].Value,
    //                    AttributeDescription = cells[1].SelectSingleNode("a").Attributes["title"].Value
    //                };

    //                data.Add(rowData);
    //            }
    //        }
    //    }
    //}
}
