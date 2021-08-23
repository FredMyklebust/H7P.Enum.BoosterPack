﻿using System;
using H7P.AutoDescriptor.ConsoleApp;

namespace H7P.AutoDescriptor.ConsoleApp
{
	internal static class ƓTilstandExtensions
	{
		/// <summary>
		/// Gets the description from the <see cref="System.ComponentModel.DescriptionAttribute"/> for the supplied <see cref="Tilstand"/> value.
		/// </summary>
		/// <exception cref="ArgumentException"> Throws if the description for the value is missing, or an invalid value is supplied.</exception> 
		/// <param name="enumValue"></param>
		/// <returns>
		/// A <see cref="string"/> with the description.
		/// </returns>
		public static string GetDescription(this Tilstand enumValue)
		{
			switch(enumValue)
			{
				case Tilstand.Valid: return "Gyldig";
				case Tilstand.Invalid: return "Ugyldig";
				default: throw new ArgumentException($"{enumValue} does not have an description attribute");
			};
		}
	}
}
