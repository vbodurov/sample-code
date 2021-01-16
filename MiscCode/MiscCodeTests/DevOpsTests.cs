using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client.SharePoint;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class DevOpsTests
    {
        [Test]
        public async Task CanConnectApiToDevOpsVariables()
        {
            const string c_collectionUri = "https://dev.azure.com/msi-cie";
            const string c_projectName = "avigilon-scarif";
            const string c_repoName = "decoding-service";

            // Interactively ask the user for credentials, caching them so the user isn't constantly prompted
            VssCredentials creds = new VssClientCredentials();
            creds.Storage = new VssClientCredentialStorage();

            // Connect to Azure DevOps Services
            VssConnection connection = new VssConnection(new Uri(c_collectionUri), creds);



            // Get a GitHttpClient to talk to the Git endpoints
            using (var taskClient = connection.GetClient<TaskAgentHttpClient>())
            {
                // Get data about a specific repository
                var group = await taskClient.GetVariableGroupAsync(c_projectName, 5621);
                Console.WriteLine(group);
            }
        }


        [Test]
        public async Task CanConnectApiToDevOpsRepo()
        {
            const string c_collectionUri = "https://dev.azure.com/msi-cie";
            const string c_projectName = "avigilon-scarif";
            const string c_repoName = "decoding-service";

            // Interactively ask the user for credentials, caching them so the user isn't constantly prompted
            VssCredentials creds = new VssClientCredentials();
            creds.Storage = new VssClientCredentialStorage();

            // Connect to Azure DevOps Services
            VssConnection connection = new VssConnection(new Uri(c_collectionUri), creds);



            // Get a GitHttpClient to talk to the Git endpoints
            using (var gitClient = connection.GetClient<GitHttpClient>())
            {
                // Get data about a specific repository
                var repo = await gitClient.GetRepositoryAsync(c_projectName, c_repoName);
                Console.WriteLine(repo);
            }
        }
    }
}