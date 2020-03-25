using Amazon.CDK;

using Rustlamb.Resources;
using Rustlamb.Resources.Functions;

namespace Rustlamb
{
    public class RustlambProps : StackProps
    {
    }

    public class RustlambStack : Amazon.CDK.Stack
    {
        internal RustlambStack(Construct scope, string id, RustlambProps props) : base(scope, id, props)
        {
            var rustlambVpc = new RustlambVpc(this, "RustlambVpc", props);
            var helloWorld = new HelloWorldHandler(this, "HelloWorld", rustlambVpc, props);
        }
    }
}
