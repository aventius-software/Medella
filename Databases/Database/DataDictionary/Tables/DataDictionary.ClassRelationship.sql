CREATE TABLE [DataDictionary].[ClassRelationship] (
    [ClassName]                      VARCHAR (250) NOT NULL,
    [RelatedToClassName]             VARCHAR (250) NOT NULL,
    [RelatedClassDescription]        VARCHAR (MAX) NULL,
    [RelationshipDescription]        VARCHAR (MAX) NOT NULL,
    [ClassNameAsPascalCase]          VARCHAR (250) NOT NULL,
    [RelatedToClassNameAsPascalCase] VARCHAR (250) NOT NULL,
    [HyperLink]                      VARCHAR (MAX) NOT NULL,
    [KeyValue]                       VARCHAR (500) NULL,
    CONSTRAINT [PK_ClassRelationship_1] PRIMARY KEY CLUSTERED ([ClassName] ASC, [RelatedToClassName] ASC)
);

