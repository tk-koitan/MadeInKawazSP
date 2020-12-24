using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoguraGestureHand : MonoBehaviour
{
    // Start is called before the first frame update
    ZakkyLib.Timer mTimer;
    [SerializeField]
    SpriteRenderer[] mHandSprites;
    void Start()
    {
        mTimer = new ZakkyLib.Timer(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (mTimer.IsTimeout())
        {
            gameObject.SetActive(false);
        }

        SpriteChange();
    }

    void SpriteChange()
    {
        for (int i = 0; i < mHandSprites.Length; i++)
        {
            Color col = mHandSprites[i].color;
            col.a = 1f - mTimer.OverTimeRate();
            mHandSprites[i].color = col;
        }
    }
}
