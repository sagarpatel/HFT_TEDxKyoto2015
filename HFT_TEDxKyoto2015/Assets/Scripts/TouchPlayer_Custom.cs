using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using HappyFunTimes;
using CSSParse;

namespace HappyFunTimesExample {

public class TouchPlayer_Custom : MonoBehaviour 
{
	private Renderer m_renderer;
	private HFTGamepad m_gamepad;
	private HFTInput m_hftInput;
	private UnityEngine.UI.Text m_text;
	private UnityEngine.UI.RawImage m_rawImage;
	private Vector3 m_position;
	private Color m_color;
	private string m_name;

		int m_playerScore = 0;
		Rigidbody m_playerRigidbody;
		float m_forceScale = 10000.0f;
		Vector3 m_previousForceVector = Vector3.zero;

	void Start()
	{
		m_gamepad  = GetComponent<HFTGamepad>();
		m_renderer = GetComponent<Renderer>();
		m_position = transform.localPosition;
		
		m_text = transform.FindChild("NameUI/Name").gameObject.GetComponent<UnityEngine.UI.Text>();
		m_rawImage = transform.FindChild("NameUI/NameBackground").gameObject.GetComponent<UnityEngine.UI.RawImage>();
		m_rawImage.material = (Material)Instantiate(m_rawImage.material);
		
		m_gamepad.OnNameChange += ChangeName;
		
		SetName(m_gamepad.Name);
		SetColor(m_gamepad.Color);

		m_playerRigidbody = GetComponent<Rigidbody>();
	}
	
	void Update()
	{
		TouchGameSettings settings = TouchGameSettings.settings();
		//float l = 1.0f; //Time.deltaTime * 5.0f;
		//float nx = m_gamepad.axes[HFTGamepad.AXIS_TOUCH_X] * 0.5f;        // -0.5 <-> 0.5
		//float ny = m_gamepad.axes[HFTGamepad.AXIS_TOUCH_Y] * 0.5f + 0.5f; //    0 <-> 1
		//m_position.x = Mathf.Lerp(m_position.x, settings.areaWidth * nx, l);
		//m_position.z = Mathf.Lerp(m_position.z, settings.areaHeight - (ny * settings.areaHeight) - 1, l);  // because in 2D down is positive.
		
		//gameObject.transform.localPosition = m_position;

		//float nx = m_gamepad.axes[HFTGamepad.AXIS_TOUCH_X] * 0.5f;        // -0.5 <-> 0.5
		//float ny = m_gamepad.axes[HFTGamepad.AXIS_TOUCH_Y] * 0.5f; //    
		
			float nx = m_gamepad.axes[HFTGamepad.AXIS_TOUCH0_X];
			float ny = -m_gamepad.axes[HFTGamepad.AXIS_TOUCH0_Y];

		Vector3 forceVector = new Vector3( nx, 0, ny );

		if(Vector3.Distance(m_previousForceVector, forceVector) > 0.2f)
		{
			
			Debug.Log(forceVector);
			m_playerRigidbody.AddForce( forceVector * m_forceScale * Time.deltaTime);

		}


			m_previousForceVector = forceVector;

	}
	
	void SetName(string name)
	{
		m_name = name;
		gameObject.name = "Player-" + m_name;
		m_text.text = name;
	}
	
	void SetColor(Color color)
	{
		m_color = color;
		m_renderer.material.color = m_color;
		m_rawImage.material.color = m_color;
	}

	void RefreshPlayerNameTag()
	{
		m_text.text = m_name + " : " + m_playerScore.ToString();

	}
	
	public void OnTriggerEnter(Collider other)
	{
			if(other.CompareTag("Goal"))
			{
				m_playerScore += 1;
				RefreshPlayerNameTag();
			}
	}
	
	private void Remove(object sender, EventArgs e)
	{
		Destroy(gameObject);
	}
	
	private void ChangeName(object sender, EventArgs e)
	{
		SetName(m_gamepad.Name);
	}
	

	}
}
