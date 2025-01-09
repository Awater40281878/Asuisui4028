using UnityEngine;

public class TextureOffsetAnimator : MonoBehaviour
{
	// 目标材质
	public Material targetMaterial;

	// 动画速度
	public float animationSpeed = 1f;

	// 当前偏移值
	private float offsetY = 0f;

	private void Start()
	{
		if (targetMaterial != null)
		{
			targetMaterial.mainTextureOffset = Vector2.zero;

		}
		else
		{
			Debug.Log("targetMaterial is null ");
		}
	}
	void Update()
	{
		if (targetMaterial != null)
		{
			// 计算新的偏移值
			offsetY -= animationSpeed * Time.deltaTime; // 减小值实现从 0 到 -1
			if (offsetY < -1f) offsetY += 1f; // 确保值在 0 到 -1 之间循环

			// 应用到材质的 Offset.Y
			Vector2 currentOffset = targetMaterial.mainTextureOffset;
			currentOffset.y = offsetY;
			targetMaterial.mainTextureOffset = currentOffset;
		}
	}
}


