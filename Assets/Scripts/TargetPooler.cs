using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPooler : MonoBehaviour
{
    public static TargetPooler TargetPool;
    [SerializeField] GameObject target;
    [SerializeField] int numTargets;
    private List<GameObject> pooledTargets;

    private void Awake() {
        TargetPool = this;
    }

    void Start()
    {
        for(int i = 0; i < numTargets; i++){
            GameObject tar = Instantiate(target);
            tar.SetActive(false);
            tar.transform.SetParent(this.transform);
            pooledTargets.Add(tar);
        }
    }

    public GameObject GetPooledTarget(){
        for(int i = 0; i < pooledTargets.Count; i++){
            if(!pooledTargets[i].activeInHierarchy && pooledTargets[i] != null){
                return pooledTargets[i];
            }
        }

        return null;
    }
}
