using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jwho303.InMemoryDatabase;
using System;

namespace Jwho303.InMemoryDatabase.Example
{
	[System.Serializable]
	public class Cube : TableEntry
	{
		#region Public Properties
		public Vector3 Position;
		public Guid ColorId;
		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public Cube(Vector3 position, Guid colorId)
		{
			Position = position;
			ColorId = colorId;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}