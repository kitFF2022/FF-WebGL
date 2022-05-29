using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public int MaxWater = 100;
    public int CurrentWater = 0;
    public int MaxLight = 100;
    public int CurrentLight = 100;
    public int plantWater = 0;
    public bool plant = false;

    void Start() {
        
        Debug.Log("shelf satrt");
        
    }

    void Update () {
        CurrentWater -= plantWater;
    }

}
