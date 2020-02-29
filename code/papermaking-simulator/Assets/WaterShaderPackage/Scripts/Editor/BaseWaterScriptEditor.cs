using UnityEditor;
using UnityEngine;

namespace nightowl.WaterShader
{

	[CustomEditor(typeof(BaseWaterScript))]
	public class BaseWaterScriptEditor : Editor
	{

		private static Material lastMaterial = null;
		
		public override void OnInspectorGUI()
		{
			var water = (BaseWaterScript)target;
			var material = water.GetComponent<MeshRenderer>().sharedMaterial;

			CheckShader(material);
			CheckNormalMaps(water);
			ShowForceUpdateButton(water);

			EditorGUI.BeginChangeCheck();
			base.OnInspectorGUI();
			if (EditorGUI.EndChangeCheck() || lastMaterial != material)
			{
				water.ForceUpdateNormalMapData();
			}
			lastMaterial = material;
		}

		private void CheckShader(Material material)
		{
			if (!material.shader.name.StartsWith("Water/Water"))
			{
				EditorGUILayout.HelpBox("Seems like the material '"+material.name+"' has not selected a valid water shader", MessageType.Error);
				if (GUILayout.Button("Fix shader (select default)"))
				{
					material.shader = Shader.Find("Water/WaterPro");
				}
			}
		}

		private void CheckNormalMaps(BaseWaterScript water)
		{
			if (water.NormalMap1 == null && water.NormalMap2 == null)
			{
				EditorGUILayout.HelpBox("Both input normal maps are null. Creating dummy normal map", MessageType.Error);
				return;
			}

			if (water.NormalMap1 != null && water.NormalMap2 != null)
			{
				if (water.NormalMap1.width != water.NormalMap2.width ||
					water.NormalMap1.height != water.NormalMap2.height)
				{
					EditorGUILayout.HelpBox("Input normal maps do not have the same resolution. Using only #1 for now.", MessageType.Warning);
					return;
				}
				return;
			}
			EditorGUILayout.HelpBox("Only one input normal map is set. Use two for better results.", MessageType.Warning);
		}

		private void ShowForceUpdateButton(BaseWaterScript water)
		{
			if (GUILayout.Button("Force update shader"))
			{ 
				water.ForceUpdateNormalMapData();
			}
		}
	}
}