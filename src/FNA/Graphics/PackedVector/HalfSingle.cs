#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2024 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using Statements
using System;
using System.Numerics;
#endregion

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	public struct HalfSingle : IPackedVector<ushort>, IEquatable<HalfSingle>, IPackedVector
	{
		#region Public Properties

		[CLSCompliant(false)]
		public ushort PackedValue
		{
			get
			{
				return packedValue;
			}
			set
			{
				packedValue = value;
			}
		}

		#endregion

		#region Private Variables

		private ushort packedValue;

		#endregion

		#region Public Constructors

		public HalfSingle(float single)
		{
			packedValue = HalfTypeHelper.SingleToHalfAsU16Bits(single);
		}

		#endregion

		#region Public Methods

		public float ToSingle()
		{
			return HalfTypeHelper.HalfAsU16BitsToSingle(packedValue);
		}

		#endregion

		#region IPackedVector Methods

		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			packedValue = HalfTypeHelper.SingleToHalfAsU16Bits(vector.X);
		}

		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(ToSingle(), 0.0f, 0.0f, 1.0f);
		}

		#endregion

		#region Public Static Operators and Override Methods

		public override bool Equals(object obj)
		{
			return (obj is HalfSingle) && Equals((HalfSingle) obj);
		}

		public bool Equals(HalfSingle other)
		{
			return packedValue == other.packedValue;
		}

		public override string ToString()
		{
			return packedValue.ToString("X");
		}

		public override int GetHashCode()
		{
			return packedValue.GetHashCode();
		}

		public static bool operator ==(HalfSingle lhs, HalfSingle rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		public static bool operator !=(HalfSingle lhs, HalfSingle rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		#endregion
	}
}
