CREATE TABLE [DataDictionary].[ClassDescription] (
    [ClassName]             VARCHAR (250) NOT NULL,
    [ClassNameAsPascalCase] VARCHAR (250) NOT NULL,
    [ShortDescription]      VARCHAR (MAX) NOT NULL,
    [LongDescription]       VARCHAR (MAX) NOT NULL,
    [HyperLink]             VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ClassDescription_1] PRIMARY KEY CLUSTERED ([ClassName] ASC)
);

