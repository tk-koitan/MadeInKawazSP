using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPusher
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        GameObject Eff;
        private void OnTriggerEnter(Collider other)
        {
            GameManager.Clear();
            GameObject obj =  Instantiate(Eff, transform.position, Quaternion.identity);
            //obj.transform.SetParent(transform);
        }
    }
}
