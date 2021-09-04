using System;
using H7P.AsString.ConsoleApp;

namespace H7P.AsString.ConsoleApp
{
	internal static class ƓStateAsStringExtensions
	{
		/// <summary>
		/// Gets the string representation for the supplied <see cref="State"/> value.
		/// </summary>
		/// <param name="enumValue">The enum to get the string representation from.</param>
		/// <exception cref="ArgumentException">Throws if an invalid enum is supplied.</exception>
		/// <returns>
		///A <see cref="string"/>.
		/// </returns>
		public static string AsString(this State enumValue)
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
