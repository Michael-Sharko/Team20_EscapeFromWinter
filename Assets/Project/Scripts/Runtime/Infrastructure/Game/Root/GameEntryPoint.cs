using UnityEngine;
using UnityEngine.SceneManagement;
using Winter.Assets.Project.Scripts.Runtime.Utils;

namespace Winter
{
    public class GameEntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if(SceneManager.GetActiveScene().name != Scenes.BOOT)
            {
                // SceneManager.LoadScene(Scenes.BOOT);
            }
        }
    }
}
