using System;
using UnityEngine;
using UnityEngine.Audio;

public static class CoinEvents
{
    public static Action<Vector3, AudioClip, AudioMixerGroup> OnCoinCollected;

    public static void CoinCollected(Vector3 position, AudioClip clip, AudioMixerGroup mixer)
    {
        OnCoinCollected?.Invoke(position, clip, mixer);
    }
}