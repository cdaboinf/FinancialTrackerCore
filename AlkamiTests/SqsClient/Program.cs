using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;

_ = new CredentialProfileStoreChain()
    .TryGetAWSCredentials("cdaboinf", out var awsCredentials);

using var sqsClient = new AmazonSQSClient(awsCredentials, RegionEndpoint.USEast2);

// pull messages:
const string queueUrl = "https://sqs.us-east-2.amazonaws.com/848362861133/FileCreate";

var cts = new CancellationTokenSource();
var receivedMessageRequest = new ReceiveMessageRequest(queueUrl);
while (cts.IsCancellationRequested == false)
{
    var response = await sqsClient.ReceiveMessageAsync(receivedMessageRequest, cts.Token);
    foreach (var message in response.Messages)
    {
        Console.WriteLine($"Received message: {message.Body}");
        Console.WriteLine($"Received time: {DateTime.Now}");
        Console.WriteLine($"Deleting message: {message.ReceiptHandle}");
        await sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
    }
}