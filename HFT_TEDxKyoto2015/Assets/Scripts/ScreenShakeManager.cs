using UnityEngine;
using System.Collections;

public class ScreenShakeManager : MonoBehaviour 
{
	Camera m_mainCamera;

	float m_screenShakeDuration = 0.75f;

	Vector3 m_camOriginalPos;
	float m_shakeRange = 7.0f;

	void Start()
	{
		m_mainCamera = FindObjectOfType<Camera>();
		m_camOriginalPos = m_mainCamera.transform.position;
	}

	public void LaunchScreenShake()
	{
		StopAllCoroutines();
		StartCoroutine(ShakeScreenCoroutine());

	}

	IEnumerator ShakeScreenCoroutine()
	{
		float timeCounter = 0;

		while(timeCounter < m_screenShakeDuration )
		{
			float progress = timeCounter / m_screenShakeDuration;
			float currentShakeRange = Mathf.Lerp(m_shakeRange, 0, progress);

			Vector3 randomOffset = new Vector3( Random.Range(-currentShakeRange, currentShakeRange),0, Random.Range(-currentShakeRange, currentShakeRange)  );
			m_mainCamera.transform.position = m_camOriginalPos + randomOffset;

			timeCounter += Time.deltaTime;
			yield return null;
		}

		m_mainCamera.transform.position = m_camOriginalPos;

	}


}
