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
		private Material _material;
		#endregion

		#region Public Methods
		public override void OnCreate()
		{
			_material = GetComponentInChildren<MeshRenderer>().material;
		}

		public void SetColor(Color color)
		{
			_material.color = color;
		}
		#endregion

		#region Private Methods

		#endregion

	}
}