using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer youSprite;
    [SerializeField]
    private Sprite[] sprites;
    private int spriteVersion = 0;

    private bool isFinished = false;
    private bool isWin = false;
    [SerializeField]
    private Transform cat;
    //private AudioSource audioSource;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isFinished)
        {
            //複数のタッチに対応する
            /*
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Move();
                }
            }
            */
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Move();
            }
            if (transform.position.x < -3 || cat.position.x < -2)
            {
                transform.parent = cat;
                isFinished = true;
                if (transform.position.x < -3)
                    isWin = true;
            }
        }

        if (isFinished)
        {
            if (isWin)
            {
                GameManager.Clear();
                isWin = false;
            }
        }
    }

    void Move()
    {
        transform.position += new Vector3(-1f, 0, 0);
        spriteVersion++;
        if (spriteVersion > 3)
            spriteVersion = 0;
        youSprite.sprite = sprites[spriteVersion];
    }
}
