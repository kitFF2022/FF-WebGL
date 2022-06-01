using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boiler : MonoBehaviour
{
    public bool boilerOn;
    public int boilerGoalTemp;
    private float boilerUpTemp;



    void Start() {
        boilerOn = false;
        boilerGoalTemp = 0;
        boilerUpTemp = 0.5f;
    }

    void FixedUpdate() {
        CTempChanged();
        Debug.Log(boilerOn);
    }

    public void CTempChanged() {
        if(boilerOn) {
            float roomTemp = Room.Instance.ReturnTemp();
            if(roomTemp<boilerGoalTemp) {
                roomTemp += boilerUpTemp;
                Room.Instance.giveTemp(roomTemp);
            } else {
                roomTemp = roomTemp;
            }
        }
    }
}
