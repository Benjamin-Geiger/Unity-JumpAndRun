using System;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Lever lever;
    
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float moveSpeed = 1;
    
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private bool isOn;

    void OnEnable() => lever.onStateChanged.AddListener(HandleLeverChanged);
    void OnDisable() => lever.onStateChanged.RemoveListener(HandleLeverChanged);

    void HandleLeverChanged(bool on)
    {
        isOn = on;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newPosition = endPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn) return;
        
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }
        
        if (transform.position == newPosition)
        {
            newPosition = (newPosition == endPosition.position) ? startPosition.position : endPosition.position;
        }
    }
}
