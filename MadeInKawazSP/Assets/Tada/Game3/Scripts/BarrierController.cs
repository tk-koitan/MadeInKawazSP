using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TadaGame3
{
    public class BarrierController : MonoBehaviour
    {
        [SerializeField]
        private TouchRotateChecker rotate_checker_;

        [SerializeField]
        private Vector3 center_position_ = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            rotate_checker_.Init(center_position_);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 add = new Vector3(0f, 0f, rotate_checker_.GetRotateLength());
            transform.localEulerAngles += add;
        }
    }
}