// See https://aka.ms/new-console-template for more information

using AlkamiTests;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;

var products = (new Data().Products).AsQueryable();
var converter = new QueryConverter();
var result = converter.ConvertToLinq("SELECT * FROM Products WHERE Price > 600", products);
result = converter.ConvertToLinq("SELECT * FROM Products WHERE Price > 800", products);

/*
foreach (var product in result)
{
    Console.WriteLine($"{product.Name} - ${product.Price}");
}
*/
// connect to AWS SQS
var credential = new CredentialProfileStoreChain()
    .TryGetAWSCredentials("cdaboinf", out var awsCredentials);

using var sqsClient = new AmazonSQSClient(awsCredentials, RegionEndpoint.USEast2);

// send message:
var queueUrl = "https://sqs.us-east-2.amazonaws.com/848362861133/FileCreate";
var messageBody = "Hello World!";

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrl,
    MessageBody = messageBody,
};

/*
try
{
    var messageResponse = sqsClient.SendMessageAsync(sendMessageRequest).Result;
    Console.WriteLine($"Message response: {messageResponse}");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
*/
// pull message:
var cts = new CancellationTokenSource();
var receivedMessageRequest = new ReceiveMessageRequest(queueUrl);
while (cts.IsCancellationRequested == false)
{
    var response = await sqsClient.ReceiveMessageAsync(receivedMessageRequest, cts.Token);
    foreach (var message in response.Messages)
    {
        Console.WriteLine($"Received message: {message.Body}");
        await sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
    }
}






    