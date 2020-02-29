using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo2 : MonoBehaviour
	{
		// Refs
		public BaseWaterScript WaterScript;
		public Texture2D Texture1;
		public Texture2D Texture2;

		private float lastTime = 0;

		// Mono
		void Update()
		{
			float time = CodeDemoHelper.HelperTimeSin;
			if ((time > 0 && lastTime <= 0) || 
				(time <= 0 && lastTime > 0))
			{
				WaterScript.NormalMap1 = Texture1;
				WaterScript.NormalMap2 = Texture2;
				WaterScript.ForceUpdateNormalMapData();
			}
			lastTime = CodeDemoHelper.HelperTimeSin;
		}
	}
}