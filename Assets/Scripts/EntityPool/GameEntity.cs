using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jwho303.InMemoryDatabase;
using System;

namespace Jwho303.EntitySystem
{
    public abstract class GameEntity<T> : MonoBehaviour where T : TableEntry
    {
        #region Public Properties
        public T TableEntry;
        public Guid Id => TableEntry.Id;
        #endregion

        #region Private Properties

        #endregion

        #region Public Methods
        public void Initialize(T tableEntry)
        {
            TableEntry = tableEntry;
		}

		internal void Deinitialize()
		{
            TableEntry = null;
		}

        public abstract void OnCreate();
		#endregion

		#region Private Methods

		#endregion
	}
}