
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

namespace RPG.Character
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private GameObject splineGameObject;
        [SerializeField] private float walkDuration = 3f;
        [SerializeField] private float pauseDuration = 2f;

        private SplineContainer splineCmp;
        private float splinePosition = 0f;
        private float splineLength = 0f;
        private float walkedLength = 0f;
        private NavMeshAgent agentCmp;
        private float walkTime = 0f;
        private float pauseTime = 0f;
        private bool isWalking = true;

        public void Awake()
        {
            if (splineGameObject == null)
            {
                Debug.LogWarning($"{name} does not have a spline!");
            }

            splineCmp = splineGameObject.GetComponent<SplineContainer>();
            splineLength = splineCmp.CalculateLength();
            agentCmp = GetComponent<NavMeshAgent>();
        }

        public Vector3 GetNextPosition()
        {

            return splineCmp.EvaluatePosition(splinePosition);
        }

        public void CalcNextPosition()
        {
            walkTime += Time.deltaTime;
            if (walkTime > walkDuration)
            {
                isWalking = false;

                if (!isWalking)
                {
                    pauseTime += Time.deltaTime;

                    if (pauseTime < pauseDuration)
                    {
                        return;
                    }

                    ResetTimers();
                }
            }

            walkedLength += Time.deltaTime * agentCmp.speed;

            splinePosition = Mathf.Clamp01(walkedLength / splineLength);

            if (walkedLength > splineLength)
            {
                walkedLength = 0f;
            }
        }

        public void ResetTimers()
        {
            walkTime = 0f;
            pauseTime = 0f;
            isWalking = true;
        }

        public Vector3 GetFartherOutPosition()
        {
            float fartherSplinePosition = splinePosition + 0.02f;

            if (fartherSplinePosition >= 1)
            {
                fartherSplinePosition -= 1;
            }

            return splineCmp.EvaluatePosition(fartherSplinePosition);
        }
    }
}