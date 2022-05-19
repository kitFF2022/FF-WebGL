using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUiEvent : MonoBehaviour
{
    [SerializeField] GameObject newProObj;
    [SerializeField] GameObject oldProObj;
    RectTransform rectNewProObj;
    RectTransform rectOldProObj;

    Vector2 NewProObjDes = Vector2.zero;
    Vector2 OldProObjDes = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    float smoothTime = 0.3f;
    void Start()
    {
        rectNewProObj = newProObj.GetComponent<RectTransform>();
        rectOldProObj = oldProObj.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        rectNewProObj.anchoredPosition = Vector2.SmoothDamp(rectNewProObj.anchoredPosition, NewProObjDes, ref velocity, smoothTime);
        rectOldProObj.anchoredPosition = Vector2.SmoothDamp(rectOldProObj.anchoredPosition, OldProObjDes, ref velocity, smoothTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            NewProObjDes = new Vector2(-Screen.width, 0);
            //OldProObjDes = new Vector2(1000, 0);
        }
    }
}
