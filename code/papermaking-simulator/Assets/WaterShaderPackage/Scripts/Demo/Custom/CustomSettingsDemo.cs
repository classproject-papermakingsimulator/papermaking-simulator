using UnityEngine;
using UnityEngine.UI;

namespace nightowl.WaterShader
{
	public class CustomSettingsDemo : MonoBehaviour
	{
		// References
		public MeshRenderer MeshRenderer;
		public Slider LightSlider;
		public Slider NormalSlider;
		public Slider ShininessSlider;
		public Slider EdgeSlider;
		public Slider ShallowSlider;
		public Slider ShallowDeepFadeSlider;
		public Slider MinAlphaSlider;
		public Slider ReflectivitySlider;
		public Slider RefractionSlider;
		public Slider FoamSlider;

		// Unity
		void Awake()
		{
			LightSlider.enabled = false;
			NormalSlider.enabled = false;
			ShininessSlider.enabled = false;
			EdgeSlider.enabled = false;
			ShallowSlider.enabled = false;
			ShallowDeepFadeSlider.enabled = false;
			MinAlphaSlider.enabled = false;
			ReflectivitySlider.enabled = false;
			RefractionSlider.enabled = false;
			FoamSlider.enabled = false;

			LightSlider.value = MeshRenderer.sharedMaterial.GetFloat("_LightOffset");
			NormalSlider.value = MeshRenderer.sharedMaterial.GetFloat("_NormalStrength");
			ShininessSlider.value = MeshRenderer.sharedMaterial.GetFloat("_Shininess");
			EdgeSlider.value = MeshRenderer.sharedMaterial.GetFloat("_EdgeFade");
			ShallowSlider.value = MeshRenderer.sharedMaterial.GetFloat("_ShallowDepth");
			ShallowDeepFadeSlider.value = MeshRenderer.sharedMaterial.GetFloat("_ShallowDeepFade");
			/*MinAlphaSlider.value = MeshRenderer.sharedMaterial.GetFloat("_ShoreEdgeIndicator");
			ReflectivitySlider.value = MeshRenderer.sharedMaterial.GetFloat("_Reflectivity");
			*/
			Debug.Log("_Refraction: " + MeshRenderer.sharedMaterial.GetFloat("_Refraction"));
			RefractionSlider.value = MeshRenderer.sharedMaterial.GetFloat("_Refraction");
			
			FoamSlider.value = MeshRenderer.sharedMaterial.GetFloat("_FoamOffset");

			LightSlider.enabled = true;
			NormalSlider.enabled = true;
			ShininessSlider.enabled = true;
			EdgeSlider.enabled = true;
			ShallowSlider.enabled = true;
			ShallowDeepFadeSlider.enabled = true;
			MinAlphaSlider.enabled = true;
			ReflectivitySlider.enabled = true;
			RefractionSlider.enabled = true;
			FoamSlider.enabled = true;
		}

		// CustomSettingsDemo
		public void OnLightChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_LightOffset", value);
		}

		public void OnNormalChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_NormalStrength", value);
		}

		public void OnShininessChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_Shininess", value);
		}

		public void OnEdgeChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_EdgeFade", value);
		}

		public void OnShallowChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_ShallowDepth", value);
		}

		public void OnShallowDeepFadeChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_ShallowDeepFade", value);
		}

		public void OnMinAlphaChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_ShoreEdgeIndicator", value);
		}

		public void OnReflectivityChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_Reflectivity", value);
		}

		public void OnRefractionChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_Refraction", value);
		}

		public void OnFoamChange(float value)
		{
			MeshRenderer.sharedMaterial.SetFloat("_FoamOffset", value);
		}
	}
}