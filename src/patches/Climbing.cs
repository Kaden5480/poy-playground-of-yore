using HarmonyLib;
using UnityEngine;

namespace PlaygroundOfYore.Patches {
    [HarmonyPatch(typeof(Climbing), "Update")]
    static class Catapult {
        private static void Check(Transform transform) {
            if (transform == null) {
                return;
            }

            Components.Catapult catapult = transform.gameObject
                .GetComponent<Components.Catapult>();
            if (catapult == null) {
                return;
            }

            catapult.Grab();
        }

        static void Postfix(Climbing __instance) {
            Check(__instance.grabbedObjectL);
            Check(__instance.grabbedObjectR);
        }
    }
}
