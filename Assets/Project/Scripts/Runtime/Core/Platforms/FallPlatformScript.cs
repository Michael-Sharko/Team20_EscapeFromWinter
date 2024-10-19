using System.Collections;
using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Player;

public class FallPlatformScript : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float fallTime = 2.0f;
    [SerializeField] private AudioSource _platformAudioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Rigidbody _fallIceRB;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerIndicator>() != null)
            StartCoroutine(IceFalldelay());
    }
    
    private IEnumerator IceFalldelay()
    {
        _platformAudioSource.PlayOneShot(_audioClip);
        yield return new WaitForSeconds(fallTime);
        _fallIceRB.useGravity = true;
    }

}
