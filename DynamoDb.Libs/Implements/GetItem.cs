using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using DynamoDb.Libs.Interfaces;
using DynamoDb.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDb.Libs.Implements
{
    public class GetItem : IGetItem
    {
        private readonly IAmazonDynamoDB _dynamoClient;

        public GetItem(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }

        public async Task<DynamoTableItems> GetItems(int? id, string tableName)
        {
            var queryRequest = RequestBuilder(id, tableName);

            var result = await ScanAsync(queryRequest);

            return new DynamoTableItems
            {
                Items = result.Items.Select(Map).ToList()
            };
        }

        private async Task<ScanResponse> ScanAsync(ScanRequest request)
        {
            var response = await _dynamoClient.ScanAsync(request);

            return response;
        }

        private Item Map(Dictionary<string, AttributeValue> result)
        {
            return new Item
            {
                Id = Convert.ToInt32(result["Id"].N),
                ReplyDateTime = result["ReplyDateTime"].N,
                Price = Convert.ToDouble(result["Price"].N)
            };
        }

        public ScanRequest RequestBuilder(int? id, string tableName)
        {
            ScanRequest request;

            if (id.HasValue == false)
            {
                request = new ScanRequest
                {
                    TableName = tableName
                };
                return request;
            }

            request = new ScanRequest
            {
                TableName = tableName,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {  ":v_Id", new AttributeValue { N = id.ToString()}}
                },
                FilterExpression = "Id = :v_Id",
                ProjectionExpression = "Id, ReplyDateTime, Price"
            };

            return request;
        }
    }
}
