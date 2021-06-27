using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPooler : MonoBehaviour
{
    public static ShotPooler ShotPool;
    [SerializeField] GameObject shot;
    [SerializeField] int numShots;
    private List<GameObject> pooledShots;

    private int counter = 0;

    private void Awake() {
        ShotPool = this;
    }

    private void Start() {
        pooledShots = new List<GameObject>();

        for(int i = 0; i < numShots; i++){
            GameObject obj = Instantiate(shot);
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
            pooledShots.Add(obj);
        }
    }

    public GameObject GetPooledShot(){
        for(int i = 0; i < pooledShots.Count; i++){
            if(!pooledShots[i].activeInHierarchy && pooledShots[i] != null){
                return pooledShots[i];
            }
        }

        GameObject obj = pooledShots[counter];
        obj.SetActive(false);
        if(counter >= numShots - 1){
            counter = 0;
        }
        else{
            counter++;
        }
        Debug.Log(counter);
        return obj;
    }


}
