using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDb.Libs.Interfaces
{
    public interface IDeleteTable
    {
        Task<DeleteTableResponse> ExecuteTableDelete(string tableName);
    }
}
