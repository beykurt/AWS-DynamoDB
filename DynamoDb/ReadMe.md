Projeyi çalıştırmak için yapılması gerekenler:

1 - DynamoDb > appsettings.json içerisindeki AWS tabını kendi AWS hesap bilgileriniz ile değiştirmelisiniz.
     "AWS": 
     {
        "Region": "eu-west-1",
        "AccessKey": "*****************",
        "SecretKey": "*****************"
      }

2 - DynamoDb servislerini direk DynamoDb > Controllers > DynamoDbController içinde bulabilirsiniz.
    
    * CREATE TABLE
        https://localhost:44387/api/DynamoDb/createTable&tableName=DynamoTest

    * PUT ITEM INTO TABLE
        https://localhost:44387/api/DynamoDb/putitems?id=1&replyDateTime=636687890200000000&price=30&tableName=DynamoTest

    * GET ITEMS FROM TABLE
        https://localhost:44387/api/DynamoDb/getitems?Id=1&tableName=DynamoTest

    * UPDATE ITEM
        https://localhost:44387/api/DynamoDb/updateitem?Id=1&price=300000&tableName=DynamoTest

    * DELETE TABLE
        https://localhost:44387/api/DynamoDb/deletetable?tableName=DynamoTest