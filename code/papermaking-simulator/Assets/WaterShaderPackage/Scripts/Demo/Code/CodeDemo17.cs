using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo17 : MonoBehaviour
	{
		// Refs
		public MultiCameraWaterScript MultiCameraWaterScriptScript;

		// Mono
		void Update()
		{
			MultiCameraWaterScriptScript.ReflectSky = CodeDemoHelper.HelperTimeSin > 0;
		}
	}
}