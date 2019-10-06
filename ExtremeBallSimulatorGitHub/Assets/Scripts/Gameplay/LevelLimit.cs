using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class LevelLimit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            LevelController.Instance.BallLost();
        }
    }
}
