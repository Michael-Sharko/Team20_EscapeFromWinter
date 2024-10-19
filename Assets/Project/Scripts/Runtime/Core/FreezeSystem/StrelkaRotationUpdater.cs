using UnityEngine;

namespace Winter.Assets.Project.Scripts.Runtime.Core.FreezeSystem
{
    public class StrelkaRotationUpdater : MonoBehaviour
    {
        [SerializeField] private Transform _strelkaTrans;
        [SerializeField] private float _endYRotationOfStrelka;

        private float _startYRotationOfStrelka;

        private void Start()
        {
            _startYRotationOfStrelka = ConvertToNegativeDegrees(_strelkaTrans.localRotation.eulerAngles.y);
        }

        public void UpdateStrelkaRotation(float freezeValue)
        {
            float rotationValue = Mathf.Lerp(_startYRotationOfStrelka, _endYRotationOfStrelka, freezeValue);
            Vector3 currentRotation = _strelkaTrans.localRotation.eulerAngles;
            currentRotation.y = rotationValue;

            _strelkaTrans.localRotation = Quaternion.Euler(currentRotation);
        }

        private float ConvertToNegativeDegrees(float angle)
        {
            return angle > 180 ? angle - 360 : angle < -180 ? angle + 360 : angle;
        }
    }
}
