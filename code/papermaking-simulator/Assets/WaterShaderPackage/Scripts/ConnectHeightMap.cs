using nightowl.DepthMap;
using UnityEngine;

namespace nightowl.WaterShader
{
	[ExecuteInEditMode]
	public class ConnectHeightMap : MonoBehaviour 
	{
		// Refs
		public AbstractHeightMapGenerator generator;
		public BaseWaterScript WaterScript;

		// Mono
		void Update ()
		{
			if (generator == null || WaterScript == null)
			{
				Debug.LogWarning("ConnectHeightMap not setup: HeightMapGenerator " + (generator == null ? "null" : "ok") + ", WaterScript " + (WaterScript == null ? "null" : "ok"));
				return;
			}
			//WaterScript.HeightTexture = generator.GetHeightMap();
		}
	}
}
