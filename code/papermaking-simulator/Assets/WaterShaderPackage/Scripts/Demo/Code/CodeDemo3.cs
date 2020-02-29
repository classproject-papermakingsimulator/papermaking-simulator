using nightowl.DepthMap;
using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo3 : MonoBehaviour
	{
		// Refs
		public Material Material;
		public HeightMapRenderer TextureRenderer;

		// Mono
		void Update()
		{
			//WaterScript.HeightTexture = CodeDemoHelper.HelperTimeSin > 0 ? TextureRenderer.HeightTexture : null;
		}
	}
}