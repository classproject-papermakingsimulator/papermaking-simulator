using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo7 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_Movement"))
			{
				// Vector4 values:
				// x = x movement 1
				// y = y movement 1
				// z = x movement 2
				// w = y movement 3
				Material.SetVector("_Movement", new Vector4(CodeDemoHelper.HelperTimeSin > 0 ? 0.05f : 0.01f, 0, 0, 0));
			}
		}
	}
}