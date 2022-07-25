//RenderHeads - Jeff Rusch
using System;
using System.Collections;
using System.Collections.Generic;
using RenderHeads.InMemoryDatabase;
using RenderHeads.EntitySystem;
using UnityEngine;

namespace RenderHeads.InMemoryDatabase.Example
{
	public class BallSystem : EntityPoolSystem<Ball, BallEntity>
	{

		#region Public Properties

		#endregion

		#region Private Properties
		private Queue<Action> _ballSpawnQueue = new Queue<Action>();
		private int _ballsToSpawnPerFrame = 5;
		#endregion

		#region Public Methods
		public BallSystem(Database database, BallPool ballPool) : base(database, ballPool, 5000)
		{

		}

		public override void OnUpdate()
		{
			if (_ballSpawnQueue.Count > 0)
			{
				_ballSpawnQueue.Dequeue().Invoke();
			}

			List<Ball> balls = _database.GetAll<Ball>();
			int count = balls.Count;

			List<Cube> cubes = new List<Cube>();

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

						if (cubes.Count == 0)
						{
							cubes = _database.GetAll<Cube>();
						}

						int randomCubeIndex = UnityEngine.Random.Range(0, cubes.Count);
						Cube randomCube = cubes[randomCubeIndex];

						AssignNewCubeTarget(balls[i], gameEntity, randomCube);
					}

					gameEntity.MoveToTarget();
				}
			}
		}

		public void AddBall(int count)
		{
			int queueCount = Mathf.FloorToInt(count / _ballsToSpawnPerFrame);

			for (int i = 0; i < queueCount; i++)
			{
				_ballSpawnQueue.Enqueue(() => { AddBallsToPlay(_ballsToSpawnPerFrame); });
			}

			int remainder = count % _ballsToSpawnPerFrame;

			if (remainder > 0)
			{
				_ballSpawnQueue.Enqueue(() => { AddBallsToPlay(remainder); });
			}
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
		private void AddBallsToPlay(int count)
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

		private void AssignNewCubeTarget(Ball ball, BallEntity gameEntity, Cube cubeTarget)
		{
			ball.TargetCubeId = cubeTarget.Id;
			gameEntity.SetTargetPosition(cubeTarget.Position);
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