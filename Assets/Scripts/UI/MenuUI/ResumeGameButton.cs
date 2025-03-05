using GameSystem;
using Interface;
using UnityEngine;

namespace MenuUI
{
    public class ResumeGameButton : MonoBehaviour, IButton
    {
        public void ButtonAction()
        {
            PauseSystem.ResumeGame();
        }
    }
}