using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadScene : MonoBehaviour
    {
        public static LoadScene Instance;

        [SerializeField] private GameObject scene;
        [SerializeField] private Slider slider;
        [SerializeField] private Text progressText;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            slider.value = 0f;
        }

        public void SetSliderValue(float progress)
        {
            slider.value = progress;
            progressText.text = $"{(int)(progress * 100)}%";
        }

        public void HideLoadScene()
        {
            scene?.SetActive(false);
        }

        public void ShowLoadScene()
        {
            slider.value = 0;
            scene?.SetActive(true);
        }
    }
}