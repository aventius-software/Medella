using DataDictionaryParser.Classes;

namespace DataDictionaryParser;

/// <summary>
/// A small utility to 'import' class and reference data (e.g. national codes for lookups)
/// from the NHS Data Dictionary website. This is just intended to be a helpful utility to get
/// national codes and descriptions from the data dictionary to help build 'seeding' data for 
/// reference tables in the main databases. This is not intended to be a full scale web scraping 
/// tool, so please do not spam the site with many requests in a short period. Use at your own 
/// risk as the authors of this code will not accept any responsibility for its use/abuse
/// </summary>
internal class Program
{
    private const string ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Medella;Integrated Security=True;Pooling=False;Connect Timeout=30";

    static async Task Main(string[] args)
    {
        // Get requested class data
        var classData = await ClassParser.ParseAsync("patient");
        
        // Save to database
        await ClassRepository.SaveClassAttributesAsync(ConnectionString, classData.ClassAttributes);
        await ClassRepository.SaveClassDescriptionAsync(ConnectionString, classData.ClassDescription);
        await ClassRepository.SaveClassRelationshipsAsync(ConnectionString, classData.ClassRelationships);
        await ClassRepository.SaveClassUsageAsync(ConnectionString, classData.ClassUsage);
    }
}
