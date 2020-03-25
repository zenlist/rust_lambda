using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3.Assets;
using Amazon.CDK.AWS.Kinesis;
using Amazon.CDK.AWS.Lambda.EventSources;

namespace Rustlamb.Resources.Functions
{
    class HelloWorldHandler : Function
    {
        internal HelloWorldHandler(Construct scope, string id, IVpc vpc, RustlambProps props) : base(scope, id, CreateProps(vpc, props))
        {
            this.Role.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("AmazonDynamoDBFullAccess"));
            // Add event sources here, pass in other items created, tables, streams, queues, etc.
            // Or setup a API Gateway integration
            var logGroup = this.CreateLogGroup();
        }

        private static FunctionProps CreateProps(IVpc vpc, RustlambProps props)
        {
            return new FunctionProps
            {
                Vpc = vpc,
                Runtime = Runtime.PROVIDED,
                Code = Code.FromAsset("./rustlamb_hello_world/target", new AssetOptions { }),
                Handler = "bootstrap",
                VpcSubnets = new SubnetSelection { SubnetType = SubnetType.PRIVATE },
            };
        }

        private LogGroup CreateLogGroup()
        {
            return new LogGroup(this, "AccountLogGroup", new LogGroupProps
            {
                LogGroupName = "/aws/lambda/" + this.FunctionName,
                Retention = RetentionDays.ONE_WEEK,
                RemovalPolicy = RemovalPolicy.DESTROY
            });
        }
    }
}
