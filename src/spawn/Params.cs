using UnityEngine;

namespace PlaygroundOfYore.Spawn {
    public class Params {
        public float size;
        public float mass;
        public Color color;
        public bool climbable;

        public const float defaultSize = 1f;
        public const float defaultMass = 0.3f;
        public Color defaultColor { get; }= new Color(
            198f/255f, 120f/255f, 221f/255f
        );

        public Params() {
            Default();
        }

        public void Default() {
            size = defaultSize;
            mass = defaultMass;
            color = defaultColor;
            climbable = false;
        }
    }
}
