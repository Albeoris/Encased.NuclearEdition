using System;
using System.IO;
using Encased.NuclearEdition.Patches;
using Harmony;
using UnityEngine;

namespace Encased.NuclearEdition
{
    // This is the entry point.
    // It should be named ModEntryPoint.
    // It should be inherited from MonoBehaviour.
    // This component will be instantiated while loading mods. You control the lifetime of the object.
    public class ModEntryPoint : MonoBehaviour
    {
        private Single _recheckTimeSec = 0;

        void Awake()
        {
            try
            {
                HarmonyInstance harmony = HarmonyInstance.Create("com.Albeoris.Encased.NuclearEdition.Patches");

                PatchGameModeProcessor.Patch(harmony);
                PatchAutoInputProcessor.Patch(harmony);
                PatchOpenContainerAbilityHandler.Patch(harmony);
                PatchSearchAbilityHandler.Patch(harmony);
                PatchWindow.Patch(harmony);

                Debug.Log("[Encased.NuclearEdition] Successfully patched via Harmony.");
            }
            catch (Exception ex)
            {
                Debug.Log($"[Encased.NuclearEdition] Failed to patch via Harmony. Error: {ex}");
            }
        }

        void Update()
        {
            try
            {
                // Exit to the main menu unloads game scenes and interface objects. We must ensure that our mod remains active.
                // Recheck it every 10 seconds
                _recheckTimeSec += Time.deltaTime;
                if (_recheckTimeSec < 10.0 /*sec*/)
                    return;

                _recheckTimeSec = 0;

                // Don't use Awake()
                // Now the initialization of mods occurs before the initialization of the environment of the game.
                // We have to wait for it to be initialized.

                // Initialize our mod

                // We can destroy this component after initialization but this mod will be unloaded when the player exit to the main menu. Leave it active in the background.
                // Destroy(this.gameObject);
            }
            catch (Exception ex)
            {
                Debug.LogError("[Encased.NuclearEdition] Something went wrong. Modification will be disabled. Error: " + ex);
                Destroy(this.gameObject);
            }
        }
    }
}