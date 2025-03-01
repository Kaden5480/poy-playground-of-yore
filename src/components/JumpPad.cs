using UnityEngine;

namespace PlaygroundOfYore {
    public class JumpPad : MonoBehaviour {
        public float jumpForce = 10f;

        private void ApplyForce(Collider collider) {
            if (collider.CompareTag("Player") == false) {
                return;
            }

            collider.attachedRigidbody.AddForce(
                gameObject.transform.up * jumpForce, ForceMode.Impulse
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
