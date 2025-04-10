#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2025 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

using System;
using System.Threading;

namespace Microsoft.Xna.Framework
{
	// tModLoader(thread-safety): Implementation for thread checking which
	// serves as as a guard for call-sites to invoke.
	public static class ThreadCheck
	{
		// Tomat: Add easy option to disable.  Purposefully kept private,
		// access it with reflection.
		private static bool enabled = false;

		private static int mainThreadId = -1;

		public static bool IsMainThread => Environment.CurrentManagedThreadId == mainThreadId;

		public static void CheckThread()
		{
			if (!enabled)
			{
				return;
			}

			if (mainThreadId < 0)
				mainThreadId = Environment.CurrentManagedThreadId;
			else if (!IsMainThread)
				throw new ThreadStateException("most FNA3D audio/graphics functions must be called on the main thread");
		}
	}
}
