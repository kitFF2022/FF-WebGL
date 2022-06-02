using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainUiEvent : MonoBehaviour
{
    [SerializeField] GameObject ProObj;
    [SerializeField] Button NewProBtn;
    [SerializeField] Button OldProBtn;
    [SerializeField] GameObject NewProMenu;
    [SerializeField] GameObject OldProMenu;
    [SerializeField] Button PrevBtn1;
    [SerializeField] Button PrevBtn2;
    [SerializeField] Button NextSceneBtn;
    [SerializeField] VideoPlayer videobga;
    RectTransform rectProObj;

    Vector2 ProObjDes;
    Vector2 velocity = Vector2.zero;
    float smoothTime = 0.3f;
    void Start()
    {
        rectProObj = ProObj.GetComponent<RectTransform>();
        ProObjDes = rectProObj.anchoredPosition;
        NewProMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width, 0);
        OldProMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width, 0);

        NewProBtn.onClick.AddListener(() => NewProBtnClicked());
        OldProBtn.onClick.AddListener(() => OldProBtnClicked());
        PrevBtn1.onClick.AddListener(() => PrevBtnClicked());
        PrevBtn2.onClick.AddListener(() => PrevBtnClicked());
        NextSceneBtn.onClick.AddListener(() => nextSceneBtnClicked());
        videobga.url = System.IO.Path.Combine(Application.streamingAssetsPath, "video.mp4");
        videobga.Play();
    }

    // Update is called once per frame
    void Update()
    {
        rectProObj.anchoredPosition = Vector2.SmoothDamp(rectProObj.anchoredPosition, ProObjDes, ref velocity, smoothTime);
    }
    void NewProBtnClicked()
    {
        ProObjDes = new Vector2(Screen.width, 0);
    }

    void OldProBtnClicked()
    {
        ProObjDes = new Vector2(-Screen.width, 0);
    }

    void PrevBtnClicked()
    {
        ProObjDes = Vector2.zero;
    }

    void nextSceneBtnClicked()
    {
        SceneManager.LoadScene("SpaceManager");
    }
}
