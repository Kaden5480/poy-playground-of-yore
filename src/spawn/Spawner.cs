using System;

using UnityEngine;

using PlaygroundOfYore.Components;

namespace PlaygroundOfYore.Spawn {
    public class Spawner {
        private Transform mainCamera = null;

        private Material MakeMaterial() {
            return new Material(Shader.Find("Standard"));
        }

        private void ApplyCommon(GameObject obj, Params parameters) {
            Vector3 start = mainCamera.position;
            Vector3 direction = mainCamera.forward;

            Vector3 spawnPoint = start + ((parameters.size + 3) * direction);
            obj.transform.position = spawnPoint;

            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = MakeMaterial();
            renderer.material.color = parameters.color;

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.mass = parameters.mass;
            rb.drag = parameters.drag;
            rb.isKinematic = parameters.isKinematic;

            if (parameters.climbable == true) {
                obj.tag = "ClimbableRigidbody";
            }

            if (parameters.configurableJoint == true) {
                ConfigurableJoint joint = obj.AddComponent<ConfigurableJoint>();
                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;
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

        private void SpawnCatapult(Params parameters) {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.localScale = new Vector3(0.2f, parameters.size, 0.2f);
            obj.transform.localRotation = Quaternion.Euler(0f, 0f, parameters.catapultAngle);

            parameters.configurableJoint = true;
            parameters.isKinematic = true;

            ApplyCommon(obj, parameters);

            ConfigurableJoint joint = obj.GetComponent<ConfigurableJoint>();
            joint.angularXMotion = ConfigurableJointMotion.Locked;
            joint.angularYMotion = ConfigurableJointMotion.Locked;
            joint.anchor = new Vector3(0f, -0.5f, 0f);
            joint.targetRotation = Quaternion.Euler(0f, 0f, parameters.catapultAngle);

            JointDrive drive = joint.angularYZDrive;
            drive.positionSpring = parameters.catapultSpring;
            joint.angularYZDrive = drive;

            obj.AddComponent<Catapult>();
        }

        private void SpawnJumpPad(Params parameters) {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.localScale = new Vector3(parameters.size, 0.1f, parameters.size);
            obj.transform.localRotation = Quaternion.Euler(0f, 0f, parameters.jumpPadAngle);

            parameters.isKinematic = true;

            ApplyCommon(obj, parameters);

            BoxCollider collider = obj.GetComponent<BoxCollider>();
            collider.isTrigger = true;

            JumpPad jumpPad = obj.AddComponent<JumpPad>();
            jumpPad.jumpForce = parameters.jumpPadForce;
        }

        private void SpawnBoostZone(Params parameters) {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.layer = LayerMask.NameToLayer("PeakBoundary");
            obj.transform.localScale = parameters.size * Vector3.one;

            parameters.isKinematic = true;

            ApplyCommon(obj, parameters);

            BoxCollider collider = obj.GetComponent<BoxCollider>();
            collider.isTrigger = true;

            BoostZone boostZone = obj.AddComponent<BoostZone>();
            boostZone.boostForce = parameters.boostZoneForce;

        }

        private void SpawnSwing(Params parameters) {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.localScale = new Vector3(0.2f, parameters.size, 0.2f);
            ApplyCommon(obj, parameters);

            ConfigurableJoint joint = obj.GetComponent<ConfigurableJoint>();
            joint.angularXMotion = ConfigurableJointMotion.Locked;
            joint.angularYMotion = ConfigurableJointMotion.Locked;
            joint.anchor = new Vector3(0f, 0.5f, 0f);
        }

        public void Spawn(Params parameters) {
            if (mainCamera == null) {
                Console.WriteLine("Main camera is null, unable to spawn");
            }

            switch (parameters.spawnType) {
                case SpawnType.Cube:
                    SpawnCube(parameters);
                    break;
                case SpawnType.Sphere:
                    SpawnSphere(parameters);
                    break;
                case SpawnType.Catapult:
                    SpawnCatapult(parameters);
                    break;
                case SpawnType.Swing:
                    SpawnSwing(parameters);
                    break;
                case SpawnType.JumpPad:
                    SpawnJumpPad(parameters);
                    break;
                case SpawnType.BoostZone:
                    SpawnBoostZone(parameters);
                    break;
                default:
                    Console.WriteLine($"Unsupported type: {parameters.spawnType}");
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
