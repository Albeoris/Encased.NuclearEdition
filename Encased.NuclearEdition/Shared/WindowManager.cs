using System;
using System.Collections.Generic;
using DarkCrystal;
using DarkCrystal.Encased.Core.GUI;

namespace Encased.NuclearEdition.Shared
{
    public sealed class WindowManager : Singleton<WindowManager>
    {
        private readonly Dictionary<Int32, System.Action> _dic = new Dictionary<Int32, System.Action>();

        public void RegisterOnClose(Window window, System.Action action)
        {
            Int32 id = window.GetInstanceID();

            if (_dic.TryGetValue(id, out var current))
            {
                _dic[id] = (System.Action)Delegate.Combine(current, action);
            }
            else
            {
                _dic[id] = action;
            }
        }

        public void RaiseWindowClose(Window window)
        {
            Int32 id = window.GetInstanceID();

            if (_dic.TryGetValue(id, out var action))
            {
                _dic.Remove(id);
                action();
            }
        }
    }
}