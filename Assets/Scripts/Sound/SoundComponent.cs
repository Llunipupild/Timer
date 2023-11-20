using UnityEngine;

namespace Sound
{
    public class SoundComponent : MonoBehaviour
    {
        private AudioSource _audioSource;
        private void Start()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            Play();
        }

        public void Play()
        {
            if (_audioSource.isPlaying) {
                return;
            }
            
            _audioSource.Play();
        }
    }
}