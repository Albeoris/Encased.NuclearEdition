using System;
using System.Collections.Generic;
using System.Linq;
using DarkCrystal.Encased;
using DarkCrystal.Encased.Core.ModuleSystem;
using UnityEngine;
using Object = System.Object;

namespace Encased.NuclearEdition.Shared
{
    public sealed class InteractiveRange
    {
        public static Single Radius => The.World.Avatar.PrimaryAttributes.Perception;

        public BoundingSphere Sphere { get; }
        public InRange[] Entities { get; }

        public InteractiveRange()
        {
            Sphere = GetBoundingSphere();
            Entities = GetNearEntities();
        }

        public IEnumerable<Container> EnumerateContainers()
        {
            foreach (var entity in Entities)
            {
                ContainerModule container = entity.Entity.GetModule<ContainerModule>();
                if (container)
                {
                    ContainerUsefulness usefulness = UsefulContainerEntityFilter.Check(container);
                    yield return new Container(entity, container, usefulness);
                }
            }
        }

        private InRange[] GetNearEntities()
        {
            NearEntityFilter filter = new NearEntityFilter(Sphere);
            return FilterNearEntities(filter).ToArray();
        }

        public static BoundingSphere GetBoundingSphere()
        {
            Single radius = Radius;
            BoundingSphere sphere = new BoundingSphere(The.World.Avatar.Actor.transform.position, radius);
            return sphere;
        }

        private static IEnumerable<InRange> FilterNearEntities(NearEntityFilter filter)
        {
            foreach (Entity entity in The.GameData.Entities.AllItems)
            {
                if (entity.HasActor() && filter.IsValid(entity, out var distance))
                    yield return new InRange(entity, distance);
            }
        }

        public sealed class InRange
        {
            public Entity Entity { get; }
            public Single Distance { get; }

            public InRange(Entity entity, Single distance)
            {
                Entity = entity;
                Distance = distance;
            }
        }

        public sealed class Container
        {
            private readonly InRange _inRange;
            
            public ContainerModule Module { get; }
            public ContainerUsefulness Usefulness { get; }
            
            public Entity Entity => _inRange.Entity;
            public Single Distance => _inRange.Distance;

            public Container(InRange inRange, ContainerModule containerModule, ContainerUsefulness usefulness)
            {
                _inRange = inRange;

                Module = containerModule;
                Usefulness = usefulness;
            }
        }
    }
}