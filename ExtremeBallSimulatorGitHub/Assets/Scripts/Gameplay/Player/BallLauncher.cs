using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class BallLauncher : MonoBehaviour
    {
        [SerializeField]
        private GameObject arrowMesh;
        [SerializeField]
        private GameObject ball;
        [SerializeField]
        private Rigidbody ballRigidbody;
        [SerializeField]
        private float impulse = 2f;

        private Vector3 ballInitPosition;

        private void Start()
        {
            ballInitPosition = ball.transform.position;
        }

        private void Update()
        {
            #if !UNITY_ANDROID
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LevelController.Instance.TapInput();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                LevelController.Instance.BallLost();
            }
            #endif
        }

        public void RestBall()
        {
            ballRigidbody.isKinematic = true;
            ball.transform.position = ballInitPosition;
            ball.transform.localRotation = Quaternion.identity;
        }

        public void LaunchBall()
        {
            ballRigidbody.isKinematic = false;
            Vector3 force = -arrowMesh.transform.up * impulse;
            ballRigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
