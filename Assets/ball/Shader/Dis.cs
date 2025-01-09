using UnityEngine;

public class Dis  : MonoBehaviour
{
    public Transform cameraTransform;  // 相机的 Transform
    public float minDistance = 2f;     // 最短距离
    public float maxDistance = 10f;    // 最远距离
    public Renderer objectRenderer;    // 物体的 Renderer，用于访问材质
    public bool two_shader = false;
	public Renderer two_shaderRenderer;
    private void Start()
    {
		cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    void Update()
    {
        CalculateDistance();
		
	}

	void CalculateDistance()
	{
		// 计算物体和相机在Z轴上的距离
		float objectZ = transform.position.z;
		float cameraZ = cameraTransform.position.z;
		float zDistance = Mathf.Abs(objectZ - cameraZ);

		// 如果Z轴距离小于最短距离，不计算
		if (zDistance < minDistance)
		{
			//Debug.Log("Too close to calculate.");
			return;
		}

		// 如果Z轴距离大于最远距离，设置为最远
		if (zDistance > maxDistance)
		{
			zDistance = maxDistance;
		}

		// 计算距离比例，1为最远，0为相机加最短距离
		float normalizedDistance = Mathf.InverseLerp(minDistance, maxDistance, zDistance);
		//Debug.Log("Distance Ratio: " + normalizedDistance);

		// 将 normalizedDistance 设置为材质的 _DepthDis 属性
		objectRenderer.material.SetFloat("_DepthDis", normalizedDistance);
		if (two_shader)
		{
			two_shaderRenderer.material.SetFloat("_DepthDis", normalizedDistance);
		}
	}

}
