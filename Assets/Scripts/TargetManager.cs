using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private TargetPooler targetPool;
    private float startDelay = 1f;
    private bool gameOver = false;

    [Header("Starting Positions")]
    [SerializeField] Vector3 startPos1;
    [SerializeField] Vector3 startPos2;
    [SerializeField] Vector3 startPos3;

    [Header("X Position to Deactivate")]
    [SerializeField] float xMax1;
    [SerializeField] float xMax2;
    [SerializeField] float xMax3;

    [Header("Speed Per Row")]
    [SerializeField] float speed1;
    [SerializeField] float speed2;
    [SerializeField] float speed3;

    [Header("Frequency of New Targets")]
    [SerializeField] int frequency1;
    [SerializeField] int frequency2;
    [SerializeField] int frequency3;

    [Header("Point Value")]
    [SerializeField] int value1;
    [SerializeField] int value2;
    [SerializeField] int value3;

    
    // Start is called before the first frame update
    void Start()
    {
        targetPool = TargetPooler.TargetPool;
    }

    private void Update() {
        gameOver = gameObject.GetComponent<GameManager>().GameOver;
    }

    IEnumerator SpawnPath(Vector3 startPos, float xMax, float speed, float frequency, int value){
        yield return new WaitForSeconds(startDelay);
        while(!gameOver){
            GameObject target = targetPool.GetPooledTarget();
            target.GetComponent<TargetController>().Speed = speed;
            target.GetComponent<TargetController>().XPosMax = xMax;
            target.GetComponent<Counter>().Value = value;
            target.SetActive(true);
            target.transform.position = startPos;    
            yield return new WaitForSeconds(Random.Range(1, frequency));
        }        
    }

    public void StartGame(){
        StartCoroutine(SpawnPath(startPos1, xMax1, speed1, frequency1, value1));
        StartCoroutine(SpawnPath(startPos2, xMax2, speed2, frequency2, value2));
        StartCoroutine(SpawnPath(startPos3, xMax3, speed3, frequency3, value3));
    }
}