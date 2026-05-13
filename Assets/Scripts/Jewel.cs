using UnityEngine;

public class Jewel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.OnGameVictory();
            Destroy(gameObject);
        }
    }
}
