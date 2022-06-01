using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Co2Maker : MonoBehaviour
{
    public bool Co2On;
    public int Co2Goal;
    private float Co2Up;
    public float rand;


    void Start() {
        Co2On = false;
        Co2Goal = 400;
        Co2Up = 1f;
        rand = UnityEngine.Random.Range(0.01f, 1f);

    }

    void FixedUpdate() {
        CCo2Changed();
        Debug.Log(Co2On);
    }

    public void CCo2Changed() {
        if(Co2On) {
            float roomCo2 = Room.Instance.ReturnCo2();
            if(roomCo2<Co2Goal) {
                roomCo2 += Co2Up*rand;
                Room.Instance.giveCo2(roomCo2);
            }
            
        }else
            {
                float roomCo2 = Room.Instance.ReturnCo2();
                roomCo2 -= Co2Up*rand;
                Room.Instance.giveCo2(roomCo2);
            }
    }

}
