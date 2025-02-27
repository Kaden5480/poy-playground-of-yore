using System;

using UnityEngine;

using PlaygroundOfYore.Config;
using PlaygroundOfYore.Spawn;

namespace PlaygroundOfYore {
    public class UI {
        private Cfg config;
        public Spawner spawner { get; } = new Spawner();
        private Params spawnParams = new Params();

        private InGameMenu inGameMenu = null;

        private const float width = 300;
        private const float height = 300;
        private const float padding = 20;
        private const float buttonWidth = 100;

        private Vector2 scrollPosition = Vector2.zero;

        public UI(Cfg config) {
            this.config = config;
        }

        public void OnSceneLoaded() {
            spawner.OnSceneLoaded();
            inGameMenu = GameObject.FindObjectOfType<InGameMenu>();
        }

        public void OnSceneUnloaded() {
            spawner.OnSceneUnloaded();
            inGameMenu = null;
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.PageDown)) {
                spawner.Spawn(PrimitiveType.Sphere, spawnParams);
            }
        }

        public void Render() {
            if (inGameMenu == null
                || inGameMenu.isMainMenu == true
                || InGameMenu.isCurrentlyNavigationMenu == false
            ) {
                return;
            }

            GUILayout.BeginArea(new Rect(10, 10, width, height), GUI.skin.box);

            scrollPosition = GUILayout.BeginScrollView(
                scrollPosition,
                GUILayout.Width(width - padding), GUILayout.Height(height - padding - 15)
            );

            spawnParams.climbable = GUILayout.Toggle(
                spawnParams.climbable, "Climbable"
            );

            GUILayout.Label($"Size: {spawnParams.size}");
            spawnParams.size = (float) Math.Round(GUILayout.HorizontalSlider(
                spawnParams.size, 0.1f, 5f
            ), 2);

            GUILayout.Label($"Mass: {spawnParams.mass}");
            spawnParams.mass = (float) Math.Round(GUILayout.HorizontalSlider(
                spawnParams.mass, 0f, 5f
            ), 2);

            GUILayout.Label("== Color ==");
            GUILayout.Label($"Red: ({255 * spawnParams.color.r})");
            int red = (int) GUILayout.HorizontalSlider(
                255f * spawnParams.color.r, 0f, 255f
            );

            GUILayout.Label($"Green: ({255 * spawnParams.color.g})");
            int green = (int) GUILayout.HorizontalSlider(
                255f * spawnParams.color.g, 0f, 255f
            );

            GUILayout.Label($"Blue: ({255 * spawnParams.color.b})");
            int blue = (int) GUILayout.HorizontalSlider(
                255f * spawnParams.color.b, 0f, 255f
            );

            spawnParams.color = new Color(
                (float) red/255f, (float) green/255f, (float) blue/255f
            );

            if (GUILayout.Button("Reset Color")) {
                spawnParams.color = spawnParams.defaultColor;
            }

            GUILayout.EndScrollView();

            GUILayout.EndArea();
        }
    }
}
