using AlgorithmApp.Core;
using Moq;

namespace AlgorithmApp.Tests.Core;

[TestFixture]
public class AlgorithmComparerTests
{
    private Mock<IAlgorithmFactory> _mockFactory;
    private Mock<IPerformanceMeasurer> _mockMeasurer;
    private AlgorithmComparer _comparer;
    
    [SetUp]
    public void Setup()
    {
        _mockFactory = new Mock<IAlgorithmFactory>();
        _mockMeasurer = new Mock<IPerformanceMeasurer>();
        _comparer = new AlgorithmComparer(_mockFactory.Object, _mockMeasurer.Object);
    }
    
    [Test]
    public void CompareAlgorithms_WithValidAlgorithms_ReturnsComparisonResult()
    {
        // Arrange
        string[] algorithms = ["Algorithm1", "Algorithm2"];
        Mock<IAlgorithm> mockAlg1 = SetupMockAlgorithm("Algorithm1");
        Mock<IAlgorithm> mockAlg2 = SetupMockAlgorithm("Algorithm2");
        
        _mockFactory.Setup(f => f.GetAlgorithm("Algorithm1")).Returns(mockAlg1.Object);
        _mockFactory.Setup(f => f.GetAlgorithm("Algorithm2")).Returns(mockAlg2.Object);
        
        var metrics1 = new PerformanceMetrics(TimeSpan.FromMilliseconds(100), 1000);
        var metrics2 = new PerformanceMetrics(TimeSpan.FromMilliseconds(50), 2000);
        
        // Setup the measurer to return different metrics for different calls
        int sequence = 0;
        _mockMeasurer.Setup(m => m.Measure(It.IsAny<Action>()))
            .Returns(() => {
                sequence++;
                return sequence == 1 ? metrics1 : metrics2;
            })
            .Callback<Action>(action => action()); // Execute the action
            
        // Act
        ComparisonResult result = _comparer.CompareAlgorithms(algorithms, 10);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Results.Count, Is.EqualTo(2));
            Assert.That(result.FastestAlgorithm, Is.EqualTo("Algorithm2"));
            Assert.That(result.MostMemoryEfficient, Is.EqualTo("Algorithm1"));
            Assert.That(result.Results["Algorithm1"], Is.EqualTo(metrics1));
            Assert.That(result.Results["Algorithm2"], Is.EqualTo(metrics2));
        });
    }
    
    [Test]
    public void CompareAlgorithms_WithMissingAlgorithm_SkipsIt()
    {
        // Arrange
        string[] algorithms = ["Algorithm1", "NonExistentAlgorithm"];
        Mock<IAlgorithm> mockAlg1 = SetupMockAlgorithm("Algorithm1");
        
        _mockFactory.Setup(f => f.GetAlgorithm("Algorithm1")).Returns(mockAlg1.Object);
        _mockFactory.Setup(f => f.GetAlgorithm("NonExistentAlgorithm")).Returns((IAlgorithm)null!);
        
        var metrics = new PerformanceMetrics(TimeSpan.FromMilliseconds(100), 1000);
        _mockMeasurer.Setup(m => m.Measure(It.IsAny<Action>()))
            .Returns(metrics)
            .Callback<Action>(action => action());
            
        // Act
        ComparisonResult result = _comparer.CompareAlgorithms(algorithms, 10);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Results.Count, Is.EqualTo(1));
            Assert.That(result.FastestAlgorithm, Is.EqualTo("Algorithm1"));
            Assert.That(result.MostMemoryEfficient, Is.EqualTo("Algorithm1"));
        });
    }
    
    [Test]
    public void CompareAlgorithms_WithInvalidInput_SkipsAlgorithm()
    {
        // Arrange
        string[] algorithms = ["Algorithm1", "InvalidAlgorithm"];
        
        Mock<IAlgorithm> mockAlg1 = SetupMockAlgorithm("Algorithm1");
        var mockInvalid = new Mock<IAlgorithm>();
        mockInvalid.Setup(a => a.Name).Returns("InvalidAlgorithm");
        mockInvalid.Setup(a => a.GenerateSampleInput(It.IsAny<int>())).Returns("invalid input");
        mockInvalid.Setup(a => a.ValidateInput(It.IsAny<object>())).Returns(false); // Input validation fails
        
        _mockFactory.Setup(f => f.GetAlgorithm("Algorithm1")).Returns(mockAlg1.Object);
        _mockFactory.Setup(f => f.GetAlgorithm("InvalidAlgorithm")).Returns(mockInvalid.Object);
        
        var metrics = new PerformanceMetrics(TimeSpan.FromMilliseconds(100), 1000);
        _mockMeasurer.Setup(m => m.Measure(It.IsAny<Action>()))
            .Returns(metrics)
            .Callback<Action>(action => action());
            
        // Act
        ComparisonResult result = _comparer.CompareAlgorithms(algorithms, 10);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Results.Count, Is.EqualTo(1));
            Assert.That(result.Results.ContainsKey("InvalidAlgorithm"), Is.False);
        });
    }
    
    [Test]
    public void CompareAlgorithms_WithNoValidAlgorithms_ReturnsEmptyResult()
    {
        // Arrange
        string[] algorithms = ["NonExistent1", "NonExistent2"];
        
        _mockFactory.Setup(f => f.GetAlgorithm(It.IsAny<string>())).Returns((IAlgorithm)null!);
        
        // Act
        ComparisonResult result = _comparer.CompareAlgorithms(algorithms, 10);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Results, Is.Empty);
            Assert.That(result.FastestAlgorithm, Is.Empty);
            Assert.That(result.MostMemoryEfficient, Is.Empty);
        });
    }
    
    // Helper method to set up a mock algorithm
    private Mock<IAlgorithm> SetupMockAlgorithm(string name)
    {
        var mockAlg = new Mock<IAlgorithm>();
        mockAlg.Setup(a => a.Name).Returns(name);
        mockAlg.Setup(a => a.GenerateSampleInput(It.IsAny<int>())).Returns(new int[] { 1, 2, 3 });
        mockAlg.Setup(a => a.ValidateInput(It.IsAny<object>())).Returns(true);
        mockAlg.Setup(a => a.ExecuteAsync(It.IsAny<object>())).Returns(new AlgorithmResult { AlgorithmName = name });
        return mockAlg;
    }
}
