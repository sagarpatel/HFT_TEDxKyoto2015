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



	public void RefreshTopScores()
	{
		TouchPlayer_Custom[] playersData = FindObjectsOfType<TouchPlayer_Custom>();
		playersData = playersData.OrderByDescending(playerData => playerData.m_playerScore).ToArray();

		playerText_1.text = playersData[0].m_text.text;
		playerColor_1.color = playersData[0].m_rawImage.material.color;

		if(playersData[1] != null)
		{
			playerText_2.text = playersData[1].m_text.text;
			playerColor_2.color = playersData[1].m_rawImage.material.color;
		}

		
		if(playersData[2] != null)
		{
			playerText_3.text = playersData[2].m_text.text;
			playerColor_3.color = playersData[2].m_rawImage.material.color;
		}

	}


	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

	}


}
