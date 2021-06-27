// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource soundSouce;
    [SerializeField] Material[] mats;
    [SerializeField] AudioClip[] beeps;

    public int Value { get; set; }

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        soundSouce = GameObject.Find("Game Manager").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioClip clip = beeps[Random.Range(0, beeps.Length)];
        soundSouce.PlayOneShot(clip, 1);
        gameManager.IncrementScore(Value);
        other.gameObject.SetActive(false);
        ChangeColor();
    }

    private void ChangeColor(){
        var mat = mats[Random.Range(0, mats.Length)];
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().material = mat;
        }
    }
}
