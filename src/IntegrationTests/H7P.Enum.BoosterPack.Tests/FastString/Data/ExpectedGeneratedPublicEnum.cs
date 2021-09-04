using System;
using H7P.FastString.ConsoleApp;

namespace H7P.FastString.ConsoleApp
{
	internal static class ƓColorToFastStringExtensions
	{
		/// <summary>
		/// Gets the string representation for the supplied <see cref="Color"/> value.
		/// </summary>
		/// <param name="enumValue">The enum to get the string representation from.</param>
		/// <returns>
		///A <see cref="string"/>.
		/// </returns>
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
