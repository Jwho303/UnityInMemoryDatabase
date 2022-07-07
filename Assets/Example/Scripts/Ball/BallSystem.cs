//RenderHeads - Jeff Rusch
using System;
using System.Collections;
using System.Collections.Generic;
using InMemoryDatabase;
using UnityEngine;

namespace RenderHeads
{
	public class BallSystem : EntitySystem<Ball, BallEntity>
	{

		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public BallSystem(Database database, BallPool ballPool) : base(database, ballPool, 100)
		{
			_entityPool.Take();
			_entityPool.Take();
			_entityPool.Take();
		}
		#endregion

		#region Private Methods
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
						AssignNewCubeTarget(balls[i], gameEntity);
					}

					gameEntity.MoveToTarget();
				}
			}
		}

		private void AssignNewCubeTarget(Ball ball, BallEntity gameEntity)
		{
			if (Helper.TrGetRandomCube(_database, out Cube randomCube))
			{
				ball.TargetCubeId = randomCube.Id;
				ball.Save<Ball>(_database);
				gameEntity.SetTargetPosition(randomCube.Position);
			}
		}

		protected override void OnCreateEntity(BallEntity gameEntity)
		{
			gameEntity.gameObject.SetActive(false);
		}

		protected override void OnGetEntity(BallEntity gameEntity)
		{
			if (Helper.TrGetRandomCube(_database, out Cube randomCube))
			{
				Ball ball = new Ball(1, Vector3.zero, randomCube.Id);
				_database.Insert(ball);
				gameEntity.Initialize(ball);

				gameEntity.gameObject.SetActive(true);
			}
		}

		protected override void OnReleaseEntity(BallEntity gameEntity)
		{
			gameEntity.gameObject.SetActive(false);
			gameEntity.Deinitialize();
		}

		#endregion

	}
}