using System;
using UnityEngine;

namespace Winter.Assets.Project.Scripts.Runtime.Core.FreezeSystem
{
    public class FreezeModel
    {
        public event Action<float> FreezeValueChanged;
        public event Action FreezeMax;

        private float _freezeValue;

        public float GetFreezeValue()
        {
            return _freezeValue;
        }

        public void SetFreezeValue(float value)
        {
            _freezeValue = Mathf.Clamp(value, 0, 1);
            FreezeValueChanged?.Invoke(_freezeValue);

            if(_freezeValue >= 1)
                FreezeMax?.Invoke();
            
        }
    }
}
