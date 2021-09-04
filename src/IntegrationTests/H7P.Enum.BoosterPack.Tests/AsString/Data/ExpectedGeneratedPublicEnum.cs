using System;
using H7P.AsString.ConsoleApp;

namespace H7P.AsString.ConsoleApp
{
	internal static class ƓColorAsStringExtensions
	{
		/// <summary>
		/// Gets the string representation for the supplied <see cref="Color"/> value.
		/// </summary>
		/// <param name="enumValue">The enum to get the string representation from.</param>
		/// <exception cref="ArgumentException">Throws if an invalid enum is supplied.</exception>
		/// <returns>
		///A <see cref="string"/>.
		/// </returns>
		public static string AsString(this Color enumValue)
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
