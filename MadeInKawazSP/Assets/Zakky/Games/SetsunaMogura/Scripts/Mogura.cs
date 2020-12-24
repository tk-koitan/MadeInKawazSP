using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mogura : MonoBehaviour
{
    ZakkyLib.Timer mMoguraTimer;
    bool mHasClearCheck;
    enum MoguraState
    {
        Moguri,
        Moving,
        Kakure
    }
    MoguraState mMoguraState = MoguraState.Moguri;

    AudioSource mAudioSource;
    [SerializeField]
    AudioClip[] mTappedSFX;

    [SerializeField]
    Transform mCameraTrans;

    // Start is called before the first frame update
    void Start()
    {
        float PyonTime = Random.Range(1f, 3f);
        mMoguraTimer = new ZakkyLib.Timer(PyonTime);
        mHasClearCheck = false;
        mAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mMoguraTimer.IsTimeout() && mMoguraState == MoguraState.Moguri)
        {
            mMoguraState = MoguraState.Moving;
            transform.DOMoveY(transform.position.y + 1.8f, 0.5f).SetEase(Ease.InOutExpo).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                mMoguraState = MoguraState.Kakure;
            });
        }

        if (Input.GetMouseButtonDown(0) && !mHasClearCheck)
        {
            Vector3 camPos = Input.mousePosition;
            camPos.z = -mCameraTrans.position.z;
            camPos = Camera.main.ScreenToWorldPoint(camPos);

            if (mMoguraState == MoguraState.Moving &&
                Vector2.Distance(camPos, transform.position) < 1f)
            {
                GameManager.Clear();
                transform.DOKill();
                mAudioSource.PlayOneShot(mTappedSFX[0]);
            }
            else
            {
                mAudioSource.PlayOneShot(mTappedSFX[1]);
            }
            mHasClearCheck = true;
        }
    }
}
