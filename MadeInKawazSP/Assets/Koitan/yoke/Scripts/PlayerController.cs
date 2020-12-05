﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yoke
{
    public class PlayerController : MonoBehaviour
    {
        // マウスの一フレーム前の座標
        Vector3 oldPos;
        // 画面幅，高さ
        [SerializeField]
        private float width, height;

        [SerializeField]
        private GameObject gestureHand;

        // クリアフラグ
        private float timer;

        //by koitan
        [SerializeField]
        private GameObject burstObj;

        // Start is called before the first frame update
        void Start()
        {
            timer = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if(timer >= 7.7f)//長い設定
            {
                GameManager.Clear();
            }

            Vector3 touchScreenPosition = Input.mousePosition;

            // 10.0fに深い意味は無い。画面に表示したいので適当な値を入れてカメラから離そうとしているだけ.
            touchScreenPosition.z = 10.0f;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(touchScreenPosition);
            //mousePos.z = 0;            

            if (Input.GetMouseButtonDown(0))
            {
                oldPos = mousePos;
                gestureHand.SetActive(false);
            }

            if (Input.GetMouseButton(0))
            {
                transform.position += mousePos - oldPos;
                //transform.Translate(mousePos - oldPos);

                //画面外に出ないように
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -width, width), Mathf.Clamp(transform.position.y, -height, height));
                oldPos = mousePos;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Instantiate(burstObj, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}


