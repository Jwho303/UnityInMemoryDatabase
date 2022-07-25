//RenderHeads - Jeff Rusch
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.EntitySystem;

namespace RenderHeads.InMemoryDatabase.Example
{
	public class BallEntity : GameEntity<Ball>
	{
		#region Public Properties

		#endregion

		#region Private Properties
		[SerializeField]
		private float _speed = 5f;
		private Vector3 _targetPosition;
		[SerializeField]
		private MeshRenderer _meshRenderer;
		#endregion

		#region Public Methods
		public override void OnCreate()
		{
			_meshRenderer = GetComponentInChildren<MeshRenderer>();
		}

		public void SetTargetPosition(Vector3 targetPosition)
		{
			_targetPosition = targetPosition;
		}

		internal void SetMaterial(Material material)
		{
			_meshRenderer.material = material;
		}

		internal bool Arrived()
		{
			return Vector3.Distance(this.transform.position, _targetPosition) < 0.1f;
		}

		internal void MoveToTarget()
		{
			Vector3 direction = _targetPosition - transform.position;
			transform.Translate(direction * _speed * Time.deltaTime);
		}


		#endregion

		#region Private Methods

		#endregion
	}
}