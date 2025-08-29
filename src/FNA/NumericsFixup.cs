global using Vector2 = System.Numerics.Vector2;
global using Vector3 = System.Numerics.Vector3;
global using Vector4 = System.Numerics.Vector4;
global using Quaternion = System.Numerics.Quaternion;
global using Point = System.Drawing.Point;
global using Rectangle = System.Drawing.Rectangle;
global using Matrix = System.Numerics.Matrix4x4;

global using static Microsoft.Xna.Framework.NumericsExtensions;

using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Xna.Framework;

public static class NumericsExtensions
{
	public static Vector3 Vector3_Up { get; } = new(0f, 1f, 0f);

	public static Vector3 Vector3_Forward { get; } = new(0f, 0f, -1f);

	internal static string DebugDisplayString(this Vector3 vector)
	{
		return string.Concat(
			vector.X.ToString(CultureInfo.InvariantCulture),
			" ",
			vector.Y.ToString(CultureInfo.InvariantCulture),
			" ",
			vector.Z.ToString(CultureInfo.InvariantCulture)
		);
	}

	[Conditional("DEBUG")]
	internal static void CheckForNaNs(this Vector2 vector)
	{
		if (float.IsNaN(vector.X) || float.IsNaN(vector.Y))
		{
			throw new InvalidOperationException("Vector2 contains NaNs!");
		}
	}

	[Conditional("DEBUG")]
	internal static void CheckForNaNs(this Vector3 vector)
	{
		if (float.IsNaN(vector.X) ||
		    float.IsNaN(vector.Y) ||
		    float.IsNaN(vector.Z))
		{
			throw new InvalidOperationException("Vector3 contains NaNs!");
		}
	}

	[Conditional("DEBUG")]
	internal static void CheckForNaNs(this Vector4 vector)
	{
		if (float.IsNaN(vector.X) ||
		    float.IsNaN(vector.Y) ||
		    float.IsNaN(vector.Z) ||
		    float.IsNaN(vector.W))
		{
			throw new InvalidOperationException("Vector4 contains NaNs!");
		}
	}

	[Conditional("DEBUG")]
	internal static void CheckForNaNs(this Matrix matrix)
	{
		if (float.IsNaN(matrix.M11) ||
		    float.IsNaN(matrix.M12) ||
		    float.IsNaN(matrix.M13) ||
		    float.IsNaN(matrix.M14) ||
		    float.IsNaN(matrix.M21) ||
		    float.IsNaN(matrix.M22) ||
		    float.IsNaN(matrix.M23) ||
		    float.IsNaN(matrix.M24) ||
		    float.IsNaN(matrix.M31) ||
		    float.IsNaN(matrix.M32) ||
		    float.IsNaN(matrix.M33) ||
		    float.IsNaN(matrix.M34) ||
		    float.IsNaN(matrix.M41) ||
		    float.IsNaN(matrix.M42) ||
		    float.IsNaN(matrix.M43) ||
		    float.IsNaN(matrix.M44))
		{
			throw new InvalidOperationException("Matrix contains NaNs!");
		}
	}

	[Conditional("DEBUG")]
	internal static void CheckForNaNs(this Quaternion quaternion)
	{
		if (float.IsNaN(quaternion.X) ||
		    float.IsNaN(quaternion.Y) ||
		    float.IsNaN(quaternion.Z) ||
		    float.IsNaN(quaternion.W))
		{
			throw new InvalidOperationException("Quaternion contains NaNs!");
		}
	}

	public static void Normalize(ref this Vector2 vector)
	{
		var val = 1.0f / (float) Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
		vector.X *= val;
		vector.Y *= val;
	}

	public static void Normalize(ref this Vector3 vector)
	{
		var factor = 1.0f / (float) Math.Sqrt(
			(vector.X * vector.X) +
			(vector.Y * vector.Y) +
			(vector.Z * vector.Z)
		);
		vector.X *= factor;
		vector.Y *= factor;
		vector.Z *= factor;
	}

	public static void Normalize(ref this Vector4 vector)
	{
		var factor = 1.0f / (float) Math.Sqrt(
			(vector.X * vector.X) +
			(vector.Y * vector.Y) +
			(vector.Z * vector.Z) +
			(vector.W * vector.W)
		);
		vector.X *= factor;
		vector.Y *= factor;
		vector.Z *= factor;
		vector.W *= factor;
	}

	public static Vector2 CatmullRom(
		Vector2 value1,
		Vector2 value2,
		Vector2 value3,
		Vector2 value4,
		float amount
	)
	{
		return new Vector2(
			MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
			MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount)
		);
	}

	public static Vector2 Hermite(
		Vector2 value1,
		Vector2 tangent1,
		Vector2 value2,
		Vector2 tangent2,
		float amount
	)
	{
		var result = new Vector2();
		Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
		return result;
	}

	public static void Hermite(
		ref Vector2 value1,
		ref Vector2 tangent1,
		ref Vector2 value2,
		ref Vector2 tangent2,
		float amount,
		out Vector2 result
	)
	{
		result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
		result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
	}

	public static Vector3 Hermite(
		Vector3 value1,
		Vector3 tangent1,
		Vector3 value2,
		Vector3 tangent2,
		float amount
	)
	{
		Vector3 result = new Vector3();
		Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
		return result;
	}

	public static void Hermite(
		ref Vector3 value1,
		ref Vector3 tangent1,
		ref Vector3 value2,
		ref Vector3 tangent2,
		float amount,
		out Vector3 result
	)
	{
		result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
		result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
		result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
	}

	public static Vector4 Hermite(
		Vector4 value1,
		Vector4 tangent1,
		Vector4 value2,
		Vector4 tangent2,
		float amount
	)
	{
		return new Vector4(
			MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
			MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount),
			MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount),
			MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount)
		);
	}

	public static void Hermite(
		ref Vector4 value1,
		ref Vector4 tangent1,
		ref Vector4 value2,
		ref Vector4 tangent2,
		float amount,
		out Vector4 result
	)
	{
		result.W = MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount);
		result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
		result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
		result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
	}

	public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
	{
		return new Vector2(
			MathHelper.SmoothStep(value1.X, value2.X, amount),
			MathHelper.SmoothStep(value1.Y, value2.Y, amount)
		);
	}

	public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
	{
		return new Vector3(
			MathHelper.SmoothStep(value1.X, value2.X, amount),
			MathHelper.SmoothStep(value1.Y, value2.Y, amount),
			MathHelper.SmoothStep(value1.Z, value2.Z, amount)
		);
	}

	public static void SmoothStep(
		ref Vector3 value1,
		ref Vector3 value2,
		float amount,
		out Vector3 result
	)
	{
		result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
		result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
		result.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
	}

	public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, float amount)
	{
		return new Vector4(
			MathHelper.SmoothStep(value1.X, value2.X, amount),
			MathHelper.SmoothStep(value1.Y, value2.Y, amount),
			MathHelper.SmoothStep(value1.Z, value2.Z, amount),
			MathHelper.SmoothStep(value1.W, value2.W, amount)
		);
	}

	public static void SmoothStep(
		ref Vector4 value1,
		ref Vector4 value2,
		float amount,
		out Vector4 result
	)
	{
		result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
		result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
		result.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
		result.W = MathHelper.SmoothStep(value1.W, value2.W, amount);
	}
}
