using System.Collections;
using IM.Platforms;
using UnityEngine;

namespace IM.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private PlatformGenerator platformGenerator;
        [SerializeField] private float timeLerp = 1f;

        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            platformGenerator.OnPlatformSpawned += MoveCameraTo;
        }

        private void OnDisable()
        {
            platformGenerator.OnPlatformSpawned -= MoveCameraTo;
        }

        private void MoveCameraTo(Vector3 position)
        {
            StartCoroutine(LerpCamera(position));
        }

        private IEnumerator LerpCamera(Vector3 position)
        {
            var time = 0f;

            var startPos = transform.position;
            var targetPos = position + _startPosition;
            
            while (time < timeLerp)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, time / timeLerp);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;
        }
    }
}