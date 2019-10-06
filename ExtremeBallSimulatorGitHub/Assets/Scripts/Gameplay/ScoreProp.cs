using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Managers;

namespace Game.Gameplay
{
    public enum ScorePropType { GREEN, ORANGE, PURPLE}

    public class ScoreProp : MonoBehaviour
    {
        [SerializeField]
        private Material materialOff;
        [SerializeField]
        private Material materialOn;
        [SerializeField]
        private MeshRenderer meshRenderer;
        [SerializeField]
        private int scoreValue;

        [HideInInspector]
        public bool illuminated;
        public ScorePropType propType;
        public Transform particleEmisionPosition;

        private void OnCollisionEnter(Collision collision)
        {
            illuminated = true;
            meshRenderer.material = materialOn;

            AudioManager.Instance?.PlayPropCollision();
            ScoreManager.Instance?.AddScore(scoreValue);
        }
    }
}
