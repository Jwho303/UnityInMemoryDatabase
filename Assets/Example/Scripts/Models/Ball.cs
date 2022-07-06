//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;

namespace RenderHeads
{
    [System.Serializable]
    public class Ball : TableEntry
    {
        #region Public Properties
        public int ColorIndex;
        public Vector3 Position;
		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public Ball(int colorIndex, Vector3 position)
		{
			ColorIndex = colorIndex;
			Position = position;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}