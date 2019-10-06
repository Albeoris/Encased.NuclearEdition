using System;
using DarkCrystal.Encased;
using DarkCrystal.Encased.Codegen.Data;
using DarkCrystal.Encased.Core;
using DarkCrystal.Encased.Core.ModuleSystem;
using DarkCrystal.Encased.Core.RolePlay;

namespace Encased.NuclearEdition.Shared
{
    public sealed class UsefulContainerEntityFilter
    {
        public Boolean IsValid(Entity entity)
        {
            if (!entity.HasActor())
                return false;

            ContainerModule container = entity.GetModule<ContainerModule>();
            if (container == null)
                return false;

            return Check(container) == ContainerUsefulness.Useful;
        }

        public static ContainerUsefulness Check(ContainerModule container)
        {
            Boolean isSearchable = container.Searchable;
            container.Searchable = false;

            ActionData openAction = new ActionData(The.World.Avatar, Common.Abilities.System.OpenContainer, container.Entity, forceToolVisual: false);
            if (!openAction.IsPossible())
            {
                container.Searchable = isSearchable;
                return ContainerUsefulness.Restricted;
            }

            var reason = openAction.GetAbilityHandler().GetBlockReasons();
            container.Searchable = isSearchable;

            if (reason == BlockReasons.None)
            {
                return container.AllItems.Count == 0
                    ? ContainerUsefulness.Empty
                    : ContainerUsefulness.Useful;
            }

            if ((reason & BlockReasons.NoWay) == BlockReasons.NoWay)
                return ContainerUsefulness.NoWay;

            if ((reason & BlockReasons.Locked) == BlockReasons.Locked)
                return ContainerUsefulness.Locked;

            if ((reason & BlockReasons.Protected) == BlockReasons.Protected)
                return ContainerUsefulness.Locked;

            return ContainerUsefulness.Restricted;
        }
    }
}