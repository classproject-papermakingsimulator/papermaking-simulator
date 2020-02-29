using UnityEngine;

namespace nightowl.DepthMap
{
	public abstract class AbstractHeightMapGenerator : MonoBehaviour
	{
		public abstract Texture GetHeightMap();
	}
}
