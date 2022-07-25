//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.InMemoryDatabase;

namespace RenderHeads.InMemoryDatabase.Example
{
    [CreateAssetMenu(fileName = "ColorSet", menuName = "ColorSet")]
    public class ColorSet : ScriptableObject
    {
        #region Public Properties
        public Color[] Colors;
        #endregion

        #region Private Properties

        #endregion

        #region Public Methods
        public void AddColorsToDatabase(Database database)
		{
			for (int i = 0; i < Colors.Length; i++)
			{
                database.Insert(new ColorEntry(Colors[i]));
			}
		}
        #endregion

        #region Private Methods

        #endregion
    }
}