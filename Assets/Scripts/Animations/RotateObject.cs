using UnityEngine;

namespace SteelLotus.Animation
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField]
        private RectTransform objectToRotate;

        [SerializeField]
        private float rotationSpeed;

        [SerializeField]
        private bool changeRotationDirection;


        void Update()
        {
            float rotationValue = rotationSpeed * Time.deltaTime;

            rotationValue *= changeRotationDirection ? -1 : 1;

            objectToRotate.Rotate(0f, 0f, rotationValue);
        }
    }
}
