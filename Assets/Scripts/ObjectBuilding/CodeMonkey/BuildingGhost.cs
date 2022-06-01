using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    public Transform visual;
    private Transform visualtemp;
    private Transform Redvisual;

    private PlacedObjectTypeSO placedObjectTypeSO;

    [SerializeField] private Material[] Ghostmaterial;
    Renderer rend;
    public int x;

    private void Start() {
        RefreshVisual();


       GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e) {
        RefreshVisual();
    }

    public void ObjectButtonClickedGhost() {
        RefreshVisual();

    }    

       void OnTriggerEnter(Collider other) 
	{
        visualtemp = visual;
		visual = Redvisual;
        Redvisual = visualtemp;
        RefreshVisual();
	}
     void OnTriggerExit(Collider other) 
	{
		visualtemp = visual;
		visual = Redvisual;
        Redvisual = visualtemp;
        RefreshVisual();
	}

    private void LateUpdate() {
     
        //Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
        //targetPosition.y = 1f;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void RefreshVisual() {
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }

        PlacedObjectTypeSO placedObjectTypeSO = GridBuildingSystem.Instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null) {
            Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
            visual = Instantiate(placedObjectTypeSO.visual, targetPosition, Quaternion.identity);
            //visual.parent = transform;
            visual.localPosition = targetPosition;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 8);
        }
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

}

