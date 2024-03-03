# Newtera Client SDK for .NET  

Newtera Client SDK provides higher level APIs for Newtera TDM services.

## Install from NuGet
To install [Newtera .NET package](https://www.nuget.org/packages/Newtera/), run the following command in Nuget Package Manager Console.

```powershell
PM> Install-Package Newtera
```

## Newtera Client Example
To connect to an Newtear TDM service, you need the following information

| Variable name | Description                                                  |
|:--------------|:-------------------------------------------------------------|
| endpoint      | \<Domain-name\> or \<ip:port\> of your object storage        |
| accessKey     | User ID that uniquely identifies your account                |
| secretKey     | Password to your account                                     |
| secure        | boolean value to enable/disable HTTPS support (default=true) |

The following examples uses a local Newtera TDM server for development purposes.

```cs
using Newtera;

var endpoint = "localhost";
var port = "8080";
var accessKey = "demo1";
var secretKey = "888";
var secure = false;
var bucketName = "tdm";
// Initialize the client with access credentials.
private static INewteraClient newtera = new NewteraClient()
                                    .WithEndpoint(endpoint, port)
                                    .WithCredentials(accessKey, secretKey)
                                    .WithSSL(secure)
                                    .Build();

// Check if a bucket exists.
var args = new BucketExistsArgs()
	.WithBucket(bucketName);
var found = await newtera.BucketExistsAsync(args).ConfigureAwait(false);
Console.WriteLine((found ? "Found" : "Couldn't find ") + "bucket " + bucketName);
Console.WriteLine();

```

## Complete _File Uploader_ Example

This example program connects to the local Newtera TDM server, uploads a file to a bucket.
```cs
using System;
using Newtera;
using Newtera.Exceptions;
using Newtera.DataModel;
using Newtera.Credentials;
using Newtera.DataModel.Args;
using System.Threading.Tasks;

namespace FileUploader
{
    class FileUpload
    {
        static void Main(string[] args)
        {
            var endpoint = "localhost";
            var port = "8080";
            var accessKey = "demo1";
            var secretKey = "888";
            var secure = false;
            try
            {
                var newtera = new NewteraClient()
                                    .WithEndpoint(endpoint, port)
                                    .WithCredentials(accessKey, secretKey)
                                    .WithSSL(secure)
                                    .Build();
                FileUpload.Run(newtera).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        // File uploader task.
        private async static Task Run(NewteraClient newtera)
        {
            var bucketName = "tdm";
			var prefix = @"Task-20230930-0023\慢充功能测试\电池循环充放电数据";
            var location   = "us-east-1";
            var objectName = "golden-oldies.zip";
            var filePath = "C:\\Users\\username\\Downloads\\golden_oldies.mp3";
            var contentType = "application/zip";

            try
            {
                var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
                bool found = await newtera.BucketExistsAsync(beArgs).ConfigureAwait(false);
                if (found)
                {
                      // Upload a file to bucket.
                    var putObjectArgs = new PutObjectArgs()
                        .WithBucket(bucketName)
						.WithPrefix(prefix)
                        .WithObject(objectName)
                        .WithFileName(filePath)
                        .WithContentType(contentType);
                    await newtera.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                    Console.WriteLine("Successfully uploaded " + objectName );
                }
            }
            catch (NewteraException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
            }
        }
    }
}
```

#### Bucket Operations

* [BucketExists.cs](https://github.com/newtera/newtera-dotnet/blob/master/Newtera.Examples/Cases/BucketExists.cs)
* [ListObjects.cs](https://github.com/newtera/newtera-dotnet/blob/master/Newtera.Examples/Cases/ListObjects.cs)

#### File Object Operations
* [FGetObject.cs](https://github.com/newtera/newtera-dotnet/blob/master/Newtera.Examples/Cases/FGetObject.cs)
* [FPutObject.cs](https://github.com/newtera/newtera-dotnet/blob/master/Newtera.Examples/Cases/FPutObject.cs)

#### Object Operations
* [GetObject.cs](https://github.com/newtera/newtera-dotnet/blob/master/Newtera.Examples/Cases/GetObject.cs)
* [PutObject.cs](https://github.com/newtera/newtera-dotnet/blob/master/Newtera.Examples/Cases/PutObject.cs)
* [StatObject.cs](https://github.com/newtera/newtera-dotnet/blob/master/Newtera.Examples/Cases/StatObject.cs)
* [RemoveObject.cs](https://github.com/newtera/newtera-dotnet/blob/master/Newtera.Examples/Cases/RemoveObject.cs)

## Explore Further
* [Newtera .NET SDK API Reference](http://newtera.net/docs/newtera/developers/dotnet/API.html)
