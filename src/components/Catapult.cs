using UnityEngine;

namespace PlaygroundOfYore.Components {
    public class Catapult : MonoBehaviour {
        public void Grab() {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
