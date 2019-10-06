using UnityEngine;

namespace AIM.CameraSystem
{

    public class CameraIdle : MonoBehaviour
    {
        public Transform targetToMove;
        public float minDistance=0.05f;
        public float speedRate = 1f;
        public float distanceLimit = 1f; 

        private Vector3 targetPosition;
        private Vector3 currentPosition;
        private float distanceToTargetPosition;

        private bool animationActive = true;
        // Use this for initialization
        void Start()
        {
            Init();
        }

        void Init()
        {
            if (!targetToMove)
            {
                targetToMove = this.transform;
            }
            currentPosition = targetToMove.localPosition;
            animationActive = true;
        }

        Vector3 speed;
        public void UpdatePosition()
        {
            currentPosition = targetToMove.localPosition;
            distanceToTargetPosition = Vector3.Distance(currentPosition, targetPosition);
            if (distanceToTargetPosition < minDistance || distanceToTargetPosition > 1)
            {
                SetNewTargetPosition();
            }
            //targetToMove.localPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * speedRate);
            targetToMove.localPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref speed, Time.deltaTime*speedRate);
        }

        public void SetNewTargetPosition()
        {
            targetPosition = Random.insideUnitSphere * distanceLimit;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (animationActive)
            {
                UpdatePosition();
            }
        }
    }
}
