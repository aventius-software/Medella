using CaseConverter;
using DataDictionaryParser.Shared;
using HtmlAgilityPack;

namespace DataDictionaryParser.Classes;

internal class ClassParser
{
    public static async Task<ClassModel> ParseAsync(string className)
    {
        // Build the URL to the page
        var hyperLink = NHSDataDictionaryHtmlParser.BuildUrl(className, "classes");

        // Load the html
        var document = await NHSDataDictionaryHtmlParser.FetchDocumentAsync(hyperLink);

        // Parse the html to find the descriptions
        var description = FindDescriptions(document.DocumentNode);
        description.ClassName = className;
        description.HyperLink = hyperLink;
        description.ClassNameAsPascalCase = className.ToPascalCase();

        // Find the classes attributes
        var attributes = FindAttributes(document.DocumentNode, className);

        // Find all the other classes this class has relationships with
        var relationships = FindRelationships(document.DocumentNode, className);

        // Find entries for 'How Used'
        var classUsage = FindUsage(document.DocumentNode, className);

        return new ClassModel
        {
            ClassDescription = description,
            ClassAttributes = attributes,
            ClassRelationships = relationships,
            ClassUsage = classUsage
        };
    }

    private static ClassDescription FindDescriptions(HtmlNode documentNode)
    {
        var data = new ClassDescription();
        var query = "//p[contains(@class, 'shortdesc')]";
        var shortDescription = documentNode.SelectSingleNode(query);

        if (shortDescription is not null)
        {
            data.ShortDescription = shortDescription.InnerText.Trim();
        }

        query = "//article[substring(@id, string-length(@id) - string-length('.description') + 1) = '.description']//p";
        var longDescription = documentNode.SelectNodes(query);

        if (longDescription is not null)
        {
            var paragraphs = new List<string>();

            foreach (var paragraph in longDescription)
            {
                paragraphs.Add(paragraph.InnerText);
            }

            data.LongDescription = string.Join(" ", paragraphs);
        }

        return data;
    }

    private static List<ClassAttribute> FindAttributes(HtmlNode documentNode, string parentClassName)
    {
        var data = new List<ClassAttribute>();
        var query = "//article[substring(@id, string-length(@id) - string-length('.attributes') + 1) = '.attributes']//tr";
        var tableRows = documentNode.SelectNodes(query);

        if (tableRows is not null)
        {
            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");

                if (cells is not null)
                {
                    var rowData = new ClassAttribute
                    {
                        ClassName = parentClassName,
                        KeyValue = cells[0].InnerText.Trim(),
                        AttributeName = cells[1].InnerText.Trim(),
                        HyperLink = cells[1].SelectSingleNode("a").Attributes["href"].Value,
                        AttributeDescription = cells[1].SelectSingleNode("a").Attributes["title"].Value,
                        ClassNameAsPascalCase = parentClassName.ToPascalCase(),
                        AttributeNameAsPascalCase = cells[1].InnerText.Trim().ToPascalCase()
                    };

                    data.Add(rowData);
                }
            }
        }

        return data;
    }

    private static List<ClassRelationship> FindRelationships(HtmlNode documentNode, string parentClassName)
    {
        var data = new List<ClassRelationship>();
        var query = "//article[substring(@id, string-length(@id) - string-length('.relationships') + 1) = '.relationships']//tr";
        var tableRows = documentNode.SelectNodes(query);

        if (tableRows is not null)
        {
            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");

                if (cells is not null)
                {
                    var rowData = new ClassRelationship
                    {
                        ClassName = parentClassName,
                        KeyValue = cells[0].InnerText.Trim(),
                        RelationshipDescription = cells[1].InnerText.Trim(),
                        RelatedToClassName = cells[2].InnerText.Trim(),
                        RelatedClassDescription = cells[2].SelectSingleNode("a").Attributes["title"].Value,
                        HyperLink = cells[2].SelectSingleNode("a").Attributes["href"].Value,
                        ClassNameAsPascalCase = parentClassName.ToPascalCase(),
                        RelatedToClassNameAsPascalCase = cells[2].InnerText.Trim().ToPascalCase()
                    };

                    data.Add(rowData);
                }
            }
        }

        return data;
    }

    private static List<ClassUsage> FindUsage(HtmlNode documentNode, string parentClassName)
    {
        var data = new List<ClassUsage>();
        var query = "//article[substring(@id, string-length(@id) - string-length('.where_used') + 1) = '.where_used']//tr";
        var tableRows = documentNode.SelectNodes(query);

        if (tableRows is not null)
        {
            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");

                if (cells is not null)
                {
                    var linkDescription = string.Empty;

                    if (cells[1].SelectSingleNode("a").Attributes.Where(x => x.Name == "title").Any())
                    {
                        linkDescription = cells[1].SelectSingleNode("a").Attributes["title"].Value;
                    }

                    var rowData = new ClassUsage
                    {
                        ClassName = parentClassName,
                        UsageType = cells[0].InnerText.Trim(),
                        LinkText = cells[1].InnerText.Trim(),
                        LinkDescription = linkDescription,
                        HyperLink = cells[1].SelectSingleNode("a").Attributes["href"].Value,
                        HowUsed = cells[2].InnerText.Trim(),
                        ClassNameAsPascalCase = parentClassName.ToPascalCase()
                    };

                    data.Add(rowData);
                }
            }
        }

        return data;
    }
}
