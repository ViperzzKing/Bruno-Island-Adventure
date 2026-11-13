using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

namespace RPG.Character
{
    public class Patrol : MonoBehaviour
    {

        [SerializeField] private GameObject splineGameObject;
        private SplineContainer splineCMP;
        private NavMeshAgent agemtCMP;

        [SerializeField] private float walkDuration = 3f;
        [SerializeField] private float pauseDuration = 2f;

        private float splinePosition;
        private float splineLength;
        private float lengthWalked;
        private float walkTime;
        private float pauseTime;
        private bool isWalking = true;

        private void Awake()
        {
            if (splineGameObject == null)
            {
                Debug.LogWarning($"{name} does not have a spline");
            }

            splineCMP = splineGameObject.GetComponent<SplineContainer>();
            splineLength = splineCMP.CalculateLength();
            agemtCMP = GetComponent<NavMeshAgent>();

        }

        public Vector3 GetNextPosition()
        {
            return splineCMP.EvaluatePosition(splinePosition);
        }

        public void CalculateNextPositon()
        {
            walkTime += Time.deltaTime;
            if (walkTime >= walkDuration)
            {
                isWalking = false;
            }
            if (!isWalking)
            {
                pauseTime += Time.deltaTime;

                if (pauseTime < pauseDuration) return;

                ResetTimers();
            }


            lengthWalked += agemtCMP.speed * Time.deltaTime;

            if (lengthWalked > splineLength)
            {
                lengthWalked = 0f;
            }

            splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
        }

        public void ResetTimers()
        {
            pauseTime = 0;
            walkTime = 0;
            isWalking = true;
        }
    }
}
