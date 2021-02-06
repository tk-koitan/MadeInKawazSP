using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面をタッチしながら回転しているかを判定する
/// </summary>

namespace TadaGame3
{
    public class TouchRotateChecker : MonoBehaviour
    {
        private Vector3 center_position_;
        private float rotate_power_;

        private float rot_;

        private Vector3 prev_pos_;

        private bool prev_touched_;

        [SerializeField]
        private Animator gesture_;

        public void Init(Vector3 center_position, float rotate_power = 3.0f)
        {
            center_position_ = center_position;
            rotate_power_ = rotate_power;

            rot_ = 0.0f;

            prev_touched_ = false;
        }

        private void Update()
        {
            if (prev_touched_)
            {
                if (!Input.GetMouseButton(0)) prev_touched_ = false;
                else
                {
                    Vector3 cur_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // 中心から見た角度と距離を求める
                    Vector3 prev_dif = prev_pos_ - center_position_;
                    Vector3 cur_dif = cur_pos - center_position_;

                    float rotate_dif = (Mathf.Atan2(cur_dif.y, cur_dif.x) - Mathf.Atan2(prev_dif.y, prev_dif.x)) * Mathf.Rad2Deg;

                    // タッチが下側の場合のみ回転させる
                    if(cur_dif.y <= 0.0f)
                        rot_ += rotate_dif * rotate_power_;

                    prev_pos_ = cur_pos;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    if (gesture_.gameObject.activeSelf) gesture_.gameObject.SetActive(false);
                    prev_touched_ = true;
                    prev_pos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }

        // 前回からどれくらい開店したか
        public float GetRotateLength()
        {
            float tmp = rot_;
            rot_ = 0.0f;
            return tmp;
        }
    }
}