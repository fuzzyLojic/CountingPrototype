using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private GameObject player;

    float angle = 0;
    public float speed = 20f;
    private float xPosMax = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if(transform.position.x >= xPosMax || transform.position.x <= -xPosMax){
            gameObject.SetActive(false);
        }
    }
}
