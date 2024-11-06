using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Winter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ClimbingSurface : MonoBehaviour
    {
        private void Start() => enabled = false;
    }
}
