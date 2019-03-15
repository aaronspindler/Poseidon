using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Poseidon.Misc;

namespace Poseidon
{
    //TODO : Change all DB operations to use a DynamoDB instance
    /// <summary>
    /// Main database class, used for all database operations
    /// Uses DynamoDB
    /// </summary>
    public static class Database
    {
        private static BasicAWSCredentials _credentials;
        private static AmazonDynamoDBClient _client;
        public static void Initialize()
        {
            //Load the credentials and make a client
            _credentials = new BasicAWSCredentials(Settings.GetAWS_Access_Key(), Settings.GetAWS_Secret_Key());
            //TODO: Make the region endpoint be able to be changed by a client
            _client = new AmazonDynamoDBClient(_credentials, RegionEndpoint.USWest2);

            CreateTables();
        }
        private static void CreateTables()
        {
            CreateTable("ECB_Data");
            CreateTable("BOC_Data");
        }

        private static async Task CreateTable(string tableName)
        {
            var hashKey = "UserID";
            var tableResponse = await _client.ListTablesAsync();
            if (!tableResponse.TableNames.Contains(tableName))
            {
                Logger.WriteLine("Table: " + tableName + " not found, creating now!");
                
                await _client.CreateTableAsync(new CreateTableRequest
                {
                    TableName = tableName,
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 5,
                        WriteCapacityUnits = 5
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = hashKey,
                            KeyType = KeyType.HASH
                        }
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition {AttributeName = hashKey, AttributeType = ScalarAttributeType.S}
                    }
                });

                var isTableAvailable = false;
                while (!isTableAvailable)
                {
                    Thread.Sleep(300);
                    var tableStatus = await _client.DescribeTableAsync(tableName);
                    isTableAvailable = tableStatus.Table.TableStatus == "ACTIVE";
                }
                
                Logger.WriteLine(tableName + " now active!");
            }
        }

        //ECB
        public static void CreateECBEntry()
        {
        }

        public static void ReadECBEntry()
        {
        }

        public static void UpdateECBEntry()
        {
        }

        public static void DeleteECBEntry()
        {
        }

        //BOC
        public static void CreateBOCEntry()
        {
        }

        public static void ReadBOCEntry()
        {
        }

        public static void UpdateBOCEntry()
        {
        }

        public static void DeleteBOCEntry()
        {
        }
    }
}