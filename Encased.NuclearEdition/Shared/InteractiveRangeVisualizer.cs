using System;
using DarkCrystal.Encased;
using DarkCrystal.Encased.Core.ModuleSystem;
using DarkCrystal.Encased.Core.Visualizers;
using UnityEngine;

namespace Encased.NuclearEdition.Shared
{
    public class InteractiveRangeVisualizer : Visualizer
    {
        private static String TypeFullName = typeof(InteractiveRangeVisualizer).FullName;
        private const string PrefabPath = "Assets/RawResources/Graphic/VFX/WeaponRange.prefab";

        public override void Init()
        {
            BoundingSphere target = (BoundingSphere)this.Target;
            GameObject gameObject = GameObject.Instantiate<GameObject>(The.AssetManager.Load<GameObject>(PrefabPath), this.transform);
            gameObject.transform.position = target.position;
            gameObject.transform.localScale = Vector3.one * target.radius * 2f;
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            if (renderer)
                renderer.material.SetColor("_Color", Colors.Green);
        }

        public static void Activate()
        {
            EntityActor actor = The.World.Avatar.Actor;
            if (!actor)
                return;

            if (actor.transform.Find(TypeFullName))
                return;

            GameObject gameObject = GameObject.Instantiate<GameObject>(The.AssetManager.Load<GameObject>(PrefabPath), actor.transform);
            gameObject.name = TypeFullName;
            gameObject.transform.localScale = Vector3.one * InteractiveRange.Radius * 2f;
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            if (renderer)
                renderer.material.SetColor("_Color", Colors.Green);
        }

        public static void Deactivate()
        {
            EntityActor actor = The.World.Avatar.Actor;
            if (!actor)
                return;

            Transform visualizer = actor.transform.Find(TypeFullName);
            if (visualizer)
                GameObject.Destroy(visualizer.gameObject);
        }
    }
}