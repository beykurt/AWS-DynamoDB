using DynamoDb.Libs.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DynamoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamoDbController : Controller
    {
        private readonly ICreateTable _createTable;
        private readonly IPutItem _putItem;
        private readonly IGetItem _getItem;
        private readonly IUpdateItem _updateItem;
        private readonly IDeleteTable _deleteTable;

        public DynamoDbController(ICreateTable dynamoDbExamples, IPutItem putItem, IGetItem getItem, IUpdateItem updateItem, IDeleteTable deleteTable)
        {
            _createTable = dynamoDbExamples;
            _putItem = putItem;
            _getItem = getItem;
            _updateItem = updateItem;
            _deleteTable = deleteTable;
        }

        //ÖRNEK POSTMAN: [GET] https://localhost:44387/api/DynamoDb/createTable&tableName=DynamoTest
        [Route("createtable")]
        public IActionResult CreateDynamoDbTable([FromQuery] string tableName)
        {
            _createTable.CreateDynamoDbTable(tableName);
            return Ok();
        }

        //ÖRNEK POSTMAN: [GET] https://localhost:44387/api/DynamoDb/putitems?id=1&replyDateTime=636687890200000000&price=30&tableName=DynamoTest
        [Route("putitems")]
        public IActionResult PutItem([FromQuery] int id, string replyDateTime, double price, string tableName)
        {
            _putItem.AddNewEntry(id, replyDateTime, price, tableName);
            return Ok();
        }

        //ÖRNEK POSTMAN: [GET] https://localhost:44387/api/DynamoDb/getitems?Id=1&tableName=DynamoTest
        [Route("getitems")]
        public async Task<IActionResult> GetItems([FromQuery] int? id, string tableName)
        {
            var response = await _getItem.GetItems(id, tableName);
            return Ok(response);
        }

        //ÖRNEK POSTMAN: [PUT] https://localhost:44387/api/DynamoDb/updateitem?Id=1&price=300000&tableName=DynamoTest
        [HttpPut]
        [Route("updateitem")]
        public async Task<IActionResult> UpdateItem([FromQuery] int id, double price, string tableName)
        {
            var response = await _updateItem.Update(id, price, tableName);
            return Ok(response);
        }

        //ÖRNEK POSTMAN: [DELETE] https://localhost:44387/api/DynamoDb/deletetable?tableName=DynamoTest
        [HttpDelete]
        [Route("deletetable")]
        public async Task<IActionResult> DeleteTable([FromQuery] string tableName)
        {
            var response = await _deleteTable.ExecuteTableDelete(tableName);
            return Ok(response);
        }
    }
}
