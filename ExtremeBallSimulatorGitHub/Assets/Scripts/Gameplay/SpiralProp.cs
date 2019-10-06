using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class SpiralProp : MonoBehaviour
    {
        [SerializeField]
        private float timeToTurn = 3f;

        void Update()
        {
            transform.Rotate(Vector3.back, Time.deltaTime * 360f / timeToTurn);
        }
    }
}
