using System.Collections;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    private const float MaxVolume = 1.0f;
    private const float MinVolume = 0.0f;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _fadeSpeed = 0.2f;

    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.TryGetComponent(out Thief thief))
            return;
        
        StartCoroutine(MaxVolume);
        _audio.Play();
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Thief thief))
            StartCoroutine(MinVolume);
    }

    private void StartCoroutine(float volume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(volume));
    }

    private IEnumerator ChangeVolume(float volume)
    {
        while (true)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, volume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}