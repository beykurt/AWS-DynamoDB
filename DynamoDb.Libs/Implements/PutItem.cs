using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using DynamoDb.Libs.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDb.Libs.Implements
{
    public class PutItem : IPutItem
    {
        private readonly IAmazonDynamoDB _dynamoClient;

        public PutItem(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }

        public async Task AddNewEntry(int id, string replyDateTime, double price, string tableName)
        {
            var queryRequest = RequestBuilder(id, replyDateTime, price, tableName);

            await PutItemAsync(queryRequest);
        }

        private async Task PutItemAsync(PutItemRequest request)
        {
            await _dynamoClient.PutItemAsync(request);
        }

        public PutItemRequest RequestBuilder(int id, string replyDateTime, double price, string tableName)
        {
            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue{N = id.ToString()}},
                    {"ReplyDateTime", new AttributeValue{N = replyDateTime}},
                    {"Price", new AttributeValue { N = price.ToString(CultureInfo.InvariantCulture)}}
                }
            };

            return request;
        }
    }
}
