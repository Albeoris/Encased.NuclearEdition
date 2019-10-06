using System;
using System.Reflection;
using DarkCrystal.Audio;
using DarkCrystal.Encased;
using DarkCrystal.Encased.Core;
using DarkCrystal.Encased.Core.GUI;
using DarkCrystal.Encased.Core.Input;
using DarkCrystal.Encased.Core.ModuleSystem;
using DarkCrystal.Encased.Core.RolePlay;
using DarkCrystal.Encased.Game;
using Encased.NuclearEdition.Shared;
using Encased.NuclearEdition.Utils;
using Harmony;
using UnityEngine;
using HarmonyPrefix = Encased.NuclearEdition.Utils.HarmonyPrefix;

namespace Encased.NuclearEdition.Patches
{
    public abstract class PatchWindow
    {
        private static readonly Type This = typeof(PatchWindow);
        private static readonly Type Native = typeof(Window);

        public static void Patch(HarmonyInstance harmony)
        {
            try
            {
                Close(harmony);

                Debug.Log($"[{nameof(PatchWindow)}] Successfully patched.");
            }
            catch (Exception ex)
            {
                Debug.Log($"[{nameof(PatchWindow)}] Failed to patch. Error: {ex}");
            }
        }

        private static void Close(HarmonyInstance harmony)
        {
            MethodInfo original = Native.GetInstanceMethod(Reflection.Void, nameof(Close),
                typeof(Boolean));

            MethodInfo prefix = This.GetStaticMethod(Reflection.Boolean, nameof(ClosePrefix),
                typeof(Boolean), typeof(Window));

            harmony.Patch(original, new HarmonyMethod(prefix));
        }

        public static Boolean ClosePrefix(Boolean immediate, Window __instance)
        {
            WindowManager.Instance.RaiseWindowClose(__instance);

            return HarmonyPrefix.CallOriginal;
        }
    }
}