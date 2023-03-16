using DynamoDb.Libs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDb.Libs.Interfaces
{
    public interface IUpdateItem
    {
        Task<Item> Update(int id, double price, string tableName);
    }
}
