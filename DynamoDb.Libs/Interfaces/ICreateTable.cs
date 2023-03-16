using System;
using System.Collections.Generic;
using System.Text;

namespace DynamoDb.Libs.Interfaces
{
    public interface ICreateTable
    {
        void CreateDynamoDbTable(string tableName);
    }
}
