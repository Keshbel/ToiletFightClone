using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class RandomPitches : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        if (!_audioSource) _audioSource = GetComponent<AudioSource>();

        _audioSource.pitch = Random.Range(0.8f, 1.2f);
    }
}
