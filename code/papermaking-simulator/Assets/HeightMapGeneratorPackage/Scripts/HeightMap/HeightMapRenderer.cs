using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nightowl.DepthMap
{
	[ExecuteInEditMode]
	public class HeightMapRenderer : AbstractHeightMapGenerator
	{
		// Events
		public event Action<RenderTexture> NewTextureCreated;

		// Refs
		public Camera cam;

		// Fields
		public bool ExecuteInEditor = true;
		public bool CreateTextureOnStart = true;
		public bool RuntimeUpdate = true;
		public bool UpdateOnNewTextureSize = true;
		public bool UseLinearEyeDepth = false;

		public LayerMask RenderLayers = -1;

		public int HeightTextureSize = 512;
		protected int oldHeightTextureSize = 512;

		public RenderTexture HeightTexture = null;

		// Mono
		void Start()
		{
#if UNITY_EDITOR
			if (!EditorApplication.isPlaying && !ExecuteInEditor)
				return;
#endif
			SetupCamera();
			if (CreateTextureOnStart)
			{
				CheckHeightMap();
				UpdateHeightMapData();
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

		// HeightMapRenderer
		protected void UpdateHeightMap()
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
			if (!HeightTexture || (oldHeightTextureSize != HeightTextureSize && UpdateOnNewTextureSize))
			{
				if (HeightTexture)
				{
					//DestroyImmediate(HeightTexture);
				}

				HeightTexture = new RenderTexture(HeightTextureSize, HeightTextureSize, 16);
				HeightTexture.name = "__HeightMap" + GetInstanceID();
				HeightTexture.hideFlags = HideFlags.DontSave;
				oldHeightTextureSize = HeightTextureSize;

				cam.targetTexture = HeightTexture;
				UpdateHeightMapData();
				if (NewTextureCreated != null)
				{
					NewTextureCreated(HeightTexture);
				}
				return true;
			}
			return false;
		}

		private void UpdateHeightMapData()
		{
			if (cam == null)
				return;

			cam.Render();
		}

		private void ClearHeightTexture()
		{
			if (HeightTexture)
			{
				//DestroyImmediate(HeightTexture); // Disabled because it would try to destroy project assets
				HeightTexture = null;
			}
		}

		private void SetupCamera()
		{
			if (cam == null)
				return;

			cam.targetTexture = HeightTexture;
			cam.pixelRect = new Rect(0, 0, HeightTextureSize, HeightTextureSize);
			cam.cullingMask = RenderLayers;
			cam.depthTextureMode = DepthTextureMode.Depth;
			var shader = UseLinearEyeDepth ? Shader.Find("HeightMapPack/DepthShaderEye") : Shader.Find("HeightMapPack/DepthShader");
			cam.SetReplacementShader(shader, "");
			cam.enabled = false;
		}
	}
}
