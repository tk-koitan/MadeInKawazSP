using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TadaGame4
{
    public class AnswerController : MonoBehaviour
    {
        [SerializeField]
        private float flash_interval_ = 0.5f;
        [SerializeField]
        private Color flash_color_ = Color.blue;

        [SerializeField]
        private SpriteRenderer filter_;

        [SerializeField]
        private float filter_fade_itnerval_ = 0.5f;

        [SerializeField]
        private List<SpriteRenderer> blocks_;

        private Queue<int> answer_indices_;

        private bool is_started_ = false;


        public void ReceiveAnswer(Queue<int> answer)
        {
            answer_indices_ = new Queue<int>(answer);
            is_started_ = true;
            filter_.DOFade(0.0f, filter_fade_itnerval_);
        }

        // Update is called once per frame
        void Update()
        {
            if (!is_started_) return;

            if (Input.GetMouseButtonDown(0))
            {
                int index = GetPushedButton(Input.mousePosition);
                Debug.Log(index);

                if (index == -1) return;

                int answer = answer_indices_.Peek();

                if(index == answer)
                {
                    // 次へ
                    answer_indices_.Dequeue();
                    // 光らせる
                    FlashBlock(blocks_[index]);

                    // 空になったらクリア
                    if(answer_indices_.Count == 0)
                    {
                        GameManager.Clear();
                        is_started_ = false;
                        // ボタンをひからせる
                        FlashBlockAll();
                    }
                }
                else
                {
                    // カメラを揺らす
                    Debug.Log("shake camera");

                    // フィルターをかける
                    filter_.DOFade(0.3f, filter_fade_itnerval_);
                }
            }
        }

        private int GetPushedButton(Vector3 pos)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos); //マウスのポジションを取得してRayに代入
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            // ブロックの範囲内にあるか確かめる
            for (int i = 0; i < blocks_.Count; ++i)
            {
                if(hit.collider.gameObject == blocks_[i].gameObject)
                {
                    return i;
                }
            }

            return -1;
        }

        private void FlashBlockAll()
        {
            StartCoroutine(FlashAll(flash_interval_));
        }

        private IEnumerator FlashAll(float interval)
        {
            yield return new WaitForSeconds(interval * 3f);

            while (true)
            {
                for(int i = 0; i < blocks_.Count; i += 2)
                {
                    FlashBlock(blocks_[i]);
                    FlashBlock(blocks_[(i + blocks_.Count - 1) % blocks_.Count]);

                    yield return new WaitForSeconds(interval * 1.1f);
                }
            }
        }

        private void FlashBlock(SpriteRenderer block)
        {
            // 20% フェードイン
            // 60% そのまま
            // 20% フェードアウト
            StartCoroutine(Flash(block, flash_interval_));
        }

        private IEnumerator Flash(SpriteRenderer block, float interval)
        {
            block.DOColor(flash_color_, interval * 0.2f);
            yield return new WaitForSeconds(interval * 0.8f);
            block.DOColor(Color.white, interval * 0.2f);
        }
    }
}