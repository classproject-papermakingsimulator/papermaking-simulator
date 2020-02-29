/*
 *	Created by Martin Reintges
 */

using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nightowl.DepthMap
{
	[ExecuteInEditMode]
	public class DisplayRenderedTexture : MonoBehaviour
	{
		// Refs
		public HeightMapRenderer Renderer;
		public RawImage DisplayImage;

		// Fields
		public bool ExecuteInEditor = true;

		// Mono
		void Start()
		{
#if UNITY_EDITOR
			if (!EditorApplication.isPlaying && !ExecuteInEditor)
				return;
#endif
			DisplayImage.texture = Renderer.HeightTexture;
			Renderer.NewTextureCreated += GeneratorOnNewTextureCreated;
		}

		void OnDestroy()
		{
			Renderer.NewTextureCreated -= GeneratorOnNewTextureCreated;
		}

		// DisplayGeneratedTexture
		private void GeneratorOnNewTextureCreated(RenderTexture renderTexture)
		{
			DisplayImage.texture = renderTexture;
		}

	}
}
