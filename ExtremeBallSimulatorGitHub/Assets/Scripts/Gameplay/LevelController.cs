using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay
{
    public class LevelController : BaseSingleton<LevelController>
    {
        [SerializeField]
        private BallLauncher ballLauncher;
        [SerializeField]
        private int initialBalls = 10;

        private bool canLaunchBall = true;
        private int _remainingBallsNum;
        public int RemainingBallsNum { get { return _remainingBallsNum; } set { _remainingBallsNum = value; BallsNumberChangeEvent?.Invoke(); } }
        public UnityAction BallsNumberChangeEvent;

        private ParticlesPool particlesPool;
        private ScoreProp[] scoreProps;
        private int scorePropsDeactivated;

        protected override void Awake()
        {
            base.Awake();
            GameObject obj = new GameObject("EndRoundParticles");
            obj.transform.SetParent(this.transform);
            particlesPool = obj.AddComponent<ParticlesPool>();
        }

        public void Init()
        {
            scoreProps = FindObjectsOfType<ScoreProp>();
            particlesPool.Init(scoreProps);
            RemainingBallsNum = initialBalls;
            scorePropsDeactivated = 0;
        }

        public void TapInput()
        {
            if (!canLaunchBall)
                return;

            ballLauncher.LaunchBall();
            canLaunchBall = false;
            RemainingBallsNum--;
        }

        public void BallLost()
        {
            StartCoroutine(BallLostCor());
        }

        private IEnumerator BallLostCor()
        {
            ballLauncher.RestBall();
            yield return EndRound();

            if (scorePropsDeactivated == scoreProps.Length)
            {
                yield return ScoreManager.Instance.MultiplyRemainingBalls(RemainingBallsNum);
                yield return new WaitForSeconds(GameManager.GameFlowTimes.timeToLevelCompletePopUp);
                GameManager.Instance.CompleteLevel();
            }
            else if (RemainingBallsNum <= 0)
            {
                yield return new WaitForSeconds(GameManager.GameFlowTimes.timeToGameOverPopUp);
                GameManager.Instance.GameOver();
            }
            else
                InitRound();
        }

        private void InitRound()
        {
            //Deactivate gameObjects to raise the performance
            particlesPool.DeactivateParticles();
            canLaunchBall = true;
        }

        #region END ROUND METHODS

        private IEnumerator EndRound()
        {
            ///DEACTIVATE SCORE PROPS ILLUMINATED
            //First only the green ones disappears, later the oranges ones...
            yield return DeactivateScorePropsByType(ScorePropType.GREEN);
            yield return DeactivateScorePropsByType(ScorePropType.ORANGE);
            yield return DeactivateScorePropsByType(ScorePropType.PURPLE, true);

            AudioManager.Instance.ResetPropCollisionPitch();

            yield return ScoreManager.Instance.CountRoundScore();
        }

        private IEnumerator DeactivateScorePropsByType(ScorePropType type, bool isTheLast = false)
        {
            bool anyDeactivated = false;

            foreach (var scoreProp in scoreProps)
            {
                if (scoreProp.illuminated)
                {
                    if (scoreProp.propType != type)
                        continue;

                    ParticleSystem particles = particlesPool.GetParticle(type);
                    particles.transform.position = scoreProp.particleEmisionPosition.position;
                    particles.gameObject.SetActive(true);
                    particles.Play();

                    scoreProp.gameObject.SetActive(false);
                    scoreProp.illuminated = false;
                    scorePropsDeactivated++;

                    if (anyDeactivated)
                        continue;

                    anyDeactivated = true;
                    AudioManager.Instance?.PlayPropFadeOut();
                }
            }

            float secondsToWait = GameManager.GameFlowTimes.secondsToDisappearEachTypeOfProp;
            if (!anyDeactivated || isTheLast)
                secondsToWait = 0f;

            yield return new WaitForSeconds(secondsToWait);
        }

        #endregion
    }
}

