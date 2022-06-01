using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Co2Maker : MonoBehaviour
{
    public bool Co2On;
    public int Co2Goal;
    private float Co2Up;

    void Start() {
        Co2On = false;
        Co2Goal = 400;
        Co2Up = 1f;
    }

    void FixedUpdate() {
        CCo2Changed();
        Debug.Log(Co2On);
    }

    public void CCo2Changed() {
        if(Co2On) {
            float roomCo2 = Room.Instance.ReturnCo2();
            if(roomCo2<Co2Goal) {
                roomCo2 += Co2Up;
                Room.Instance.giveCo2(roomCo2);
            } else {
                roomCo2 -= Co2Up/2;
                Room.Instance.giveCo2(roomCo2);
            }
        }
    }

}
