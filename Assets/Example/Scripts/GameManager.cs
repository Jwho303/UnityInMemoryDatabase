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

		private string _savePath = string.Empty;
		#endregion

		#region Public Methods
		public void Start()
		{
			_savePath = System.IO.Path.Combine(Application.persistentDataPath, "save.json");

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

		public void AddBall()
		{
			_ballSystem.AddBall();
		}

		public void RemoveBall()
		{
			_ballSystem.RemoveBall();
		}

		public void AddCube()
		{
			_cubeSystem.AddCube();
		}

		public void RemoveCube()
		{
			_cubeSystem.RemoveCube();
		}

		public void Save()
		{
			_ballSystem.Save();
			_dataBase.Save(_savePath);

			_dataBase.PrintSnapShot();
			Debug.Log($"Save file to ({_savePath})");
		}

		public void Load()
		{
			_dataBase.Load(_savePath);
			_ballSystem.Load();
		}
		#endregion

		#region Private Methods

		#endregion
	}
}