using Amazon.DynamoDBv2;
using System;

namespace AWSDynamoDBPartitionCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the table name: ");
            string tableName = Convert.ToString(Console.ReadLine());
            if(tableName.Length > 0)
            {
                PartitionCounter partitionCounter = new PartitionCounter(tableName);
                int? partitions = partitionCounter.GetPartitionCount();

                if(partitions != null)
                    Console.WriteLine("The number of partitions for DynamoDB table " + tableName + " is: " + partitionCounter.GetPartitionCount());
            }
            Console.ReadLine();
        }
    }
}
