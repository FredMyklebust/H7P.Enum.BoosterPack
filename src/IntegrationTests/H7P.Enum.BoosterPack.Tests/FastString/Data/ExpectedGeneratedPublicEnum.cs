using System;
using H7P.FastString.ConsoleApp;

namespace H7P.FastString.ConsoleApp
{
	internal static class ƓColorToFastStringExtensions
	{
		public static string ToFastString(this Color enumValue)
		{
			switch(enumValue)
			{
				case Color.Red: return "Red";
				case Color.Green: return "Green";
				default: throw new ArgumentException($"Invalid value {enumValue} specified", "enumValue");
			}
		}
	}
}
