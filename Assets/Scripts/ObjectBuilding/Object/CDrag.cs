using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDrag : MonoBehaviour
{
	/// <summary>
	/// 3d 텍스트 오브젝트
	/// </summary>
	public Coroutine cor = null; 

	// Use this for initialization
	void Start ()
	{
		
		
	}

	/// <summary>
	/// 마우스 업 이벤트
	/// </summary>
	void OnMouseUp()
	{
		CameraController.Instance.StartCRT();
		
	}

	/// <summary>
	/// 드래그 이벤트
	/// </summary>
	void OnMouseDrag()
	{	
		CameraController.Instance.StopCRT();
		
		
		//마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
		
		//마우스 좌표를 받아온다.
		Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);
	}

	private void LateUpdate() {
     
        transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }
}
