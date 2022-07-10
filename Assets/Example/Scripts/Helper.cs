//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMemoryDatabase;
using System;

namespace RenderHeads
{
	public static class Helper
	{
		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public static bool TryGetCube(Database database, Guid cubeId, out Cube cube)
		{
			return database.Get(cubeId, out cube);
		}

		public static bool TrGetRandomCube(Database database, out Cube cube)
		{
			List<Cube> cubes = database.GetAll<Cube>();
			int randomCubeIndex = UnityEngine.Random.Range(0, cubes.Count);
			cube = cubes[randomCubeIndex];
			bool result = cube != null;

			if (!result)
			{
				Debug.LogError($"[Helper] No cube found!");
			}

			return result;
		}

		internal static Guid GetRandomColorId(Database database)
		{
			List<ColorEntry> colors = database.GetAll<ColorEntry>();
			int randomColorIndex = UnityEngine.Random.Range(0, colors.Count);

			return colors[randomColorIndex].Id;
		}

		internal static Color GetColor(Database database, Guid colorId)
		{
			database.Get(colorId, out ColorEntry colorEntry);
			return colorEntry.Color;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}