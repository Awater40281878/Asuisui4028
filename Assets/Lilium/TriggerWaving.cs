using UnityEngine;

public class TriggerWaving : MonoBehaviour
{
	public Animator animator; // 关联Animator组件
	public string triggerName = "waving"; // 触发器的名称
	public float interval = 4f; // 触发间隔
	public GameObject TailEffectL;
	public GameObject TailEffectR;

	public Animation AniTail;

	public Animation WingL_Rig_Fly_Enter;
	public DynamicBone DBTailL;
	public DynamicBone DBTailR;

	private void Start()
	{
		// 开始重复调用触发方法
		InvokeRepeating(nameof(TriggerAnimation), 0f, interval);
	}

	private void TriggerAnimation()
	{
		if (animator != null && !string.IsNullOrEmpty(triggerName))
		{
			animator.SetTrigger(triggerName);
			TailEffectL.SetActive(false);
			TailEffectR.SetActive(false);
			DBTailL.enabled = false;
			DBTailR.enabled = false;
		}

	}

	private void OnDestroy()
	{ 
		
		// 确保在对象销毁时停止重复调用
		CancelInvoke(nameof(TriggerAnimation));
	}
	public void TailEffectSetActive()
	{
		TailEffectL.SetActive(true);
		TailEffectR.SetActive(true);
		AniTail.Play();

		WingL_Rig_Fly_Enter.Play();
		
	}

}
