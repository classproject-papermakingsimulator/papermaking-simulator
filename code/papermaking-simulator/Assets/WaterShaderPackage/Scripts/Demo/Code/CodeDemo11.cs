using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo11 : MonoBehaviour
	{
		// Refs
		public Material Material;
		public Color DeepColor;
		public Color CounterColor;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_DeepSeaColor"))
			{
				Material.SetColor("_DeepSeaColor",
					CodeDemoHelper.HelperTimeNormalized*DeepColor + (1 - CodeDemoHelper.HelperTimeNormalized)*CounterColor);
			}
		}
	}
}