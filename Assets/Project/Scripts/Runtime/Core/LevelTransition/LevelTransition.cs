using UnityEngine;
using UnityEngine.SceneManagement;
using Winter.Assets.Project.Scripts.Runtime.Core.Player;

namespace Winter.Assets.Project.Scripts.Runtime.Core.LevelTransition
{
    public class LevelTransition : MonoBehaviour
    {
        [SerializeField] private string _levelName;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<PlayerIndicator>() != null)
            {
                SceneManager.LoadScene(_levelName);
            }
        }
    }
}
