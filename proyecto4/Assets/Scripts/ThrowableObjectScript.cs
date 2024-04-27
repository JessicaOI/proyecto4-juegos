using GamePlay;
using UnityEngine;

public class ThrowableObjectScript : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float soundRange = 25f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayImpactSound();
        }
    }

    private void PlayImpactSound()
    {
        if (source == null || impactSound == null)
        {
            Debug.LogError("AudioSource or AudioClip is missing!");
            return;
        }

        if (!source.isPlaying) // Check to prevent overlapping sounds
        {
            source.clip = impactSound;
            source.Play();

            // Assume you have a method to create and process sound objects in your game
            var sound = new Sound(transform.position, soundRange, Sound.SoundType.Interesting);
            Sounds.MakeSound(sound);
        }
    }
}
