/*
 *	Created by Martin Reintges
 */

using System;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nightowl.DepthMap
{
	[ExecuteInEditMode]
	public class HeightMapGenerator : AbstractHeightMapGenerator
	{
		// Events
		public event Action<Texture2D> NewTextureCreated;

		// Fields
		public bool ExecuteInEditor = true;
		public bool CreateTextureOnStart = true;
		public bool RuntimeUpdate = true;
		public bool UpdateOnNewTextureSize = true;

		public Texture2D HeightTexture { get; private set; }

		public int HeightTextureSize = 512;
		protected int oldHeightTextureSize = 512;

		public float ScanDistanceY = 10f;

		protected float factorX = 1;
		protected float factorZ = 1;
		protected Vector3 offset = Vector3.zero;

		// Mono
		void Start()
		{
#if UNITY_EDITOR
			if (!EditorApplication.isPlaying && !ExecuteInEditor)
				return;
#endif
			if (CreateTextureOnStart)
			{
				CheckHeightMap();
			}
		}

		void Update()
		{
			UpdateHeightMap();
		}

		void OnDisable()
		{
			ClearHeightTexture();
		}

		// AbstractHeightMapGenerator
		public override Texture GetHeightMap()
		{
			return HeightTexture;
		}

		// HeightMapGenerator
		private void UpdateHeightMap()
		{
			bool isUpdated = CheckHeightMap();

			if (!RuntimeUpdate || isUpdated)
				return;

			UpdateHeightMapData();
		}

		public void UpdateHeightMapNow()
		{
			if (!CheckHeightMap())
			{
				UpdateHeightMapData();
			}
		}

		private bool CheckHeightMap()
		{
			factorX = transform.localScale.x / HeightTextureSize * 2f;
			factorZ = transform.localScale.z / HeightTextureSize * 2f;
			offset = -transform.right * transform.localScale.x + -transform.forward * transform.localScale.z;

			if (HeightTexture == null || (UpdateOnNewTextureSize && HeightTextureSize != oldHeightTextureSize))
			{
				CreateNewHeightMap();
				UpdateHeightMapData();
				return true;
			}
			return false;
		}

		private void CreateNewHeightMap()
		{
			ClearHeightTexture();

			oldHeightTextureSize = HeightTextureSize;
			HeightTexture = new Texture2D(HeightTextureSize, HeightTextureSize);

			if (NewTextureCreated != null)
			{
				NewTextureCreated(HeightTexture);
			}
		}

		private void UpdateHeightMapData()
		{
			for (int a = 0; a < HeightTextureSize; ++a)
			{
				for (int b = 0; b < HeightTextureSize; ++b)
				{
					UpdateHeightMapPixel(a, b);
				}
			}
			HeightTexture.Apply();
		}

		private void UpdateHeightMapPixel(int x, int y)
		{
			float value = 0;
			var ray = new Ray(GetPixelWorldPosition(x, y), -transform.up);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, ScanDistanceY))
			{
				value = 1 - (transform.position.y - hit.point.y) / ScanDistanceY;
			}

			HeightTexture.SetPixel(x, y, new Color(value, value, value, 1));
		}

		private Vector3 GetPixelWorldPosition(int x, int y)
		{
			var pos = transform.position + offset;

			pos += transform.right * x * factorX;
			pos += transform.forward * y * factorZ;

			return pos;
		}

		private void ClearHeightTexture()
		{
			if (HeightTexture)
			{
				DestroyImmediate(HeightTexture);
				HeightTexture = null;
			}
		}

		public void SaveTexture(string path)
		{
#if UNITY_WEBPLAYER
			Debug.LogWarning("SaveTexture is not available for WebPlayer. Sorry, I can't do anything about it");
#else
		File.WriteAllBytes(path, HeightTexture.EncodeToPNG());
#endif
		}
	}
}
