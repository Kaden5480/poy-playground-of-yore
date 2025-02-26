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
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            CommonAwake();
        }

        public void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            CommonSceneLoad();
        }

        private void OnSceneUnloaded(Scene scene) {
            CommonSceneUnload();
        }

        public void Update() {
            CommonUpdate();
        }

        private void OnGUI() {
            CommonGUI();
        }

#elif MELONLOADER
using MelonLoader;

[assembly: MelonInfo(typeof(PlaygroundOfYore.Plugin), "PlaygroundOfYore", PluginInfo.PLUGIN_VERSION, "Kaden5480")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

namespace PlaygroundOfYore {
    public class Plugin: MelonMod {
        public override void OnInitializeMelon() {
            CommonAwake();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName) {
            CommonSceneLoad();
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName) {
            CommonSceneUnload();
        }

        public override void OnUpdate() {
            CommonUpdate();
        }

        public override void OnGUI() {
            CommonGUI();
        }

#endif

        private PlaygroundOfYore.Config.Cfg config = new PlaygroundOfYore.Config.Cfg();
        private UI ui;

        private void CommonAwake() {
            ui = new UI(config);
        }

        private void CommonSceneLoad() {
            ui.OnSceneLoaded();
        }

        private void CommonSceneUnload() {
            ui.OnSceneUnloaded();
        }

        private void CommonUpdate() {
            ui.Update();
        }

        private void CommonGUI() {
            ui.Render();
        }
    }
}
