using System;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Provider;

namespace OPServer
{
    public static class Main1
    {
        static void Main(string[] args)
        {
            
            try
            {
                Uri providerEndpoint = new Uri("http://localhost/server.aspx");

                OpenIdProvider op = new OpenIdProvider(); return;
                // Send user input through identifier parser so we accept more free-form input.
                string rpSite = Identifier.Parse("123123");
                op.PrepareUnsolicitedAssertion(providerEndpoint, rpSite, Util.BuildIdentityUrl(), Util.BuildIdentityUrl()).Send();
            }
            catch (ProtocolException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
