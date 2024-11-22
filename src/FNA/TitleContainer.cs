#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2024 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

using System;
using System.IO;

namespace Microsoft.Xna.Framework;

public static class TitleContainer
{
	internal static string TitlePath => FNAPlatform.TitleLocation;

	// On Linux, the file system is case-sensitive.  This means that unless you
	// really focused on it, there's a good chance that your filenames are not
	// actually accurate!  The result: File/DirectoryNotFound.  This is a quick
	// alternative to MONO_IOMAP=all, but the point is that you should NOT
	// depend on either of these two things.  PLEASE, fix your paths!
	// -flibit
	private static readonly bool use_case_sensitivity_hack = Environment.GetEnvironmentVariable("FNA_CASE_SENSITIVITY_HACK") == "1";

	public static Stream OpenStream(string name)
	{
		return File.OpenRead(GetNormalizedPathName(name));
	}

	internal static IntPtr ReadToPointer(string name, out IntPtr size)
	{
		name = GetNormalizedPathName(name);
		
		if (!File.Exists(name))
		{
			throw new FileNotFoundException(name);
		}
		return FNAPlatform.ReadFileToPointer(name, out size);
	}

	private static string GetNormalizedPathName(string name)
	{
		var safeName = MonoGame.Utilities.FileHelpers.NormalizeFilePathSeparators(name);

		if (use_case_sensitivity_hack)
		{
			safeName = GetCaseName(
				Path.IsPathRooted(safeName)
					? safeName
					: Path.Combine(TitlePath, safeName)
			);
		}

		return Path.IsPathRooted(safeName)
			? safeName
			: Path.Combine(TitlePath, safeName);
	}

	private static string GetCaseName(string name)
	{
		if (File.Exists(name))
		{
			return name;
		}

		string[] splits = name.Split(Path.DirectorySeparatorChar);
		splits[0] = "/";
		int i;

		// The directories...
		for (i = 1; i < splits.Length - 1; i += 1)
		{
			splits[0] += SearchCase(
				splits[i],
				Directory.GetDirectories(splits[0])
			);
		}

		// The file...
		splits[0] += SearchCase(
			splits[i],
			Directory.GetFiles(splits[0])
		);

		// Finally.
		splits[0] = splits[0].Remove(0, 1);
		FNALoggerEXT.LogError(
			"Case sensitivity!\n\t"                   +
			name.Substring(TitlePath.Length) + "\n\t" +
			splits[0].Substring(TitlePath.Length)
		);
		return splits[0];
	}

	private static string SearchCase(string name, string[] list)
	{
		foreach (string l in list)
		{
			string li = l.Substring(l.LastIndexOf("/") + 1);
			if (name.ToLower().Equals(li.ToLower()))
			{
				return Path.DirectorySeparatorChar + li;
			}
		}
		// If you got here, get ready to crash!
		return Path.DirectorySeparatorChar + name;
	}
}
