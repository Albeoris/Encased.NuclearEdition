using System;
using System.Collections.Generic;
using System.Linq;
using DarkCrystal.Encased.Core.ModuleSystem;

namespace Encased.NuclearEdition.Shared
{
    public class UnionContainer
    {
        private readonly ContainerModule _selectedContainer;
        private readonly Dictionary<Item, ContainerModule> _itemToContainer = new Dictionary<Item, ContainerModule>();

        public ContainerModule SelectedContainer => _selectedContainer;
        public ContainerModule MergedContainer { get; }
        public Int32 Count { get; private set; }

        public UnionContainer(InteractiveRange range)
        {
            MergedContainer = new Entity().AddModule<ContainerModule>();
            Union(range, out _selectedContainer);
        }

        public UnionContainer(InteractiveRange range, ContainerModule selectedContainer)
        {
            _selectedContainer = selectedContainer;
            MergedContainer = new Entity().AddModule<ContainerModule>();

            Union(selectedContainer);
            Union(range, out _);
        }

        public void Union(InteractiveRange range, out ContainerModule nearestContainer)
        {
            Guid mainId = _selectedContainer?.Entity.Guid ?? Guid.Empty;
            Single distance = Single.MaxValue;
            nearestContainer = null;

            foreach (var container in range.EnumerateContainers())
            {
                if (container.Entity.Guid == mainId)
                    continue;

                if (container.Usefulness == ContainerUsefulness.Useful)
                {
                    if (container.Distance < distance)
                    {
                        distance = container.Distance;
                        nearestContainer = container.Module;
                    }

                    Union(container.Module);
                }
            }
        }

        public void Union(ContainerModule container)
        {
            Count++;

            MarkAsSearched(container);

            foreach (Item item in container.AllItems)
            {
                // When added to the union container, same items will not stack.
                // This is necessary to return they to their places if
                // the player will not pick them up before closing the exchange window.
                MergedContainer.AddItem(item, stackItem: false);

                _itemToContainer.Add(item, container);
            }
        }

        public void Commit()
        {
            DistributeItems();
            StoreRemainingItems();

            MergedContainer.Entity.Release();
        }

        private void DistributeItems()
        {
            foreach (var pair in _itemToContainer)
            {
                var item = pair.Key;
                var owner = pair.Value;

                if (MergedContainer.Contains(item))
                {
                    // Forget about the items left in the container.
                    // They still lie in specific containers.
                    // The number of items on the stack could change - it will change in place.
                    MergedContainer.RemoveItemStack(item, unequip: false);
                }
                else
                {
                    owner.RemoveItemStack(item, unequip: true);
                }
            }

            _itemToContainer.Clear();
        }

        private void StoreRemainingItems()
        {
            foreach (Item item in MergedContainer.AllItems)
                _selectedContainer.AddItem(item, stackItem: false);

            MergedContainer.AllItems.Clear();
        }

        private static void MarkAsSearched(ContainerModule container)
        {
            if (container.Searchable)
            {
                container.Searchable = false;
                container.AlreadySearched = true;
            }
        }
    }
}