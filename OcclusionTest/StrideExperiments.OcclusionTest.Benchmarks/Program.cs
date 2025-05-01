using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using StrideExperiments.OcclusionTest.UnitTests;

[SimpleJob(RuntimeMoniker.Net80)]
[PlainExporter]
public class OcclusionTest
{
    [Benchmark]
    public void Benchmark()
    {
        var benchmark = new ObjectInViewTest();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<OcclusionTest>();
    }
}