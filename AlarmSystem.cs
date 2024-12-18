using System.Collections;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _fadeSpeed = 0.2f;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0.0f;

    private Coroutine _coroutineFadeIn;
    private Coroutine _coroutineFadeOut;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Thief thief))
        {
            if (_coroutineFadeOut != null)
            {
                StopCoroutine(_coroutineFadeOut);
                _coroutineFadeOut = null;
            }

            _coroutineFadeIn ??= StartCoroutine(FadeIn());
            _audio.Play();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Thief thief))
        {
            if (_coroutineFadeIn != null)
            {
                StopCoroutine(_coroutineFadeIn);
                _coroutineFadeIn = null;
            }

            _coroutineFadeOut ??= StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        while (_audio.volume <= _maxVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _maxVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        while (_audio.volume >= _minVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _minVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}