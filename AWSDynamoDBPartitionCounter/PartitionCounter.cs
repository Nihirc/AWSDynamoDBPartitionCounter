using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;

namespace AWSDynamoDBPartitionCounter
{
    internal class PartitionCounter
    {
        public string TableName { get; private set; }
        private readonly AmazonDynamoDBClient client;
        private readonly AmazonDynamoDBStreamsClient streamsClient;

        /// <summary>
        /// Constructor that takes a table name as input.
        /// </summary>
        /// <param name="tableName"></param>
        public PartitionCounter(string tableName)
        {
            this.TableName = tableName;
            client = DynamoClient.Instance().clientInstance;
            streamsClient = DynamoStreamsClient.Instance().streamsInstance;
        }

        /// <summary>
        /// Gets DynamoDB stream information
        /// </summary>
        /// <returns>Returns DescribeStreamResponse object that contains information regarding the DynamoDB stream</returns>
        private DescribeStreamResponse GetStreamDescription()
        {
            try
            {
                var streamARN = TableStreamValidator();
                if (streamARN != null)
                {
                    var request = new DescribeStreamRequest
                    {
                        StreamArn = streamARN
                    };

                    var response = streamsClient.DescribeStream(request);
                    return response;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Checks whether the table exists and validates if the streams has been enabled on the table. If so, returns the stream ARN.
        /// </summary>
        /// <returns>returns null or string value - DynamoDB stream ARN</returns>
        private string TableStreamValidator()
        {
            try
            {
                if (client != null)
                {
                    var request = new DescribeTableRequest(TableName);
                    var tableDescription = client.DescribeTable(request).Table;

                    if (tableDescription.TableStatus.Value == "ACTIVE")
                    {
                        if(tableDescription.StreamSpecification.StreamEnabled)
                        {
                            Console.WriteLine("Stream ARN: " + tableDescription.LatestStreamArn);
                            return tableDescription.LatestStreamArn;
                        }
                        else
                        {
                            Console.WriteLine("Please enable streams on the table.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The table is not in active status. Please try again once the table is in active state.");
                    }
                }
                return null;
            }
            catch (ResourceNotFoundException notFoundException)
            {
                Console.WriteLine("Unable to find table with name \"" + TableName + "\" in region " + client.Config.RegionEndpoint);
                Console.WriteLine("Error: " + notFoundException.Message);
                Console.WriteLine("RequestID: " + notFoundException.RequestId);
                Console.WriteLine("Exception: \n" + notFoundException.InnerException);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
                return null;
            }
        }

        protected internal int? GetPartitionCount()
        {
            try
            {
                var partitionCount = 0;
                var streamResponse = GetStreamDescription();
                if (streamResponse != null)
                {
                    var listOfShards = streamResponse.StreamDescription.Shards;

                    foreach (Shard shard in listOfShards)
                    {
                        if (shard.SequenceNumberRange.EndingSequenceNumber == null)
                        {
                            partitionCount++;
                        }
                    }
                    return partitionCount;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
                return null;
            }
        }
    }
}
