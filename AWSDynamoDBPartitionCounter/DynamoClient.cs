using Amazon;
using Amazon.DynamoDBv2;
using System.Configuration;

namespace AWSDynamoDBPartitionCounter
{
    internal class DynamoClient
    {
        private static DynamoClient _instance;
        public AmazonDynamoDBClient clientInstance;
        private string awsRegion = ConfigurationManager.AppSettings["AWS-Region"];

        public static DynamoClient Instance()
        {
            if (_instance == null)
            {
                _instance = new DynamoClient();
            }
            return _instance;
        }

        private DynamoClient()
        {
            var region = RegionEndpoint.GetBySystemName(awsRegion);
            clientInstance = new AmazonDynamoDBClient(region);
        }
    }
}
