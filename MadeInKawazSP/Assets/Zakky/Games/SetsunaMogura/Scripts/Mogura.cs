using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mogura : MonoBehaviour
{
    float PyonTime;
    ZakkyLib.Timer mMoguraTimer;
    // Start is called before the first frame update
    void Start()
    {
        PyonTime = Random.Range(1f, 3f);
        mMoguraTimer = new ZakkyLib.Timer(PyonTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
