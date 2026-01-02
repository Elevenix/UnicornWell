using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class RandomPitch : MonoBehaviour
{
    [SerializeField] private float minPitch,maxPitch;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource> ();
    }

    public void PlaySound()
    {
        float pitch = Random.Range (minPitch,maxPitch);
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
