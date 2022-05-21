using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUiEvent : MonoBehaviour
{
    [SerializeField] GameObject ProObj;
    RectTransform rectProObj;

    Vector2 ProObjDes;
    Vector2 velocity = Vector2.zero;
    float smoothTime = 0.3f;
    void Start()
    {
        rectProObj = ProObj.GetComponent<RectTransform>();
        ProObjDes = rectProObj.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rectProObj.anchoredPosition = Vector2.SmoothDamp(rectProObj.anchoredPosition, ProObjDes, ref velocity, smoothTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ProObjDes = new Vector2(-Screen.width, 0);
            //OldProObjDes = new Vector2(1000, 0);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SceneManager.LoadScene("SpaceManager");
        }
    }
}
