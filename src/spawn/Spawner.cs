using System;

using UnityEngine;

namespace PlaygroundOfYore.Spawn {
    public class Spawner {
        private Transform mainCamera = null;

        private Material MakeMaterial() {
            return new Material(Shader.Find("Standard"));
        }

        private void ApplyCommon(GameObject obj, Params parameters) {
            Vector3 start = mainCamera.position;
            Vector3 direction = mainCamera.forward;

            Vector3 spawnPoint = start + ((2 * parameters.size + 5) * direction);
            obj.transform.position = spawnPoint;

            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = MakeMaterial();
            renderer.material.color = parameters.color;

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.mass = parameters.mass;
            rb.drag = parameters.drag;

            if (parameters.climbable == true) {
                obj.tag = "ClimbableRigidbody";
            }
        }

        private void SpawnCube(Params parameters) {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.localScale = parameters.size * Vector3.one;
            ApplyCommon(obj, parameters);
        }

        private void SpawnSphere(Params parameters) {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.localScale = 2 * parameters.size * Vector3.one;
            ApplyCommon(obj, parameters);
        }

        public void Spawn(Params parameters) {
            if (mainCamera == null) {
                Console.WriteLine("Main camera is null, unable to spawn");
            }

            switch (parameters.primitiveType) {
                case PrimitiveType.Cube:
                    SpawnCube(parameters);
                    break;
                case PrimitiveType.Sphere:
                    SpawnSphere(parameters);
                    break;
                default:
                    Console.WriteLine($"Unsupported type: {parameters.primitiveType}");
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
