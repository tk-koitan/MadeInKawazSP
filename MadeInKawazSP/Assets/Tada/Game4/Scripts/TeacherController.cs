using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TadaGame4
{
    public class TeacherController : MonoBehaviour
    {
        [SerializeField]
        private List<SpriteRenderer> blocks_;

        private Queue<int> answer_indices_;

        private TadaLib.Timer timer_;

        private bool is_started_ = false;
        private float interval_;

        public void PrintAnswer(Queue<int> answer, float interval)
        {
            answer_indices_ = new Queue<int>(answer);
            is_started_ = true;
            interval_ = interval;
            timer_ = new TadaLib.Timer(interval);
        }

        // Update is called once per frame
        void Update()
        {
            if (!is_started_) return;

            if (timer_.IsTimeout())
            {
                int index = answer_indices_.Dequeue();
                FlashBlock(blocks_[index]);
                timer_.TimeReset();

                // もう終わった
                if(answer_indices_.Count == 0)
                {
                    is_started_ = false;
                    return;
                }
            }
        }

        private void FlashBlock(SpriteRenderer block)
        {
            // 20% フェードイン
            // 60% そのまま
            // 20% フェードアウト
            StartCoroutine(Flash(block, interval_));
        }

        private IEnumerator Flash(SpriteRenderer block, float interval)
        {
            block.DOFade(1.0f, interval * 0.2f);
            yield return new WaitForSeconds(interval * 0.8f);
            block.DOFade(0.0f, interval * 0.2f);
        }
    }
}