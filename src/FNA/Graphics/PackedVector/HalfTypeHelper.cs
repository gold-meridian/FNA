using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics.PackedVector;

internal static class HalfTypeHelper
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Half SingleToHalf(float single)
	{
		return (Half) single;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float HalfToSingle(Half half)
	{
		return (float) half;
	}

	public static ushort SingleToHalfAsU16Bits(float single)
	{
		return Unsafe.BitCast<Half, ushort>(SingleToHalf(single));
	}

	public static float HalfAsU16BitsToSingle(ushort half)
	{
		return HalfToSingle(Unsafe.BitCast<ushort, Half>(half));
	}
	
	public static Half U16BitsToHalf(ushort value)
	{
		return Unsafe.BitCast<ushort, Half>(value);
	}
}
