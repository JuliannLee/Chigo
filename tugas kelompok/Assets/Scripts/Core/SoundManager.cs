using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        source = GetComponent<AudioSource>();

        if (source == null)
        {
            Debug.LogError("AudioSource component missing from SoundManager game object.");
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        if (_sound != null)
        {
            source.PlayOneShot(_sound);
        }
        else
        {
            Debug.LogError("Attempted to play a null AudioClip.");
        }
    }
}
