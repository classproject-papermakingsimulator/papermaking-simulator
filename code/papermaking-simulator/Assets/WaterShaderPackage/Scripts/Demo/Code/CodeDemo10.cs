using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo10 : MonoBehaviour
	{
		// Refs
		public Material Material;
		public Color ShallowColor;
		public Color CounterColor;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_ShallowColor"))
			{
				Material.SetColor("_ShallowColor",
					CodeDemoHelper.HelperTimeNormalized*ShallowColor + (1 - CodeDemoHelper.HelperTimeNormalized)*CounterColor);
			}
		}
	}
}