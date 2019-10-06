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
using DarkCrystal.Encased.Game;
using Encased.NuclearEdition.Proxies;
using Encased.NuclearEdition.Shared;
using Encased.NuclearEdition.Utils;
using Harmony;
using UnityEngine;
using Action = DarkCrystal.Encased.Core.Action;
using HarmonyPrefix = Encased.NuclearEdition.Utils.HarmonyPrefix;

namespace Encased.NuclearEdition.Patches
{
    public abstract class PatchOpenContainerAbilityHandler
    {
        private static readonly Type This = typeof(PatchOpenContainerAbilityHandler);
        private static readonly Type Native = typeof(OpenContainerAbilityHandler);

        public static void Patch(HarmonyInstance harmony)
        {
            try
            {
                Apply(harmony);

                Debug.Log($"[{nameof(PatchOpenContainerAbilityHandler)}] Successfully patched.");
            }
            catch (Exception ex)
            {
                Debug.Log($"[{nameof(PatchOpenContainerAbilityHandler)}] Failed to patch. Error: {ex}");
            }
        }

        private static void Apply(HarmonyInstance harmony)
        {
            MethodInfo original = Native.GetInstanceMethod(Reflection.Void, nameof(Apply));

            MethodInfo prefix = This.GetStaticMethod(Reflection.Boolean, nameof(ApplyPrefix),
                typeof(OpenContainerAbilityHandler));

            harmony.Patch(original, new HarmonyMethod(prefix));
        }

        private static Boolean ApplyPrefix(OpenContainerAbilityHandler __instance)
        {
            if (!The.InputManager.IsPressed(KeyCode.LeftShift))
                return HarmonyPrefix.CallOriginal;

            ProxyAbilityHandler handler = new ProxyAbilityHandler(__instance);
            ActionData actionData = handler.ActionData;
            
            Entity targetEntity = actionData.TargetEntity;
            Character performer = actionData.Performer;

            String field = targetEntity.GetField<String>(CustomField.UseSound);
            if (!String.IsNullOrEmpty(field))
                Singleton<AudioManager>.Instance.Play(field);

            ContainerModule container = targetEntity.GetModule<ContainerModule>();
            
            var union = new UnionContainer(new InteractiveRange(), container);

            var window = Window.Create<ContainerWindow, BaseInventoryModule, BaseInventoryModule>(null, union.MergedContainer, performer.InventoryModule);
            WindowManager.Instance.RegisterOnClose(window, () => union.Commit());

            InteractiveRangeVisualizer.Deactivate();
            window.ShowModal(false);

            The.EventManager.ContainerOpened.Raise(targetEntity);

            return HarmonyPrefix.SkipOriginal;
        }
    }
}