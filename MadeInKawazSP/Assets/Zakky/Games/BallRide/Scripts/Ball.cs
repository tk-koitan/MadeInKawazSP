using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    GameObject mStick;
    public float mBallRadius
    {
        get;
        private set;
    }

    Rigidbody2D mRigidbody2D;

    [SerializeField]
    Wall mWall;
    MousePos mMousePos;

    void Awake()
    {
        mBallRadius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        transform.position = new Vector3(Random.Range(-1f, 1f), mStick.transform.position.y + mBallRadius, 0f);
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
        float mSideWall = 4f;

        public Wall(Ball mBall)
        {
            this.mBall = mBall;
        }

        public float ContactWallPosX()
        {
            return Mathf.Max(0f, mSideWall - mBall.mBallRadius);
        }
    }

    public class MousePos
    {
        Vector3 mOldPos;
        Vector3 mCurrentPos;

        float mXInput;
        float mTime;

        public MousePos()
        {
            mTime = Time.time;
        }

        //返り値は-1, 0, 1のいずれか
        public float MouseXInput()
        {
            if (mTime != Time.time)
            {
                mTime = Time.time;

                float result = 0f;
                mCurrentPos = CurrentMousePos();

                if (Input.GetMouseButton(0))
                {
                    result = System.Math.Sign(mCurrentPos.x - mOldPos.x);
                }

                mOldPos = CurrentMousePos();

                mXInput = result;
            }
            return mXInput;
        }

        Vector3 CurrentMousePos()
        {
            Vector3 touchScreenPosition = Input.mousePosition;

            // 1.0fに深い意味は無い。でもとるとなんかバグる
            touchScreenPosition.z = 10.0f;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(touchScreenPosition);

            return mousePos;
        }

        public bool IsMouseXDragged()
        {
            return MouseXInput() != 0;
        }
    }
}