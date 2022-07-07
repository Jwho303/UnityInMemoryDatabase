//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RenderHeads
{
	public class GameManager : MonoBehaviour
	{
		#region Public Properties

		#endregion

		#region Private Properties
		
		private GameDatabase _dataBase;

		[SerializeField]
		private BallPool _ballPool;
		private BallSystem _ballSystem;

		[SerializeField]
		private CubePool _cubePool;
		private CubeSystem _cubeSystem;
		#endregion

		#region Public Methods
		public void Start()
		{
			_dataBase = new GameDatabase();
			_dataBase.Initialize();

			_ballSystem = new BallSystem(_dataBase, _ballPool);
			_cubeSystem = new CubeSystem(_dataBase, _cubePool);
		}

		public void Update()
		{
			_ballSystem.OnUpdate();
			_cubeSystem.OnUpdate();
		}
		#endregion

		#region Private Methods

		#endregion
	}
}