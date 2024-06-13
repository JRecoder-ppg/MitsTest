using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text.Json;

namespace MitsTest.Services
{
    public class SecretManagerService
    {
        public async Task<T> GetSecret<T>(string secretName, string region)
        {
            if (string.IsNullOrEmpty(secretName) || string.IsNullOrEmpty(region))
            {
                throw new ArgumentNullException("Input values can be empty or null");
            }
            try
            {
#if DEBUG
                // Adding AccessKey, SecretKey and Session Token
                // Load AWS profile from %USERPROFILE%\.aws
                string awsAccessKeyId = string.Empty;
                string awsSecretAccessKey = string.Empty;
                string awsSessionToken = string.Empty;
                string profileName = "your-profile-aws-name";

                var sharedFile = new SharedCredentialsFile();
                if (sharedFile.TryGetProfile(profileName, out var profile))
                {
                    if (AWSCredentialsFactory.TryGetAWSCredentials(profile, sharedFile, out var awsCredentials))
                    {
                        var credentials = awsCredentials.GetCredentials();
                        awsAccessKeyId = credentials.AccessKey;
                        awsSecretAccessKey = credentials.SecretKey;
                        awsSessionToken = credentials.Token;
                    }
                    else
                    {
                        throw new Exception($"Unable to obtain {profileName} profile credentials");
                    }
                }
                else
                {
                    throw new Exception($"Unable to find {profileName} profile in the credentials file");
                }

                IAmazonSecretsManager client = new AmazonSecretsManagerClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, RegionEndpoint.GetBySystemName(region));
#else
                // Takes the role from AWS
                IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
#endif



                var request = new GetSecretValueRequest
                {
                    SecretId = secretName,
                };

                var response = await client.GetSecretValueAsync(request);
                if (response.SecretString == null)
                {
                    throw new ArgumentNullException(response.SecretString);
                }

                return JsonSerializer.Deserialize<T>(response.SecretString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
