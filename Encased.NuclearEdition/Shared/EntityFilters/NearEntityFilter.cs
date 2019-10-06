using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using DarkCrystal.Encased.Core.ModuleSystem;
using DarkCrystal.Encased.Core.Silhouettes;
using UnityEngine;

namespace Encased.NuclearEdition.Shared
{
    public sealed class NearEntityFilter
    {
        private readonly Vector3 _position;
        private readonly Single _rangeSqr;

        public NearEntityFilter(BoundingSphere sphere)
        {
            _position = sphere.position;
            _rangeSqr = sphere.radius * sphere.radius;
        }

        public Boolean IsValid(Entity entity, out Single distanceSqr)
        {
            PositionModule module = entity.GetModule<PositionModule>();
            if (module != null)
            {
                distanceSqr = (module.WorldPosition - _position).sqrMagnitude;
                return _rangeSqr >= distanceSqr;
            }

            EntityActor actor = entity.Actor;
            if (actor != null)
            {
                distanceSqr = (actor.transform.position - _position).sqrMagnitude;
                return _rangeSqr >= distanceSqr;
            }

            distanceSqr = 0f;
            return false;
        }
    }
}
