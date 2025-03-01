using UnityEngine;

namespace PlaygroundOfYore {
    public class BoostZone : MonoBehaviour {
        public float boostForce = 10f;

        private void ApplyForce(Collider collider) {
            if (collider.CompareTag("Player") == false) {
                return;
            }

            collider.attachedRigidbody.AddForce(
                gameObject.transform.forward * boostForce, ForceMode.Impulse
            );
        }

        public void OnTriggerEnter(Collider other) {
            ApplyForce(other);
        }

        public void OnTriggerStay(Collider other) {
            ApplyForce(other);
        }
    }
}
