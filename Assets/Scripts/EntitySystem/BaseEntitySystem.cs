using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jwho303.InMemoryDatabase;

namespace Jwho303.EntitySystem
{
    public abstract class BaseEntitySystem<T> where T : TableEntry
    {
        #region Public Properties

        #endregion

        #region Private Properties
        protected Database _database;
        #endregion

        #region Public Methods
        public BaseEntitySystem(Database database)
        {
            _database = database;
        }

        public abstract void OnUpdate();
        #endregion

        #region Private Methods

        #endregion
    }
}