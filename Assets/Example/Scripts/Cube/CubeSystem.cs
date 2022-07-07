//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using InMemoryDatabase;
using UnityEngine;

namespace RenderHeads
{
	public class CubeSystem : EntitySystem<Cube, CubeEntity>
	{

		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public CubeSystem(Database database, EntityPool<Cube, CubeEntity> entityPool) : base(database, entityPool, 0)
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

		}

		protected override void OnCreateEntity(CubeEntity gameEntity)
		{
			
		}

		protected override void OnGetEntity(CubeEntity gameEntity)
		{
			
		}

		protected override void OnReleaseEntity(CubeEntity gameEntity)
		{
			
		}
		#endregion

		#region Private Methods

		#endregion

	}
}