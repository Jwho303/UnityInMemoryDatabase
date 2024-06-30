using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jwho303.InMemoryDatabase;
using System;

namespace Jwho303.InMemoryDatabase.Example
{
    [System.Serializable]
    public class Ball : TableEntry
    {
        #region Public Properties
        public Guid ColorId;
        public Vector3 Position;
		public Guid TargetCubeId;
		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public Ball(Guid colorId, Vector3 position, Guid targetCube)
		{
			ColorId = colorId;
			Position = position;
			TargetCubeId = targetCube;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}