using System;
using H7P.FastString.ConsoleApp;

namespace H7P.FastString.ConsoleApp
{
	internal static class ƓColorFastStringExtensions
	{
		public static string ToFastString(this Color enumValue)
		{
			return enumValue switch
			{
				Color.Red => "Red",
				Color.Green => "Green",
				_ => throw new ArgumentException($"{enumValue} invalid enum-specified")
			};
		}
	}
}
