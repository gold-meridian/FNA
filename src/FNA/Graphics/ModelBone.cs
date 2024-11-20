#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2024 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using Statements
using System.Collections.Generic;
using System.Numerics;
#endregion

namespace Microsoft.Xna.Framework.Graphics
{
	/// <summary>
	/// Represents bone data for a model.
	/// </summary>
	public sealed class ModelBone
	{
		#region Public Properties

		/// <summary>
		/// Gets a collection of bones that are children of this bone.
		/// </summary>
		public ModelBoneCollection Children
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the index of this bone in the Bones collection.
		/// </summary>
		public int Index
		{
			get;
			internal set;
		}

		/// <summary>
		/// Gets the name of this bone.
		/// </summary>
		public string Name
		{
			get;
			internal set;
		}

		/// <summary>
		/// Gets the parent of this bone.
		/// </summary>
		public ModelBone Parent
		{
			get;
			internal set;
		}

		/// <summary>
		/// Gets or sets the Matrix4x4 used to transform this bone relative to its parent
		/// bone.
		/// </summary>
		public Matrix4x4 Transform
		{
			get;
			set;
		}

		#endregion

		#region Private Variables

		private List<ModelBone> children = new List<ModelBone>();
		private List<ModelMesh> meshes = new List<ModelMesh>();

		#endregion

		#region Internal Constructor

		internal ModelBone()
		{
			Children = new ModelBoneCollection(new List<ModelBone>());
			meshes = new List<ModelMesh>();
		}

		#endregion

		#region Internal Methods

		internal void AddMesh(ModelMesh mesh)
		{
			meshes.Add(mesh);
		}

		internal void AddChild(ModelBone modelBone)
		{
			children.Add(modelBone);
			Children = new ModelBoneCollection(children);
		}

		#endregion
	}
}
