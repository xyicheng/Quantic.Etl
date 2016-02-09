using System;
using System.IO;
using System.Reflection;

namespace Quantic.Common
{
	/// <summary>
	///     Contains memembers to obtain runtime information.
	/// </summary>
	public static class Runtime
	{
		/// <summary>
		///     Gets the full path to the calling assembly.
		/// </summary>
		/// <value>
		///     The application path.
		/// </value>
		public static string ApplicationPath => Path.GetFullPath(Assembly.GetCallingAssembly().Location);

		/// <summary>
		///     Gets the version of the executing assembly.
		/// </summary>
		public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;
	}
}