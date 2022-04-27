using System;
using System.Collections;
using System.Collections.Generic;
using Collectibles;
using UnityEngine;
using Random = UnityEngine.Random;

public class Canister_spawner : MonoBehaviour
{
    public float elapsed = 0f;
    public GameObject canister;
    public float interval;
    public GascanCollectible values;

    private void Start()
    {
        values = GameObject.Find("Model").GetComponent<GascanCollectible>();
        interval = values.spawnInterval;
    }
    
    private void Update()
    {
        elapsed += Time.deltaTime;
        if (!(elapsed >= interval)) return;
        elapsed %= interval;
        MakeGas();
    }

    void MakeGas()
    {
         Instantiate(canister, new Vector3(Random.Range(-10,10),gameObject.transform.position.y,transform.position.z+11), Quaternion.identity);
    }

}
