# AWSDynamoDBPartitionCounter

C# console application that will allow you to get the number of partitions for a DynamoDB table. It requires stream to be enabled for the DynamoDB table.

You must be signed up for the following services:
  * AWS DynamoDB
  * AWS Identity and Access Management. For more information, see http://aws.amazon.com/iam.

# Running the sample

The basic steps for running the Amazon DynamoDB sample are:
  1. Create a credentials file in the location C:\Users\\\<user>\\\.aws\ with name "credentials".
  2. Create a new profile ```development``` and fill in your Access Key ID and Secret Access Key:
  
    ```
    [development]
    aws_access_key_id =
    aws_secret_access_key =
    ```
  3. Save the file.
  4. Edit the app.config file to ensure that correct region is selected.
  5. Compile and run the executable.

It is best to run the application during the time period when the DynamoDB is not utilized heavily. It is possible for shards to split when DynamoDB is being utilized heavily.
