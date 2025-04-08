using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;

_ = new CredentialProfileStoreChain()
    .TryGetAWSCredentials("cdaboinf", out var awsCredentials);

using var sqsClient = new AmazonSQSClient(awsCredentials, RegionEndpoint.USEast2);

// send message:
const string queueUrl = "https://sqs.us-east-2.amazonaws.com/848362861133/FileCreate";
var messageBody = $"Message from SqsService at {DateTime.Now}";

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrl,
    MessageBody = messageBody,
};

try
{
    var messageResponse = sqsClient.SendMessageAsync(sendMessageRequest).Result;
    Console.WriteLine($"Message ID: {messageResponse.MessageId}");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}