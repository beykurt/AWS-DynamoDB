using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDb.Libs.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DynamoDb.Libs.Implements
{
    public class DeleteTable : IDeleteTable
    {
        private readonly IAmazonDynamoDB _dynamoClient;

        public DeleteTable(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoClient = dynamoDbClient;
        }

        public async Task<DeleteTableResponse> ExecuteTableDelete(string tableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };
            var response = await _dynamoClient.DeleteTableAsync(request);

            return response;
        }
    }
}
