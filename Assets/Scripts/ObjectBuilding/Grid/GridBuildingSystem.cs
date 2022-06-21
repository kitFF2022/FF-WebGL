using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour
{

    public static GridBuildingSystem Instance { get; private set; }

    public event EventHandler OnSelectedChanged;
    public List<GameObject> co2;
    public List<GameObject> watertank;
    public List<GameObject> Boiler;
    public List<GameObject> shelf;
    GameObject dataObject;

    Datas data;

    public List<List<GameObject>> objectlist;

    private GridXZ<GridObject> grid;
    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList = null;
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject saveText;

    BuildingGhost buildingGhost;
    private PlacedObjectTypeSO placedObjectTypeSO;
    private PlacedObjectTypeSO.Dir dir;
    int ObjectRotation = 180;

    void Start()
    {
        Datas.jsonProjectData walls = data.projectdata;
        Debug.Log("wall count -> " + walls.WallCount);
        for (int i = 0; i < walls.WallCount; i++)
        {
            float posx = walls.Walls[i].Position[0];
            float posy = walls.Walls[i].Position[1];
            float posz = walls.Walls[i].Position[2];
            Vector3 pos = new Vector3(posx * 4, posy, posz * 4);
            float rotx = walls.Walls[i].Rotation[0];
            float roty = walls.Walls[i].Rotation[1];
            float rotz = walls.Walls[i].Rotation[2];
            Quaternion rot = Quaternion.Euler(rotx, roty - 90, rotz);
            Transform wall = Instantiate(placedObjectTypeSOList[4].prefab, pos, rot);
            float scalez = walls.Walls[i].Scale[2];
            wall.localScale = new Vector3(scalez, 6, 4);
        }
    }


    private void Awake()
    {
        Instance = this;
        co2 = new List<GameObject>();
        watertank = new List<GameObject>();
        Boiler = new List<GameObject>();
        shelf = new List<GameObject>();

        objectlist = new List<List<GameObject>>();


        int gridWidth = 400;
        int gridHeight = 400;
        float cellSize = 2f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-240, 0, -240), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));

        placedObjectTypeSO = null;// placedObjectTypeSOList[0];

        dataObject = GameObject.FindGameObjectWithTag("Data");
        data = dataObject.GetComponent<Datas>();



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

        public void SetPlacedObject(Transform transform)
        {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, z);
        }

        public void ClearPlacedObject()
        {
            transform = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanBuild()
        {

            if (ObjectCollision.Instance.collisionOn == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return x + "," + z + "\n" + transform;
        }
    }

    public Vector3 GetMouseWorldSnappedPosition()
    {        //고스트를 위해 마우스 위치 따주기
        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (placedObjectTypeSO != null)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * .5f * grid.GetCellSize() + new Vector3(grid.cellSize, 0, grid.cellSize) * .5f;
                return placedObjectWorldPosition;
            }
            else
            {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = Mouse3D.GetMouseWorldPosition();
                return placedObjectWorldPosition;
            }

        }
        else
        {
            return mousePosition;
        }
    }

    public void ObjectButtonClickedGrid()
    {
        placedObjectTypeSO = placedObjectTypeSOList[0]; RefreshSelectedObjectType();
        buildingGhost = ghost.GetComponent<BuildingGhost>();
        var visual = buildingGhost.visual;
        Debug.Log(visual);
        Debug.Log(visual.GetChild(0).transform);

        visual = visual.GetChild(0).transform;
        Debug.Log(visual);
        Vector3 ghostPosition = buildingGhost.visual.GetChild(0).transform.position;
        Debug.Log(ghostPosition);



    }
    public void BoilerButtonClickedGrid()
    {
        placedObjectTypeSO = placedObjectTypeSOList[1]; RefreshSelectedObjectType();
        buildingGhost = ghost.GetComponent<BuildingGhost>();
        var visual = buildingGhost.visual;
        Debug.Log(visual);
        Debug.Log(visual.GetChild(0).transform);

        visual = visual.GetChild(0).transform;
        Debug.Log(visual);
        Vector3 ghostPosition = buildingGhost.visual.GetChild(0).transform.position;
        Debug.Log(ghostPosition);

    }

    public void WaterTankButtonClickedGrid()
    {
        placedObjectTypeSO = placedObjectTypeSOList[2]; RefreshSelectedObjectType();
        buildingGhost = ghost.GetComponent<BuildingGhost>();
        var visual = buildingGhost.visual;
        Debug.Log(visual);
        Debug.Log(visual.GetChild(0).transform);

        visual = visual.GetChild(0).transform;
        Debug.Log(visual);
        Vector3 ghostPosition = buildingGhost.visual.GetChild(0).transform.position;
        Debug.Log(ghostPosition);

    }

    public void Co2ButtonClickedGrid()
    {
        placedObjectTypeSO = placedObjectTypeSOList[3]; RefreshSelectedObjectType();
        buildingGhost = ghost.GetComponent<BuildingGhost>();
        var visual = buildingGhost.visual;
        Debug.Log(visual);
        Debug.Log(visual.GetChild(0).transform);

        visual = visual.GetChild(0).transform;
        Debug.Log(visual);
        Vector3 ghostPosition = buildingGhost.visual.GetChild(0).transform.position;
        Debug.Log(ghostPosition);

    }

    public void saveBtnClicked()
    {
        objectlist.Add(shelf);
        objectlist.Add(Boiler);
        objectlist.Add(watertank);
        objectlist.Add(co2);

        Debug.Log(data.currentProject);
        data.SetObjectTransformList(objectlist);
        string savedata = data.getProjectObjectTransformListJson();
        data.PostProjectObjectData(savedata);
        saveText.SetActive(true);
        StartCoroutine(waitforsave());

    }

    IEnumerator waitforsave() {
        yield return new WaitForSeconds(1.0f);
        saveText.SetActive(false);
    }


    public void PlaceButtonClicked()
    {
        buildingGhost = ghost.GetComponent<BuildingGhost>();
        var visual = buildingGhost.visual;
        Vector3 ghostPosition = buildingGhost.visual.GetChild(0).transform.position;

        if (Input.GetKey(KeyCode.LeftShift))
        {       //왼 쉬프트 눌러 그리드 활성화
            grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);

            GridObject gridObject = grid.GetGridObject(x, z);
            if (placedObjectTypeSO != null)
            {
                if (gridObject.CanBuild())
                {
                    try
                    {
                        Transform builtplacedObject = Instantiate(placedObjectTypeSO.prefab, ghostPosition, Quaternion.Euler(0, ObjectRotation, 0));
                        gridObject.SetPlacedObject(builtplacedObject);

                        if (placedObjectTypeSO == placedObjectTypeSOList[0])
                        {
                            shelf.Add(builtplacedObject.gameObject);
                            Debug.Log("shelfAdded");

                        }
                        if (placedObjectTypeSO == placedObjectTypeSOList[1])
                        {

                            Boiler.Add(builtplacedObject.gameObject);
                            Debug.Log("BoilerAdded");

                        }
                        if (placedObjectTypeSO == placedObjectTypeSOList[2])
                        {

                            watertank.Add(builtplacedObject.gameObject);
                            Debug.Log("waterAdded");

                        }
                        if (placedObjectTypeSO == placedObjectTypeSOList[3])
                        {

                            co2.Add(builtplacedObject.gameObject);
                            Debug.Log("co2Added");

                        }


                    }
                    catch (NullReferenceException ex)
                    {

                    }

                }
                else
                {
                    if (placedObjectTypeSO != null)
                    {
                        UtilsClass.CreateWorldTextPopup("Cannot Build Here!", ghostPosition);
                    }
                }
            }
        }
        else
        {
            grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);
            GridObject gridObject = grid.GetGridObject(x, z);
            if (placedObjectTypeSO != null)
            {
                if (gridObject.CanBuild())
                {
                    try
                    {
                        Transform builtplacedObject = Instantiate(placedObjectTypeSO.prefab, ghostPosition, Quaternion.Euler(0, ObjectRotation, 0));
                        gridObject.SetPlacedObject(builtplacedObject);

                        if (placedObjectTypeSO == placedObjectTypeSOList[0])
                        {

                            shelf.Add(builtplacedObject.gameObject);
                            Debug.Log("shelfAdded");

                        }
                        if (placedObjectTypeSO == placedObjectTypeSOList[1])
                        {
                            Boiler.Add(builtplacedObject.gameObject);
                            Debug.Log("BoilerAdded");

                        }
                        if (placedObjectTypeSO == placedObjectTypeSOList[2])
                        {
                            watertank.Add(builtplacedObject.gameObject);
                            Debug.Log("waterAdded");

                        }
                        if (placedObjectTypeSO == placedObjectTypeSOList[3])
                        {
                            co2.Add(builtplacedObject.gameObject);
                            Debug.Log("co2Added");

                        }

                    }
                    catch (NullReferenceException ex)
                    {

                    }


                }
                else
                {
                    if (placedObjectTypeSO != null)
                    {
                        UtilsClass.CreateWorldTextPopup("Cannot Build Here!", ghostPosition);
                    }
                }
            }

        }



    }
    public void DeselectButtonClicked()
    {
        DeselectObjectType();
    }

    public void LeftRotate()
    {
        buildingGhost = ghost.GetComponent<BuildingGhost>();
        var visual = buildingGhost.visual;
        ObjectRotation = ObjectRotation - 15;

    }

    public void RightRotate()
    {
        buildingGhost = ghost.GetComponent<BuildingGhost>();
        var visual = buildingGhost.visual;
        ObjectRotation = ObjectRotation + 15;

    }

    private void Update()
    {


        if (data.currentProjectObjectDataLoad)
        {
            for (int i = 0; i < data.projectObjectDataFromServer.ObjectCount; i++)
            {
                Datas.jsonProjectObjectDataObject tempObject = data.projectObjectDataFromServer.Objects[i];
                float posx = tempObject.Position[0];
                float posy = tempObject.Position[1];
                float posz = tempObject.Position[2];
                Vector3 pos = new Vector3(posx, posy, posz);
                float rotx = tempObject.Rotation[0];
                float roty = tempObject.Rotation[1];
                float rotz = tempObject.Rotation[2];
                Quaternion rot = Quaternion.Euler(new Vector3(rotx, roty, rotz));

                Transform builtplacedObject = Instantiate(placedObjectTypeSOList[tempObject.objectId].prefab, pos, rot);

                if (placedObjectTypeSOList[tempObject.objectId] == placedObjectTypeSOList[0])
                {
                    shelf.Add(builtplacedObject.gameObject);
                    Debug.Log("shelfAdded");
                }
                if (placedObjectTypeSOList[tempObject.objectId] == placedObjectTypeSOList[1])
                {
                    Boiler.Add(builtplacedObject.gameObject);
                    Debug.Log("BoilerAdded");
                }
                if (placedObjectTypeSOList[tempObject.objectId] == placedObjectTypeSOList[2])
                {
                    watertank.Add(builtplacedObject.gameObject);
                    Debug.Log("waterAdded");
                }
                if (placedObjectTypeSOList[tempObject.objectId] == placedObjectTypeSOList[3])
                {
                    co2.Add(builtplacedObject.gameObject);
                    Debug.Log("co2Added");
                }
            }
            data.currentProjectObjectDataLoad = false;
        }

        //if (Input.GetKeyDown(KeyCode.Space)) { placedObjectTypeSO = placedObjectTypeSOList[0]; RefreshSelectedObjectType(); }

        /* if (placedObjectTypeSO != null) {
             buildingGhost = ghost.GetComponent<BuildingGhost>();
             var visual = buildingGhost.visual;
             visual.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);

         }*/  /*      if (placedObjectTypeSO != null) {
             ObjectRotation = ObjectRotation - (int)(Input.GetAxis("Mouse ScrollWheel") * 50.0f);

         }*/



    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO()
    {
        return placedObjectTypeSO;
    }

    private void DeselectObjectType()
    {     // 오브젝트 선택 해제
        placedObjectTypeSO = null; RefreshSelectedObjectType();
        ObjectRotation = 180;
    }

    private void RefreshSelectedObjectType()
    {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public Quaternion GetPlacedObjectRotation()
    {
        if (placedObjectTypeSO != null)
        {
            if (placedObjectTypeSO == placedObjectTypeSOList[2])
            {
                return Quaternion.Euler(-90, ObjectRotation, 0);

            }
            else
            {
                return Quaternion.Euler(0, ObjectRotation + 90, 0);

            }
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public void Demolition()
    {

    }
}
