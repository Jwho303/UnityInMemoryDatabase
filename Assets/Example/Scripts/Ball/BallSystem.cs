//RenderHeads - Jeff Rusch
using System;
using System.Collections;
using System.Collections.Generic;
using InMemoryDatabase;
using UnityEngine;

namespace RenderHeads
{
	public class BallSystem : EntitySystem<Ball>
	{

		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public BallSystem(Database database, EntityPool<Ball> entityPool) : base(database, entityPool, 100)
		{
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
				if (_entityPool.TryGet(balls[i].Id, out GameEntity<Ball> gameEntity))
				{
					//Debug.Log(gameEntity.Id);
				}
			}
		}

		protected override void OnCreateEntity(GameEntity<Ball> gameEntity)
		{
			gameEntity.gameObject.SetActive(false);
		}

		protected override void OnGetEntity(GameEntity<Ball> gameEntity)
		{
			Ball ball = new Ball(1, Vector3.zero);
			_database.Insert(ball);
			gameEntity.Initialize(ball);
			gameEntity.gameObject.SetActive(true);
		}

		protected override void OnReleaseEntity(GameEntity<Ball> gameEntity)
		{
			gameEntity.gameObject.SetActive(false);
			gameEntity.Deinitialize();
		}

		#endregion

	}
}