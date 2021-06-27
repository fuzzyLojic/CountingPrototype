using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float Speed { get; set; }
    public float XPosMax { get; set; }
    public int Value { get; set; }
    private Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().material;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
        if(transform.position.x >= XPosMax || transform.position.x <= -XPosMax){
            ResetColor();
            gameObject.SetActive(false);
        }
    }

    private void ResetColor(){
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }
}
