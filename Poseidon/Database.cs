using System;
using System.Collections.Generic;
using System.Threading;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Poseidon.Misc;
using Poseidon.Models.FiatCurrency.BankOfCanada;
using Poseidon.Models.FiatCurrency.EuropeanCentralBank;
using Poseidon.Models.FiatCurrency.Fixer;

namespace Poseidon
{
    //TODO : Change all DB operations to use a DynamoDB instance
    /// <summary>
    ///     Main database class, used for all database operations
    ///     Uses DynamoDB
    /// </summary>
    public static class Database
    {
        private static BasicAWSCredentials _credentials;
        private static AmazonDynamoDBClient _client;

        /// <summary>
        ///     Initialize the database using credentials from the settings file
        /// </summary>
        public static void Startup()
        {
            //Load the credentials and make a client
            _credentials = new BasicAWSCredentials(Settings.GetAWS_Access_Key(), Settings.GetAWS_Secret_Key());
            _client = new AmazonDynamoDBClient(_credentials, RegionEndpoint.GetBySystemName(Settings.GetAWS_Region_Endpoint()));

            CreateTables();
        }

        private static void CreateTables()
        {
            CreateTable("Poseidon.ECB_Data", "Date");
            CreateTable("Poseidon.BOC_Data", "Date");
            CreateTable("Poseidon.FIXER_Data", "Date");
            CreateTable("Poseidon.KRAKEN_Data", "Date");
        }

        /// <summary>
        ///     Creates a table in AWS DynamoDB using your TableName
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
                    Thread.Sleep(10);
                    var tableStatus = _client.DescribeTableAsync(tableName);
                    isTableAvailable = tableStatus.Result.Table.TableStatus == "ACTIVE";
                }

                Logger.WriteLine(tableName + " now active!");
            }
        }


        /// <summary>
        ///     Delete a table
        /// </summary>
        /// <param name="tableName">Name of table you want to delete</param>
        private static void DeleteTable(string tableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };

            var response = _client.DeleteTableAsync(request);
            Logger.WriteLine("Attempted to Delete Table: " + tableName);
        }

        //ECB
        public static async void CreateECBEntry(EuropeanCentralBankEntry entry)
        {
            var context = new DynamoDBContext(_client);
            await context.SaveAsync(entry);
        }

        public static EuropeanCentralBankEntry ReadECBEntry(string date)
        {
            var context = new DynamoDBContext(_client);
                        var entryRetrieved = context.LoadAsync<EuropeanCentralBankEntry>(date);
                        return entryRetrieved.Result;
        }

        public static async void DeleteECBEntry(string date)
        {
            if (date.Length != 10)
            {
                Logger.WriteLine("Format for \"date\"deleting ECB Entry not correct. Please use format YYYY/MM/DD");
                return;
            }

            var context = new DynamoDBContext(_client);
            await context.DeleteAsync<EuropeanCentralBankEntry>(date);
            Logger.WriteLine("Deleted ECB Data for date: " + date);
        }

        //BOC
        public static async void CreateBOCEntry(BankOfCanadaEntry entry)
        {
            var context = new DynamoDBContext(_client);
            await context.SaveAsync(entry);
        }

        public static BankOfCanadaEntry ReadBOCEntry(string date)
        {
            var context = new DynamoDBContext(_client);
            var entryRetrieved = context.LoadAsync<BankOfCanadaEntry>(date);
            return entryRetrieved.Result;
        }

        public static async void DeleteBOCEntry(string date)
        {
            if (date.Length != 10)
            {
                Logger.WriteLine("Format for \"date\"deleting BOC Entry not correct. Please use format YYYY/MM/DD");
                return;
            }

            var context = new DynamoDBContext(_client);
            await context.DeleteAsync<BankOfCanadaEntry>(date);
            Logger.WriteLine("Deleted BOC Data for date: " + date);
        }

        //FIXER
        public static async void CreateFixerEntry(FixerEntry entry)
        {
            var context = new DynamoDBContext(_client);
            await context.SaveAsync(entry);
        }

        public static FixerEntry ReadFixerEntry(string date)
        {
            var context = new DynamoDBContext(_client);
            var entryRetrieved = context.LoadAsync<FixerEntry>(date);
            return entryRetrieved.Result;
        }

        public static async void DeleteFixerEntry(string date)
        {
            if (date.Length != 10)
            {
                Logger.WriteLine("Format for \"date\"deleting Fixer Entry not correct. Please use format YYYY/MM/DD");
                return;
            }

            var context = new DynamoDBContext(_client);
            await context.DeleteAsync<FixerEntry>(date);
            Logger.WriteLine("Deleted Fixer Data for date: " + date);
        }

        //Kraken
        public static async void CreateKrakenEntry()
        {
        }

        public static void ReadKrakenEntry()
        {
        }

        public static void DeleteKrakenEntry()
        {
        }
    }
}