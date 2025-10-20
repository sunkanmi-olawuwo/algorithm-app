using AlgorithmApp.Core;
using Moq;

namespace AlgorithmApp.Tests.Core;

[TestFixture]
public class AlgorithmFactoryTests
{
    private AlgorithmFactory _factory;
    private List<IAlgorithm> _algorithms;
    
    [SetUp]
    public void Setup()
    {
        _algorithms = new List<IAlgorithm>
        {
            CreateMockAlgorithm("Algorithm1", "Category1"),
            CreateMockAlgorithm("Algorithm2", "Category1"),
            CreateMockAlgorithm("Algorithm3", "Category2"),
            CreateMockAlgorithm("Algorithm4", "Category3")
        };
        
        _factory = new AlgorithmFactory(_algorithms);
    }
    
    [Test]
    public void GetAlgorithm_WithExistingName_ReturnsAlgorithm()
    {
        // Act
        IAlgorithm result = _factory.GetAlgorithm("Algorithm2");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Algorithm2"));
    }
    
    [Test]
    public void GetAlgorithm_WithNonExistingName_ReturnsNull()
    {
        // Act
        IAlgorithm result = _factory.GetAlgorithm("NonExistingAlgorithm");
        
        // Assert
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public void GetAlgorithm_IsCaseInsensitive()
    {
        // Act
        IAlgorithm result = _factory.GetAlgorithm("aLgoRiThm3");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Algorithm3"));
    }
    
    [Test]
    public void GetAlgorithmsByCategory_ReturnsCorrectAlgorithms()
    {
        // Act
        var result = _factory.GetAlgorithmsByCategory("Category1").ToList();
        
        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("Algorithm1"));
        Assert.That(result[1].Name, Is.EqualTo("Algorithm2"));
    }
    
    [Test]
    public void GetAlgorithmsByCategory_WithNonExistingCategory_ReturnsEmptyCollection()
    {
        // Act
        IEnumerable<IAlgorithm> result = _factory.GetAlgorithmsByCategory("NonExistingCategory");
        
        // Assert
        Assert.That(result, Is.Empty);
    }
    
    [Test]
    public void GetAlgorithmsByCategory_IsCaseInsensitive()
    {
        // Act
        var result = _factory.GetAlgorithmsByCategory("cAtEgOrY3").ToList();
        
        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Algorithm4"));
    }
    
    [Test]
    public void GetCategories_ReturnsAllUniqueCategories()
    {
        // Act
        var result = _factory.GetCategories().ToList();
        
        // Assert
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result, Contains.Item("Category1"));
        Assert.That(result, Contains.Item("Category2"));
        Assert.That(result, Contains.Item("Category3"));
    }
    
    [Test]
    public void GetAllAlgorithms_ReturnsAllAlgorithms()
    {
        // Act
        var result = _factory.GetAllAlgorithms().ToList();
        
        // Assert
        Assert.That(result, Has.Count.EqualTo(4));
        Assert.That(result[0].Name, Is.EqualTo("Algorithm1"));
        Assert.That(result[1].Name, Is.EqualTo("Algorithm2"));
        Assert.That(result[2].Name, Is.EqualTo("Algorithm3"));
        Assert.That(result[3].Name, Is.EqualTo("Algorithm4"));
    }
    
    // Helper method to create mock algorithms
    private static IAlgorithm CreateMockAlgorithm(string name, string category)
    {
        var mock = new Mock<IAlgorithm>();
        mock.Setup(a => a.Name).Returns(name);
        mock.Setup(a => a.Category).Returns(category);
        return mock.Object;
    }
}
