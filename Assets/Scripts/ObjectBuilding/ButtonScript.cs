﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] public GameObject cameraController;
    private PlacedObjectTypeSO placedObjectTypeSO;
    private Shelf max;
    public bool Sstate = false;
    public bool Ostate = false;
    public bool Astate = false;
    public bool Gstate = false;
    public bool Hstate = false;
    public bool Pstate = false;

    public RectTransform ScalePopUp;
    public RectTransform ObjectPopUp;
    public RectTransform AssetPopUp;
    public RectTransform GrapicPopUp;
    public RectTransform HelpPopUp;
    public GameObject PopUpPanelBackGround;
    public RectTransform ObjectStatePopup;
    public RectTransform ObjectPlacePopup;
    public GameObject GoBack2;
    public GameObject GoBack3;
    public GameObject ObjectButton;
    public GameObject PollButton;
    public RectTransform PollPopUp;







    public RectTransform targetRectTr;
    public Camera uiCamera;
    public RectTransform menuUITr;


    private Vector2 screenPoint;
    private GameObject currentGameObject;
    private Transform currenttransform;
   


    void Start()
   {
    PopUpPanelBackGround.SetActive(false);
    ObjectStatePopup.anchoredPosition = Vector3.down * 1000;
    uiCamera = Camera.main;
   }

   private void Update() {
    if (Input.GetMouseButtonUp(0)) {          // 왼 클릭으로 취소

        ObjectStatePopup.anchoredPosition = Vector3.down * 1000;

        

    }
    if (Input.GetMouseButtonUp(1)) {      //오른 클릭으로 오브젝트 클릭
        placedObjectTypeSO = GridBuildingSystem.Instance.GetPlacedObjectTypeSO();
        if(placedObjectTypeSO == null) {

            if(Mouse3D.GetMouseClickedObject()) {  
             
                RaycastHit raycastHit = Mouse3D.GetMouseClickedObjectHit();
                currenttransform = raycastHit.transform;

                currentGameObject = raycastHit.transform.gameObject;
                Debug.Log(currentGameObject);
                max = raycastHit.transform.gameObject.GetComponent<Shelf>();
                Debug.Log(max);
                ObjectPopUpOn();
                //CameraController.Instance.toggleFalsed();
                cameraController.GetComponent<CameraController>().SetToggle(false);

            } else {
                ObjectStatePopup.anchoredPosition = Vector3.down * 1000;

            
            }
        }
        
    } 
    
   }
  
    public void Scale()
    {
           if(Sstate == true)
           {
                ScalePopUp.anchoredPosition = Vector3.down * 1000;
                Sstate = false;
                PopUpPanelBackGround.SetActive(false);
           }
           else
           {
                Ostate = false;
                ObjectPopUp.anchoredPosition = Vector3.down * 1000;
                Astate = false;
                AssetPopUp.anchoredPosition = Vector3.down * 1000;
                Gstate = false;
                GrapicPopUp.anchoredPosition = Vector3.down * 1000;
                Hstate = false;
                HelpPopUp.anchoredPosition = Vector3.down * 1000;
                ScalePopUp.anchoredPosition = Vector3.zero;
               Sstate = true;
               if(Sstate==true || Ostate==true || Astate==true || Gstate==true || Hstate==true)
               {
                   PopUpPanelBackGround.SetActive(true);
               }
               

           }
    }

    public void Go4Clicked() {
        GoBack2.SetActive(false);
        GoBack3.SetActive(true);
        ObjectButton.SetActive(false);
        PollButton.SetActive(true);
    }

    public void PollButtonClicked() {

        if(Pstate == true)
            {
                PollPopUp.anchoredPosition = Vector3.down * 1100;
                Pstate = false;
                
            }
        else {
            PollPopUp.anchoredPosition = Vector3.zero;
            Pstate = true;


        }

    }

     public void PollButtonClose() {
        PollPopUp.anchoredPosition = Vector3.down * 1000;

    }



     public void Go3Clicked() {
        GoBack2.SetActive(true);
        GoBack3.SetActive(false);
        ObjectButton.SetActive(true);
        PollButton.SetActive(false);


    }

    public void ObjectPopUpOn() {
        //카메라 위치
        Vector2 mousePos = Input.mousePosition;
        //Debug.Log(mousePos.ToString());
        ObjectStatePopup.anchoredPosition = mousePos + new Vector2(-350, -400);
  
        
        /*RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, Input.mousePosition, uiCamera, out screenPoint);
        Debug.Log(screenPoint.ToString());
        menuUITr.localPosition = screenPoint;*/
        
    }

    public void ObjectPlacePopUpOn() {
        //카메라 위치
       
        //Debug.Log(mousePos.ToString());
        ObjectPlacePopup.anchoredPosition = new Vector2(518, -422);
  
        
        /*RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, Input.mousePosition, uiCamera, out screenPoint);
        Debug.Log(screenPoint.ToString());
        menuUITr.localPosition = screenPoint;*/
        
    }

    public void ObjectPlacePopUpOff() {

       
        ObjectPlacePopup.anchoredPosition = Vector3.down * 1000;

        
    }

    public void ObjectInfoButtonClicked() {
        Debug.Log(currentGameObject.GetComponent<Shelf>().MaxWater);
    }

    public void ObjectDemolitionClicked() {
        Destroy(currenttransform.parent.gameObject);

        Debug.Log(currentGameObject.GetComponent<Shelf>().MaxWater);

    }

    public void ObjectCoppyClicked() {

    }

     public void ObjectRePlaceClicked() {
        Destroy(currenttransform.parent.gameObject);
    }



    public void ObjectButtonClickedUI() {
        Sstate = false;
        ScalePopUp.anchoredPosition = Vector3.down * 1000;
        Ostate = false;
        ObjectPopUp.anchoredPosition = Vector3.down * 1000;
        Astate = false;
        AssetPopUp.anchoredPosition = Vector3.down * 1000;
        Gstate = false;
        GrapicPopUp.anchoredPosition = Vector3.down * 1000;
        Hstate = false;
        HelpPopUp.anchoredPosition = Vector3.down * 1000;
        PopUpPanelBackGround.SetActive(false);

    }

        public void Object()
    {
           if(Ostate == true)
           {
                ObjectPopUp.anchoredPosition = Vector3.down * 1000;
               Ostate = false;
           }
           else
           {
                Sstate = false;
                ScalePopUp.anchoredPosition = Vector3.down * 1000;
                Astate = false;
                AssetPopUp.anchoredPosition = Vector3.down * 1000;
                Gstate = false;
                GrapicPopUp.anchoredPosition = Vector3.down * 1000;
                Hstate = false;
                HelpPopUp.anchoredPosition = Vector3.down * 1000;
                ObjectPopUp.anchoredPosition = Vector3.zero;
               Ostate = true;

           }
    }
        public void Asset()
    {
           if(Astate == true)
           {
                AssetPopUp.anchoredPosition = Vector3.down * 1000;
               Astate = false;
           }
           else
           {
                Ostate = false;
                ObjectPopUp.anchoredPosition = Vector3.down * 1000;
                Sstate = false;
                ScalePopUp.anchoredPosition = Vector3.down * 1000;
                Gstate = false;
                GrapicPopUp.anchoredPosition = Vector3.down * 1000;
                Hstate = false;
                HelpPopUp.anchoredPosition = Vector3.down * 1000;
                AssetPopUp.anchoredPosition = Vector3.zero;
               Astate = true;

           }
    }
        public void Grapic()
    {
           if(Gstate == true)
           {
                GrapicPopUp.anchoredPosition = Vector3.down * 1000;
               Gstate = false;
           }
           else
           {
                Ostate = false;
                ObjectPopUp.anchoredPosition = Vector3.down * 1000;
                Astate = false;
                AssetPopUp.anchoredPosition = Vector3.down * 1000;
                Sstate = false;
                ScalePopUp.anchoredPosition = Vector3.down * 1000;
                Hstate = false;
                HelpPopUp.anchoredPosition = Vector3.down * 1000;
                GrapicPopUp.anchoredPosition = Vector3.zero;
               Gstate = true;

           }
    }
        public void Help()
    {
           if(Hstate == true)
           {
                HelpPopUp.anchoredPosition = Vector3.down * 1000;
               Hstate = false;
           }
           else
           {
                Ostate = false;
                ObjectPopUp.anchoredPosition = Vector3.down * 1000;
                Astate = false;
                AssetPopUp.anchoredPosition = Vector3.down * 1000;
                Gstate = false;
                GrapicPopUp.anchoredPosition = Vector3.down * 1000;
                Sstate = false;
                ScalePopUp.anchoredPosition = Vector3.down * 1000;
                HelpPopUp.anchoredPosition = Vector3.zero;
               Hstate = true;

           }
    }
    

  
}
