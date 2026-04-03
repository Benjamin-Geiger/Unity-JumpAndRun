using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float platformSpeed;
    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 end;
    private Vector3 oldPos;
    private Vector3 newPos;
    void FixedUpdate()
    {
        oldPos = transform.position;
        
        float pingPong = Mathf.PingPong(Time.fixedTime * this.platformSpeed,1.0f);
        var newPosition = Vector3.Lerp(this.start, this.end, pingPong);
        this.transform.localPosition = newPosition;
        
        newPos = newPosition;
        
    }
    
    public Vector3 GetVelocity()
    {
        Vector3 velocity = (newPos - oldPos) / Time.fixedDeltaTime;
        return velocity;
    }
}
