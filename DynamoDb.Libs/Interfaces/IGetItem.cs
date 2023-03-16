using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DynamoDb.Libs.Models;

namespace DynamoDb.Libs.Interfaces
{
    public interface IGetItem
    {
        Task<DynamoTableItems> GetItems(int? id, string tableName);
    }
}
