using System;
using H7P.FastString.ConsoleApp;

namespace H7P.FastString.ConsoleApp
{
	internal static class ƓStateFastStringExtensions
	{
		public static string ToFastString(this State enumValue)
		{
			return enumValue switch
			{
				State.Valid => "Valid",
				State.Invalid => "Invalid",
				_ => throw new ArgumentException($"{enumValue} invalid enum-specified")
			};
		}
	}
}
