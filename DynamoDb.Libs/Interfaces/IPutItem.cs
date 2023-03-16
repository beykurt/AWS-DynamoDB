using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDb.Libs.Interfaces
{
    public interface IPutItem
    {
        Task AddNewEntry(int id, string replyDateTime, double price, string tableName);
    }
}
