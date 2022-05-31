using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public static Room Instance { get; private set; }

    public float RoomTemp = 0;
    public float RoomHumid = 0;
    public float RoomCo2 = 0;
    public float RoomWater = 0;
    public float RoomLight = 0;

    private void Awake() {
        Instance = this;
        RoomTemp = 20;
        RoomHumid = 50;
        RoomCo2 = 400;
        RoomWater = 0;
        RoomLight = 0;
    }

    private void Update() {

    }

    public void RoomTempChange() {
        
    }

    public void RoomHumidChange() {

    }

    public void RoomCo2Change() {

    }

    public void RoomWaterChange() {

    }

    public void RoomLightChange() {

    }



    public float ReturnTemp() {
        return RoomTemp;
    }

    public float ReturnHumid() {
        return RoomHumid;
    }

    public float ReturnCo2() {
        return RoomCo2;
    }

    public float ReturnWater() {
        return RoomWater;
    }
    
    public float ReturnLight() {
        return RoomLight;
    }


}
