using System;
using System.Collections.Generic;
using DarkCrystal;
using DarkCrystal.Encased;
using DarkCrystal.Encased.Codegen.Data;
using DarkCrystal.Encased.Core;
using DarkCrystal.Encased.Core.Commands;
using DarkCrystal.Encased.Core.Input;
using DarkCrystal.Encased.Core.ModuleSystem;
using DarkCrystal.Encased.Core.Silhouettes;
using UnityEngine;
using Object = System.Object;

namespace Encased.NuclearEdition.Shared
{
    public sealed class SilhouetteMaker
    {
        private readonly HoverData _hoverData;
        private readonly ActionData _actionData;

        public SilhouetteMaker(HoverData hoverData, ActionData actionData)
        {
            _hoverData = hoverData;
            _actionData = actionData;
        }

        public void Hover()
        {
            foreach (var targetEntity in GetEntities())
            {
                if (!targetEntity.HasActor())
                    continue;

                GameObject gameObject = targetEntity.Actor.gameObject;

                if (_actionData.Ability.Is<AttackAbility>())
                {
                    _hoverData.AddCommand(new SilhouetteCommand(gameObject, SilhouetteType.Enemy));
                    continue;
                }

                if (targetEntity.Is<Character>())
                {
                    _hoverData.AddCommand(new SilhouetteCommand(gameObject, SilhouetteType.Ally));
                    continue;
                }

                ContainerModule container = targetEntity.GetModule<ContainerModule>();
                if (container != null)
                {
                    HighlightContainer(container);
                    continue;
                }

                if (TryHighlightDoor(targetEntity))
                    continue;

                _hoverData.AddCommand(new SilhouetteCommand(gameObject, SilhouetteType.PointOfInterest));
            }
        }

        private Boolean TryHighlightDoor(Entity targetEntity)
        {
            DoorModule door = targetEntity.GetModule<DoorModule>();
            if (!door)
                return false;

            GameObject gameObject = targetEntity.Actor.gameObject;

            if (door.IsClosed)
            {
                //ActionData openAction = new ActionData(The.World.Avatar, Common.Abilities.System.OpenDoor, targetEntity, forceToolVisual: false);
                _hoverData.AddCommand(new SilhouetteCommand(gameObject, SilhouetteColors.Instance.Warning));
            }
            else
            {
                _hoverData.AddCommand(new SilhouetteCommand(gameObject, SilhouetteType.PointOfInterest));
            }

            return true;

        }

        private IEnumerable<Entity> GetEntities()
        {
            Guid targetId = Guid.Empty;

            var entity = _actionData.TargetEntity;
            if (entity)
            {
                targetId = entity.Guid;
                yield return entity;
            }

            if (!The.InputManager.IsPressed(KeyCode.LeftShift))
                yield break;

            var range = new InteractiveRange();

            foreach (var near in range.Entities)
            {
                if (near.Entity.Guid != targetId)
                    yield return near.Entity;
            }
        }

        private void HighlightContainer(ContainerModule container)
        {
            ContainerUsefulness usefulness = UsefulContainerEntityFilter.Check(container);
            if (TryMakeContainerSilhouette(usefulness, out var silhouette))
                _hoverData.AddCommand(new SilhouetteCommand(container.Entity.Actor.gameObject, silhouette));
        }

        private Boolean TryMakeContainerSilhouette(ContainerUsefulness reason, out SilhouetteType silhouette)
        {
            switch (reason)
            {
                case ContainerUsefulness.NoWay:
                    silhouette = SilhouetteType.Transparent;
                    return false;
                case ContainerUsefulness.Useful:
                    silhouette = SilhouetteColors.Instance.Items;
                    break;
                case ContainerUsefulness.Empty:
                    silhouette = SilhouetteColors.Instance.NotInterested;
                    break;
                case ContainerUsefulness.Locked:
                    silhouette = SilhouetteColors.Instance.Locked;
                    break;
                case ContainerUsefulness.Restricted:
                    silhouette = SilhouetteColors.Instance.Restricted;
                    break;
                default:
                    throw new NotSupportedException(reason.ToString());
            }

            return true;
        }
    }
}