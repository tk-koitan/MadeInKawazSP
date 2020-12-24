using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mogura : MonoBehaviour
{
    float PyonTime;
    ZakkyLib.Timer mMoguraTimer;
    bool mHasClearCheck;
    enum MoguraState
    {
        Moguri,
        Moving,
        Kakure
    }
    MoguraState mMoguraState = MoguraState.Moguri;
    // Start is called before the first frame update
    void Start()
    {
        PyonTime = Random.Range(1f, 3f);
        mMoguraTimer = new ZakkyLib.Timer(PyonTime);
        mHasClearCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mMoguraTimer.IsTimeout() && mMoguraState == MoguraState.Moguri)
        {
            mMoguraState = MoguraState.Moving;
            transform.DOMoveY(transform.position.y + 1.8f, 0.5f).SetEase(Ease.InOutExpo).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {

                //transform.DOMoveY(0f, 0.5f).OnComplete(() =>
                //{
                    mMoguraState = MoguraState.Kakure;
                //});
            });
        }

        if (Input.GetMouseButtonDown(0) && mHasClearCheck)
        {
            if (mMoguraState == MoguraState.Moving)
            {
                GameManager.Clear();
            }
            else
            {
                mHasClearCheck = false;
            }
        }
        
    }
}
