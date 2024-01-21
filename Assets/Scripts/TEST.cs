using Scripts.Game;
using UnityEngine;

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