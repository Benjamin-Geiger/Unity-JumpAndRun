using UnityEngine;

public class ZombieHeadTrigger : MonoBehaviour
{
    [SerializeField] private ZombieMovement zombieMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        zombieMovement.OnPlayerStomp();
    }
}
