using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using HappyFunTimes;
using CSSParse;

namespace HappyFunTimesExample 
{


	public class GoalBehavior : MonoBehaviour 
	{
				
		public System.Random m_rand;
		public Vector3 m_position;

		float m_fleeImpulseCooldown = 3.0f;
		float m_fleeImpulseScale = 1000.0f;
		Rigidbody m_goalRigibody;

		void Start() 
		{
			m_rand = new System.Random();
			m_position = new Vector3();
			m_goalRigibody = GetComponent<Rigidbody>();
			StartCoroutine(ImpulseAwayFromPlayersCoroutine());
		}
		
		void OnTriggerEnter(Collider other) 
		{
			if(other.CompareTag("Player"))
			{
				PickPosition();
			}

		}
		
		private void PickPosition() 
		{
			TouchGameSettings settings = TouchGameSettings.settings();
			m_position.x = m_rand.Next(settings.areaWidth ) - settings.areaWidth  / 2;
			m_position.z = m_rand.Next(settings.areaHeight);
			gameObject.transform.localPosition = m_position;
			m_goalRigibody.velocity = Vector3.zero;
		}


		IEnumerator ImpulseAwayFromPlayersCoroutine()
		{
			while(true)
			{
				Vector3 playersPos = GetAveragePlayersPosition();
				Vector3 awayVec = -(playersPos - transform.position).normalized;
				m_goalRigibody.AddForce(awayVec * m_fleeImpulseScale);

				yield return new WaitForSeconds(m_fleeImpulseCooldown);
			}

		}

		Vector3 GetAveragePlayersPosition()
		{

			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			Vector3 posSummer = Vector3.zero;
			for(int i = 0; i < players.Length; i++)
			{
				posSummer += players[i].transform.position;
			}
			Vector3 averagePos = posSummer / (float)players.Length;
			return averagePos;
		}
		
	}
}
