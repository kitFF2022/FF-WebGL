using UnityEngine;

public class SubcameraPlayer : MonoBehaviour
{
	[SerializeField]
	private	VirtualJoystick	virtualJoystick;
	private	float moveSpeed = 10.0f;
	[SerializeField] private Transform cameraCenter;
    [SerializeField] private Transform camera;
	

	private void Update()
	{
		float x = virtualJoystick.Horizontal;
		float y = virtualJoystick.Vertical;	

		if ( x != 0 || y != 0 )
		{
			Vector3 move = 
			new Vector3(0, 0, 1) * y + camera.right * x;

			// 이동량을 좌표에 반영
			cameraCenter.position += move * moveSpeed * Time.deltaTime;
		}


	}



}

