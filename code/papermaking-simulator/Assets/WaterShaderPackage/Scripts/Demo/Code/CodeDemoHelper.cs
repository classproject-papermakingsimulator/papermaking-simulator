using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemoHelper
	{
		// CodeDemoHelper
		public static float HelperTimeSin
		{
			get { return Mathf.Sin(Time.time); }
		}

		public static float HelperTimeNormalized
		{
			get { return Mathf.Sin(Time.time)*0.5f + 0.5f; }
		}
	}
}