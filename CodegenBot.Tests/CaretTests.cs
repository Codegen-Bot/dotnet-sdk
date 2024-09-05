using FluentAssertions;
using Moq;

namespace CodegenBot.Tests;

[TestClass]
public class CaretTests
{
    [TestMethod]
    public void CaretShouldBeImplicitlyConvertibleToString()
    {
        var uut = CaretRef.New("test1");
        string x = uut.ToString();
    }
}
