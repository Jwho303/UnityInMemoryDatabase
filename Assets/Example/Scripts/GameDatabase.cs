//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;
namespace RenderHeads
{
	public class GameDatabase : Database
	{
		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public override void Init()
		{
			CreateTable<Ball>();
		}
		#endregion

		#region Private Methods

		#endregion

	}
}