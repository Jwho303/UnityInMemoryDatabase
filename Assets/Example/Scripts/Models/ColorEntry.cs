//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;

namespace RenderHeads
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