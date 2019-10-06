using System;
using System.Collections.Generic;
using DarkCrystal;
using DarkCrystal.Encased;
using DarkCrystal.Encased.Core.Silhouettes;
using Encased.NuclearEdition.Utils;
using UnityEngine;

namespace Encased.NuclearEdition.Shared
{
    public sealed class SilhouetteColors : Singleton<SilhouetteColors>
    {
        public SilhouetteType NotInterested { get; }
        public SilhouetteType Locked { get; }
        public SilhouetteType Warning { get; set; }
        public SilhouetteType Restricted { get; set; }
        public SilhouetteType Items { get; set; }

        private SilhouetteColors()
        {
            var currentIndex = Enum<SilhouetteType>.Count;
            var silhouetteColors = new List<Color>(currentIndex);
            var outlineColors = new List<Color>(currentIndex);

            MakeDefaultColors(silhouetteColors, outlineColors);

            // ----------------------------------------------------------------------------------------------
            NotInterested = RegisterColor(Colors.LightGray.A(0.5f), Colors.DarkGray.A(0.5f));
            Locked = RegisterColor(Colors.Violet.A(0.5f), Colors.DarkViolet.A(0.5f));
            Warning = RegisterColor(Colors.Yellow.A(0.5f), Colors.Gold.A(0.5f));
            Restricted = RegisterColor(Colors.Red.A(0.5f), Colors.DarkRed.A(0.5f));
            Items = RegisterColor(Colors.Green.A(0.5f), Colors.DarkGreen.A(0.5f));
            // ----------------------------------------------------------------------------------------------

            CommitChanges(silhouetteColors.ToArray(), outlineColors.ToArray());

            SilhouetteType RegisterColor(Color silhouette, Color outline)
            {
                SilhouetteType index = (SilhouetteType) currentIndex++;
                silhouetteColors.Add(silhouette);
                outlineColors.Add(outline);
                return index;
            }
        }

        private static void MakeDefaultColors(List<Color> silhouetteColors, List<Color> outlineColors)
        {
            Color clear = Color.clear;
            foreach (SilhouetteType silhouetteType in Enum<SilhouetteType>.Values)
            {
                var flag = silhouetteType == SilhouetteType.Avatar;
                silhouetteColors.Add(flag ? silhouetteType.GetColor() : clear);
                outlineColors.Add(flag ? clear : silhouetteType.GetColor());
            }
        }

        private static void CommitChanges(Color[] silhouetteColors, Color[] outlineColors)
        {
            var renderer = The.SilhouetteManager.Renderer;

            var silhouetteField = new InstanceFieldAccessor<SilhouetteManager.SilhouetteRenderer, ComputeBuffer>(renderer, "SilhouetteColors");
            var outlineField = new InstanceFieldAccessor<SilhouetteManager.SilhouetteRenderer, ComputeBuffer>(renderer, "OutlineColors");
            
            ChangeShaderBuffer("_SilhouetteColors", silhouetteColors, silhouetteField);
            ChangeShaderBuffer("_OutlineColors", outlineColors, outlineField);
        }

        private static void ChangeShaderBuffer(String bufferName, Color[] colors, IInstanceFieldAccessor<ComputeBuffer> field)
        {
            Int32 stride = sizeof(Single) * 4; // Marshal.SizeOf(Color)

            ComputeBuffer buffer = new ComputeBuffer(colors.Length, stride, ComputeBufferType.Default);
            buffer.SetData(colors);

            Shader.SetGlobalBuffer(bufferName, buffer);

            field.Value.Dispose();
            field.Value = buffer;
        }
    }
}