using System;
using MongoDB.Bson;
using MongoDbGenericRepository.Utils;
using Xunit;

namespace CoreUnitTests.Infrastructure;

public class TestMongoId
{
    [Fact]
    public void TestId_Guid_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<Guid>();
        Assert.NotEqual(Guid.Empty, testMongoId);
    }

    [Fact]
    public void TestId_Int16_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<Int16>();
        Assert.NotEqual(0, testMongoId);
    }

    [Fact]
    public void TestId_Short_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<short>();
        Assert.NotEqual(0, testMongoId);
    }

    [Fact]
    public void TestId_Int32_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<Int32>();
        Assert.NotEqual(0, testMongoId);
    }

    [Fact]
    public void TestId_Int_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<int>();
        Assert.NotEqual(0, testMongoId);
    }

    [Fact]
    public void TestId_Int64_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<Int64>();
        Assert.NotEqual(0, testMongoId);
    }

    [Fact]
    public void TestId_Long_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<long>();
        Assert.NotEqual(0, testMongoId);
    }

    [Fact]
    public void TestId_String_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<string>();
        Assert.NotEqual(string.Empty, testMongoId);
        Assert.True(Guid.TryParse(testMongoId, out var guidId));
        Assert.NotEqual(Guid.Empty, guidId);
    }

    [Fact]
    public void TestId_ObjectId_Success()
    {
        // Arrange
        var testMongoId = IdGenerator.GetId<ObjectId>();
        Assert.NotEqual(ObjectId.Empty, testMongoId);
    }

    [Fact]
    public void TestId_InvalidType_ThrowsArgumentException()
    {
        // Arrange
#pragma warning disable IDE0022 // Use expression body for method
        _ = Assert.Throws<ArgumentException>(() => IdGenerator.GetId<DateTime>());
#pragma warning restore IDE0022 // Use expression body for method
    }
}
