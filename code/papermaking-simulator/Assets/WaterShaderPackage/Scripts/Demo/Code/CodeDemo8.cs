using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo8 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_LightDir"))
			{
				// Vector4 values:
				// x = x direction
				// y = -y direction
				// z = z direction
				// w = unused
				Material.SetVector("_LightDir", new Vector4(CodeDemoHelper.HelperTimeSin*0.3f, 1, 0, 0));
			}
		}
	}
}