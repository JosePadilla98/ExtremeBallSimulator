using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class ArrowOscilator : MonoBehaviour
    {
        [SerializeField]
        private float oscilationRange = 100f;
        [SerializeField]
        private float oscilationTime = 2f;
        [SerializeField]
        private bool useSinFunction = true;

        private float timeCounter;

        private void ResetRotation()
        {
            timeCounter = 0f;
        }

        private void Update()
        {
            float t;
            if(useSinFunction)
                t = (Mathf.Sin(timeCounter * Mathf.PI * 2f) + 1f) / 2f;
            else
                t = Mathf.PingPong(timeCounter * 2f, 1f);

            float zAxisDegrees = Mathf.Lerp(-oscilationRange, oscilationRange, t);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, zAxisDegrees);
            timeCounter += Time.deltaTime / oscilationTime;
        }
    }
}
