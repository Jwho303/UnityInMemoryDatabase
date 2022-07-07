//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;

namespace RenderHeads
{
    [System.Serializable]
    public class Cube : TableEntry
    {
        #region Public Properties
        public Vector3 Position;

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public Cube(Vector3 position)
		{
			Position = position;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}