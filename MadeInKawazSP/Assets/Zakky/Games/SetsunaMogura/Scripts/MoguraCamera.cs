using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoguraCamera : MonoBehaviour
{
    //寄る
    //暗くもなる
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveZ(-5f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
