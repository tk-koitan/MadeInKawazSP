using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TadaGame4
{
    public class QuizProvider : MonoBehaviour
    {
        [SerializeField]
        private float begin_wait_time_ = 1.0f;

        [SerializeField]
        private float interval_ = 0.5f;

        [SerializeField]
        private TeacherController teaher_;
        [SerializeField]
        private AnswerController answer_;

        [SerializeField]
        private int block_num_ = 4;
        [SerializeField]
        private int answer_length_ = 4;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(begin_wait_time_);

            // 答えの作成
            Queue<int> ans = new Queue<int>();

            int prev = Random.Range(0, block_num_);
            ans.Enqueue(prev);

            for(int i = 0; i < answer_length_ - 1; ++i)
            {
                int add = Random.Range(0, block_num_ - 1);
                if(add == prev)
                {
                    add = block_num_ - 1;
                }

                ans.Enqueue(add);
                prev = add;
            }

            // 答えの表示
            teaher_.PrintAnswer(ans, interval_);

            yield return new WaitForSeconds(interval_ * answer_length_);

            // 回答させる
            answer_.ReceiveAnswer(ans);
        }
    }
}