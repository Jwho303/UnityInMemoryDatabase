using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jwho303.InMemoryDatabase;

namespace Jwho303.InMemoryDatabase.Example
{
    public class ColorEntry : TableEntry
    {
		#region Public Properties
		public Color Color {get; set;}
        #endregion

        #region Private Properties

        #endregion

        #region Public Methods
        public ColorEntry(Color color)
        {
            Color = color;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}