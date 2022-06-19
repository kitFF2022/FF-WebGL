using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectName
{
    public string Name;
}
public class Datas : MonoBehaviour
{
    [System.Runtime.InteropServices.DllImport("__Internal")]
    static extern string getToken();
    string APIbaseURL;
    string token;
    List<List<float>> WallPositionList;
    List<List<float>> WallRotationList;
    List<List<float>> WallScaleList;
    List<List<List<float>>> WallTransformList;
    bool networkStatus;
    int currentProject;
    // Start is called before the first frame update
    void Start()
    {
        APIbaseURL = "http://mmyu.direct.quickconnect.to:8880";
        setUserToken();
        WallPositionList = new List<List<float>>();
        WallRotationList = new List<List<float>>();
        WallScaleList = new List<List<float>>();
        networkStatus = false;
        currentProject = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //////////////////////
    // WE NEED TEST!!!! //
    //////////////////////
    public static Dictionary<string, string> GetBrowserParameters()
    {
        Dictionary<string, string> ret =
            new Dictionary<string, string>();
        System.Uri uri = new System.Uri(getToken());
        string linkParams = uri.Query;

        if (linkParams.Length == 0)
            return ret;
        if (linkParams[0] == '?')
            linkParams = linkParams.Substring(1);
        string[] sections = linkParams.Split(new char[] { '&' }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string sec in sections)
        {
            string[] split = sec.Split(new char[] { '=' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (split.Length == 0)
                continue;
            if (split.Length == 2)
            {
                if (ret.ContainsKey(split[0]) == true)
                    ret[split[0]] = split[1];
                else
                    ret.Add(split[0], split[1]);
            }
            else
            {
                if (ret.ContainsKey(sec) == true)
                    ret[sec] = "";
                else
                    ret.Add(sec, "");
            }

        }
        return ret;
    }
    //////////////////////
    // WE NEED TEST!!!! //
    //////////////////////
    bool setUserToken()
    {
        Dictionary<string, string> browserParameters = GetBrowserParameters();
        if (browserParameters.Count == 0)
            return false;
        else
        {
            foreach (KeyValuePair<string, string> keyValuePair in browserParameters)
            {
                if (keyValuePair.Key == "token")
                {
                    token = keyValuePair.Value;
                    Debug.Log(token);
                    return true;
                }
            }
            return false;
        }
    }

    public bool SetWallTransformList(List<GameObject> wallList)
    {
        if (wallList == null) return false;
        else
        {
            for (int i = 0; i < wallList.Count - 1; i++)
            {
                List<float> temp = new List<float>();
                temp.Add(wallList[i].transform.position.x);
                temp.Add(wallList[i].transform.position.y);
                temp.Add(wallList[i].transform.position.z);
                List<float> temp2 = new List<float>();
                temp2.Add(wallList[i].transform.rotation.eulerAngles.x);
                temp2.Add(wallList[i].transform.rotation.eulerAngles.y);
                temp2.Add(wallList[i].transform.rotation.eulerAngles.z);
                List<float> temp3 = new List<float>();
                temp3.Add(wallList[i].transform.localScale.x);
                temp3.Add(wallList[i].transform.localScale.y);
                temp3.Add(wallList[i].transform.localScale.z);
                WallPositionList.Add(temp);
                WallRotationList.Add(temp2);
                WallScaleList.Add(temp3);
            }
            return true;
        }
    }

    public List<List<List<float>>> GetWallTransformList()
    {
        WallTransformList = new List<List<List<float>>>();
        WallTransformList.Add(WallPositionList);
        WallTransformList.Add(WallRotationList);
        WallTransformList.Add(WallScaleList);
        return WallTransformList;
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
                networkStatus = true;
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
                Debug.Log(request.downloadHandler.text);
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
    public void PostProjectData(string projectDataStr, int projectId) //projectDataStr might be change to another class
    {
        //todo: projectDataJson.......
        if (projectId == 0) return;
        else StartCoroutine(APIPostProjectData(projectDataJson, projectId));
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
        if (projectId == 0) return;
        else StartCoroutine(APIGetProjectObjectData(projectId));
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
    public void PostProjectObjectData(string projectObjectData, int projectId)
    {
        //todo: projectObjectDataJson..........
        if (projectId == 0) return
        else StartCoroutine(APIPostProjectObjectData(projectObjectDataJson, projectId));
    }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
