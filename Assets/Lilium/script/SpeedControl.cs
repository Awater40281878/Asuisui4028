using UnityEngine;

public class ControlAnimationSpeed : MonoBehaviour
{
	private Animator animator;
	public AnimationCurve speedCurve;  // 在编辑器中可编辑的曲线
	private float time;
	private bool isAnimationPlaying = false;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		// 检查动画是否开始播放
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Action-waving-new"))
		{
			// 如果动画开始播放，记录为播放状态
			if (!isAnimationPlaying)
			{
				isAnimationPlaying = true;
				time = 0;  // 从0开始重新计时
			}

			// 在动画播放时更新速度
			time += Time.deltaTime * 0.5f;  // 控制播放速度

			// 使用 AnimationCurve 来计算速度
			float speed = speedCurve.Evaluate(time % 1);  // 使用曲线来评估当前的速度

			// 设置 Speed 参数，控制动画速度
			animator.SetFloat("Speed", speed);
		}
		else
		{
			// 如果动画没有播放，停止控制动画速度
			if (isAnimationPlaying)
			{
				isAnimationPlaying = false;
				animator.SetFloat("Speed", 0);  // 恢复默认速度
			}
		}
	}
}


