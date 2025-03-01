using UnityEngine;

namespace PlaygroundOfYore.Spawn {
    public class Params {
        public PrimitiveType primitiveType;
        public float size;
        public float mass;
        public float drag;
        public Color color;
        public bool climbable;

        public const PrimitiveType defaultPrimitiveType = PrimitiveType.Sphere;
        public const float defaultDrag = 0f;
        public const float defaultSize = 1f;
        public const float defaultMass = 0.3f;
        public Color defaultColor { get; }= new Color(
            198f/255f, 120f/255f, 221f/255f
        );

        public Params() {
            Default();
        }

        public void Default() {
            primitiveType = defaultPrimitiveType;
            size = defaultSize;
            mass = defaultMass;
            drag = defaultDrag;
            color = defaultColor;
            climbable = false;
        }
    }
}
