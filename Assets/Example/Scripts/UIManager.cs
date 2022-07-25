//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RenderHeads.InMemoryDatabase.Example
{
	public class UIManager : MonoBehaviour
	{
		#region Public Properties

		#endregion

		#region Private Properties
		[SerializeField]
		TextMeshProUGUI _fpsCounter;

		[SerializeField]
		TextMeshProUGUI _ballCounter;

		[SerializeField]
		TextMeshProUGUI _cubeCounter;

		private float _refreshInterval = 1f;
		private float _refreshTimer = 0f;
		#endregion

		#region Public Methods
		public void OnUpdate()
		{
			if (Time.unscaledTime > _refreshTimer)
			{
				_fpsCounter.SetText($"FPS: {(int)(1f / Time.unscaledDeltaTime)}");
				_refreshTimer = Time.unscaledTime + _refreshInterval;
			}
		}

		public void SetBallCounter(int count)
		{
			_ballCounter.SetText($"Balls: {count}");
		}

		public void SetCubeCounter(int count)
		{
			_cubeCounter.SetText($"Cubes: {count}");
		}
		#endregion

		#region Private Methods

		#endregion
	}
}