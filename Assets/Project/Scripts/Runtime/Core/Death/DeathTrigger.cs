using System;
using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Player;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Death
{
    [RequireComponent(typeof(Collider))]
    public class DeathTrigger : MonoBehaviour
    {
        public event Action Enter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<PlayerIndicator>() != null)
                Enter?.Invoke();
        }
    }
}
