using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailEffectController : MonoBehaviour
{
	public static TailEffectController instance;
	private GameObject TailEffectL;
	private GameObject TailEffectR;
	public Material targetMaterial;
	private void Start()
	{
		TailEffectL = transform.parent.GetChild(0).gameObject;
		TailEffectR = transform.parent.GetChild(1).gameObject;
	}
	private void Awake()
	{
		instance = this;
	}
	public void TailEffectOn()
	{
		TailEffectL.SetActive(true);
		TailEffectR.SetActive(true);
		
	}
	public void TailEffectOff()
	{
		if (targetMaterial != null)
		{
			targetMaterial.mainTextureOffset = Vector2.zero;

		}
		else
		{
			Debug.Log("targetMaterial is null ");
		}
		TailEffectL.SetActive(false);
		TailEffectR.SetActive(false);
	}
}
