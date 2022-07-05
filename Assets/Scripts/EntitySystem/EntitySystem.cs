//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;
using System;

namespace RenderHeads
{
    public abstract class EntitySystem<T> where T : TableEntry
    {
        #region Public Properties

        #endregion

        #region Private Properties
        protected Database _database;
        protected EntityPool<T> _entityPool;
        #endregion

        #region Public Methods
        public EntitySystem(Database database, EntityPool<T> entityPool, int maxSize)
        {
            _database = database;
            _entityPool = entityPool;

            _entityPool.Initialize(OnCreateEntity, OnGetEntity, OnReleaseEntity, maxSize);
        }

		protected abstract void OnReleaseEntity(GameEntity<T> gameEntity);
		protected abstract void OnGetEntity(GameEntity<T> gameEntity);
		protected abstract void OnCreateEntity(GameEntity<T> gameEntity);
		public abstract void OnUpdate();
        #endregion

        #region Private Methods

        #endregion
    }
}