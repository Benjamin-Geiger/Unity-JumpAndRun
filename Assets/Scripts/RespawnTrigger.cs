using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        var characterController = other.gameObject.GetComponent<CharacterController>();
        var character = other.gameObject.GetComponent<Character>();
        character.InflictDamage(100.0f);
        
        //Respawn(characterController);
    }

    private void Respawn(CharacterController character)
    {
        character.enabled = false;
        character.transform.position = respawnPoint.position;
        character.enabled = true;
    }
}
