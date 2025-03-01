using UnityEngine;

namespace PlaygroundOfYore.Spawn {
    public class Params {
        public SpawnType spawnType;
        public float catapultAngle;
        public float catapultSpring;
        public float jumpPadAngle;
        public float jumpPadForce;
        public float boostZoneForce;
        public float liftSpeed;
        public float liftHeight;
        public float size;
        public float mass;
        public float drag;
        public Color color;
        public bool climbable;
        public bool isKinematic;
        public bool configurableJoint;

        public Color defaultColor { get; }= new Color(
            198f/255f, 120f/255f, 221f/255f
        );

        public Params() {
            Default();
        }

        public void Default() {
            catapultAngle = 45f;
            catapultSpring = 1000f;
            jumpPadAngle = 45f;
            jumpPadForce = 10f;
            boostZoneForce = 10f;
            liftSpeed = 1f;
            liftHeight = 10f;
            spawnType = SpawnType.Sphere;
            size = 1f;
            mass = 0.3f;
            drag = 0f;
            color = defaultColor;
            isKinematic = false;
            climbable = false;
            configurableJoint = false;
        }
    }
}
