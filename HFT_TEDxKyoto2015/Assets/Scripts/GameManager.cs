using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyFunTimesExample;
using System.Linq;

public class GameManager : MonoBehaviour 
{
	public Text playerText_1;
	public RawImage playerColor_1;
	
	public Text playerText_2;
	public RawImage playerColor_2;

	public Text playerText_3;
	public RawImage playerColor_3;

	public GameObject m_floorObject;
	Material m_floorMaterial;

	float m_pulsePeriod = 3.0f;

	void Start()
	{
		m_floorMaterial = m_floorObject.GetComponent<MeshRenderer>().material;
	}

	public void RefreshTopScores()
	{
		TouchPlayer_Custom[] playersData = FindObjectsOfType<TouchPlayer_Custom>();
		playersData = playersData.OrderByDescending(playerData => playerData.m_playerScore).ToArray();

		playerText_1.text = playersData[0].m_text.text;
		Color pColor  = playersData[0].m_rawImage.material.color;
		pColor.a = 1.0f;
		playerColor_1.color = pColor;

		m_floorMaterial.color = playerColor_1.color; 

		if(playersData[1] != null)
		{
			playerText_2.text = playersData[1].m_text.text;
			pColor = playersData[1].m_rawImage.material.color;
			pColor.a = 1.0f;
			playerColor_2.color = pColor;
		}

		
		if(playersData[2] != null)
		{
			playerText_3.text = playersData[2].m_text.text;
			pColor = playersData[2].m_rawImage.material.color;
			pColor.a = 1.0f;
			playerColor_3.color = pColor;
		}

	}


	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

	}

	IEnumerator PulseFloorColor()
	{


	}


}
