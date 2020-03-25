using System.Linq;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace Rustlamb.Resources
{
    class RustlambVpc : Vpc
    {
        internal RustlambVpc(Construct scope, string id, RustlambProps props) : base(scope, id, CreateProps())
        {
            this.AddEndpoints();
        }

        private static VpcProps CreateProps()
        {
            return new VpcProps
            {
                Cidr = "10.1.128.0/17",
                MaxAzs = 3,
                SubnetConfiguration = new SubnetConfiguration[] {
                    new SubnetConfiguration{
                        Name = "RustlambPrivate",
                        CidrMask = 20,
                        SubnetType = SubnetType.PUBLIC
                    },
                     new SubnetConfiguration{
                        Name = "RustlambPrivate",
                        CidrMask = 20,
                        SubnetType = SubnetType.PRIVATE
                    }
                }
            };
        }

        public void AddEndpoints()
        {
            this.AddGatewayEndpoint("DynamoDB", new GatewayVpcEndpointOptions
            {
                Service = GatewayVpcEndpointAwsService.DYNAMODB,
                Subnets = new SubnetSelection[] {
                    new SubnetSelection { SubnetType = SubnetType.PRIVATE },
                    new SubnetSelection { SubnetType = SubnetType.PUBLIC }
                }
            });

            this.AddGatewayEndpoint("S3", new GatewayVpcEndpointOptions
            {
                Service = GatewayVpcEndpointAwsService.S3,
                Subnets = new SubnetSelection[] {
                    new SubnetSelection { SubnetType = SubnetType.PRIVATE },
                    new SubnetSelection { SubnetType = SubnetType.PUBLIC }
                }
            });
        }
    }
}
