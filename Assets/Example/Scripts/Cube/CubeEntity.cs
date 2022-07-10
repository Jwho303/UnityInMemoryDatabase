//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads
{
	public class CubeEntity : GameEntity<Cube>
	{
		#region Public Properties

		#endregion

		#region Private Properties
		[SerializeField]
		private MeshRenderer _meshRenderer;
		#endregion

		#region Public Methods
		public override void OnCreate()
		{
			_meshRenderer = GetComponentInChildren<MeshRenderer>();
		}

		public void SetMaterial(Material material)
		{
			_meshRenderer.material = material;
		}
		#endregion

		#region Private Methods

		#endregion

	}
}