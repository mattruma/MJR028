# Introduction

This is an example of a high data ingestion system.

Data is inserted into a Cosmos DB via an Azure Function, an Http Trigger.

This example shows the proper way to managing connections for the Cosmos DB `CosmosClient` class.

If an error is encountered when writing to the Comsos DB, the following will happen:

1. The Azure Function will failover an write to an Azure Blob Storage container, using output bindings.
2. The Azure Function will send a message to an Azure Storage queue, using output bindings, the Azure Storae Queue can be used as a central spot to send email messages.
