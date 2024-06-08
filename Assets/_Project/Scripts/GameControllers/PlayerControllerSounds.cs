using UnityEngine;

public class PlayerControllerSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSteps;

    public void PlaySteps()
    {
        if (!audioSteps.isPlaying)
            audioSteps.Play();
    }

    public void StopSteps()
    {
        if (audioSteps.isPlaying)
            audioSteps.Stop();
    }
}
