using System.Diagnostics;

namespace Multitasking;

internal class ParallelForDemo
{
	static void Main(string[] args)
	{
		int[] durchgänge = { 1000, 10000, 50000, 100000, 250000, 500000, 1000000, 5000000, 10000000, 100000000 };

		foreach (int i in durchgänge)
		{
			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(i);
			sw.Stop();
			Console.WriteLine($"For Durchgänge {i}: {sw.ElapsedMilliseconds}");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(i);
			sw2.Stop();
			Console.WriteLine($"Parallel For Durchgänge {i}: {sw2.ElapsedMilliseconds}");
		}

		Console.ReadKey();

		/*
		    For Durchgänge 1000: 0
			Parallel For Durchgänge 1000: 33
			For Durchgänge 10000: 3
			Parallel For Durchgänge 10000: 33
			For Durchgänge 50000: 14
			Parallel For Durchgänge 50000: 7
			For Durchgänge 100000: 30
			Parallel For Durchgänge 100000: 17
			For Durchgänge 250000: 148
			Parallel For Durchgänge 250000: 107
			For Durchgänge 500000: 248				------- Ab hier doppelt so schnell
			Parallel For Durchgänge 500000: 125
			For Durchgänge 1000000: 405
			Parallel For Durchgänge 1000000: 161
			For Durchgänge 5000000: 2328
			Parallel For Durchgänge 5000000: 540
			For Durchgänge 10000000: 1681
			Parallel For Durchgänge 10000000: 1028
			For Durchgänge 100000000: 13525
			Parallel For Durchgänge 100000000: 5564
		*/
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
		{
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
		}
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		Parallel.For(0, iterations, i =>
		{
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
		});
	}
}
