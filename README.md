# AWSDynamoDBPartitionCounter

C# console application that will allow you to get the number of partitions for a DynamoDB table. It requires stream to be enabled for the DynamoDB table.
It is best to run the application during the time period when the DynamoDB is not utilized heavily. It is possible for shards to split when DynamoDB is being utilized heavily.  

Modify app.config file to provide AWS profile and region.
