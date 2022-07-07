//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;
using System;

namespace RenderHeads
{
    public abstract class EntitySystem<T, Y> where T : TableEntry where Y: GameEntity<T>
    {
        #region Public Properties

        #endregion

        #region Private Properties
        protected Database _database;
        protected EntityPool<T, Y> _entityPool;
        #endregion

        #region Public Methods
        public EntitySystem(Database database, EntityPool<T, Y> entityPool, int maxSize)
        {
            _database = database;
            _entityPool = entityPool;

            _entityPool.Initialize(OnCreateEntity, OnGetEntity, OnReleaseEntity, maxSize);
            Debug.Log($"[{this.GetType().Name}] Hello");
        }

		protected abstract void OnReleaseEntity(Y gameEntity);
		protected abstract void OnGetEntity(Y gameEntity);
		protected abstract void OnCreateEntity(Y gameEntity);
		public abstract void OnUpdate();
        #endregion

        #region Private Methods

        #endregion
    }
}