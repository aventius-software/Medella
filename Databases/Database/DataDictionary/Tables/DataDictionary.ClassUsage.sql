CREATE TABLE [DataDictionary].[ClassUsage] (
    [ClassName]             VARCHAR (250) NOT NULL,
    [UsageType]             VARCHAR (50)  NOT NULL,
    [LinkText]              VARCHAR (250) NOT NULL,
    [LinkDescription]       VARCHAR (MAX) NULL,
    [ClassNameAsPascalCase] VARCHAR (250) NOT NULL,
    [HyperLink]             VARCHAR (MAX) NOT NULL,
    [HowUsed]               VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ClassUsage] PRIMARY KEY CLUSTERED ([ClassName] ASC, [UsageType] ASC, [LinkText] ASC)
);

