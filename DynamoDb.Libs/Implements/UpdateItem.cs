using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDb.Libs.Interfaces;
using DynamoDb.Libs.Models;

namespace DynamoDb.Libs.Implements
{
    public class UpdateItem : IUpdateItem
    {
        private readonly IAmazonDynamoDB _dynamoClient;
        private IGetItem _getItem;
        public UpdateItem(IGetItem getItem, IAmazonDynamoDB dynamoClient)
        {
            _getItem = getItem;
            _dynamoClient = dynamoClient;
        }

        public async Task<Item> Update(int id, double price, string tableName)
        {
            var response = await _getItem.GetItems(id, tableName);
            var currentPrice = response.Items.Select(p => p.Price).FirstOrDefault();
            var replyDateTime = response.Items.Select(p => p.ReplyDateTime).FirstOrDefault();
            var request = RequestBuilder(id, price, currentPrice, replyDateTime, tableName);
            var result = await UpdateItemAsync(request);

            return new Item
            {
                Id = Convert.ToInt32(result.Attributes["Id"].N),
                ReplyDateTime = result.Attributes["ReplyDateTime"].N,
                Price = Convert.ToDouble(result.Attributes["Price"].N)
            };
        }

        private async Task<UpdateItemResponse> UpdateItemAsync(UpdateItemRequest request)
        {
            var response = await _dynamoClient.UpdateItemAsync(request);

            return response;
        }

        public UpdateItemRequest RequestBuilder(int id, double price, double currentPrice, string replyDateTime, string tableName)
        {
            var request = new UpdateItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue  { N = id.ToString() } },
                    {"ReplyDateTime", new AttributeValue { N = replyDateTime} },
                },
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    {"#P", "Price" }
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":newprice", new AttributeValue { N = price.ToString() } },
                    {":currprice", new AttributeValue { N = currentPrice.ToString() } },
                },
                UpdateExpression = "SET #P = :newprice",
                ConditionExpression = "#P = :currprice",
                ReturnValues = "ALL_NEW"
            };

            return request;
        }
    }
}
