using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public static ObjectCollision Instance { get; private set; }

    [SerializeField] private Material[] material;
    Renderer rend;
    public int collisionOn;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        collisionOn = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[collisionOn];
        
    }

    // Update is called once per frame
    void Update()
    {
        rend.sharedMaterial = material[collisionOn];

        


        
    }
    void OnTriggerStay(Collider other) {
        if (other.transform.tag == "Object") {
            //Debug.Log("if");

            collisionOn = 1;
        } 
        if (other.transform.tag == "Wall") {
            //Debug.Log("if");

            collisionOn = 1;
        } 
    }
    void OnTriggerExit(Collider other) 
	{
		if (other.transform.tag == "Object") {
            //Debug.Log("if");

            collisionOn = 0;
        } 
        if (other.transform.tag == "Wall") {
            //Debug.Log("if");

            collisionOn = 0;
        } 
	}

}
