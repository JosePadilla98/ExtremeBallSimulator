using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [System.Serializable]
    public struct EndRoundParticlesInfo
    {
        public ParticleSystem particlesPrefab;
        public Color greenParticlesColor;
        public Color orangeParticlesColor;
        public Color purpleParticlesColor;
    }

    public class ParticlesPool : MonoBehaviour
    {
        private ParticleSystem[] pool;
        private int index;
        private EndRoundParticlesInfo particlesInfo;

        private void Awake()
        {
            particlesInfo = GameManager.Instance.particlesPoolInfo;
        }

        public void Init(ScoreProp[] scorePropsInLevel)
        {
            //Count each type of scoreProp in the level
            int greenTypes = 0;
            int orangeTypes = 0;
            int purpleType = 0;

            foreach (var scoreProp in scorePropsInLevel)
            {
                switch (scoreProp.propType)
                {
                    case ScorePropType.GREEN:
                        greenTypes++;
                        break;
                    case ScorePropType.ORANGE:
                        orangeTypes++;
                        break;
                    case ScorePropType.PURPLE:
                        purpleType++;
                        break;
                }
            }

            //take the largest number among the 3 types previously counted
            int biggestNumber = greenTypes;
            if (biggestNumber < orangeTypes)
                biggestNumber = orangeTypes;
            if (biggestNumber < purpleType)
                biggestNumber = purpleType;

            //Create the pool
            pool = new ParticleSystem[biggestNumber];
            for (int i = 0; i < biggestNumber; i++)
            {
                pool[i] = Instantiate<ParticleSystem>(particlesInfo.particlesPrefab, transform);
                pool[i].gameObject.SetActive(false);
            }
        }

        public ParticleSystem GetParticle(ScorePropType propType)
        {
            index++;
            if (index >= pool.Length)
                index = 0;

            ParticleSystem particles = pool[index];
            var mainModule = particles.main;
            switch (propType)
            {
                case ScorePropType.GREEN:
                    mainModule.startColor = particlesInfo.greenParticlesColor;
                    break;
                case ScorePropType.ORANGE:
                    mainModule.startColor = particlesInfo.orangeParticlesColor;
                    break;
                case ScorePropType.PURPLE:
                    mainModule.startColor = particlesInfo.purpleParticlesColor;
                    break;
            }

            return particles;
        }

        public void DeactivateParticles()
        {
            foreach (var particle in pool)
            {
                particle.gameObject.SetActive(false);
            }
        }
    }

}
