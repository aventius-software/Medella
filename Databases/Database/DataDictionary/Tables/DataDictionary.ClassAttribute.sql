CREATE TABLE [DataDictionary].[ClassAttribute] (
    [ClassName]                 VARCHAR (250) NOT NULL,
    [AttributeName]             VARCHAR (250) NOT NULL,
    [AttributeDescription]      VARCHAR (MAX) NOT NULL,
    [ClassNameAsPascalCase]     VARCHAR (250) NOT NULL,
    [AttributeNameAsPascalCase] VARCHAR (250) NOT NULL,
    [HyperLink]                 VARCHAR (MAX) NOT NULL,
    [KeyValue]                  VARCHAR (500) NULL,
    CONSTRAINT [PK_ClassAttribute_1] PRIMARY KEY CLUSTERED ([ClassName] ASC, [AttributeName] ASC)
);

