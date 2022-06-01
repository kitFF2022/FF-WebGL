using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] public GameObject cameraController;

    [SerializeField] private Transform cameraCenter;
    [SerializeField] private Transform camera;

    private PlacedObjectTypeSO placedObjectTypeSO;
    private Shelf max;
    public bool IsSceneFour = false;
    public bool IsObjectClickedInScene4 = false;
    public bool Sstate = false;
    public bool Ostate = false;
    public bool Astate = false;
    public bool Gstate = false;
    public bool Hstate = false;
    public bool Pstate = false;

    public RectTransform ScalePopUp;
    public RectTransform ObjectInfoPopUp;
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
    public GameObject ShelfInfo;
    public GameObject BoilerInfo;
    public GameObject WaterTankInfo;
    public GameObject WindowInfo;
    public GameObject Co2MakerInfo;
    public GameObject BoilerPower;

    public Text BoilText;

    public RectTransform PollPopUp;







    public RectTransform targetRectTr;
    public Camera uiCamera;
    public RectTransform menuUITr;


    private Vector2 screenPoint;
    private GameObject currentGameObject;
    private Transform currenttransform;

    public Vector3 currenttransformFront;
    public Vector3 cameraTarget;

    

    void Start()
   {
    PopUpPanelBackGround.SetActive(false);
    ObjectStatePopup.anchoredPosition = Vector3.down * 1000;
    uiCamera = Camera.main;
    currenttransformFront = Vector3.zero;
    cameraTarget = cameraCenter.transform.position- new Vector3(0,1,0);
   }

   private void Update() {
        if(IsObjectClickedInScene4 == true) {
            camera.position = Vector3.Lerp(camera.position, currenttransformFront, 0.01f); 
            camera.transform.LookAt(cameraTarget);
        }
        if(IsObjectClickedInScene4 == false) {
            camera.transform.eulerAngles = new Vector3(90,0,0);
        }


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
        if (Input.GetMouseButtonUp(0)) {
            if(IsSceneFour == true) {
                if(placedObjectTypeSO == null) {
                    if(!EventSystem.current.IsPointerOverGameObject()) {
                    if(Mouse3D.GetMouseClickedObject()) {  
                        IsObjectClickedInScene4 = true;
                        RaycastHit raycastHit = Mouse3D.GetMouseClickedObjectHit();
                        currenttransform = raycastHit.transform;
                        currenttransformFront = currenttransform.position + currenttransform.parent.forward*10 + currenttransform.parent.right*10 + new Vector3(0,10,0);
                        Debug.Log(currenttransform.position);
                        Debug.Log(currenttransformFront);
                        cameraTarget = currenttransform.position + currenttransform.parent.forward*3 + currenttransform.parent.right*-5;
                        
                        currentGameObject = raycastHit.transform.gameObject;
                        
                        max = raycastHit.transform.gameObject.GetComponent<Shelf>();
                        Debug.Log(max);
                        //ObjectPopUpOn();
                        //CameraController.Instance.toggleFalsed();
                        cameraController.GetComponent<CameraController>().SetToggle(false);
                        camera.GetComponent<Camera>().orthographic = false;
                        //camera.position = Vector3.Lerp(camera.position, currenttransformFront, 0.05f); 
                        ObjectInfoPopUp.anchoredPosition = new Vector2(478, -2);
                        Debug.Log(currentGameObject.name);
                        if(currentGameObject.name == "Shelf Without Crates") {
                            ShelfInfo.SetActive(true);
                            BoilerInfo.SetActive(false);
                        }
                        if(currentGameObject.name == "cistern") {
                            BoilerInfo.SetActive(true);
                            ShelfInfo.SetActive(false);

                        }
                        

                    } else {
                        
                        currenttransformFront = cameraCenter.transform.position;
                        camera.position = currenttransformFront;
                        //IsObjectClickedInScene4 = false;

                        ObjectInfoPopUp.anchoredPosition = Vector3.down * 1200;
                        ShelfInfo.SetActive(false);
                        BoilerInfo.SetActive(false);

                        IsObjectClickedInScene4 = false;
                        camera.GetComponent<Camera>().orthographic = true;
                        
                        
                    
                    }
                    }
                }
            }
        }
    
   }

   public void PlantOnBtn() {
       bool plantOn = currenttransform.GetComponent<Shelf>().plantOn;
        if(plantOn == false) {
            currenttransform.GetComponent<Shelf>().plantOn = true;
            currenttransform.GetComponent<Shelf>().planttray1.SetActive(true);
            currenttransform.GetComponent<Shelf>().planttray2.SetActive(true);
            currenttransform.GetComponent<Shelf>().planttray3.SetActive(true);
        }
        else {
            currenttransform.GetComponent<Shelf>().plantOn = false;
            currenttransform.GetComponent<Shelf>().planttray1.SetActive(false);
            currenttransform.GetComponent<Shelf>().planttray2.SetActive(false);
            currenttransform.GetComponent<Shelf>().planttray3.SetActive(false);
        }


    }


    public void BoilerOnBtn() {
       bool BoilerOn = currenttransform.GetComponent<Boiler>().boilerOn;
       Debug.Log(BoilerOn);
        if(BoilerOn == false) {
            currenttransform.GetComponent<Boiler>().boilerOn= true;
            BoilerPower.GetComponent<Image>().color = Color.green;

        }
        else {
            currenttransform.GetComponent<Boiler>().boilerOn = false;
            BoilerPower.GetComponent<Image>().color = Color.red;


        }


    }


    public void BoilerP() {
        currenttransform.GetComponent<Boiler>().boilerGoalTemp += 1;
        BoilText.text = currenttransform.GetComponent<Boiler>().boilerGoalTemp.ToString();
    }  
    
    public void BoilerM() {
        currenttransform.GetComponent<Boiler>().boilerGoalTemp -=1;
        BoilText.text = currenttransform.GetComponent<Boiler>().boilerGoalTemp.ToString();
    }

    

    public void LightOnBtn() {
       bool LightOn = currenttransform.GetComponent<Shelf>().LightOn;

        if(LightOn) {
            currenttransform.GetComponent<Shelf>().LightOn = false;
            currenttransform.GetComponent<Shelf>().led1.SetActive(false);
            currenttransform.GetComponent<Shelf>().led2.SetActive(false);
            currenttransform.GetComponent<Shelf>().led3.SetActive(false);
        } else {
            currenttransform.GetComponent<Shelf>().LightOn = true;
            currenttransform.GetComponent<Shelf>().led1.SetActive(true);
            currenttransform.GetComponent<Shelf>().led2.SetActive(true);
            currenttransform.GetComponent<Shelf>().led3.SetActive(true);
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
        IsSceneFour = true;
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
        IsSceneFour = false;
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
