using Amazon;
using Amazon.DynamoDBv2;
using System.Configuration;

namespace AWSDynamoDBPartitionCounter
{
    internal class DynamoStreamsClient
    {
        private static DynamoStreamsClient _instance;
        public AmazonDynamoDBStreamsClient streamsInstance;
        private string awsRegion = ConfigurationManager.AppSettings["AWS-Region"];

        public static DynamoStreamsClient Instance()
        {
            if(_instance == null)
            {
                _instance = new DynamoStreamsClient();
            }
            return _instance;
        }

        private DynamoStreamsClient()
        {
            var region = RegionEndpoint.GetBySystemName(awsRegion);
            streamsInstance = new AmazonDynamoDBStreamsClient(region);
        }
    }
}
