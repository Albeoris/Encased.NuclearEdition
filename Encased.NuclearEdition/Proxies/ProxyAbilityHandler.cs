using DarkCrystal.Encased.Core;
using DarkCrystal.Encased.Core.RolePlay;
using Encased.NuclearEdition.Utils;

namespace Encased.NuclearEdition.Proxies
{
    public sealed class ProxyAbilityHandler
    {
        private static readonly DGetFieldValue<AbilityHandler, ActionData> GetActionDataFunction = InstanceFieldAccessor.GetValueDelegate<AbilityHandler, ActionData>("ActionData");

        private readonly AbilityHandler _handler;

        public ProxyAbilityHandler(AbilityHandler handler)
        {
            _handler = handler;
        }

        public ActionData ActionData => GetActionDataFunction(_handler);
    }
}