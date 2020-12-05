﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Needle;
    int sum = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (sum == -(4 - 1) || (Random.Range(0, 2) == 0 && sum != 4 - 1))
            {
                sum++;
                GameObject obj = Instantiate(Needle, new Vector2(1.7f * (1.5f - 1 * i), -5f), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5f / 4);
            }
            else
            {
                sum--;
                GameObject obj = Instantiate(Needle, new Vector2(1.7f * (1.5f - 1 * i), 5f), Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5f / 4);
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //
    //}
}
