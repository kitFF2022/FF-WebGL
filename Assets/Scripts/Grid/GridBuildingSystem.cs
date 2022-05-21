using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour {

    public static GridBuildingSystem Instance { get; private set; }

    public event EventHandler OnSelectedChanged;
    


    private GridXZ<GridObject> grid;
    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList = null;
    private PlacedObjectTypeSO placedObjectTypeSO;
    private PlacedObjectTypeSO.Dir dir;
    int ObjectRotation = 0;

    
    private void Awake() {
        Instance = this;

        
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
        
        placedObjectTypeSO = null;// placedObjectTypeSOList[0];
    }
    

    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetPlacedObject(Transform transform) {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, z);
        }

        public void ClearPlacedObject() {
            transform = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanBuild() {
            
            if(ObjectCollision.Instance.collisionOn == 0) {
                return true;
            } else {
                return false;
            }
        }

        public override string ToString() 
        {
            return x + "," + z + "\n" + transform;
        }
    }

     public Vector3 GetMouseWorldSnappedPosition() {        //고스트를 위해 마우스 위치 따주기
        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (placedObjectTypeSO != null) {

            if(Input.GetKey(KeyCode.LeftShift)) {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * .5f * grid.GetCellSize()+ new Vector3(grid.cellSize, 0, grid.cellSize) * .5f;
                return placedObjectWorldPosition;
            }
            else {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = Mouse3D.GetMouseWorldPosition();
                return placedObjectWorldPosition;
            }
           
        } 
        else {
            return mousePosition;
        }
    }

    public void ObjectButtonClickedGrid() {
        placedObjectTypeSO = placedObjectTypeSOList[0]; RefreshSelectedObjectType();

    }      


    
    
    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) { placedObjectTypeSO = placedObjectTypeSOList[0]; RefreshSelectedObjectType(); }

        if (placedObjectTypeSO != null) {
            ObjectRotation = ObjectRotation - (int)(Input.GetAxis("Mouse ScrollWheel") * 50.0f);
        
        }
        if (Input.GetMouseButtonDown(0)) {      // 마우스 왼클릭
            if(!EventSystem.current.IsPointerOverGameObject()) {
                Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
                if(Input.GetKey(KeyCode.LeftShift)) {       //왼 쉬프트 눌러 그리드 활성화
                    grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);

                    GridObject gridObject = grid.GetGridObject(x,z);
                    if (placedObjectTypeSO != null) {
                    if (gridObject.CanBuild()) {
                        try {
                            Transform builtplacedObject = Instantiate(placedObjectTypeSO.prefab, grid.GetWorldPosition(x, z)+ new Vector3(grid.cellSize, 0, grid.cellSize) * .5f, Quaternion.Euler(0, ObjectRotation, 0));
                        gridObject.SetPlacedObject(builtplacedObject);
                        
                        }       
                        catch (NullReferenceException ex) {
                            
                        }   
                        
                    } else {
                        if(placedObjectTypeSO != null) {
                            UtilsClass.CreateWorldTextPopup("Cannot Build Here!", mousePosition);
                        }
                    }
                    }
                } else {
                    grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);
                    GridObject gridObject = grid.GetGridObject(x,z);
                    if (placedObjectTypeSO != null) {
                    if (gridObject.CanBuild()) {
                        try {
                            Transform builtplacedObject =  Instantiate(placedObjectTypeSO.prefab, Mouse3D.GetMouseWorldPosition(), Quaternion.Euler(0, ObjectRotation, 0));
                            gridObject.SetPlacedObject(builtplacedObject);
                            
                        }       
                        catch (NullReferenceException ex) {
                            
                        }                
                        
                        
                    } else {
                        if(placedObjectTypeSO != null) {
                            UtilsClass.CreateWorldTextPopup("Cannot Build Here!", mousePosition);
                        }
                    }
                    }
                
                }
            }
        
        }
        if (Input.GetMouseButtonDown(1)) {      //오른쪽 마우스로 빌딩 취소
            DeselectObjectType();
        }
 


        
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
        return placedObjectTypeSO;
    }
    
    private void DeselectObjectType() {     // 오브젝트 선택 해제
    placedObjectTypeSO = null; RefreshSelectedObjectType();
    ObjectRotation = 0;
    }

        private void RefreshSelectedObjectType() {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public Quaternion GetPlacedObjectRotation() {
        if (placedObjectTypeSO != null) {
            return Quaternion.Euler(0, ObjectRotation, 0);
        } else {
            return Quaternion.identity;
        }
    }

    public void Demolition() {
        
    }
}
