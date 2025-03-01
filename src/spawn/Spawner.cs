using System;

using UnityEngine;

namespace PlaygroundOfYore.Spawn {
    public class Spawner {
        private Transform mainCamera = null;

        private Material MakeMaterial() {
            return new Material(Shader.Find("Standard"));
        }

        private void SpawnSphere(Params parameters) {
            Vector3 start = mainCamera.position;
            Vector3 direction = mainCamera.forward;

            Vector3 spawnPoint = start + ((2 * parameters.size + 5) * direction);

            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

            renderer.material = MakeMaterial();
            renderer.material.color = parameters.color;

            obj.transform.position = spawnPoint;
            obj.transform.localScale = 2 * parameters.size * Vector3.one;

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.mass = parameters.mass;
            rb.drag = parameters.drag;

            if (parameters.climbable == true) {
                obj.tag = "ClimbableRigidbody";
            }
        }

        public void Spawn(PrimitiveType type, Params parameters) {
            if (mainCamera == null) {
                Console.WriteLine("Main camera is null, unable to spawn");
            }

            switch (type) {
                case PrimitiveType.Sphere:
                    SpawnSphere(parameters);
                    break;
                default:
                    Console.WriteLine($"Unsupported type: {type}");
                    break;
            }
        }

        public void OnSceneLoaded() {
            GameObject mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
            mainCamera = mainCameraObj.transform;
        }

        public void OnSceneUnloaded() {
            mainCamera = null;
        }
    }
}
