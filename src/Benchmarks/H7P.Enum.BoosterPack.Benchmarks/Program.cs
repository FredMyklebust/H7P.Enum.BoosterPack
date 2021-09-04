using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using H7P.AutoDescriptor;
using System;
using System.ComponentModel;
using System.Linq;

namespace H7P.Enum.BoosterPack.Benchmarks
{
    public enum Size
    {
        Pewee,
        Small,
        Medium,
        Large,
        Huge,
        Jumbo
    }

    [Describable]
    public enum EggSize
    {
        [Description("Egg is Pewee")]
        Pewee,

        [Description("Egg is Small")]
        Small,

        [Description("Egg is Medium")]
        Medium,

        [Description("Egg is Large")]
        Large,

        [Description("Egg is Huge")]
        Huge,

        [Description("Egg is Jumbo")]
        Jumbo
    }

    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48), SimpleJob(RuntimeMoniker.Net50), SimpleJob(RuntimeMoniker.NetCoreApp31)]
    public class ToFastStringBenchmarks
    {

        [Params(Size.Pewee, Size.Small, Size.Medium, Size.Large, Size.Huge, Size.Jumbo)]
        public Size SizeToBench { get; set; }

        [Benchmark(Baseline = true)] public string DefaultToString() => SizeToBench.ToString();

        [Benchmark] public string ToFastString() => SizeToBench.ToFastString();
    }

    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48), SimpleJob(RuntimeMoniker.Net50), SimpleJob(RuntimeMoniker.NetCoreApp31)]
    public class ToDescriptionBenchmarks
    {

        [Params(EggSize.Pewee, EggSize.Small, EggSize.Medium, EggSize.Large, EggSize.Huge, EggSize.Jumbo)]
        public EggSize SizeToBench { get; set; }

        [Benchmark(Baseline = true)]
        public string UseReflection()
        {
            var fi = SizeToBench.GetType().GetField(SizeToBench.ToString());

            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes?.Length > 0)
            {
                return attributes.First().Description;
            }

            throw new ArgumentException($"No description for value {SizeToBench} found", "SizeToBench");
        }

        [Benchmark] public string ToDescription() => SizeToBench.GetDescription();
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
