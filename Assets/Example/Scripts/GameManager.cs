using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jwho303.InMemoryDatabase.Example
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

			_database.Subscribe<Ball>(() => { _uiManager.SetBallCounter(_database.GetAll<Ball>().Count); });
			_database.Subscribe<Cube>(() => { _uiManager.SetCubeCounter(_database.GetAll<Cube>().Count); });
		}

		public void Update()
		{
			_uiManager.OnUpdate();
		}

		public void FixedUpdate()
		{
			_cubeSystem.OnFixedUpdate();
			_ballSystem.OnUpdate();
		}

		public void AddBall()
		{
			_ballSystem.AddBall(1);
		}

		public void Add50Ball()
		{
			_ballSystem.AddBall(50);
		}

		public void RemoveBall()
		{
			_ballSystem.RemoveBall(1);
		}

		public void Remove50Ball()
		{
			_ballSystem.RemoveBall(50);
		}

		public void AddCube()
		{
			_cubeSystem.AddCube();
		}

		public void RemoveCube()
		{
			_cubeSystem.RemoveCube();
		}
		#endregion

		#region Private Methods

		#endregion
	}
}