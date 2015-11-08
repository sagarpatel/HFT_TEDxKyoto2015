using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using HappyFunTimes;
using CSSParse;
using System.Linq;

namespace HappyFunTimesExample 
{


	public class GoalBehavior : MonoBehaviour 
	{
				
		public System.Random m_rand;
		public Vector3 m_position;

		float m_fleeImpulseCooldown = 1.42f;
		float m_fleeImpulseScale = 5000.0f;
		Rigidbody m_goalRigibody;
		float m_fleeTimeCounter = 0;
		public AnimationCurve m_scaleCurve;

		Vector3 m_originalScale;
		Vector3 m_minScale = new Vector3(10,10,10);

		float m_explosionRadius = 150.0f;
		float m_explosionPower  = 7000.0f;



		void Start() 
		{
			m_rand = new System.Random();
			m_position = new Vector3();
			m_goalRigibody = GetComponent<Rigidbody>();
			//StartCoroutine(ImpulseAwayFromPlayersCoroutine());
			m_originalScale = transform.localScale;

		}
		
		void OnTriggerEnter(Collider other) 
		{
			if(other.CompareTag("Player"))
			{
				ApplyExplosionForce();
				PickPosition();
			}

		}

		void ApplyExplosionForce()
		{
			Vector3 explosionPos = transform.position;
			Collider[] collidersInExplosionRange = Physics.OverlapSphere(explosionPos, m_explosionRadius);
			int counter = 0;

			for(int i = 0; i < collidersInExplosionRange.Length; i++)
			{
				Debug.Log( collidersInExplosionRange[i] );

				if(collidersInExplosionRange[i].CompareTag("Player"))
				{
					counter += 1;
					Rigidbody targetRigidbody = collidersInExplosionRange[i].GetComponent<Rigidbody>();
					targetRigidbody.AddExplosionForce(m_explosionPower,explosionPos, m_explosionRadius);
					//Debug.Log( collidersInExplosionRange[i] );

				}
			}

			Debug.Log(counter);

		}
		
		private void PickPosition() 
		{
			TouchGameSettings settings = TouchGameSettings.settings();
			m_position.x = m_rand.Next(settings.areaWidth ) - settings.areaWidth  / 2;
			m_position.z = m_rand.Next(settings.areaHeight);
			gameObject.transform.localPosition = m_position;
			m_goalRigibody.velocity = Vector3.zero;
		}

		void Update()
		{
			m_fleeTimeCounter += Time.deltaTime;
			float progress = m_fleeTimeCounter / m_fleeImpulseCooldown;
			float step = m_scaleCurve.Evaluate(progress);

			transform.localScale = Vector3.Lerp(m_minScale, m_originalScale, step);

		}

		IEnumerator ImpulseAwayFromPlayersCoroutine()
		{
			while(true)
			{
				Vector3 fleePos = GetClosestPlayerPosition(); //GetAveragePlayersPosition();
				Vector3 awayVec = -(fleePos - transform.position).normalized;
				m_goalRigibody.AddForce(awayVec * m_fleeImpulseScale);

				m_fleeTimeCounter = 0;
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

		Vector3 GetClosestPlayerPosition()
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			players = players.OrderBy(player => Vector3.Distance(player.transform.position, transform.position)).ToArray();
			return players[0].transform.position;
		}
		
	}
}
