# AWSDynamoDBPartitionCounter

C# console application that will allow you to get the number of partitions for a DynamoDB table. It requires stream to be enabled for the DynamoDB table.
It is best to run the application during low usage of DynamoDB table as it is possible for shards to split during the heavy usage of the table and give incorrect number of partitions.
