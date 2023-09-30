using System;

namespace SteelLotus.Core.Events
{
    public class GameEvents : Singleton<GameEvents>
    {
        public event Action OnGameStart;

        public void CallOnGameStart()
        {
            if (OnGameStart != null)
                OnGameStart();
        }
    }
}
