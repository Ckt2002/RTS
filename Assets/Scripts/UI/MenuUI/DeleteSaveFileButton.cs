using FireBase;
using Interface;
using UnityEngine;

namespace MenuUI
{
    public class DeleteSaveFileButton : MonoBehaviour, IButton
    {
        [SerializeField] private LoadGameSlot loadGameSlot;

        public void ButtonAction()
        {
            var fileName = loadGameSlot.fileName;
            if (loadGameSlot.isCloudData)
                FirebaseDeleteData.DeleteFile(fileName);
            else
                DeleteFileSaveSystem.DeleteFileLocal(fileName);

            // Disable current slot
            loadGameSlot.gameObject.SetActive(false);
        }
    }
}