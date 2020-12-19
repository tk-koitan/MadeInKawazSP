using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPusher
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameManager.Clear();
        }
    }
}
