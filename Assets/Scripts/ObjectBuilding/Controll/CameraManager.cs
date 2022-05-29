using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  [SerializeField] public GameObject MainCamera;
  [SerializeField] public GameObject SubCamera;


  void Start() {
      mainOn();
  }

  public void mainOn() {
      MainCamera.SetActive(true);
      SubCamera.SetActive(false);
  }

  public void subOn() {
      SubCamera.SetActive(true);
      MainCamera.SetActive(false);
  }
}
