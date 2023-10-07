using FluentAssertions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace ChocoUlid.Tests;

public class UlidTypeTests
{
    private readonly UlidType _type = new UlidType();

    [Fact]
    public void ParseResult_ForNull_ReturnsNullValueNode()
    {
        var node = _type.ParseResult(null);
        node.Should().Be(NullValueNode.Default);
    }

    [Theory]
    [MemberData(nameof(GetUlids))]
    public void ParseResult_ForValidUlidString_ReturnsStringValueNode(Ulid ulid)
    {
        var ulidString = ulid.ToString();
        var node = _type.ParseResult(ulidString);
        node.Should().BeOfType<StringValueNode>()
            .Which.Value.Should().Be(ulidString);
    }

    [Theory]
    [MemberData(nameof(GetUlids))]
    public void ParseResult_ForValidUlid_ReturnsStringValueNode(Ulid ulid)
    {
        var node = _type.ParseResult(ulid);
        node.Should().BeOfType<StringValueNode>()
            .Which.Value.Should().Be(ulid.ToString());
    }

    [Theory]
    [MemberData(nameof(GetInvalidArguments))]
    public void ParseResult_ForInvalidArgument_ThrowsSerializationException(object value)
    {
        var action = () => _type.ParseResult(value);
        action.Should().Throw<SerializationException>();
    }

    [Theory]
    [MemberData(nameof(GetUlids))]
    public void ParseLiteral_ForValidUlidString_ReturnsUlid(Ulid ulid)
    {
        var ulidString = ulid.ToString();
        var node = new StringValueNode(ulidString);
        var result = _type.ParseLiteral(node);
        result.Should().NotBeNull()
            .And.BeOfType<Ulid>()
            .And.Match(r => r.ToString() == ulidString);
    }

    [Theory]
    [InlineData("")]
    [InlineData("aofmeruifasd")]
    [InlineData("47749")]
    [InlineData("8b280616-1b05-490c-a7dc-1088943abc39")]
    [InlineData("0")]
    public void ParseLiteral_ForInvalidUlidString_ThrowsSerializationException(string ulidString)
    {
        var node = new StringValueNode(ulidString);
        var action = () => _type.ParseLiteral(node);
        action.Should().Throw<SerializationException>();
    }

    [Fact]
    public void TryDeserialize_ForNull_ReturnsNull()
    {
        var result = _type.TryDeserialize(null, out var runtimeValue);
        result.Should().BeTrue();
        runtimeValue.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(GetUlids))]
    public void TryDeserialize_ForValidUlidString_ReturnsUlid(Ulid ulid)
    {
        var ulidString = ulid.ToString();
        var result = _type.TryDeserialize(ulidString, out var runtimeValue);
        result.Should().BeTrue();
        runtimeValue.Should().BeOfType<Ulid>()
            .And.Match(r => r.ToString() == ulidString);
    }

    [Theory]
    [MemberData(nameof(GetUlids))]
    public void TryDeserialize_ForValidUlid_ReturnsUlid(Ulid ulid)
    {
        var result = _type.TryDeserialize(ulid, out var runtimeValue);
        result.Should().BeTrue();
        runtimeValue.Should().BeOfType<Ulid>()
            .And.Match(r => r.ToString() == ulid.ToString());
    }

    [Theory]
    [MemberData(nameof(GetInvalidArguments))]
    public void TryDeserialize_ForInvalidArguments_ReturnsNull(object value)
    {
        var result = _type.TryDeserialize(value, out var runtimeValue);
        result.Should().BeFalse();
        runtimeValue.Should().BeNull();
    }

    [Fact]
    public void TrySerialize_ForNull_ReturnsNull()
    {
        var result = _type.TrySerialize(null, out var runtimeValue);
        result.Should().BeTrue();
        runtimeValue.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(GetUlids))]
    public void TrySerialize_ForValidUlid_ReturnsUlidString(Ulid ulid)
    {
        var result = _type.TrySerialize(ulid, out var resultValue);
        result.Should().BeTrue();
        resultValue.Should().Be(ulid.ToString());
    }

    [Theory]
    [MemberData(nameof(GetInvalidArguments))]
    public void TrySerialize_ForInvalidArguments_ReturnsNull(object value)
    {
        var result = _type.TrySerialize(value, out var runtimeValue);
        result.Should().BeFalse();
        runtimeValue.Should().BeNull();
    }

    public static IEnumerable<object[]> GetUlids()
    {
        yield return new object[] { Ulid.Empty };
        foreach (var _ in Enumerable.Range(0, 4))
        {
            yield return new object[] { Ulid.NewUlid() };
        }
    }

    public static IEnumerable<object[]> GetInvalidArguments()
    {
        yield return new object[] { Guid.NewGuid() };
        yield return new object[] { 0 };
        yield return new object[] { 44.4 };
        yield return new object[] { true };
    }
}