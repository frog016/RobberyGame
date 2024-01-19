using System;
using System.Collections;
using UnityEngine;

namespace Animation
{
    public class DoorAnimator : MonoBehaviour
    {
        [SerializeField] private float _animationDuration = 1f;
        [SerializeField] private float _angleAnimationRotation = 90f;

        private Vector3 _initialRotationEuler;
        private Coroutine _animationCoroutine;

        private void Awake()
        {
            _initialRotationEuler = transform.rotation.eulerAngles;
        }

        public void AnimateOpen(int direction, float delay, Action endedCallback = null)
        {
            if (_animationCoroutine != null)
                StopCoroutine(_animationCoroutine);

            var endRotation = Quaternion.Euler(0, 0, _initialRotationEuler.z + direction * _angleAnimationRotation);
            _animationCoroutine = StartCoroutine(AnimateCoroutine(endRotation, delay, endedCallback));
        }

        public void AnimateClose(float delay, Action endedCallback = null)
        {
            if (_animationCoroutine != null)
                StopCoroutine(_animationCoroutine);

            var endRotation = Quaternion.Euler(_initialRotationEuler);
            _animationCoroutine = StartCoroutine(AnimateCoroutine(endRotation, delay, endedCallback));
        }


        private IEnumerator AnimateCoroutine(Quaternion endRotation, float delay, Action endedCallback = null)
        {
            yield return new WaitForSeconds(delay);

            var startRotation = transform.rotation;

            var time = 0f;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;

                time += Time.deltaTime / _animationDuration;
            }

            endedCallback?.Invoke();
        }
    }
}
