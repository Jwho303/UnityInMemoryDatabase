//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;
using System;

namespace RenderHeads
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
        #endregion

        #region Private Methods

        #endregion
    }
}