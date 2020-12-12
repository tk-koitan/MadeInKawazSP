using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TadaGame3
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem burst_eff_;

        private float timer_;

        private bool destoryed_ = false;

        [SerializeField]
        private TouchRotateChecker checker_;

        // Start is called before the first frame update
        void Start()
        {
            timer_ = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            timer_ += Time.deltaTime;

            if (destoryed_) return;

            if(timer_ > 3.9f)
            {
                GameManager.Clear();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                burst_eff_.gameObject.SetActive(true);
                burst_eff_.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                destoryed_ = true;
                checker_.enabled = false;
            }
        }
    }
}