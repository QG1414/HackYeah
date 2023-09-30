using System;

namespace SteelLotus.Core.Events
{
    public class PlayerEvents : Singleton<PlayerEvents>
    {
        public event Action<string> OnPlayerDeath;

        public void CallOnPlayerDeath(string deathType)
        {
            if (OnPlayerDeath != null)
                OnPlayerDeath(deathType);
        }

    }
}
