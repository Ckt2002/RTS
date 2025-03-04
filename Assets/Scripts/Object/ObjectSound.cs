using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource moveSound;
    [SerializeField] private AudioSource dieSound;

    public void PlayAttackSound()
    {
        if (attackSound != null)
            attackSound.Play();
    }

    public void PlayMoveSound()
    {
        if (moveSound != null)
            moveSound.Play();
    }

    public void PlayDieSound()
    {
        if (dieSound != null)
            dieSound.Play();
    }
}