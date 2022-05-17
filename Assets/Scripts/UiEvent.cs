using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiEvent : MonoBehaviour
{
    [SerializeField] Button drawBtn;
    [SerializeField] Text distanceText;
    [SerializeField] Button distPBtn;
    [SerializeField] Button distMBtn;
    [SerializeField] Button undoBtn;
    [SerializeField] Button resetBtn;
    [SerializeField] Text anText;

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

    // Start is called before the first frame update
    void Start()
    {
        drawBtn.onClick.AddListener(() => DrawBtnClicked());
        distPBtn.onClick.AddListener(() => DistPClicked());
        distMBtn.onClick.AddListener(() => DistMClicked());
        undoBtn.onClick.AddListener(() => undoClicked());
        resetBtn.onClick.AddListener(() => resetClicked());

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
                    //capsule.transform.position = hit.point;
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        pressed = true;
                        startPoint = hit.point;
                    }
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        endPoint = hit.point;
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
                        CurrentWall.transform.position = hit.point;
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

}
