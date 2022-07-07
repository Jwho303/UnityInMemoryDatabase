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

			_cubeSystem = new CubeSystem(_dataBase, _cubePool);
			_ballSystem = new BallSystem(_dataBase, _ballPool);
		}

		public void Update()
		{
			_cubeSystem.OnUpdate();
			_ballSystem.OnUpdate();
		}
		#endregion

		#region Private Methods

		#endregion
	}
}