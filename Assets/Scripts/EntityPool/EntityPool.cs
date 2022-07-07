//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;
using System;

namespace RenderHeads
{
	public abstract class EntityPool<T, Y> : MonoBehaviour where T : TableEntry where Y : GameEntity<T>
	{
		#region Public Properties
		public Y Prefab;
		public List<Y> GameEntities = new List<Y>();
		#endregion

		#region Private Properties
		private Action<Y> _onCreateEntity = delegate { };
		private Action<Y> _onGetEntity = delegate { };
		private Action<Y> _onReleaseEntity = delegate { };
		private int _maxSize;

		#endregion

		#region Public Methods
		public void Initialize(Action<Y> onCreateEntity, Action<Y> onGetEntity, Action<Y> onReleaseEntity, int maxSize)
		{
			_onCreateEntity += onCreateEntity;
			_onGetEntity += onGetEntity;
			_onReleaseEntity += onReleaseEntity;
			_maxSize = maxSize;

			if (_maxSize < 0)
			{
				Debug.LogError($"[{this.GetType().Name}] Max pool size set to ({maxSize})");
			}
			else
			{
				for (int i = 0; i < _maxSize; i++)
				{
					CreateEntity();
				}
			}
		}

		public Y CreateEntity()
		{
			Y gameEntity = Instantiate(Prefab, this.transform);
			AddToPool(gameEntity);

			_onCreateEntity(gameEntity);

			return gameEntity as Y;
		}

		public void AddToPool(Y gameEntity)
		{
			GameEntities.Add(gameEntity);
		}

		public Y Take(T tableEntry)
		{
			Y gameEntity = null;

			for (int i = 0; i < GameEntities.Count; i++)
			{
				if (!GameEntities[i].gameObject.activeInHierarchy)
				{
					gameEntity = GameEntities[i] as Y;
					break;
				}
			}

			if (gameEntity == null)
			{
				gameEntity = CreateEntity();
			}

			gameEntity.Initialize(tableEntry);
			_onGetEntity(gameEntity);

			return gameEntity as Y;
		}

		public void ReleaseAll()
		{
			int count = GameEntities.Count;

			for (int i = 0; i < count; i++)
			{
				Release(GameEntities[i]);
			}
		}

		public void Release(Y gameEntity)
		{
			_onReleaseEntity(gameEntity);
		}

		public bool TryGet(Guid guid, out Y gameEntity)
		{
			gameEntity = null;
			int count = GameEntities.Count;

			for (int i = 0; i < count; i++)
			{
				if (GameEntities[i].gameObject.activeInHierarchy)
				{
					if (GameEntities[i].Id == guid)
					{
						gameEntity = GameEntities[i];
						break;
					}
				}
			}

			return gameEntity != null;
		}

		#endregion

		#region Private Methods

		#endregion
	}
}