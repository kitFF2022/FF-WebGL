using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloorShape : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image floorShapePan;
    List<GameObject> pointList;
    GameObject pointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        pointList = new List<GameObject>();
        pointPrefab = Resources.Load("Prefabs/Point") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            GameObject point = MonoBehaviour.Instantiate(pointPrefab) as GameObject;
            point.transform.SetParent(floorShapePan.transform);
            point.transform.position = Input.mousePosition;
            pointList.Add(point);
            for (int i = 0; i < pointList.Count; i++)
            {
                Debug.Log("pointer " + i + " -> " + pointList[i].transform.position);
            }
        }

    }
}
