//RenderHeads - Jeff Rusch
using System;
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
		public CubeSystem(Database database, EntityPool<Cube, CubeEntity> entityPool) : base(database, entityPool, 10)
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

		internal void AddCube()
		{
			List<Cube> cubes = _database.GetAll<Cube>();
			int cube1Index = UnityEngine.Random.Range(0, cubes.Count);
			int cube2Index = -1;

			while (cube2Index == -1)
			{
				int randomIndex = UnityEngine.Random.Range(0, cubes.Count);
				if (randomIndex != cube1Index)
				{
					cube2Index = randomIndex;
				}
			}

			Vector3 position = Vector3.Lerp(cubes[cube1Index].Position, cubes[cube2Index].Position, 0.5f);
			Cube cubeEntry = new Cube(position);
			cubeEntry = _database.Insert(cubeEntry);
			_entityPool.Take(cubeEntry);
		}

		internal void RemoveCube()
		{
			List<Cube> cubes = _database.GetAll<Cube>();
			if (cubes.Count > 4)
			{

			}
		}

		public override void OnUpdate()
		{

		}

		protected override void OnCreateEntity(CubeEntity gameEntity)
		{
			gameEntity.gameObject.SetActive(false);
		}

		protected override void OnGetEntity(CubeEntity gameEntity)
		{
			gameEntity.transform.position = gameEntity.TableEntry.Position;
			gameEntity.gameObject.SetActive(true);
		}

		protected override void OnReleaseEntity(CubeEntity gameEntity)
		{
			gameEntity.gameObject.SetActive(false);
			gameEntity.Deinitialize();
		}
		#endregion

		#region Private Methods

		#endregion

	}
}