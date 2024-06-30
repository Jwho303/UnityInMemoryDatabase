using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jwho303.InMemoryDatabase;

namespace Jwho303.InMemoryDatabase.Example
{
	public class GameDatabase : Database
	{
		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public override void Initialize()
		{
			CreateTable<ColorEntry>();
			CreateTable<Cube>();
			CreateTable<Ball>();
		}
		#endregion

		#region Private Methods

		#endregion

	}
}