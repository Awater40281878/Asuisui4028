using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynaBoneCtrllor : MonoBehaviour
{
	// Start is called before the first frame update
	public DynamicBone DBTailL;
	public DynamicBone DBTailR;
	public void DB_Enabled()
	{
		DBTailL.enabled = true;
		DBTailR.enabled = true;
	}
}
