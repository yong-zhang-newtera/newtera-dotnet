/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtera.Exceptions;

namespace Newtera.Tests;

/// <summary>
///     Summary description for UnitTest1
/// </summary>
[TestClass]
[Ignore("Class was previously skipped by unit tests.. See #211")]
public class UnitTest1
{
    public UnitTest1()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                                               | SecurityProtocolType.Tls11
                                               | SecurityProtocolType.Tls12;
        using var newtera = new NewteraClient()
            .WithEndpoint(TestHelper.Endpoint)
            .WithCredentials(TestHelper.AccessKey, TestHelper.SecretKey)
            .Build();
    }

    /// <summary>
    ///     Gets or sets the test context which provides
    ///     information about and functionality for the current test run.
    /// </summary>
    public TestContext TestContext { get; set; }

    [TestMethod]
    public void TestWithUrl()
    {
        using var newtera = new NewteraClient()
            .WithEndpoint("localhost", 8080)
            .Build();
    }

    [TestMethod]
    public void TestWithoutPort()
    {
        using var newtera = new NewteraClient()
            .WithEndpoint("localhost")
            .Build();
    }

    [TestMethod]
    public void TestWithTrailingSlash()
    {
        using var newtera = new NewteraClient()
            .WithEndpoint("localhost:8080/")
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidEndpointException))]
    public void TestUrlFailsWithMalformedScheme()
    {
        using var newtera = new NewteraClient()
            .WithEndpoint("htp://localhost:8080")
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidEndpointException))]
    public void TestUrlFailsWithPath()
    {
        using var newtera = new NewteraClient()
            .WithEndpoint("localhost:8080/foo")
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidEndpointException))]
    public void TestUrlFailsWithQuery()
    {
        using var newtera = new NewteraClient()
            .WithEndpoint("http://localhost:8080/?foo=bar")
            .Build();
    }

    #region Additional test attributes

    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //

    #endregion
}
