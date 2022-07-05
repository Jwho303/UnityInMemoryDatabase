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
			
		}
		#endregion

		#region Private Methods
		public override void OnUpdate()
		{
			
		}

		protected override void OnCreateEntity(GameEntity<Ball> gameEntity)
		{
			gameEntity.gameObject.SetActive(false);
		}

		protected override void OnGetEntity(GameEntity<Ball> gameEntity)
		{
			gameEntity.gameObject.SetActive(true);
		}

		protected override void OnReleaseEntity(GameEntity<Ball> gameEntity)
		{
			gameEntity.gameObject.SetActive(false);
		}

		#endregion

	}
}