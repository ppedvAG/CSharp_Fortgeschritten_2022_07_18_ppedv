namespace LinqErweiterungsmethoden2
{
	public static class ExtensionMethods
	{
		public static int Quersumme(this int x)
		{
			return x.ToString().ToCharArray().Sum(e => (int) char.GetNumericValue(e));
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
		{
			return list.OrderBy(e => Random.Shared.Next());
		}
	}
}
