using UnityEngine;

public class SoundRandomiser : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sounds = null;
    private static AudioClip _lastSound = null;

    [SerializeField] private bool _allowRepeat = true;

    public AudioClip ReturnRandomSound()
    {
        // Returns a random sound from the array           
        AudioClip clip;
        do
        {
            clip = _sounds[Random.Range(0, _sounds.Length)];
        } while (clip == _lastSound && _allowRepeat == true);

        _lastSound = clip; //save to static so it doesn't repeat the same sound twice in a row
        return clip;
    }
}