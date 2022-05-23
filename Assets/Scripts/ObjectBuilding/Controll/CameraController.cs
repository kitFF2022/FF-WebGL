using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    //Move Camera Speed and Rotate
    public float turnSpeed = 4.0f;
    public float moveSpeed = 2.0f;
    public float zoomSpeed = 10.0f;
    float mouseX;
    float mouseY;
    private float xRotate = 0.0f;
    public GameObject CrossHair;

    [SerializeField] private GameObject userCamera;
    [SerializeField] private Transform cameraCenter;
    [SerializeField] private Transform camera;
    

    //"ctr + T" Toggle
    private bool toggle = true;
    void Start() {
        
    }

    public bool GetToggle() {
        return toggle;
    }

    public void SetToggle(bool temp) {
        toggle = temp;
    }
    void Awake() {
        Instance = this;
    }

    void Update()
    {
        if(toggle) {
            //mouse Hide and Lock
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            CrossHair.SetActive(true);

            //Move
        
            MouseRotate();
            CamMove();
            //Zoom();
        }

        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CrossHair.SetActive(false);
        }

        if(Input.GetKeyUp(KeyCode.T)) {
            toggle = !toggle;
        }
        
    }

    public void toggleFalsed() {
        toggle = false;
    }
    public void toggleTrued() {
        toggle = true;
    }

    void MouseRotate() {
        
        if (Input.GetMouseButton(0)) {
            
                float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
                float yRotate = transform.eulerAngles.y + yRotateSize;

                float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
                xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);
            
                transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
            
            
        }
            
        
    }

    void KeyBoardMove() {
        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void CamMove() {
    /* hAxis = Input.GetAxisRaw("Horizontal");
    vAxis = Input.GetAxisRaw("Vertical");

    moveCam = new Vector3(hAxis, 0, vAxis).normalized;
    cameraCenter.position += moveCam * camSpeed * Time.deltaTime;*/


    Vector3 move = 
    camera.forward * Input.GetAxis("Vertical") + camera.right * Input.GetAxis("Horizontal");

    // 이동량을 좌표에 반영
    cameraCenter.position += move * moveSpeed * Time.deltaTime;

    }

    void CamRotate() {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y") * -1;

        cameraCenter.rotation = Quaternion.Euler(
            new Vector3(
                cameraCenter.rotation.x + mouseY, 
                cameraCenter.rotation.y + mouseX,0
                ) * moveSpeed);
       
   }

    /*void Zoom() {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if(distance != 0) {
            userCamera.fieldOfView += distance;
        }
    }*/
}