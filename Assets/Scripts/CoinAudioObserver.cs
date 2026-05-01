using UnityEngine;
using UnityEngine.Audio;

public class CoinAudioObserver : MonoBehaviour
{
    private void OnEnable()
    {
        CoinEvents.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        CoinEvents.OnCoinCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected(Vector3 position, AudioClip clip, AudioMixerGroup mixer)
    {
        if (clip == null) return;

        var go = new GameObject("CoinCollectSound");
        go.transform.position = position;

        var source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = mixer;
        source.Play();

        Destroy(go, clip.length);
    }
}