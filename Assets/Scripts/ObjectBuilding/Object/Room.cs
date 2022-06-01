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
    public float MaxRoomWater = 1000;
    public float RoomLight = 0;

    public float BoilerTemp;
    public float WindowTemp;
    public float WindowHumid;
    public float plenthumid;
    public float Co2MakerCo2;
    public float plentCo2;

    public bool BoilerOn = false;
    public bool WaterOn = false;
    public bool WindowOn = false;
    public bool Co2On = false;


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
        if(BoilerOn) {
            RoomTemp += BoilerTemp;
            RoomTemp -= WindowTemp;
        }
        else {
            RoomTemp -= WindowTemp;
        }
    }

    public void RoomHumidChange() {
        if(RoomHumid > 0) {
            RoomHumid -= WindowHumid;
        }

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
    public float giveTemp(float temp) {
        RoomTemp = temp;
        return RoomTemp;
    }


    public float ReturnHumid() {
        return RoomHumid;
    }

    public float giveHumid(float humid) {
        RoomHumid = humid;
        return RoomHumid;
    }


    public float ReturnCo2() {
        return RoomCo2;
    }

    public float giveCo2(float co2) {
        RoomCo2 = co2;
        return RoomCo2;
    }


    public float ReturnWater() {
        return RoomWater;
    }

    public float giveWater(float water) {
        RoomWater = water;
        return RoomWater;
    }

    
    public float ReturnLight() {
        return RoomLight;
    }

    public float giveLight(float light) {
        RoomLight = light;
        return RoomLight;
    }

    public bool RoomWaterOn() {
        WaterOn = true;
        return WaterOn;
    }

    public bool RoomWaterOff() {
        WaterOn = false;
        return WaterOn;

    }
    public bool Roomwaterbool() {
        return WaterOn;
    }

}
