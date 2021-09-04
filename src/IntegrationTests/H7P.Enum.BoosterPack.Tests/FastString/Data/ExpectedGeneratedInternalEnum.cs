using System;
using H7P.FastString.ConsoleApp;

namespace H7P.FastString.ConsoleApp
{
	internal static class ƓStateToFastStringExtensions
	{
		/// <summary>
		/// Gets the string representation for the supplied <see cref="State"/> value.
		/// </summary>
		/// <param name="enumValue">The enum to get the string representation from.</param>
		/// <returns>
		///A <see cref="string"/>.
		/// </returns>
		public static string ToFastString(this State enumValue)
		{
			switch(enumValue)
			{
				case State.Valid: return "Valid";
				case State.Invalid: return "Invalid";
				default: throw new ArgumentException($"Invalid value {enumValue} specified", "enumValue");
			}
		}
	}
}
