//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;
using System;

namespace RenderHeads
{
    public abstract class EntityPool<T> : MonoBehaviour where T : TableEntry
    {
        #region Public Properties
        public GameEntity<T> Prefab;
        #endregion

        #region Private Properties
        private List<GameEntity<T>> _gameEntities = new List<GameEntity<T>>();
        private Action<GameEntity<T>> _onCreateEntity = delegate { };
        private Action<GameEntity<T>> _onGetEntity = delegate { };
        private Action<GameEntity<T>> _onReleaseEntity = delegate { };
        private int _maxSize;

		#endregion

		#region Public Methods
		public void Initialize(Action<GameEntity<T>> onCreateEntity, Action<GameEntity<T>> onGetEntity, Action<GameEntity<T>> onReleaseEntity, int maxSize)
		{
			_onCreateEntity = onCreateEntity;
			_onGetEntity = onGetEntity;
			_onReleaseEntity = onReleaseEntity;
			_maxSize = maxSize;

			for (int i = 0; i < _maxSize; i++)
			{
                CreateEntity();
			}
		}

		public GameEntity<T> Get()
        {
            GameEntity<T> gameEntity = null;

            for (int i = 0; i < _gameEntities.Count; i++)
            {
                if (!_gameEntities[i].gameObject.activeInHierarchy)
                {
                    gameEntity = _gameEntities[i];
                    break;
                }
            }

            if (gameEntity == null)
			{
                gameEntity = CreateEntity();
			}

            _onGetEntity(gameEntity);

            return gameEntity;
        }

        public void Release(GameEntity<T> gameEntity)
        {
            _onReleaseEntity(gameEntity);
        }

        #endregion

        #region Private Methods
        private GameEntity<T> CreateEntity()
		{
            GameEntity<T> gameEntity = Instantiate(Prefab, this.transform);
            _gameEntities.Add(gameEntity);

            _onCreateEntity(gameEntity);

            return gameEntity;
        }


        #endregion
    }
}