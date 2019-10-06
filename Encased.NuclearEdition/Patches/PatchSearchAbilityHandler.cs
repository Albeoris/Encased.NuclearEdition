using System;
using System.Reflection;
using DarkCrystal.Encased;
using DarkCrystal.Encased.Core;
using DarkCrystal.Encased.Core.GUI;
using DarkCrystal.Encased.Core.ModuleSystem;
using DarkCrystal.Encased.Core.RolePlay;
using DarkCrystal.Encased.Game;
using Encased.NuclearEdition.Proxies;
using Encased.NuclearEdition.Shared;
using Encased.NuclearEdition.Utils;
using Harmony;
using UnityEngine;
using HarmonyPrefix = Encased.NuclearEdition.Utils.HarmonyPrefix;
// ReSharper disable InconsistentNaming

namespace Encased.NuclearEdition.Patches
{
    public abstract class PatchSearchAbilityHandler
    {
        private static readonly Type This = typeof(PatchSearchAbilityHandler);
        private static readonly Type Native = typeof(SearchAbilityHandler);

        public static void Patch(HarmonyInstance harmony)
        {
            try
            {
                Apply(harmony);

                Debug.Log($"[{nameof(PatchSearchAbilityHandler)}] Successfully patched.");
            }
            catch (Exception ex)
            {
                Debug.Log($"[{nameof(PatchSearchAbilityHandler)}] Failed to patch. Error: {ex}");
            }
        }

        private static void Apply(HarmonyInstance harmony)
        {
            MethodInfo original = Native.GetInstanceMethod(Reflection.Void, nameof(Apply));

            MethodInfo prefix = This.GetStaticMethod(Reflection.Boolean, nameof(ApplyPrefix),
                typeof(SearchAbilityHandler));

            harmony.Patch(original, new HarmonyMethod(prefix));
        }

        private static Boolean ApplyPrefix(SearchAbilityHandler __instance)
        {
            if (!The.InputManager.IsPressed(KeyCode.LeftShift))
                return HarmonyPrefix.CallOriginal;

            ProxyAbilityHandler handler = new ProxyAbilityHandler(__instance);
            ActionData actionData = handler.ActionData;

            Entity targetEntity = actionData.TargetEntity;
            Character performer = actionData.Performer;

            ContainerModule container = targetEntity.GetModule<ContainerModule>();
            if (container.AlreadySearched)
                return HarmonyPrefix.SkipOriginal;

            container.AlreadySearched = true;
            container.Searchable = false;

            // Idk why
            performer.EnsureModule<InventoryModule>();

            var merged = new UnionContainer(new InteractiveRange(), container);

            if (merged.Count < 2 && merged.MergedContainer.AllItems.Count == 0)
            {
                merged.Commit();
                
                The.NotificationsManager.ShowNotificationOnPlayer("[15376]Found nothing");
                return HarmonyPrefix.SkipOriginal;
            }


            var window = Window.Create<ContainerWindow, BaseInventoryModule, BaseInventoryModule>(null, merged.MergedContainer, performer.InventoryModule);
            WindowManager.Instance.RegisterOnClose(window, () => merged.Commit());

            InteractiveRangeVisualizer.Deactivate();
            window.ShowModal(immediate: false);

            The.EventManager.ContainerOpened.Raise(targetEntity);

            return HarmonyPrefix.SkipOriginal;
        }
    }
}