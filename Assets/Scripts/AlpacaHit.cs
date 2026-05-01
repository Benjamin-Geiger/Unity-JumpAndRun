using UnityEngine;

public class AlpacaHit : MonoBehaviour
{
    [SerializeField] private Alpaca alpaca;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        alpaca.OnAlpacaStomped();
    }
}
