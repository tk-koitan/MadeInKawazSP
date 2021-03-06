﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    Transform mStickTrans;
    [SerializeField]
    GameObject mGestureHand;
    public float mBallRadius
    {
        get;
        private set;
    }

    Rigidbody2D mRigidbody2D;

    [SerializeField]
    Wall mWall;
    [SerializeField]
    MousePos mMousePos;

    void Awake()
    {
        mBallRadius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        transform.position = new Vector3(Random.Range(-1f, 1f), mStickTrans.position.y + mBallRadius, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
        mWall = new Wall(this);
        mMousePos = new MousePos();
    }

    // Update is called once per frame
    void Update()
    {
        BallMove();

        SetGestureHandNonActive();
    }

    void BallMove() 
    {
        AddVelocity();

        ScreenClamp();

        BallRotation();
    }

    void AddVelocity()
    {
        mRigidbody2D.velocity += new Vector2(50f * XInput() * Time.deltaTime, 0f);
    }

    void SetGestureHandNonActive()
    {
        if (XInput() != 0) mGestureHand.SetActive(false);
    }

    //返り値は-1, 0, 1のいずれか
    float XInput()
    {
        if (mMousePos.IsMouseXDragged()) return mMousePos.MouseXInput();
        return System.Math.Sign(Input.GetAxis("Horizontal"));
    }

    void ScreenClamp()
    {
        if (-mWall.ContactWallPosX() > transform.position.x && mRigidbody2D.velocity.x < 0f)
        {
            Vector3 vec = transform.position;
            vec.x = -mWall.ContactWallPosX();
            transform.position = vec;
            mRigidbody2D.velocity = Vector2.zero;
        }
        else if (mWall.ContactWallPosX() < transform.position.x && mRigidbody2D.velocity.x > 0f)
        {
            Vector3 vec = transform.position;
            vec.x = mWall.ContactWallPosX();
            transform.position = vec;
            mRigidbody2D.velocity = Vector2.zero;
        }
    }

    void BallRotation()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * -transform.position.x / mBallRadius);
    }

    [System.Serializable]
    public class Wall
    {
        Ball mBall;

        [SerializeField]
        float mSideWall = 10f;

        public Wall(Ball mBall)
        {
            this.mBall = mBall;
        }

        public float ContactWallPosX()
        {
            return Mathf.Max(0f, mSideWall - mBall.mBallRadius);
        }
    }

    [System.Serializable]
    public class MousePos
    {
        Vector3 mOldPos;

        float mXInput;
        //ここゲームのフレームレート参照してやったほういい
        float mTime;

        enum XInputMode
        {
            DRAG_X_DIRECTION,
            SCREEN_LEFT_OR_RIGHT
        }
        [SerializeField]
        XInputMode mXInputMode = XInputMode.SCREEN_LEFT_OR_RIGHT;

        System.Func<float>[] mMouseXInputFunc;

        public MousePos()
        {
            //ビルドできなかったため by koitan
            //mTime = Time.time;

            mMouseXInputFunc = new System.Func<float>[(int)XInputMode.SCREEN_LEFT_OR_RIGHT + 1];
            mMouseXInputFunc[(int)XInputMode.DRAG_X_DIRECTION] = DragXDirection;
            mMouseXInputFunc[(int)XInputMode.SCREEN_LEFT_OR_RIGHT] = ScreenLeftOrRight;
        }

        float DragXDirection()
        {
            float result = 0f;
            if (Input.GetMouseButton(0))
            {
                result = System.Math.Sign(Input.mousePosition.x - mOldPos.x);
            }
            mOldPos = Input.mousePosition;
            return result;
        }

        float ScreenLeftOrRight()
        {
            float result = 0f;
            if (Input.GetMouseButton(0))
            {
                result = System.Math.Sign(Input.mousePosition.x - Screen.width / 2);
            }
            return result;
        }

        //返り値は-1, 0, 1のいずれか
        public float MouseXInput()
        {
            if (mTime != Time.time)
            {
                mTime = Time.time;
                mXInput = mMouseXInputFunc[(int)mXInputMode]();
            }
            return mXInput;
        }

        public bool IsMouseXDragged()
        {
            return MouseXInput() != 0;
        }
    }
}