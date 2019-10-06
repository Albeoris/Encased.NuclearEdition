using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DarkCrystal.Encased.Core;
using DarkCrystal.Encased.Core.Banners;
using DarkCrystal.Encased.Core.Commands;
using DarkCrystal.Encased.Core.Common;
using DarkCrystal.Encased.Core.Cursors;
using DarkCrystal.Encased.Core.Input;
using DarkCrystal.Encased.Core.ModuleSystem;
using DarkCrystal.Serialization;
using Encased.NuclearEdition.Shared;
using Encased.NuclearEdition.Utils;
using Harmony;
using UnityEngine;
using Action = DarkCrystal.Encased.Core.Action;
using HarmonyPrefix = Encased.NuclearEdition.Utils.HarmonyPrefix;

namespace Encased.NuclearEdition.Patches
{
    public abstract class PatchGameModeProcessor
    {
        private static readonly Type This = TypeCache<PatchGameModeProcessor>.Type;
        private static readonly Type Native = TypeCache<GameModeProcessor>.Type;

        public static void Patch(HarmonyInstance harmony)
        {
            try
            {
                OnHover(harmony);

                Debug.Log($"[{nameof(PatchGameModeProcessor)}] Successfully patched.");
            }
            catch (Exception ex)
            {
                Debug.Log($"[{nameof(PatchGameModeProcessor)}] Failed to patch. Error: {ex}");
            }
        }

        private static void OnHover(HarmonyInstance harmony)
        {
            MethodInfo original = Native.GetInstanceMethod(Reflection.Void, nameof(OnHover),
                typeof(Action), typeof(HoverData));

            MethodInfo prefix = This.GetStaticMethod(Reflection.Boolean, nameof(OnHoverPrefix),
                typeof(Action), typeof(HoverData), typeof(GameModeProcessor));

            harmony.Patch(original, new HarmonyMethod(prefix));
        }

        private static Boolean OnHoverPrefix(Action action, HoverData hoverData, GameModeProcessor __instance)
        {
            if (!(__instance is PeaceModeProcessor))
                return HarmonyPrefix.CallOriginal;

            Boolean flag = action.Data.IsPossible();
            if (flag)
            {
                if (action.Ability != null)
                    hoverData.CursorType = action.Data.GetCursor(action.BlockReasons);
                else if (action.Tool != null)
                    hoverData.CursorType = CursorType.Variants;
            }

            SetSilhouette(hoverData, action.Data);

            if (action.TargetEntity != null)
            {
                SetBadge(hoverData, action.TargetEntity);
                if (flag && action.Ability != null)
                    hoverData.AddCommand(ActionTooltipBuilder.GetBanner(action, RestrictionStrategy.DefaultTooltip));
            }

            return HarmonyPrefix.SkipOriginal;
        }

        private static void SetSilhouette(HoverData hoverData, ActionData actionData)
        {
            SilhouetteMaker maker = new SilhouetteMaker(hoverData, actionData);
            maker.Hover();
        }

        private static void SetBadge(HoverData hoverData, Entity targetEntity)
        {
            if (targetEntity != null && targetEntity.SubclassIndex == TypeIndex.Character)
            {
                hoverData.AddCommand(new BadgeCommand(targetEntity));
            }
        }
    }
}