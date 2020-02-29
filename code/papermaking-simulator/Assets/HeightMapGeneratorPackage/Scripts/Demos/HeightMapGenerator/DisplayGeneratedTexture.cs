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
	public class DisplayGeneratedTexture : MonoBehaviour
	{
		// Refs
		public HeightMapGenerator Generator;
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
			DisplayImage.texture = Generator.HeightTexture;
			Generator.NewTextureCreated += GeneratorOnNewTextureCreated;
		}

		void OnDestroy()
		{
			Generator.NewTextureCreated -= GeneratorOnNewTextureCreated;
		}

		// DisplayGeneratedTexture
		private void GeneratorOnNewTextureCreated(Texture2D texture2D)
		{
			DisplayImage.texture = texture2D;
		}

	}
}
