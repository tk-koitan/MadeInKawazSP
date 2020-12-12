using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatCamera : MonoBehaviour
{
    [SerializeField]
    Transform PlayerTrans;
    float CameraXLimit = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (PlayerTrans.position.x - transform.position.x > CameraXLimit)
        {
            Vector3 tmp = transform.position;
            tmp.x = PlayerTrans.position.x - CameraXLimit;
            //transform.position = tmp;
            transform.DOLocalMove(tmp, 0.3f);
        }
        else if (PlayerTrans.position.x - transform.position.x < -CameraXLimit)
        {
            Vector3 tmp = transform.position;
            tmp.x = PlayerTrans.position.x + CameraXLimit;
            //transform.position = tmp;
            transform.DOLocalMove(tmp, 0.3f);
        }
    }
}
