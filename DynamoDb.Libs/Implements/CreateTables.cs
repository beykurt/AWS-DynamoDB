using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDb.Libs.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace DynamoDb.Libs.Implements
{
    public class CreateTables : ICreateTable
    {
        private readonly IAmazonDynamoDB _dynamoClient;

        public CreateTables(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }

        public void CreateDynamoDbTable(string tableName)
        {
            try
            {
                CreateTempTable(tableName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreateTempTable(string tableName)
        {
            Console.WriteLine("Creating Table");

            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "ReplyDateTime",
                        AttributeType = "N"
                    },
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH" //Partition Key
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "ReplyDateTime",
                        KeyType = "Range" //Sort Key
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                },
                TableName = tableName
            };

            var response = _dynamoClient.CreateTableAsync(request);

            WaitUntilTableReady(tableName);
        }

        public void WaitUntilTableReady(string tableName)
        {
            string status = null;

            do
            {
                Thread.Sleep(5000);
                try
                {
                    var res = _dynamoClient.DescribeTableAsync(new DescribeTableRequest
                    {
                        TableName = tableName
                    });
                    status = res.Result.Table.TableStatus;
                }
                catch (ResourceNotFoundException)
                {
                }
            } while (status != "ACTIVE");
            {
                Console.WriteLine("Table Created Successfully");
            };
        }
    }
}
