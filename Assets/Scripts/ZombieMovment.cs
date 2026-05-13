using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private float waitTime = 1.5f;

    private Vector3 targetPosition;
    private bool isWaiting = false;
    
    private Animator animator;
    private Sequence sequence;
    //private bool isPlaying = false;
    
    [SerializeField] private AudioClip zombieSound;
    private AudioSource audioSource;
    private bool isDead = false;
    private bool isAttacking = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        PickNewTarget();
        
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = zombieSound;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    void SetAnimationState()
    {
        animator.SetBool("Dead", isDead);
        animator.SetBool("Attack", isAttacking);
    }

    // private void Squashed()
    // {
    //     if (sequence != null && sequence.IsActive()) sequence.Kill();
    //     
    //     this.sequence = DOTween.Sequence();
    //
    //     var tween = this.transform.DOScaleY(0.5f, 0.2f);
    //     tween.SetEase(Ease.InExpo);
    //     sequence.Append(tween);
    //     
    //     sequence.Append(transform.DOScaleY(1.0f, 0.2f));
    // }
    //
    // IEnumerator PlaySequence()
    // {
    //     isPlaying = true;
    //     this.Squashed();
    //     this.sequence.Play();
    //     yield return this.sequence.WaitForCompletion();
    //     isPlaying = false;
    // }

    public void OnPlayerStomp()
    {
        isDead = true;
        audioSource.Play();
        
        StopCoroutine("WaitAtPosition");
        StartCoroutine(WaitAtPosition());
        
        // if (isPlaying) return;
        // StartCoroutine(PlaySequence());
    }

    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        SetAnimationState();
        
        if (isWaiting) return;

        Vector3 toTarget = targetPosition - transform.position;
        toTarget.y = 0f;

        if (toTarget.magnitude < 0.1f)
        {
            StartCoroutine(WaitAtPosition());
            return;
        }

        Vector3 direction = toTarget.normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        transform.forward = direction;
    }

    private void PickNewTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        targetPosition = transform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);
    }

    private IEnumerator WaitAtPosition()
    {
        if (isDead)
        {
            yield return new WaitForSeconds(4.0f);
            Destroy(gameObject);
        }
        else
        {
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);
            PickNewTarget();
            isWaiting = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopCoroutine("WaitAtPosition");
        PickNewTarget();
        isWaiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var character = other.gameObject.GetComponentInChildren<Character>();
            character.InflictDamage(25.0f);
            isAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isAttacking = false;
    }
}
