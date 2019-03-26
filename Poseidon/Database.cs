using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Poseidon.Misc;
using Poseidon.Models.FiatCurrency.BankOfCanada;
using Poseidon.Models.FiatCurrency.EuropeanCentralBank;

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

        /// <summary>
        /// Initialize the database using credentials from the settings file
        /// </summary>
        public static void Startup()
        {
            //Load the credentials and make a client
            _credentials = new BasicAWSCredentials(Settings.GetAWS_Access_Key(), Settings.GetAWS_Secret_Key());
            //TODO: Make the region endpoint be able to be changed by a client
            _client = new AmazonDynamoDBClient(_credentials, RegionEndpoint.USWest2);

            CreateTables();
        }

        private static void CreateTables()
        {
            CreateTable("ECB_Data", "EntryID");
            CreateTable("BOC_Data", "EntryID");
            CreateTable("Kraken_Data", "EntryID");
        }

        /// <summary>
        /// Creates a table in AWS DynamoDB using your TableName
        /// </summary>
        /// <param name="tableName">Table Name for the Table in DynamoDB</param>
        /// <returns></returns>
        private static void CreateTable(string tableName, string hashKey)
        {
            var tableResponse = _client.ListTablesAsync();
            if (!tableResponse.Result.TableNames.Contains(tableName))
            {
                Logger.WriteLine("Table: " + tableName + " not found, creating now!");

                _client.CreateTableAsync(new CreateTableRequest
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
                    var tableStatus = _client.DescribeTableAsync(tableName);
                    isTableAvailable = tableStatus.Result.Table.TableStatus == "ACTIVE";
                }

                Logger.WriteLine(tableName + " now active!");
            }
        }
        

        /// <summary>
        /// Delete a table
        /// </summary>
        /// <param name="tableName">Name of table you want to delete</param>
        private static void DeleteTable(string tableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };

            var response = _client.DeleteTableAsync(request);
            Logger.WriteLine("Attempted to Delete Table: " +  tableName);
        }

        //ECB
        public static async void CreateECBEntry(EuropeanCentralBankEntry entry)
        {
            var context = new DynamoDBContext(_client);
            await context.SaveAsync(entry);
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
        public static async void CreateBOCEntry(BankOfCanadaEntry entry)
        {
             var context = new DynamoDBContext(_client);
             await context.SaveAsync(entry);
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
        
        //Kraken
        public static async void CreateKrakenEntry()
        {
            
        }

        public static void ReadKrakenEntry()
        {
            
        }

        public static void UpdateKrakenEntry()
        {
            
        }

        public static void DeleteKrakenEntry()
        {
            
        }
    }
}