using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Benchmark;

internal class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run(typeof(Benchmarks));
		//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig()); //Zum Debuggen (umschalten auf Debug), obere Zeile auskommentieren
	}
}