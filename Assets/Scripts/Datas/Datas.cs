using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectName
{
    public string Name;
}
public class Datas : MonoBehaviour
{
    [System.Serializable]
    public class jsonProjectDataWall
    {
        public List<float> Position;
        public List<float> Rotation;
        public List<float> Scale;
        public static jsonProjectDataWall CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<jsonProjectDataWall>(jsonString);
        }
        public string SaveToString()
        {
            return JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class jsonProjectData
    {
        public int WallCount;
        public List<jsonProjectDataWall> Walls;
        public static jsonProjectData CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<jsonProjectData>(jsonString);
        }
        public string SaveToString()
        {
            return JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class jsonProject
    {
        public int id;
        public string Name;
        public string Owner;
        public string Data;
        public string ProfilePic;

        public static jsonProject CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<jsonProject>(jsonString);
        }
    }
    [System.Serializable]
    public class jsonMessage
    {
        public List<jsonProject> message;

        public static jsonMessage CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<jsonMessage>(jsonString);
        }
    }

    [SerializeField] Button OldProjectPrefabBtn;
    [SerializeField] GameObject MainSceneScrollViewContent;

    string APIbaseURL;
    string token;
    public jsonMessage projectsFromServer;
    public jsonProjectData projectDataFromServer;
    bool networkStatus;
    public int currentProject;
    public List<Button> OldProjectBtnList;
    public bool currentProjectInitialized;
    public bool currentProjectDataLoad;
    jsonProjectData projectdata;
    // Start is called before the first frame update
    void Start()
    {
        int browserParam = Application.absoluteURL.IndexOf("?");
        if (browserParam != -1)
        {
            token = Application.absoluteURL.Split("?"[0])[1];
            token = token.Substring(6);
        }
        else
        {
            token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJFbWFpbGFkZHIiOiJjYW5hbjgxODFAZ21haWwuY29tIiwiZXhwaXJlcyI6MTY1NTc0NjMwMy43NDcyODc4fQ.9EAL7aLIu3Aw0Wz19zeewD76GZZe1-3OA3iE2QTWaeQ";
            //todo: alarm to browser
        }
        Debug.Log("token -> " + token);

        APIbaseURL = "http://mmyu.direct.quickconnect.to:8880";
        networkStatus = false;
        currentProject = 0;
        currentProjectInitialized = false;
        OldProjectBtnList = new List<Button>();
        currentProjectDataLoad = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool SetWallTransformList(List<GameObject> wallList)
    {
        if (wallList == null)
        {
            Debug.Log("SetWallTransformList failed");
            return false;
        }
        else
        {
            projectdata = new jsonProjectData();
            projectdata.Walls = new List<jsonProjectDataWall>();
            projectdata.WallCount = wallList.Count - 1;
            for (int i = 0; i < wallList.Count - 1; i++)
            {
                float posx = wallList[i].transform.position.x;
                float posy = wallList[i].transform.position.y;
                float posz = wallList[i].transform.position.z;
                List<float> pos = new List<float>();
                pos.Add(posx);
                pos.Add(posy);
                pos.Add(posz);
                float rotx = wallList[i].transform.rotation.eulerAngles.x;
                float roty = wallList[i].transform.rotation.eulerAngles.y;
                float rotz = wallList[i].transform.rotation.eulerAngles.z;
                List<float> rot = new List<float>();
                rot.Add(rotx);
                rot.Add(roty);
                rot.Add(rotz);
                float sclx = wallList[i].transform.localScale.x;
                float scly = wallList[i].transform.localScale.y;
                float sclz = wallList[i].transform.localScale.z;
                List<float> scl = new List<float>();
                scl.Add(sclx);
                scl.Add(scly);
                scl.Add(sclz);
                projectdata.Walls.Add(new jsonProjectDataWall());
                projectdata.Walls[i].Position = pos;
                projectdata.Walls[i].Rotation = rot;
                projectdata.Walls[i].Scale = scl;
            }
            Debug.Log("SetWallTransformList complete");
            return true;
        }
    }

    public string getWallTransformListJson()
    {
        return projectdata.SaveToString();
    }

    IEnumerator APIGetRecentProject()
    {
        string url = APIbaseURL + "/project";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                networkStatus = false;
                //todo: error message popup
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                projectsFromServer = jsonMessage.CreateFromJson(request.downloadHandler.text);
                networkStatus = true;
                int last = projectsFromServer.message.Count - 1;
                currentProject = projectsFromServer.message[last].id;
                currentProjectInitialized = true;
                Debug.Log(currentProject);
                SceneManager.LoadScene("SpaceManager");
            }
        }
    }
    public bool GetRecentProject()
    {
        StartCoroutine(APIGetRecentProject());
        return true;
    }


    public void OldProjectBtnClicked(int projectId)
    {
        currentProject = projectId;
        currentProjectInitialized = true;
        Debug.Log(currentProject);
        SceneManager.LoadScene("SpaceManager");
    }



    IEnumerator APIGetProjects()
    {
        string url = APIbaseURL + "/project";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                networkStatus = false;
                //todo: error message popup
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                projectsFromServer = jsonMessage.CreateFromJson(request.downloadHandler.text);
                for (int i = 0; i < projectsFromServer.message.Count; i++)
                {
                    Button button = Instantiate(OldProjectPrefabBtn) as Button;
                    button.transform.SetParent(MainSceneScrollViewContent.transform);
                    button.GetComponentInChildren<Text>().text = projectsFromServer.message[i].Name;
                    int temp = projectsFromServer.message[i].id;
                    button.onClick.AddListener(() => OldProjectBtnClicked(temp));
                    OldProjectBtnList.Add(button);
                }
                networkStatus = true;
            }
        }
    }
    public void GetProjects()
    {
        StartCoroutine(APIGetProjects());
    }


    IEnumerator APIPostProject(string projectNamestr)
    {
        ProjectName projname = new ProjectName
        {
            Name = projectNamestr
        };
        string json = JsonUtility.ToJson(projname);
        string url = APIbaseURL + "/project";
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                networkStatus = false;
                //todo: error message popup
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                GetRecentProject();
            }
        }

    }
    public void PostProject(string projectNamestr)
    {
        if (projectNamestr == "") return;
        else
        {
            StartCoroutine(APIPostProject(projectNamestr));
        }
    }


    IEnumerator APIGetProjectData(int projectId)
    {
        string url = APIbaseURL + "/project/data/id=" + projectId;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                networkStatus = false;
                //todo: error message popup
            }
            else
            {
                string jsontemp = request.downloadHandler.text.Replace('\'', '\"');
                jsontemp = jsontemp.Substring(12);
                jsontemp = jsontemp.Substring(0, jsontemp.Length - 2);
                Debug.Log(request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text.Replace('\'', '\"'));
                Debug.Log(jsontemp);
                projectDataFromServer = jsonProjectData.CreateFromJson(jsontemp);
                currentProjectDataLoad = true;
                networkStatus = true;
            }
        }
    }
    public void GetProjectData(int projectId)
    {
        if (projectId == 0) return;
        else StartCoroutine(APIGetProjectData(projectId));
    }


    IEnumerator APIPostProjectData(string projectDataJson, int projectId)
    {
        string url = APIbaseURL + "/project/data/id=" + projectId;
        using (UnityWebRequest request = UnityWebRequest.Post(url, projectDataJson))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(projectDataJson);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                networkStatus = false;
                //todo: error message popup
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                networkStatus = true;
            }
        }
    }
    public void PostProjectData(string projectDataStr) //projectDataStr might be change to another class
    {
        //todo: projectDataJson.......
        if (currentProject == 0) return;
        else StartCoroutine(APIPostProjectData(projectDataStr, currentProject));
    }


    IEnumerator APIGetProjectObjectData(int projectId)
    {
        string url = APIbaseURL + "/project/data/object/id=" + projectId;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                networkStatus = false;
                //todo: error message popup
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                networkStatus = true;
            }
        }
    }
    public void GetProjectObjectData(int projectId)
    {
        if (currentProject == 0) return;
        else StartCoroutine(APIGetProjectObjectData(currentProject));
    }


    IEnumerator APIPostProjectObjectData(string projectObjectData, int projectId) //projectObjectData might be change to another class
    {
        string url = APIbaseURL + "/project/data/object/id=" + projectId;
        using (UnityWebRequest request = UnityWebRequest.Post(url, projectObjectData))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(projectObjectData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                networkStatus = false;
                //todo: error message popup
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                networkStatus = true;
            }
        }
    }
    public void PostProjectObjectData(string projectObjectData)
    {
        if (currentProject == 0) return;
        else StartCoroutine(APIPostProjectObjectData(projectObjectData, currentProject));
    }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
