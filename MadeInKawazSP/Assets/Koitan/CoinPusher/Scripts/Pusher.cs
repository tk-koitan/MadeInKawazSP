using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CoinPusher
{
    public class Pusher : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField]
        Vector3 offset;
        [SerializeField]
        float moveInterval;
        private Vector3 originPos;
        private float t = 0;
        [SerializeField]
        GameObject coin;
        [SerializeField]
        int coinNum;
        [SerializeField]
        TextMeshProUGUI textMesh;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            originPos = transform.position;
            t = 0;
        }

        // Update is called once per frame
        void Update()
        {
            t += Time.deltaTime;
            //rb.MovePosition(originPos + Mathf.Sin(2 * Mathf.PI * t / moveInterval) * offset);
            rb.velocity = originPos + Mathf.Sin(2 * Mathf.PI * t / moveInterval) * offset - transform.position;
            if (Input.GetMouseButtonDown(0) && coinNum > 0)
            {
                coinNum--;
                Vector3 tmpPos = Input.mousePosition;
                tmpPos.y = Screen.height / 5 * 3;
                tmpPos.z = 10f;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(tmpPos);
                Instantiate(coin, mousePos, Quaternion.Euler(90, 0, 0));
            }
            textMesh.text = "のこり<size=150%><color=red>" + coinNum.ToString() + "</color></size>枚";
        }
    }
}