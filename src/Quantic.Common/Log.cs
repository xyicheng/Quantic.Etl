using System;
using System.Diagnostics;
using log4net;

namespace Quantic.Common
{
	public static class Log
	{
		/// <summary>
		///     Gets a logging type for the calling member's declaring type.
		/// </summary>
		/// <returns></returns>
		public static ILog Get()
		{
			return LogManager.GetLogger(new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name);
		}

		/// <summary>
		///     Gets the specified named logging instance.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static ILog Get(string name)
		{
			return LogManager.GetLogger(name);
		}

		/// <summary>
		///     Gets a logging instance for the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static ILog Get(Type type)
		{
			Requires.NotNull(type, nameof(type));

			return LogManager.GetLogger(type);
		}
	}
}