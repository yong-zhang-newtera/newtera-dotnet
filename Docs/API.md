# .NET Client API Reference

## Initialize Newtera Client object.

## Newtera

```cs
INewteraClient newteraClient = new NewteraClient()
                              .WithEndpoint("localhost", "8080")
                              .WithCredentials("demo1", "888")
                              .WithSSL(false)
                              .Build();
```

## 1. Constructors

<a name="constructors"></a>

|                                                                                                                                                                 |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `public NewteraClient(string endpoint, string accessKey = "", string secretKey = "", string region = "", string sessionToken="")`                                 |
| Creates Newtera client object with given endpoint.AccessKey, secretKey, region and sessionToken are optional parameters, and can be omitted for anonymous access. |
| The client object uses Http access by default. To use Https, chain method WithSSL() to client object to use secure transfer protocol                            |


__Parameters__

| Param          | Type     | Description                                                                                                                     |
|----------------|----------|---------------------------------------------------------------------------------------------------------------------------------|
| `endpoint`     | _string_ | endPoint is an URL, domain name, IPv4 address or IPv6 address.Valid endpoints are listed below:                                 |                                                                                                                 |                                                                                                                   |
| `accessKey`    | _string_ | accessKey is like user-id that uniquely identifies your account.This field is optional and can be omitted for anonymous access. |
| `secretKey`    | _string_ | secretKey is the password to your account.This field is optional and can be omitted for anonymous access.                       |                                                      |
|                                                                                                                                              |
|----------------------------------------------------------------------------------------------------------------------------------------------|
| `public NewteraClient()`                                                                                                                       |
| Creates Newtera client. This client gives an empty object that can be used with Chaining to populate only the member variables we need.        |
| The next important step is to connect to an endpoint. You can chain one of the overloaded method WithEndpoint() to client object to connect. |
| This client object also uses Http access by default. To use Https, chain method WithSSL() or WithSSL(true) to client object to use secure transfer protocol.  |
| To use non-anonymous access, chain method WithCredentials() to the client object along with the access key & secret key.                     |
| Finally chain the method Build() to get the finally built client object.                                                                     |

__Examples__

### Newtera


```cs
// 1. Using Builder with public NewteraClient(), Endpoint, Credentials & Secure (HTTPS) connection
INewteraClient newteraClient = new NewteraClient()
                              .WithEndpoint("play.min.io")
                              .WithCredentials("Q3AM3UQ867SPQQA43P2F", "zuf+tfteSlswRu7BJ86wekitnifILbZam1KYY3TG")
                              .WithSSL()
                              .Build()
// 2. Using Builder with public NewteraClient(), Endpoint, Credentials & Secure (HTTPS) connection
INewteraClient newteraClient = new NewteraClient()
                              .WithEndpoint("play.min.io", 9000, true)
                              .WithCredentials("Q3AM3UQ867SPQQA43P2F", "zuf+tfteSlswRu7BJ86wekitnifILbZam1KYY3TG")
                              .WithSSL()
                              .Build()
```

<a name="bucketExists"></a>
### BucketExistsAsync(string bucketName)

`Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken))`

Checks if a bucket exists.


__Parameters__


| Param                 | Type                                 | Description                                                |
|:----------------------|:-------------------------------------|:-----------------------------------------------------------|
| ``bucketName``        | _string_                             | Name of the bucket.                                        |
| ``cancellationToken`` | _System.Threading.CancellationToken_ | Optional parameter. Defaults to default(CancellationToken) |


| Return Type                                | Exceptions                                                |
|:-------------------------------------------|:----------------------------------------------------------|
| ``Task<bool>`` : true if the bucket exists | Listed Exceptions:                                        |
|                                            | ``InvalidBucketNameException`` : upon invalid bucket name |
|                                            | ``ConnectionException`` : upon connection error           |
|                                            | ``AccessDeniedException`` : upon access denial            |
|                                            | ``ErrorResponseException`` : upon unsuccessful execution  |
|                                            | ``InternalClientException`` : upon internal library error |



__Example__


```cs
try
{
   // Check whether 'my-bucketname' exists or not.
   bool found = await newteraClient.BucketExistsAsync(bucketName);
   Console.WriteLine("bucket-name " + ((found == true) ? "exists" : "does not exist"));
}
catch (NewteraException e)
{
   Console.WriteLine("[Bucket]  Exception: {0}", e);
}
```


### BucketExistsAsync(BucketExistsArgs)

`Task<bool> BucketExistsAsync(BucketExistsArgs args, CancellationToken cancellationToken = default(CancellationToken))`

Checks if a bucket exists.


__Parameters__


| Param                 | Type                                 | Description                                                |
|:----------------------|:-------------------------------------|:-----------------------------------------------------------|
| ``args``              | _BucketExistsArgs_                   | Argument object - bucket name.                             |
| ``cancellationToken`` | _System.Threading.CancellationToken_ | Optional parameter. Defaults to default(CancellationToken) |


| Return Type                                | Exceptions                                                |
|:-------------------------------------------|:----------------------------------------------------------|
| ``Task<bool>`` : true if the bucket exists | Listed Exceptions:                                        |
|                                            | ``InvalidBucketNameException`` : upon invalid bucket name |
|                                            | ``ConnectionException`` : upon connection error           |
|                                            | ``AccessDeniedException`` : upon access denial            |
|                                            | ``ErrorResponseException`` : upon unsuccessful execution  |
|                                            | ``InternalClientException`` : upon internal library error |



__Example__


```cs
try
{
   // Check whether 'my-bucketname' exists or not.
   bool found = await newteraClient.BucketExistsAsync(args);
   Console.WriteLine(args.BucketName + " " + ((found == true) ? "exists" : "does not exist"));
}
catch (NewteraException e)
{
   Console.WriteLine("[Bucket]  Exception: {0}", e);
}
```

<a name="listObjects"></a>
### ListObjectsAsync(ListObjectArgs args)

`IObservable<Item> ListObjectsAsync(ListObjectArgs args, CancellationToken cancellationToken = default(CancellationToken))`

Lists all objects (with version IDs, if existing) in a bucket.

__Parameters__


| Param                 | Type                                 | Description                                                                                |
|:----------------------|:-------------------------------------|:-------------------------------------------------------------------------------------------|
| ``args``              | _ListObjectArgs_                     | ListObjectArgs object - encapsulates bucket name, prefix, show recursively, show versions. |
| ``cancellationToken`` | _System.Threading.CancellationToken_ | Optional parameter. Defaults to default(CancellationToken)                                 |


| Return Type                                   | Exceptions |
|:----------------------------------------------|:-----------|
| ``IObservable<Item>``:an Observable of Items. | _None_     |


__Example__


```cs
try
{
    // Just list of objects
    // Check whether 'mybucket' exists or not.
    bool found = newteraClient.BucketExistsAsync("mybucket");
    if (found)
    {
        // List objects from 'my-bucketname'
        ListObjectArgs args = new ListObjectArgs()
                                  .WithBucket("mybucket")
                                  .WithPrefix("prefix")
                                  .WithRecursive(true);
        IObservable<Item> observable = newteraClient.ListObjectsAsync(args);
        IDisposable subscription = observable.Subscribe(
                item => Console.WriteLine("OnNext: {0}", item.Key),
                ex => Console.WriteLine("OnError: {0}", ex.Message),
                () => Console.WriteLine("OnComplete: {0}"));
    }
    else
    {
        Console.WriteLine("mybucket does not exist");
    }
}
catch (NewteraException e)
{
    Console.WriteLine("Error occurred: " + e);
}

try
{
    // List of objects with version IDs.
    // Check whether 'mybucket' exists or not.
    bool found = newteraClient.BucketExistsAsync("mybucket");
    if (found)
    {
        // List objects from 'my-bucketname'
        ListObjectArgs args = new ListObjectArgs()
                                  .WithBucket("mybucket")
                                  .WithPrefix("prefix")
                                  .WithRecursive(true)
                                  .WithVersions(true)
        IObservable<Item> observable = newteraClient.ListObjectsAsync(args, true);
        IDisposable subscription = observable.Subscribe(
                item => Console.WriteLine("OnNext: {0} - {1}", item.Key, item.VersionId),
                ex => Console.WriteLine("OnError: {0}", ex.Message),
                () => Console.WriteLine("OnComplete: {0}"));
    }
    else
    {
        Console.WriteLine("mybucket does not exist");
    }
}
catch (NewteraException e)
{
    Console.WriteLine("Error occurred: " + e);
}

```

## 3. Object operations

<a name="getObject"></a>
### GetObjectAsync(GetObjectArgs args, ServerSideEncryption sse)

`Task GetObjectAsync(GetObjectArgs args, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken))`

Downloads an object.


__Parameters__


| Param                 | Type                                 | Description                                                                                                               |
|:----------------------|:-------------------------------------|:--------------------------------------------------------------------------------------------------------------------------|
| ``args``              | _GetObjectArgs_                      | GetObjectArgs Argument Object encapsulating bucket, object names, version Id, ServerSideEncryption object, offset, length |
| ``cancellationToken`` | _System.Threading.CancellationToken_ | Optional parameter. Defaults to default(CancellationToken)                                                                |


| Return Type                                                                | Exceptions                                                 |
|:---------------------------------------------------------------------------|:-----------------------------------------------------------|
| ``Task``: Task callback returns an InputStream containing the object data. | Listed Exceptions:                                         |
|                                                                            | ``InvalidBucketNameException`` : upon invalid bucket name. |
|                                                                            | ``ConnectionException`` : upon connection error.           |
|                                                                            | ``InternalClientException`` : upon internal library error. |


__Examples__


```cs
// 1. With Bucket & Object names.
try
{
   // Check whether the object exists using statObject().
   // If the object is not found, statObject() throws an exception,
   // else it means that the object exists.
   // Execution is successful.
   StatObjectArgs statObjectArgs = new StatObjectArgs()
                                       .WithBucket("mybucket")
                                       .WithObject("myobject");
   await newteraClient.StatObjectAsync(statObjectArgs);

   // Get input stream to have content of 'my-objectname' from 'my-bucketname'
   GetObjectArgs getObjectArgs = new GetObjectArgs()
                                     .WithBucket("mybucket")
                                     .WithObject("myobject")
                                     .WithCallbackStream((stream) =>
                                          {
                                              stream.CopyTo(Console.OpenStandardOutput());
                                          });
   await newteraClient.GetObjectAsync(getObjectArgs);
}
catch (NewteraException e)
{
    Console.WriteLine("Error occurred: " + e);
}

// 2. With Offset Length specifying a range of bytes & the object as a stream.
try
{
    // Check whether the object exists using statObject().
    // If the object is not found, statObject() throws an exception,
    // else it means that the object exists.
    // Execution is successful.
    StatObjectArgs statObjectArgs = new StatObjectArgs()
                                        .WithBucket("mybucket")
                                        .WithObject("myobject");
    await newteraClient.StatObjectAsync(statObjectArgs);

    // Get input stream to have content of 'my-objectname' from 'my-bucketname'
    GetObjectArgs getObjectArgs = new GetObjectArgs()
                                      .WithBucket("mybucket")
                                      .WithObject("myobject")
                                      .WithOffset(1024L)
                                      .WithObjectSize(10L)
                                      .WithCallbackStream((stream) =>
    {
        stream.CopyTo(Console.OpenStandardOutput());
    });
    await newteraClient.GetObjectAsync(getObjectArgs);
}
catch (NewteraException e)
{
    Console.WriteLine("Error occurred: " + e);
}

// 3. Downloads and saves the object as a file in the local filesystem.
try
{
    // Check whether the object exists using statObjectAsync().
    // If the object is not found, statObjectAsync() throws an exception,
    // else it means that the object exists.
    // Execution is successful.
    StatObjectArgs statObjectArgs = new StatObjectArgs()
                                        .WithBucket("mybucket")
                                        .WithObject("myobject");
    await newteraClient.StatObjectAsync(statObjectArgs);

    // Gets the object's data and stores it in photo.jpg
    GetObjectArgs getObjectArgs = new GetObjectArgs()
                                      .WithBucket("mybucket")
                                      .WithObject("myobject")
                                      .WithFileName("photo.jpg");
    await newteraClient.GetObjectAsync(getObjectArgs);
}
catch (NewteraException e)
{
    Console.WriteLine("Error occurred: " + e);
}
```

<a name="putObject"></a>
### PutObjectAsync(PutObjectArgs args)

` Task PutObjectAsync(PutObjectArgs args, CancellationToken cancellationToken = default(CancellationToken))`


PutObjectAsync: Uploads object from a file or stream.


__Parameters__

| Param                 | Type                                 | Description                                                                                                                       |
|:----------------------|:-------------------------------------|:----------------------------------------------------------------------------------------------------------------------------------|
| ``args``              | _PutObjectArgs_                      | Arguments object - bucket name, object name, file name, object data stream, object size, content type, object metadata, operation executing progress etc. |
| ``cancellationToken`` | _System.Threading.CancellationToken_ | Optional parameter. Defaults to default(CancellationToken)                                                                        |


| Return Type | Exceptions                                                                        |
|:------------|:----------------------------------------------------------------------------------|
| ``Task``    | Listed Exceptions:                                                                |
|             | ``AuthorizationException`` : upon access or secret key wrong or not found         |
|             | ``InvalidBucketNameException`` : upon invalid bucket name                         |
|             | ``InvalidObjectNameException`` : upon invalid object name                         |
|             | ``ConnectionException`` : upon connection error                                   |
|             | ``InternalClientException`` : upon internal library error                         |
|             | ``EntityTooLargeException``: upon proposed upload size exceeding max allowed      |
|             | ``UnexpectedShortReadException``: data read was shorter than size of input buffer |
|             | ``ArgumentNullException``: upon null input stream                                 |
|             | ``InvalidOperationException``: upon input value to PutObjectArgs being invalid    |

__Example__


The maximum size of a single object is limited to 5TB. putObject transparently uploads objects larger than 5MiB in multiple parts. Uploaded data is carefully verified using MD5SUM signatures.


```cs
try
{
    Aes aesEncryption = Aes.Create();
    aesEncryption.KeySize = 256;
    aesEncryption.GenerateKey();
    var ssec = new SSEC(aesEncryption.Key);
    var progress = new Progress<ProgressReport>(progressReport =>
    {
        Console.WriteLine(
                $"Percentage: {progressReport.Percentage}% TotalBytesTransferred: {progressReport.TotalBytesTransferred} bytes");
        if (progressReport.Percentage != 100)
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        else Console.WriteLine();
    });
    PutObjectArgs putObjectArgs = new PutObjectArgs()
                                      .WithBucket("mybucket")
                                      .WithObject("island.jpg")
                                      .WithFilename("/mnt/photos/island.jpg")
                                      .WithContentType("application/octet-stream")
                                      .WithServerSideEncryption(ssec)
                                      .WithProgress(progress);
    await newtera.PutObjectAsync(putObjectArgs);
    Console.WriteLine("island.jpg is uploaded successfully");
}
catch(NewteraException e)
{
    Console.WriteLine("Error occurred: " + e);
}
```


<a name="statObject"></a>
### StatObjectAsync(StatObjectArgs args)

`Task<ObjectStat> StatObjectAsync(StatObjectArgs args, CancellationToken cancellationToken = default(CancellationToken))`

Gets metadata of an object.


__Parameters__


| Param                 | Type                                 | Description                                                                              |
|:----------------------|:-------------------------------------|:-----------------------------------------------------------------------------------------|
| ``args``              | _StatObjectArgs_                     | StatObjectArgs Argument Object with bucket, object names & server side encryption object |
| ``cancellationToken`` | _System.Threading.CancellationToken_ | Optional parameter. Defaults to default(CancellationToken)                               |


| Return Type                                       | Exceptions                                                |
|:--------------------------------------------------|:----------------------------------------------------------|
| ``Task<ObjectStat>``: Populated object meta data. | Listed Exceptions:                                        |
|                                                   | ``InvalidBucketNameException`` : upon invalid bucket name |
|                                                   | ``ConnectionException`` : upon connection error           |
|                                                   | ``InternalClientException`` : upon internal library error |



__Example__


```cs
try
{
   // Get the metadata of the object.
   StatObjectArgs statObjectArgs = new StatObjectArgs()
                                       .WithBucket("mybucket")
                                       .WithObject("myobject");
   ObjectStat objectStat = await newteraClient.StatObjectAsync(statObjectArgs);
   Console.WriteLine(objectStat);
}
catch(NewteraException e)
{
   Console.WriteLine("Error occurred: " + e);
}
```


<a name="removeObject"></a>
### RemoveObjectAsync(RemoveObjectArgs args)

`Task RemoveObjectAsync(RemoveObjectArgs args, CancellationToken cancellationToken = default(CancellationToken))`

Removes an object.

__Parameters__


| Param                 | Type                                 | Description                                                |
|:----------------------|:-------------------------------------|:-----------------------------------------------------------|
| ``args``              | _RemoveObjectArgs_                   | Arguments Object.                                          |
| ``cancellationToken`` | _System.Threading.CancellationToken_ | Optional parameter. Defaults to default(CancellationToken) |

| Return Type | Exceptions                                                |
|:------------|:----------------------------------------------------------|
| ``Task``    | Listed Exceptions:                                        |
|             | ``InvalidBucketNameException`` : upon invalid bucket name |
|             | ``ConnectionException`` : upon connection error           |
|             | ``InternalClientException`` : upon internal library error |



__Example__


```cs
// 1. Remove object myobject from the bucket mybucket.
try
{
    RemoveObjectArgs rmArgs = new RemoveObjectArgs()
                                  .WithBucket("mybucket")
                                  .WithObject("myobject");
    await newteraClient.RemoveObjectAsync(args);
    Console.WriteLine("successfully removed mybucket/myobject");
}
catch (NewteraException e)
{
    Console.WriteLine("Error: " + e);
}

// 2. Remove one version of object myobject with versionID from mybucket.
try
{
    RemoveObjectArgs rmArgs = new RemoveObjectArgs()
                                  .WithBucket("mybucket")
                                  .WithObject("myobject")
    await newteraClient.RemoveObjectAsync(args);
    Console.WriteLine("successfully removed mybucket/myobject{versionId}");
}
catch (NewteraException e)
{
    Console.WriteLine("Error: " + e);
}

```
