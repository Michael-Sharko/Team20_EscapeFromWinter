using System;
using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.SlipperyIce;

namespace Winter.Assets.Project.Scripts.Runtime.Core.TriggerObservable
{
    [RequireComponent(typeof(Collider))]
    public class SlipperyTriggerObserver : MonoBehaviour
    {
        public event Action<Collider> Enter;
        public event Action<Collider> Exit;

        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.GetComponent<SlipperySurface>() != null)
                Enter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.transform.GetComponent<SlipperySurface>() != null)
                Exit?.Invoke(other);
        }
    }
}
