//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using InMemoryDatabase;
using UnityEngine;

namespace RenderHeads
{
	public class CubeSystem : EntitySystem<Cube>
	{

		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public CubeSystem(Database database, EntityPool<Cube> entityPool) : base(database, entityPool, 0)
		{
			CubeEntity[] cubes = GameObject.FindObjectsOfType<CubeEntity>();
			int count = cubes.Length;

			for (int i = 0; i < count; i++)
			{
				cubes[i].TableEntry.Position = cubes[i].transform.position;
				Cube cubeEntry = _database.Insert(cubes[i].TableEntry);
				cubes[i].Initialize(cubeEntry);
				_entityPool.AddToPool(cubes[i]);
			}
		}

		public override void OnUpdate()
		{
			List<Cube> cubes = _database.GetAll<Cube>();
			int count = cubes.Count;

			for (int i = 0; i < count; i++)
			{
				if (_entityPool.TryGet(cubes[i].Id, out GameEntity<Cube> gameEntity))
				{
					Debug.Log(gameEntity.Id);
				}
			}
		}

		protected override void OnCreateEntity(GameEntity<Cube> gameEntity)
		{
			
		}

		protected override void OnGetEntity(GameEntity<Cube> gameEntity)
		{
			
		}

		protected override void OnReleaseEntity(GameEntity<Cube> gameEntity)
		{
			
		}
		#endregion

		#region Private Methods

		#endregion

	}
}