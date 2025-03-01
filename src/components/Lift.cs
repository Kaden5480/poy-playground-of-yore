using System.Collections;

using UnityEngine;

namespace PlaygroundOfYore {
    public class Lift : MonoBehaviour {
        private Vector3 startPosition;

        public float height = 10f;
        public float speed = 1f;

        public void Awake() {
            startPosition = gameObject.transform.position;
        }

        public void Start() {
            StartCoroutine(LiftCoroutine());
        }

        private Vector3 ClampVec3(Vector3 value, Vector3 min, Vector3 max) {
            float x = Mathf.Clamp(value.x, min.x, max.x);
            float y = Mathf.Clamp(value.y, min.y, max.y);
            float z = Mathf.Clamp(value.z, min.z, max.z);

            return new Vector3(x, y, z);
        }

        private Vector3 LerpVec3(Vector3 start, Vector3 end, float t) {
            float x = Mathf.Lerp(start.x, end.x, t);
            float y = Mathf.Lerp(start.y, end.y, t);
            float z = Mathf.Lerp(start.z, end.z, t);

            return new Vector3(x, y, z);
        }

        private IEnumerator LiftCoroutine() {
            for (;;) {
                float duration = height / speed;

                Vector3 start = startPosition;
                Vector3 end = startPosition + (height * gameObject.transform.up);

                float timer = 0f;
                while (timer < duration) {
                    gameObject.transform.position = LerpVec3(
                        start, end, timer / duration
                    );

                    timer += Time.deltaTime;
                    yield return null;
                }

                timer = 0f;
                while (timer < duration) {
                    gameObject.transform.position = LerpVec3(
                        end, start, timer / duration
                    );

                    timer += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}
