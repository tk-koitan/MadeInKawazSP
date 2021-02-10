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

        [SerializeField]
        [Range(0.0f, 180.0f)]
        private float max_angle_ = 90.0f;

        private float rot_z_;

        // Start is called before the first frame update
        void Start()
        {
            rotate_checker_.Init(center_position_);
            rot_z_ = transform.localEulerAngles.z;
        }

        // Update is called once per frame
        void Update()
        {
            float add = rotate_checker_.GetRotateLength();
            rot_z_ = Mathf.Clamp(rot_z_ + add, -max_angle_, max_angle_);
            transform.localEulerAngles = new Vector3(0f, 0f, rot_z_);
        }
    }
}