using nightowl.DepthMap;
using UnityEngine;

[ExecuteInEditMode]
public class LoadRenderTexture : MonoBehaviour 
{
	// Refs
	public HeightMapRenderer HeightMapRenderer;
	public string RenderTexturePath;

	// Mono
	void Awake () 
	{
		if (ShouldLoad())
		{
			Load();
		}
	}

	// LoadRenderTexture
	private bool ShouldLoad()
	{
		if (HeightMapRenderer == null || string.IsNullOrEmpty(RenderTexturePath))
			return false;

		return HeightMapRenderer.HeightTexture == null;
	}

	private void Load()
	{
		HeightMapRenderer.HeightTexture = Resources.Load<RenderTexture>(RenderTexturePath);
	}
}
