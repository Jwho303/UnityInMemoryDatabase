using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jwho303.InMemoryDatabase;
using System;

namespace Jwho303.EntitySystem
{
	public abstract class EntityPool<T, Y> : MonoBehaviour where T : TableEntry where Y : GameEntity<T>
	{
		#region Public Properties
		public Y Prefab;
		#endregion

		#region Private Properties
		private Dictionary<Guid, Y> _activeGameEntities = new Dictionary<Guid, Y>();
		private List<Y> _gameEntities = new List<Y>();

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
			_gameEntities.Add(gameEntity);
		}

		public Y Take(T tableEntry)
		{
			Y gameEntity = null;

			for (int i = 0; i < _gameEntities.Count; i++)
			{
				if (!_gameEntities[i].gameObject.activeInHierarchy)
				{
					gameEntity = _gameEntities[i] as Y;
					break;
				}
			}

			if (gameEntity == null)
			{
				gameEntity = CreateEntity();
			}

			_activeGameEntities.Add(tableEntry.Id, gameEntity);

			gameEntity.Initialize(tableEntry);
			_onGetEntity(gameEntity);

			return gameEntity as Y;
		}

		public List<Y> Take(List<T> tableEntries)
		{
			List<Y> gameEntities = new List<Y>();

			for (int i = 0; i < _gameEntities.Count; i++)
			{
				if (!_gameEntities[i].gameObject.activeInHierarchy)
				{
					gameEntities.Add(_gameEntities[i]);

					if (gameEntities.Count == tableEntries.Count)
					{
						break;
					}
				}
			}

			int entitiesCount = gameEntities.Count;

			if (entitiesCount < tableEntries.Count)
			{
				int difference = tableEntries.Count - entitiesCount;

				for (int i = 0; i < difference; i++)
				{
					gameEntities.Add(CreateEntity());
				}
			}

			Debug.Assert(entitiesCount == tableEntries.Count, $"[{this.GetType().Name}] gameEntities ({entitiesCount}) != tableEntries ({tableEntries.Count})");

			for (int i = 0; i < entitiesCount; i++)
			{
				_activeGameEntities.Add(tableEntries[i].Id, gameEntities[i]);

				gameEntities[i].Initialize(tableEntries[i]);
				_onGetEntity(gameEntities[i]);
			}

			return gameEntities;
		}

		public void ReleaseAll()
		{
			int count = _gameEntities.Count;

			for (int i = 0; i < count; i++)
			{
				Release(_gameEntities[i]);
			}
		}

		public void Release(Y gameEntity)
		{
			_activeGameEntities.Remove(gameEntity.Id);

			_onReleaseEntity(gameEntity);
		}

		public bool TryGet(Guid guid, out Y gameEntity)
		{
			gameEntity = _activeGameEntities[guid];

			return gameEntity != null;
		}

		#endregion

		#region Private Methods

		#endregion
	}
}