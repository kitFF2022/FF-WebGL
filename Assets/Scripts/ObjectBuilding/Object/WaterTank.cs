using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour
{

    public float MaxWaterTank = 1000;
    public float CurrentWaterTank;




    void Start() {
        Room.Instance.RoomWaterOn();
        Room.Instance.giveWater(MaxWaterTank);
    }



}
