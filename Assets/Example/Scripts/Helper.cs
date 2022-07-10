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
		private static Dictionary<Guid, Material> _colorMaterials = new Dictionary<Guid, Material>();
		#endregion

		#region Public Methods
		public static void CreatMaterials(Database database)
		{
			List<ColorEntry> colors = database.GetAll<ColorEntry>();

			for (int i = 0; i < colors.Count; i++)
			{
				Material material = new Material(Shader.Find("Standard"))
				{
					color = colors[i].Color,
					enableInstancing = true,
				};
				material.SetFloat("_Glossiness", 0f);

				_colorMaterials.Add(colors[i].Id, material);
			}
		}

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

		internal static Material GetMaterial(Guid colorId)
		{
			return _colorMaterials[colorId];
		}
		#endregion

		#region Private Methods

		#endregion
	}
}