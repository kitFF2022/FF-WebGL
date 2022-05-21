using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectInfo : MonoBehaviour
{
    public static GetObjectInfo Instance { get; private set; }

    //private List<GameObject> gameObjectList;
    private GameObject currentGameObject;
    RaycastHit raycastHit;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameObjectList = new List<GameObject>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetCurrentGameObject() {
        raycastHit = Mouse3D.GetMouseClickedObjectHit();
        currentGameObject = raycastHit.transform.gameObject;
        Debug.Log(currentGameObject);
        //gameObjectList.Add(raycastHit.transform.gameObject);
        return currentGameObject;
    }
}
