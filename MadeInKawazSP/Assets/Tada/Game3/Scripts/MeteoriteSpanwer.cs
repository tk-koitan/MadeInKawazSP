using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TadaGame3
{
    public class MeteoriteSpanwer : MonoBehaviour
    {
        [SerializeField]
        private MeteoriteController meteo_;

        [SerializeField]
        private float meteo_interval_ = 0.5f;

        [SerializeField]
        private int meteo_num_ = 3;

        [SerializeField]
        private float distance_ = 10.0f;

        [SerializeField]
        private float meteo_speed_ = 10.0f;

        [SerializeField]
        private SpriteRenderer meteo_warning_;

        private IEnumerator Start()
        {
            // 隕石の作成
            List<MeteoriteController> meteos = new List<MeteoriteController>();

            for (int i = 0; i < meteo_num_; ++i)
            {
                MeteoriteController meteo = Instantiate(meteo_);
                meteo.gameObject.SetActive(false);
                meteos.Add(meteo);
            }

            yield return new WaitForSeconds(meteo_interval_ * 0.25f);

            foreach(var meteo in meteos)
            {
                // 角度を決める
                float angle = Random.Range(60f, 120f);
                float rand = Random.Range(0.0f, 1.0f);
                if (rand < 0.5f) angle += 180f;
                angle *= Mathf.Deg2Rad;

                Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);

                // 警告を出す
                meteo_warning_.transform.position = dir * -distance_ * 0.75f;
                meteo_warning_.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f);

                // 2回点滅
                float alpha = 0.0f;

                for(int i = 0; i < 4; ++i)
                {
                    alpha = 1f - alpha;
                    meteo_warning_.DOFade(alpha, meteo_interval_ * 0.24f);
                    yield return new WaitForSeconds(meteo_interval_ * 0.25f);
                }

                meteo.Init(dir * -distance_, meteo_speed_, dir);

                meteo.gameObject.SetActive(true);
            }
        }
    }
}