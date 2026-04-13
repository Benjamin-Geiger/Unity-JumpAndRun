using System.Collections;
using UnityEngine;

public class SmoothStepTweening : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float waitTime;
    
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    private IEnumerator SmoothMove()
    {
        yield return new WaitForSeconds(waitTime);
        yield return this.StartCoroutine(this.SmoothStepForward());
        yield return new WaitForSeconds(waitTime);
        yield return this.StartCoroutine(this.SmoothStepBackwards());
    }

    private IEnumerator SmoothStepForward()
    {
        // t = Time.time - startTime / duration
        float t = 0.0f;
        while (t < 1.0f)
        {
            float g = Mathf.SmoothStep(0.0f, 1.0f, t);
            this.transform.position = Vector3.Lerp(this.startPosition, this.endPosition, g);
            
            t += Time.deltaTime * speed;
            yield return null;
        }
        this.transform.position = this.endPosition;
    }
    
    private IEnumerator SmoothStepBackwards()
    {
        // t = Time.time - startTime / duration
        float t = 1.0f;
        while (t > 0.0f)
        {
            float g = Mathf.SmoothStep(0.0f, 1.0f, t);
            this.transform.position = Vector3.Lerp(this.startPosition, this.endPosition, g);
            
            t += Time.deltaTime * speed;
            yield return null;
        }
        this.transform.position = this.startPosition;
    }
    void Start()
    {
        this.transform.position = this.startPosition;
        this.StartCoroutine(this.SmoothMove());
    }
    
    void Update()
    {
        
    }
}
