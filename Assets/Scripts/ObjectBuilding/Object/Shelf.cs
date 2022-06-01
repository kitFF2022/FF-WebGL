using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class Shelf : MonoBehaviour
{
    public int MaxWater = 100;
    public float CurrentWater = 0;
    public bool MaxLight = false;
    private float plantInWater;
    private float plantOuthumid;


    public float plantInCo2 = 1;

    public bool plantOn = false;
    public bool LightOn = false;

    public bool WaterTank;


    public GameObject planttray1;
    public GameObject planttray2;
    public GameObject planttray3;
    public GameObject led1;
    public GameObject led2;
    public GameObject led3;
    private Button PlantOnButton;
    private Button LEDOnButton;
    public float rand;

    public void Start() {
        
        Debug.Log("shelf satrt");
        plantInWater = 1f;
        plantOuthumid = 1f;
        rand = UnityEngine.Random.Range(0.01f, 0.2f);

        /*PlantOnButton = GameObject.Find("PlantOn").GetComponent<Button>();
        PlantOnButton.onClick.AddListener(() => PlantOnBtn());
        LEDOnButton = GameObject.Find("LEDOn").GetComponent<Button>();
        LEDOnButton.onClick.AddListener(() => LightOnBtn());*/
        
        
    }

    void FixedUpdate () {
        CWaterChanged();
        ChumidChanged();
        LightChanged();
    }

  

    private void CWaterChanged() {
        if(plantOn) {
            if(Room.Instance.Roomwaterbool()) {
                if (CurrentWater < plantInWater) {
                    float roomWater = Room.Instance.ReturnWater();
                    if(roomWater < MaxWater) {
                        CurrentWater = CurrentWater;
                    } 
                    else {
                        roomWater -= MaxWater;
                        Room.Instance.giveWater(roomWater);
                        CurrentWater = MaxWater;
                        CurrentWater -= plantInWater * rand;
                        
                    }
                }
                else {
                    CurrentWater -= plantInWater * rand;
                }    
            } else {
                CurrentWater = CurrentWater;

            }
        }
        
    }  


    private void ChumidChanged() {
        if(plantOn) {
            float roomhumid = Room.Instance.ReturnHumid();
            if(roomhumid <100) {
                roomhumid += plantOuthumid*rand;
                Room.Instance.giveHumid(roomhumid);
            }
       
        }
        
    } 

    
    private void LightChanged() {
        if(LightOn) {
            Room.Instance.giveLight(100);
        } else {
            Room.Instance.giveLight(0);
        }
    }

  


}
