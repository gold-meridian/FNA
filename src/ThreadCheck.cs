#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2023 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */

/* Derived from code by the Mono.Xna Team (Copyright 2006).
 * Released under the MIT License. See monoxna.LICENSE for details.
 */
#endregion

#if USE_TML_EXTENSIONS
using System;
using System.Threading;

namespace Microsoft.Xna.Framework {
	public static class ThreadCheck {
		private static int mainThreadId = -1;

		public static bool IsMainThread => Environment.CurrentManagedThreadId == mainThreadId;

		public static void CheckThread() {
			if (mainThreadId < 0)
				mainThreadId = Environment.CurrentManagedThreadId;
			else if (!IsMainThread)
				throw new ThreadStateException("Most FNA3D audio/graphics functions must be called on the main thread!");
		}
	}
}
#endif
