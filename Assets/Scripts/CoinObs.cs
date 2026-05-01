using UnityEngine;
using UnityEngine.Audio;

public class CoinObs : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        UIManager.Instance.CollectCoin();
        
        CoinEvents.CoinCollected(transform.position, collectSound, sfxMixerGroup);

        Destroy(gameObject);
    }
}
