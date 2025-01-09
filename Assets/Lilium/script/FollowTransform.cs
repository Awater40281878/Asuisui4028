using UnityEngine;

public class FollowTransform : MonoBehaviour
{
	// 需要跟随的目标
	public Transform targetTransform;

	// 更新模式
	public bool followInFixedUpdate = false; // 如果为 true，使用 FixedUpdate，通常用于物理相关。

	void Update()
	{
		if (!followInFixedUpdate)
		{
			FollowTarget();
		}
	}

	void FixedUpdate()
	{
		if (followInFixedUpdate)
		{
			FollowTarget();
		}
	}

	// 跟随逻辑
	private void FollowTarget()
	{
		if (targetTransform != null)
		{
			transform.position = targetTransform.position;
		}
	}
}
