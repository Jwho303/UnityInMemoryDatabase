//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads
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
		private Material _material;
		#endregion

		#region Public Methods
		public override void OnCreate()
		{
			_material = GetComponent<MeshRenderer>().material;
		}

		public void SetTargetPosition(Vector3 targetPosition)
		{
			_targetPosition = targetPosition;
		}

		public void SetColor(Color color)
		{
			_material.color = color;
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