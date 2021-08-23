﻿using System;
using H7P.FastString.ConsoleApp;

namespace H7P.FastString.ConsoleApp
{
	internal static class ƓStateFastStringExtensions
	{
		public static string ToFastString(this State enumValue)
		{
			switch(enumValue)
			{
				case State.Valid: return "Valid";
				case State.Invalid: return "Invalid";
				default: throw new ArgumentException($"{enumValue} invalid enum-specified");
			};
		}
	}
}
