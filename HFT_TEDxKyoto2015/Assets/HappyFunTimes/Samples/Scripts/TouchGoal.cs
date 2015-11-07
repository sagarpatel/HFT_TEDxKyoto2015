using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using HappyFunTimes;
using CSSParse;

namespace HappyFunTimesExample {

class TouchGoal : MonoBehaviour {

    void Start() {
        m_rand = new System.Random();
        m_position = new Vector3();
    }

    void OnTriggerEnter(Collider other) {
        PickPosition();
    }

    private void PickPosition() {
        TouchGameSettings settings = TouchGameSettings.settings();
        m_position.x = m_rand.Next(settings.areaWidth ) - settings.areaWidth  / 2;
        m_position.z = m_rand.Next(settings.areaHeight);
        gameObject.transform.localPosition = m_position;
    }

    public System.Random m_rand;
    public Vector3 m_position;
}

}  // namespace HappyFunTimesExample
