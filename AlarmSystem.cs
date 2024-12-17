using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _fadeSpeed = 0.1f;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0.0f;

    private bool _isThiefInside = false;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.TryGetComponent(out Thief thief))
            if(_audio.isPlaying == false)
                _audio.Play();
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Thief thief))
        {
            _isThiefInside = true;
           _audio.Play();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Thief thief))
            _isThiefInside = false;
    }

    private void Update()
    {
        if (_isThiefInside)
            FadeVolumeTo(_maxVolume);
        else
           FadeVolumeTo(_minVolume);
    }

    private void FadeVolumeTo(float targetVolume)
    {
        _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, _fadeSpeed * Time.deltaTime);
    }
}