using GameSystem;
using Interface;
using UnityEngine;

namespace MenuUI
{
    public class PauseGameButton : MonoBehaviour, IButton
    {
        public void ButtonAction()
        {
            PauseSystem.PauseGame();
        }
    }
}