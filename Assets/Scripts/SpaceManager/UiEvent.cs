using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UiEvent : MonoBehaviour
{
    [SerializeField] Button drawBtn;
    [SerializeField] Text distanceText;
    [SerializeField] Button distPBtn;
    [SerializeField] Button distMBtn;
    [SerializeField] Button undoBtn;
    [SerializeField] Button resetBtn;
    [SerializeField] Text anText;
    [SerializeField] Button leftBtn;
    [SerializeField] Button rightBtn;
    [SerializeField] Button downBtn;
    [SerializeField] Button upBtn;
    [SerializeField] Button snapBtn;

    bool drawBtnToggle;

    Vector3 startPoint;
    Vector3 middlePoint;
    Vector3 endPoint;
    bool pressed;
    GameObject wallPrefab;
    List<GameObject> wallList;
    GameObject CurrentWall = null;
    Vector3 cameraTarget = new Vector3(0, 30, 0);
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.3f;

    bool snapping = true;

    // Start is called before the first frame update
    void Start()
    {
        drawBtn.onClick.AddListener(() => DrawBtnClicked());
        distPBtn.onClick.AddListener(() => DistPClicked());
        distMBtn.onClick.AddListener(() => DistMClicked());
        undoBtn.onClick.AddListener(() => undoClicked());
        resetBtn.onClick.AddListener(() => resetClicked());
        leftBtn.onClick.AddListener(() => leftClicked());
        rightBtn.onClick.AddListener(() => rightClicked());
        downBtn.onClick.AddListener(() => downClicked());
        upBtn.onClick.AddListener(() => upClicked());
        snapBtn.onClick.AddListener(() => snapClicked());

        drawBtnToggle = false;
        pressed = false;
        wallList = new List<GameObject>();
        wallPrefab = Resources.Load("Prefabs/Wall") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (drawBtnToggle)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (CurrentWall == null)
                {
                    CurrentWall = MonoBehaviour.Instantiate(wallPrefab) as GameObject;
                    wallList.Add(CurrentWall);
                }
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 calibPoint = hit.point;
                    if (snapping)
                    {
                        double x = calibPoint.x - Math.Truncate(calibPoint.x);
                        double z = calibPoint.z - Math.Truncate(calibPoint.z);
                        if (x > 0.5f) calibPoint.x = (int)hit.point.x;
                        else calibPoint.x = (int)hit.point.x - 1;
                        if (z > 0.5f) calibPoint.z = (int)hit.point.z;
                        else calibPoint.z = (int)hit.point.z - 1;
                    }
                    //capsule.transform.position = hit.point;
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        pressed = true;
                        startPoint = calibPoint;
                    }
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        endPoint = calibPoint;
                    }
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        pressed = false;
                        CurrentWall = null;
                    }
                    if (pressed)
                    {
                        middlePoint = (startPoint + endPoint) / 2;
                        CurrentWall.transform.position = middlePoint;
                        CurrentWall.transform.forward = startPoint - endPoint;
                        float dist = Vector3.Distance(startPoint, endPoint);
                        CurrentWall.transform.localScale = new Vector3(0.1f, 0.1f, dist);
                        distanceText.text = "벽 길이 : " + dist;
                    }
                    else if (CurrentWall != null)
                    {
                        CurrentWall.transform.position = calibPoint;
                        CurrentWall.transform.localScale = new Vector3(0.2f, 0.1f, 0.2f);
                    }


                }
            }
        }


        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cameraTarget, ref velocity, smoothTime);
    }

    public void DrawBtnClicked()
    {
        anText.text = "";
        anText.gameObject.SetActive(false);
        if (drawBtnToggle)
        {
            CurrentWall.SetActive(false);
            drawBtn.GetComponentInChildren<Text>().text = "그리기";
        }
        if (!drawBtnToggle)
        {
            if (CurrentWall != null)
            {
                CurrentWall.SetActive(true);
            }
            drawBtn.GetComponentInChildren<Text>().text = "그리기 끝내기";
        }

        drawBtnToggle = !drawBtnToggle;
    }

    public void DistPClicked()
    {
        if (cameraTarget != new Vector3(0, 5, 0))
            cameraTarget = cameraTarget - new Vector3(0, 5, 0);
    }

    public void DistMClicked()
    {
        cameraTarget = cameraTarget + new Vector3(0, 5, 0);
    }

    public void undoClicked()
    {
        if (wallList.Count != 0)
        {
            CurrentWall = null;
            Destroy(wallList[wallList.Count - 1]);
            wallList.RemoveAt(wallList.Count - 1);
            if (wallList.Count != 0)
            {
                Destroy(wallList[wallList.Count - 1]);
                wallList.RemoveAt(wallList.Count - 1);
            }
        }

    }

    public void resetClicked()
    {
        if (wallList.Count != 0)
        {
            CurrentWall = null;
            int c = wallList.Count;
            for (int i = 0; i < c; i++)
            {
                Destroy(wallList[wallList.Count - 1]);
                wallList.RemoveAt(wallList.Count - 1);
            }
        }
    }

    public void leftClicked()
    {
        cameraTarget = cameraTarget - new Vector3(5, 0, 0);
    }
    public void rightClicked()
    {
        cameraTarget = cameraTarget + new Vector3(5, 0, 0);
    }
    public void downClicked()
    {
        cameraTarget = cameraTarget - new Vector3(0, 0, 5);
    }
    public void upClicked()
    {
        cameraTarget = cameraTarget + new Vector3(0, 0, 5);
    }

    public void snapClicked()
    {
        if (snapping)
        {
            snapBtn.GetComponentInChildren<Text>().text = "스내핑 끔";
        }
        else
        {
            snapBtn.GetComponentInChildren<Text>().text = "스내핑 켬";
        }
        snapping = !snapping;
    }
}
