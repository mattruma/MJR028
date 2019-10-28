# Introduction

This is an example of a high data ingestion system.

 ![Architecture](/Architecture.png)

Data is inserted into a Cosmos DB via an Azure Function, an Http Trigger.

This example shows the proper way to managing connections for the Cosmos DB `CosmosClient` class.

If an error is encountered when writing to the Comsos DB, the following will happen:

1. The Azure Function will failover an write to an Azure Blob Storage container, using output bindings.
2. The Azure Function will send a message to an Azure Storage queue, using output bindings, the Azure Storae Queue can be used as a central spot to send email messages.

You will need to create the following resources in your Azure environment:

* Resource Group
* Cosmos DB
* Function App
* Storage Account

Create the following containers/queues in your storage account:

* Queue called `queuetrigger1`
* Blob container called `httptrigger1`
* Blob container called `queuetrigger1`

Your `local.settings.json` should look as follows:

```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "CosmosDBConnectionString": "YOUR_COSMOS_DB_CONNECTION_STRING",
    "CosmosDBDatabase": "YOUR_RESOURCE_GROUP_NAME",
    "StorageConnectionString": "YOUR_STORAGE_CONNECTION_STRING"
  }
}
```


You can also do this locally using the **Azure Storage Emulator**, located at https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator and the **Cosmos DB Emulator**, located at https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator.
