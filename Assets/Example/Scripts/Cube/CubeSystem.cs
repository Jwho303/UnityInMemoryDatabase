//RenderHeads - Jeff Rusch
using System;
using System.Collections;
using System.Collections.Generic;
using RenderHeads.InMemoryDatabase;
using RenderHeads.EntitySystem;
using UnityEngine;
using System.Linq;

namespace RenderHeads.Example
{
	public class CubeSystem : EntityPoolSystem<Cube, CubeEntity>
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
				cubes[i].TableEntry.ColorId = Helper.GetRandomColorId(_database);
				Cube cubeEntry = _database.Insert(cubes[i].TableEntry);
				cubes[i].OnCreate();
				cubes[i].Initialize(cubeEntry);
				cubes[i].SetMaterial(Helper.GetMaterial(cubes[i].TableEntry.ColorId));
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
			Cube cubeEntry = new Cube(position, Helper.GetRandomColorId(_database));
			cubeEntry = _database.Insert(cubeEntry);
			_entityPool.Take(cubeEntry);
		}

		internal void RemoveCube()
		{
			List<Cube> cubes = _database.GetAll<Cube>();
			if (cubes.Count > 4)
			{
				Cube cubeEntry = cubes.Last();
				if (_entityPool.TryGet(cubeEntry.Id, out CubeEntity gameEntity))
				{
					_entityPool.Release(gameEntity);
					_database.Remove(cubeEntry);
				}
			}
		}

		public override void OnUpdate()
		{

		}

		internal void OnFixedUpdate()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out RaycastHit raycastHit))
				{
					CubeEntity cubeEntity = raycastHit.collider.GetComponent<CubeEntity>();
					if (cubeEntity != null)
					{
						if (Helper.TryGetCube(_database, cubeEntity.TableEntry.Id, out Cube cube))
						{
							cube.ColorId = Helper.GetRandomColorId(_database);
							cubeEntity.SetMaterial(Helper.GetMaterial(cube.ColorId));
						}
					}
				}
			}
		}

		protected override void OnCreateEntity(CubeEntity gameEntity)
		{
			gameEntity.OnCreate();
			gameEntity.gameObject.SetActive(false);
		}

		protected override void OnGetEntity(CubeEntity gameEntity)
		{
			gameEntity.transform.position = gameEntity.TableEntry.Position;
			gameEntity.SetMaterial(Helper.GetMaterial(gameEntity.TableEntry.ColorId));
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