using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyFunTimesExample;
using System.Linq;

public class GameManager : MonoBehaviour 
{
	public Text playerText_1;
	public RawImage playerColor_1;


	public void RefreshTopScores()
	{
		TouchPlayer_Custom[] playersData = FindObjectsOfType<TouchPlayer_Custom>();
		playersData = playersData.OrderByDescending(playerData => playerData.m_playerScore).ToArray();

		playerText_1.text = playersData[0].m_text.text;
		playerColor_1.color = playersData[0].m_rawImage.color;

	}



}
