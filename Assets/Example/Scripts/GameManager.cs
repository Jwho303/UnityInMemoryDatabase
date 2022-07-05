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
		[SerializeField]
		private BallPool _ballPool;
		private BallSystem _ballSystem;
		private GameDatabase _dataBase;
		#endregion

		#region Public Methods
		public void Start()
		{
			_ballSystem = new BallSystem(_dataBase, _ballPool);
		}

		public void Update()
		{
			_ballSystem.OnUpdate();
		}
		#endregion

		#region Private Methods

		#endregion
	}
}