﻿using UnityEngine;

namespace GameSystem
{
    public class PauseSystem : MonoBehaviour
    {
        public static bool isPausing { get; private set; }

        public static void PauseGame()
        {
            isPausing = true;
        }

        public static void ResumeGame()
        {
            isPausing = false;
        }
    }
}