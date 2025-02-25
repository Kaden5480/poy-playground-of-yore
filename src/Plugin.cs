using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

#if BEPINEX
using BepInEx;

namespace PlaygroundOfYore {
    [BepInPlugin("com.github.Kaden5480.poy-playground-of-yore", "PlaygroundOfYore", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        public void Awake() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Update() {
            CommonUpdate();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            CommonSceneLoad();
        }

#elif MELONLOADER
using MelonLoader;

[assembly: MelonInfo(typeof(PlaygroundOfYore.Plugin), "PlaygroundOfYore", PluginInfo.PLUGIN_VERSION, "Kaden5480")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

namespace PlaygroundOfYore {
    public class Plugin: MelonMod {
        public override void OnSceneWasLoaded(int buildIndex, string sceneName) {
            CommonSceneLoad();
        }

        public override void OnUpdate() {
            CommonUpdate();
        }

#endif

        private Transform mainCameraTransform;

        private Material MakeMaterial() {
            return new Material(Shader.Find("Standard"));
        }

        private void SpawnBall() {
            if (mainCameraTransform == null) {
                return;
            }

            const float radius = 1.5f;

            Vector3 start = mainCameraTransform.position;
            Vector3 direction = mainCameraTransform.forward;

            Vector3 spawnPoint = start + ((2*radius + 5) * direction);

            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

            renderer.material = MakeMaterial();
            renderer.material.color = new Color(
                198f/255f, 120f/255f, 221f/255f
            );

            obj.transform.position = spawnPoint;
            obj.transform.localScale = 2 * radius * Vector3.one;

            obj.AddComponent<Rigidbody>();
        }

        private void CommonSceneLoad() {
            GameObject mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
            mainCameraTransform = mainCameraObj.transform;
        }

        private void CommonUpdate() {
            if (Input.GetKeyDown(KeyCode.PageDown) == true) {
                SpawnBall();
            }
        }
    }
}
