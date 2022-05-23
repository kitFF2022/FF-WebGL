using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour {

    public static Mouse3D Instance { get; private set; }

    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();
    [SerializeField] private LayerMask mouseObjectLayerMask = new LayerMask();


    private void Awake() {
        Instance = this;
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            transform.position = raycastHit.point;
        }
    }
    
    public static Vector3 GetMouseWorldPosition() => (Instance.GetRaycastHit_Instance()).point;
    public static bool GetMouseClickedObject() => Instance.GetMouseClickedObject_Instance();
    public static RaycastHit GetMouseClickedObjectHit() => Instance.GetMouseClickedObjectHit_Instance();


    
    
    private RaycastHit GetRaycastHit_Instance() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999f, mouseColliderLayerMask)) {
            
            return hit;
        } else {
            //Debug.Log(hit.point);
            return hit;

        }
    }

    private bool GetMouseClickedObject_Instance() {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999f, mouseObjectLayerMask)) {
            
            return true;
        } else {
            //Debug.Log(hit.point);
            return false;

        }
        
    }

     private RaycastHit GetMouseClickedObjectHit_Instance() {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999f, mouseObjectLayerMask)) {
  
            return hit;
        } else {
            //Debug.Log(hit.point);
            return hit;

        }
        
    }

}
