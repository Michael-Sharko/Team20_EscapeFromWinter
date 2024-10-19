using UnityEngine;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Intro
{
    public class IntroController : MonoBehaviour
    {
        [SerializeField] private GameObject _introObject;

        public void Show()
        {
            _introObject.SetActive(true);
        }

        public void Hide()
        {
            _introObject.SetActive(false);
        }
    }
}
