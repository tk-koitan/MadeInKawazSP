using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TadaGame3
{
    public class MeteoriteController : MonoBehaviour
    {
        private float speed_;
        private Vector3 dir_;

        private bool inited_ = false;
        private bool destoryed_ = false;

        [SerializeField]
        private ParticleSystem burst_eff_;

        [SerializeField]
        private ParticleSystem drop_eff_;

        public void Init(Vector3 spawner_pos, float speed, Vector3 dir)
        {
            transform.position = spawner_pos;
            transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f);

            speed_ = speed;
            dir_ = dir;
            inited_ = true;
        }

        private void Update()
        {
            if (!inited_) return;
            if (destoryed_) return;

            transform.position += dir_ * Time.deltaTime * speed_;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "StageObject")
            {
                destoryed_ = true;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;

                burst_eff_.gameObject.SetActive(true);
                drop_eff_.gameObject.SetActive(false);
            }
        }
    }
}