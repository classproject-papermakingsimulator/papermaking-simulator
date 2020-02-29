using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo18 : MonoBehaviour
	{
		// Refs
		public MultiCameraWaterScript MultiCameraWaterScriptScript;

		// Mono
		void Update()
		{
			var layersDefault = 1;
			var layersReflect = LayerMask.GetMask("ReflectionOnly");
			MultiCameraWaterScriptScript.ReflectLayers = CodeDemoHelper.HelperTimeSin < 0 ? layersDefault : layersReflect;
		}
	}
}