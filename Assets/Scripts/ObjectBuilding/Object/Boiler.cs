using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boiler : MonoBehaviour
{
    public bool boilerOn;
    public int boilerGoalTemp;
    private float boilerUpTemp;
    public float rand;


    void Start() {
        boilerOn = false;
        boilerGoalTemp = 0;
        boilerUpTemp = 1f;
        rand = UnityEngine.Random.Range(0.01f, 0.2f);
    }

    void FixedUpdate() {
        CTempChanged();
        Debug.Log(boilerOn);
    }

    public void CTempChanged() {
        if(boilerOn) {
            float roomTemp = Room.Instance.ReturnTemp();
            if(roomTemp<boilerGoalTemp) {
                roomTemp += boilerUpTemp*rand;
                Room.Instance.giveTemp(roomTemp);
            } else {
                roomTemp = roomTemp;
            }
        }
    }
}
