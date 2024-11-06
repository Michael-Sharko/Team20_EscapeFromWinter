using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.SlipperyIce;

namespace Winter
{
    public class ClimbingRockTriggerObserver : MonoBehaviour
    {
        public event Action<Collider> Enter;
        public event Action<Collider> Exit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<ClimbingSurface>() != null)
                Enter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.GetComponent<ClimbingSurface>() != null)
                Exit?.Invoke(other);
        }
    }
}
