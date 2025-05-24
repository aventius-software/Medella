namespace DataDictionaryParser.Classes;

internal class ClassModel
{
    public ClassDescription ClassDescription;
    public List<ClassAttribute> ClassAttributes = [];
    public List<ClassRelationship> ClassRelationships = [];
    public List<ClassUsage> ClassUsage = [];
}
