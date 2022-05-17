using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed = 4.0f;
    private float moveSpeed = 2.0f;
    private float xRotate = 0.0f;
    private bool toggle = false;
    [SerializeField] private Text toggleText;
    // Start is called before the first frame update
    void Start()
    {
        toggleText.gameObject.SetActive(toggle);
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            MouseRotate();
            KeyBoardMove();
        }
        ToggleScan();

    }

    void MouseRotate()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }
    void KeyBoardMove()
    {
        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");
        if (Input.GetKey("space"))
        {
            move += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            move -= new Vector3(0, 1, 0);
        }

        transform.position += move * moveSpeed * Time.deltaTime;
    }
    void ToggleScan()
    {
        if (Input.GetKeyUp("t"))
        {
            toggle = !toggle;
            toggleText.gameObject.SetActive(toggle);
            if (toggle)
            {
                //WebGL 적용을 위하여 별도 UI를 사용할것
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
