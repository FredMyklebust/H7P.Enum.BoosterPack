using System;
using H7P.AutoDescriptor.ConsoleApp;

namespace H7P.AutoDescriptor.ConsoleApp
{
	internal static class ƓColorGetDescriptionExtensions
	{
		/// <summary>
		/// Gets the description from the <see cref="System.ComponentModel.DescriptionAttribute"/> for the supplied <see cref="Color"/> value.
		/// </summary>
		/// <param name="enumValue">The enum to get descriptions from.</param>
		/// <exception cref="ArgumentException">Throws if the enum-value doesn't have a description, or an invalid enum is supplied.</exception>
		/// <returns>
		///A <see cref="string"/> with the description.
		/// </returns>
		public static string GetDescription(this Color enumValue)
		{
			switch(enumValue)
			{
				case Color.Red: return "Rød";
				case Color.Green: return "Grønn";
				default: throw new ArgumentException($"Invalid value {enumValue} specified", "enumValue");
			}
		}
	}
}
