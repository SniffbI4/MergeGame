using Scripts.Game;
using UnityEngine;

#if UNITY_EDITOR

namespace DefaultNamespace
{
    public class TEST : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.GetService<GameFlow>().PauseGame();
            }
        }
    }
}
#endif