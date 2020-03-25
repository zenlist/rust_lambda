using Amazon.CDK;
using System;

namespace Rustlamb
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var environment = new Amazon.CDK.Environment { Account = "YOUR_ACCOUNT_ID", Region = "YOUR_REGION" };
            var app = new App();
            new RustlambStack(app, "RustlambStack", new RustlambProps
            {
                Env = environment
            });
            app.Synth();
        }
    }
}
