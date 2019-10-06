using System;
using System.Reflection;
using DarkCrystal;
using DarkCrystal.Audio;
using DarkCrystal.Encased;
using DarkCrystal.Encased.Core;
using DarkCrystal.Encased.Core.GUI;
using DarkCrystal.Encased.Core.Input;
using DarkCrystal.Encased.Core.ModuleSystem;
using DarkCrystal.Encased.Core.RolePlay;
using DarkCrystal.Encased.Core.Visualizers;
using DarkCrystal.Encased.Game;
using Encased.NuclearEdition.Shared;
using Encased.NuclearEdition.Utils;
using Harmony;
using UnityEngine;
using Action = DarkCrystal.Encased.Core.Action;
using HarmonyPrefix = Encased.NuclearEdition.Utils.HarmonyPrefix;

namespace Encased.NuclearEdition.Patches
{
    public abstract class PatchAutoInputProcessor
    {
        private static readonly Type This = TypeCache<PatchAutoInputProcessor>.Type;
        private static readonly Type Native = typeof(AutoInputProcessor<PeaceModeProcessor>);

        public static void Patch(HarmonyInstance harmony)
        {
            try
            {
                OnKeyEvent(harmony);

                Debug.Log($"[{nameof(PatchAutoInputProcessor)}] Successfully patched.");
            }
            catch (Exception ex)
            {
                Debug.Log($"[{nameof(PatchAutoInputProcessor)}] Failed to patch. Error: {ex}");
            }
        }

        private static void OnKeyEvent(HarmonyInstance harmony)
        {
            MethodInfo original = Native.GetInstanceMethod(Reflection.Void, nameof(OnKeyEvent),
                typeof(Action), typeof(InputEvent));

            MethodInfo prefix = This.GetStaticMethod(Reflection.Boolean, nameof(OnKeyEventPrefix),
                typeof(Action), typeof(InputEvent));

            harmony.Patch(original, new HarmonyMethod(prefix));
        }

        //private static Visualizer LootRange;
        private static Boolean OnKeyEventPrefix(Action action, InputEvent inputEvent)
        {
            switch (inputEvent.Code)
            {
                case KeyCode.LeftShift:
                    OnShift(inputEvent);
                    break;
                case KeyCode.Space:
                    OnSpace(inputEvent);
                    break;
            }

            return HarmonyPrefix.CallOriginal;
        }

        private static void OnShift(InputEvent inputEvent)
        {
            if (inputEvent.Type == InputEventType.KeyDown)
            {
                InteractiveRangeVisualizer.Activate();
                CachingInputProcessor.HoverHash.Clear();
            }
            else if (inputEvent.Type == InputEventType.KeyUp)
            {
                InteractiveRangeVisualizer.Deactivate();
                CachingInputProcessor.HoverHash.Clear();
            }
        }

        private static void OnSpace(InputEvent inputEvent)
        {
            if (inputEvent.Modifiers == KeyModifiers.Shift)
                CollectLoot();
        }

        private static void CollectLoot()
        {
            var merged = new UnionContainer(new InteractiveRange());
            if (merged.Count == 0)
                return;

            Entity nearestContainer = merged.SelectedContainer.Entity;

            String useSound = nearestContainer.GetField<String>(CustomField.UseSound);
            if (!String.IsNullOrEmpty(useSound))
                Singleton<AudioManager>.Instance.Play(useSound);

            var window = Window.Create<ContainerWindow, BaseInventoryModule, BaseInventoryModule>(null, merged.MergedContainer, The.World.Avatar.InventoryModule);
            WindowManager.Instance.RegisterOnClose(window, () => merged.Commit());

            InteractiveRangeVisualizer.Deactivate();
            window.ShowModal(false);

            The.EventManager.ContainerOpened.Raise(nearestContainer);
        }
    }
}