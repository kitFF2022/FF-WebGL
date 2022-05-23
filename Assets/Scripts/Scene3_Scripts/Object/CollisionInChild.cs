using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInChild : MonoBehaviour
{
    [SerializeField] private Material[] material;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[ObjectCollision.Instance.collisionOn];
        
    }

    // Update is called once per frame
    void Update()
    {
        rend.sharedMaterial = material[ObjectCollision.Instance.collisionOn];
    }
}
