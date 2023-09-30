using UnityEngine;


namespace SteelLotus.Core.SaveLoadSystem
{
    public class DataManager : MonoBehaviour
    {
        private Encrypter encrypter;

        public Encrypter Encrypter { get => encrypter; }

        public void Init()
        {
            encrypter = new Encrypter();
            encrypter.Initialize();
        }
    }
}
