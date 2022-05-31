using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public int MaxWater = 100;
    public float CurrentWater = 0;
    public bool MaxLight = false;
    public float plantInWater = 1;
    public float plantOutWater = 1;


    public float plantInCo2 = 1;

    public bool plant = false;

    public bool WaterTank;

    void Start() {
        
        Debug.Log("shelf satrt");
        
    }

    void Update () {
        
    }

}
