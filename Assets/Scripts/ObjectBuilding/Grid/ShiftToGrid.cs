using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftToGrid : MonoBehaviour
{
    [SerializeField] private Material[] material;
    Renderer rend;
    public int x;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x];
        
    }

    // Update is called once per frame
    void Update()
    {
        rend.sharedMaterial = material[x];

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            x = 1;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) {
            x = 0;
        }
        
    }


    
}
