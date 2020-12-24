using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField]
    GameObject cmr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = Input.mousePosition;
        camPos.z = -cmr.transform.position.z;
        camPos = Camera.main.ScreenToWorldPoint(camPos);
        transform.position = camPos;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(camPos.z);
        }
    }
}
