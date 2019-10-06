using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class BonusProp : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particleSystem;
        [SerializeField]
        private float speed = 1f;
        private Vector3 direction = Vector3.right;

        private void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                particleSystem.Play();
                AudioManager.Instance?.PlayFxBonus();
                ScoreManager.Instance.SetBonus(true);
            }
            else //Is a wall, because is the only other object that can collide according the layers
            {
                //flip the velocity
                direction = -direction;
            }
        }
    }
}
