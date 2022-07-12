//RenderHeads - Jeff Rusch
using System;
using System.Collections;
using System.Collections.Generic;
using RenderHeads.InMemoryDatabase;
using RenderHeads.EntitySystem;
using UnityEngine;

namespace RenderHeads.Example
{
	public class BallSystem : EntityPoolSystem<Ball, BallEntity>
	{

		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public BallSystem(Database database, BallPool ballPool) : base(database, ballPool, 5000)
		{

		}

		public override void OnUpdate()
		{
			List<Ball> balls = _database.GetAll<Ball>();
			int count = balls.Count;

			for (int i = 0; i < count; i++)
			{
				if (_entityPool.TryGet(balls[i].Id, out BallEntity gameEntity))
				{
					if (gameEntity.Arrived())
					{
						if (Helper.TryGetCube(_database, balls[i].TargetCubeId, out Cube cube))
						{
							balls[i].ColorId = cube.ColorId;
							gameEntity.SetMaterial(Helper.GetMaterial(gameEntity.TableEntry.ColorId));
						}

						AssignNewCubeTarget(balls[i], gameEntity);
					}

					gameEntity.MoveToTarget();
				}
			}
		}

		public void AddBall(int count)
		{
			List<Cube> cubes = _database.GetAll<Cube>();
			List<Ball> balls = new List<Ball>();
			for (int i = 0; i < count; i++)
			{
				int randomCubeIndex = UnityEngine.Random.Range(0, cubes.Count);
				Ball ball = new Ball(Helper.GetRandomColorId(_database), Vector3.zero, cubes[randomCubeIndex].Id);
				_database.Insert(ball);
				balls.Add(ball);
			}

			_entityPool.Take(balls);
		}

		public void RemoveBall(int count)
		{
			List<Ball> balls = _database.GetAll<Ball>();

			for (int i = 0; i < count; i++)
			{
				if (balls.Count > 0)
				{
					int randomBallIndex = UnityEngine.Random.Range(0, balls.Count);
					if (_entityPool.TryGet(balls[randomBallIndex].Id, out BallEntity ballEntity))
					{
						_entityPool.Release(ballEntity);
						_database.Remove(balls[randomBallIndex]);
					}
					else
					{
						Debug.LogError($"Cound not get a ball with id ({balls[randomBallIndex].Id})");
					}
				}
			}
		}

		#endregion

		#region Private Methods
		private void AssignNewCubeTarget(Ball ball, BallEntity gameEntity)
		{
			if (Helper.TrGetRandomCube(_database, out Cube randomCube))
			{
				ball.TargetCubeId = randomCube.Id;
				gameEntity.SetTargetPosition(randomCube.Position);
			}
		}

		protected override void OnCreateEntity(BallEntity gameEntity)
		{
			gameEntity.OnCreate();
			gameEntity.gameObject.SetActive(false);
		}

		protected override void OnGetEntity(BallEntity gameEntity)
		{
			gameEntity.transform.position = gameEntity.TableEntry.Position;
			gameEntity.SetMaterial(Helper.GetMaterial(gameEntity.TableEntry.ColorId));
			gameEntity.gameObject.SetActive(true);
		}

		protected override void OnReleaseEntity(BallEntity gameEntity)
		{
			gameEntity.gameObject.SetActive(false);

			gameEntity.Deinitialize();
		}

		#endregion

	}
}