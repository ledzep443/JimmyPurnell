using Shared.Helper.IHelper;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helper
{
    public class AWSS3Helper : IAWSS3Helper
    {
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private static IAmazonS3 client;

        public AWSS3Helper()
        {
            client = new AmazonS3Client(bucketRegion);
           
        }

        
    }
}
