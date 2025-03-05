﻿using UnityEngine;

namespace GameSystem
{
    public class SaveLoadSystem : MonoBehaviour
    {
        public static SaveLoadSystem Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveGame()
        {
        }

        public void LoadGame()
        {
        }
    }
}