namespace DotnetBotfactory.Tests;

public class TemplateReverseEngineer
{
    public string GetTemplateSourceCode(IReadOnlyList<TemplateVariant> templates)
    {
        
    }
}

public class TemplateVariant
{
    public string Value { get; set; }
    public Dictionary<string, object> Configuration { get; set; }
}

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        
    }
}