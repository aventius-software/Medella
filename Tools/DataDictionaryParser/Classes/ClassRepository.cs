using Microsoft.Data.SqlClient;

namespace DataDictionaryParser.Classes;

internal class ClassRepository
{
    public static async Task SaveClassAttributesAsync(string connectionString, List<ClassAttribute> models, CancellationToken cancellationToken = default)
    {
        string TableName = "DataDictionary.ClassAttribute";
        string TempTableName = "#TempClassAttribute";

        using (SqlConnection connection = new(connectionString))
        {
            // Open the connection
            await connection.OpenAsync(cancellationToken);

            // Drop the temp table if it already exists...
            string dropTable = $"DROP TABLE IF EXISTS {TempTableName}";

            using (SqlCommand command = new(dropTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Create temp table
            string createTempTable = $@"
            CREATE TABLE {TempTableName} (	            
	            [ClassName] [varchar](250) NOT NULL,
	            [AttributeName] [varchar](250) NOT NULL,
	            [AttributeDescription] [varchar](max) NOT NULL,
	            [ClassNameAsPascalCase] [varchar](250) NOT NULL,
	            [AttributeNameAsPascalCase] [varchar](250) NOT NULL,
	            [HyperLink] [varchar](max) NOT NULL,
	            [KeyValue] [varchar](500) NULL
            )
            ";

            using (SqlCommand command = new(createTempTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Insert data into the temp table
            string insertQuery = $@"
            INSERT INTO {TempTableName} (
                ClassName
	            ,AttributeName
	            ,AttributeDescription
	            ,ClassNameAsPascalCase
	            ,AttributeNameAsPascalCase
	            ,HyperLink
	            ,KeyValue
            ) VALUES (
                @ClassName
	            ,@AttributeName
	            ,@AttributeDescription
	            ,@ClassNameAsPascalCase
	            ,@AttributeNameAsPascalCase
	            ,@HyperLink
	            ,@KeyValue
            )
            ";

            using (SqlCommand command = new(insertQuery, connection))
            {
                command.Parameters.Add("@ClassName", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@AttributeName", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@AttributeDescription", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@ClassNameAsPascalCase", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@AttributeNameAsPascalCase", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@HyperLink", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@KeyValue", System.Data.SqlDbType.VarChar);

                foreach (var model in models)
                {
                    command.Parameters["@ClassName"].Value = model.ClassName;
                    command.Parameters["@AttributeName"].Value = model.AttributeName;
                    command.Parameters["@AttributeDescription"].Value = model.AttributeDescription;
                    command.Parameters["@ClassNameAsPascalCase"].Value = model.ClassNameAsPascalCase;
                    command.Parameters["@AttributeNameAsPascalCase"].Value = model.AttributeNameAsPascalCase;
                    command.Parameters["@HyperLink"].Value = model.HyperLink;
                    command.Parameters["@KeyValue"].Value = model.KeyValue;

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }

            // Merge the data in the temp table into the final table
            string mergeIntoTable = $@"
            MERGE
            {TableName} trg

            USING
            {TempTableName} src
            ON
            trg.ClassName = src.ClassName
            AND trg.AttributeName = src.AttributeName

            WHEN NOT MATCHED BY TARGET THEN INSERT (
                ClassName
	            ,AttributeName
	            ,AttributeDescription
	            ,ClassNameAsPascalCase
	            ,AttributeNameAsPascalCase
	            ,HyperLink
	            ,KeyValue
            ) VALUES (
                src.ClassName
	            ,src.AttributeName
	            ,src.AttributeDescription
	            ,src.ClassNameAsPascalCase
	            ,src.AttributeNameAsPascalCase
	            ,src.HyperLink
	            ,src.KeyValue
            );
            ";

            using (SqlCommand command = new(mergeIntoTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Finally drop the temp table
            using (SqlCommand command = new(dropTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }

    public static async Task SaveClassDescriptionAsync(string connectionString, ClassDescription model, CancellationToken cancellationToken = default)
    {
        string TableName = "DataDictionary.ClassDescription";
        string TempTableName = "#TempClassDescription";

        using (SqlConnection connection = new(connectionString))
        {
            // Open the connection
            await connection.OpenAsync(cancellationToken);

            // Drop the temp table if it already exists...
            string dropTable = $"DROP TABLE IF EXISTS {TempTableName}";

            using (SqlCommand command = new(dropTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Create temp table
            string createTempTable = $@"
            CREATE TABLE {TempTableName} (
                [ClassName] [varchar](250) NOT NULL,
                [ClassNameAsPascalCase] [varchar](250) NOT NULL,
                [ShortDescription] [varchar](max) NOT NULL,
                [LongDescription] [varchar](max) NOT NULL,
                [HyperLink] [varchar](max) NOT NULL
            )
            ";

            using (SqlCommand command = new(createTempTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Insert data into the temp table
            string insertQuery = $@"
            INSERT INTO {TempTableName} (
                ClassName
	            ,ClassNameAsPascalCase
	            ,ShortDescription
	            ,HyperLink
	            ,LongDescription
            ) VALUES (
                @ClassName
	            ,@ClassNameAsPascalCase
	            ,@ShortDescription
	            ,@HyperLink
	            ,@LongDescription
            )
            ";

            using (SqlCommand command = new(insertQuery, connection))
            {
                command.Parameters.Add("@ClassName", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@ClassNameAsPascalCase", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@ShortDescription", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@LongDescription", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@HyperLink", System.Data.SqlDbType.VarChar);

                command.Parameters["@ClassName"].Value = model.ClassName;
                command.Parameters["@ClassNameAsPascalCase"].Value = model.ClassNameAsPascalCase;
                command.Parameters["@ShortDescription"].Value = model.ShortDescription;
                command.Parameters["@LongDescription"].Value = model.LongDescription;
                command.Parameters["@HyperLink"].Value = model.HyperLink;

                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Merge the data in the temp table into the final table
            string mergeIntoTable = $@"
            MERGE
            {TableName} trg

            USING
            {TempTableName} src
            ON
            trg.ClassName = src.ClassName
            
            WHEN NOT MATCHED BY TARGET THEN INSERT (
                ClassName
	            ,ClassNameAsPascalCase
	            ,ShortDescription
	            ,HyperLink
	            ,LongDescription
            ) VALUES (
                src.ClassName
	            ,src.ClassNameAsPascalCase
	            ,src.ShortDescription
	            ,src.HyperLink
	            ,src.LongDescription
            );
            ";

            using (SqlCommand command = new(mergeIntoTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Finally drop the temp table
            using (SqlCommand command = new(dropTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }

    public static async Task SaveClassRelationshipsAsync(string connectionString, List<ClassRelationship> models, CancellationToken cancellationToken = default)
    {
        string TableName = "DataDictionary.ClassRelationship";
        string TempTableName = "#TempClassRelationship";

        using (SqlConnection connection = new(connectionString))
        {
            // Open the connection
            await connection.OpenAsync(cancellationToken);

            // Now drop the temp table if it exists (which is shouldn't)
            string dropTable = $"DROP TABLE IF EXISTS {TempTableName}";

            using (SqlCommand command = new(dropTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Create temp table
            string createTempTable = $@"
            CREATE TABLE {TempTableName} (	          
                [ClassName] [varchar](250) NOT NULL,
                [RelatedToClassName] [varchar](250) NOT NULL,
                [RelatedClassDescription] [varchar](max) NULL,
                [RelationshipDescription] [varchar](max) NOT NULL,
                [ClassNameAsPascalCase] [varchar](250) NOT NULL,
                [RelatedToClassNameAsPascalCase] [varchar](250) NOT NULL,
                [HyperLink] [varchar](max) NOT NULL,
                [KeyValue] [varchar](500) NULL
            )
            ";

            using (SqlCommand command = new(createTempTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Insert data into the temp table
            string insertQuery = $@"
            INSERT INTO {TempTableName} (
                ClassName 
                ,RelatedToClassName
                ,RelatedClassDescription
                ,RelationshipDescription
                ,ClassNameAsPascalCase
                ,RelatedToClassNameAsPascalCase
                ,HyperLink
                ,KeyValue
            ) VALUES (
                @ClassName 
                ,@RelatedToClassName
                ,@RelatedClassDescription
                ,@RelationshipDescription
                ,@ClassNameAsPascalCase
                ,@RelatedToClassNameAsPascalCase
                ,@HyperLink
                ,@KeyValue
            )
            ";

            using (SqlCommand command = new(insertQuery, connection))
            {
                command.Parameters.Add("@ClassName", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@RelatedToClassName", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@RelatedClassDescription", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@RelationshipDescription", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@ClassNameAsPascalCase", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@RelatedToClassNameAsPascalCase", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@HyperLink", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@KeyValue", System.Data.SqlDbType.VarChar);

                foreach (var model in models)
                {
                    command.Parameters["@ClassName"].Value = model.ClassName;
                    command.Parameters["@RelatedToClassName"].Value = model.RelatedToClassName;
                    command.Parameters["@RelatedClassDescription"].Value = model.RelatedClassDescription;
                    command.Parameters["@RelationshipDescription"].Value = model.RelationshipDescription;
                    command.Parameters["@ClassNameAsPascalCase"].Value = model.ClassNameAsPascalCase;
                    command.Parameters["@RelatedToClassNameAsPascalCase"].Value = model.RelatedToClassNameAsPascalCase;
                    command.Parameters["@HyperLink"].Value = model.HyperLink;
                    command.Parameters["@KeyValue"].Value = model.KeyValue;

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }

            // Merge the data in the temp table into the final table
            string mergeIntoTable = $@"
            MERGE
            {TableName} trg

            USING
            {TempTableName} src
            ON
            trg.ClassName = src.ClassName
            AND trg.RelatedToClassName = src.RelatedToClassName            

            WHEN NOT MATCHED BY TARGET THEN INSERT (
                ClassName 
                ,RelatedToClassName
                ,RelatedClassDescription
                ,RelationshipDescription
                ,ClassNameAsPascalCase
                ,RelatedToClassNameAsPascalCase
                ,HyperLink
                ,KeyValue
            ) VALUES (
                src.ClassName 
                ,src.RelatedToClassName
                ,src.RelatedClassDescription
                ,src.RelationshipDescription
                ,src.ClassNameAsPascalCase
                ,src.RelatedToClassNameAsPascalCase
                ,src.HyperLink
                ,src.KeyValue
            );
            ";

            using (SqlCommand command = new(mergeIntoTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Finally drop the temp table
            using (SqlCommand command = new(dropTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }

    public static async Task SaveClassUsageAsync(string connectionString, List<ClassUsage> models, CancellationToken cancellationToken = default)
    {
        string TableName = "DataDictionary.ClassUsage";
        string TempTableName = "#TempClassUsage";

        using (SqlConnection connection = new(connectionString))
        {
            // Open the connection
            await connection.OpenAsync(cancellationToken);

            // Now drop the temp table if it exists (which is shouldn't)
            string dropTable = $"DROP TABLE IF EXISTS {TempTableName}";

            using (SqlCommand command = new(dropTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Create temp table
            string createTempTable = $@"
            CREATE TABLE {TempTableName} (
	            [ClassName] [varchar](250) NOT NULL,
	            [UsageType] [varchar](50) NOT NULL,
	            [LinkText] [varchar](250) NOT NULL,
	            [LinkDescription] [varchar](max) NULL,
                [ClassNameAsPascalCase] [varchar](250) NOT NULL,
	            [HyperLink] [varchar](max) NOT NULL,
	            [HowUsed] [varchar](max) NOT NULL
            )
            ";

            using (SqlCommand command = new(createTempTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Insert data into the temp table
            string insertQuery = $@"
            INSERT INTO {TempTableName} (
                ClassName 
                ,UsageType
                ,LinkText
                ,LinkDescription
                ,ClassNameAsPascalCase
                ,HyperLink
                ,HowUsed
            ) VALUES (
                @ClassName 
                ,@UsageType
                ,@LinkText
                ,@LinkDescription
                ,@ClassNameAsPascalCase
                ,@HyperLink
                ,@HowUsed
            )
            ";

            using (SqlCommand command = new(insertQuery, connection))
            {
                command.Parameters.Add("@ClassName", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@UsageType", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@LinkText", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@LinkDescription", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@ClassNameAsPascalCase", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@HyperLink", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@HowUsed", System.Data.SqlDbType.VarChar);

                foreach (var model in models)
                {
                    command.Parameters["@ClassName"].Value = model.ClassName;
                    command.Parameters["@UsageType"].Value = model.UsageType;
                    command.Parameters["@LinkText"].Value = model.LinkText;
                    command.Parameters["@LinkDescription"].Value = model.LinkDescription;
                    command.Parameters["@ClassNameAsPascalCase"].Value = model.ClassNameAsPascalCase;
                    command.Parameters["@HyperLink"].Value = model.HyperLink;
                    command.Parameters["@HowUsed"].Value = model.HowUsed;

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }

            // Merge the data in the temp table into the final table
            string mergeIntoTable = $@"
            MERGE
            {TableName} trg

            USING
            {TempTableName} src
            ON
            trg.ClassName = src.ClassName
            AND trg.UsageType = src.UsageType
            AND trg.LinkText = src.LinkText

            WHEN NOT MATCHED BY TARGET THEN INSERT (
                ClassName
                ,UsageType
                ,LinkText
                ,LinkDescription
                ,ClassNameAsPascalCase
                ,HyperLink
                ,HowUsed
            ) VALUES (
                src.ClassName
                ,src.UsageType
                ,src.LinkText
                ,src.LinkDescription
                ,src.ClassNameAsPascalCase
                ,src.HyperLink
                ,src.HowUsed
            );
            ";

            using (SqlCommand command = new(mergeIntoTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            // Finally drop the temp table
            using (SqlCommand command = new(dropTable, connection))
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }
}
