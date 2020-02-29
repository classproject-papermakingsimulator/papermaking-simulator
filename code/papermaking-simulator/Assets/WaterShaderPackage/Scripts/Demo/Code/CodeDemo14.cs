using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo14 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_ShoreEdgeIndicator"))
			{
				Material.SetFloat("_ShoreEdgeIndicator", CodeDemoHelper.HelperTimeNormalized*1f);
			}
		}
	}
}