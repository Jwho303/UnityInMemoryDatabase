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

		private GameDatabase _database;

		[SerializeField]
		private ColorSet _colorsSet;

		[SerializeField]
		private BallPool _ballPool;
		private BallSystem _ballSystem;

		[SerializeField]
		private CubePool _cubePool;
		private CubeSystem _cubeSystem;

		[SerializeField]
		private UIManager _uiManager;
		#endregion

		#region Public Methods
		public void Start()
		{
			Application.targetFrameRate = 60;

			_database = new GameDatabase();
			_database.Initialize();

			_colorsSet.AddColorsToDatabase(_database);
			Helper.CreatMaterials(_database);

			_cubeSystem = new CubeSystem(_database, _cubePool);
			_ballSystem = new BallSystem(_database, _ballPool);
		}

		public void Update()
		{
			_cubeSystem.OnUpdate();
			_ballSystem.OnUpdate();
			_uiManager.OnUpdate();
		}

		public void AddBall()
		{
			_ballSystem.AddBall();

			_uiManager.SetBallCounter(_database.GetAll<Ball>().Count);
		}

		public void Add50Ball()
		{
			for (int i = 0; i < 50; i++)
			{
				_ballSystem.AddBall();
			}

			_uiManager.SetBallCounter(_database.GetAll<Ball>().Count);
		}

		public void RemoveBall()
		{
			_ballSystem.RemoveBall();

			_uiManager.SetBallCounter(_database.GetAll<Ball>().Count);
		}

		public void Remove50Ball()
		{
			for (int i = 0; i < 50; i++)
			{
				_ballSystem.RemoveBall();
			}

			_uiManager.SetBallCounter(_database.GetAll<Ball>().Count);
		}

		public void AddCube()
		{
			_cubeSystem.AddCube();

			_uiManager.SetCubeCounter(_database.GetAll<Cube>().Count);
		}

		public void RemoveCube()
		{
			_cubeSystem.RemoveCube();

			_uiManager.SetCubeCounter(_database.GetAll<Cube>().Count);
		}
		#endregion

		#region Private Methods

		#endregion
	}
}