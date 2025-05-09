﻿using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    [SerializeField] private float delayBeforeStart = 2f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float downDistance = 2f;
    [SerializeField] private float upDistance = 2f;

    private Vector3 startPosition;
    private float timeSinceStart;
    private bool started = false;

    private float totalDistance => downDistance + upDistance;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (!started)
        {
            if (timeSinceStart >= delayBeforeStart)
            {
                started = true;
                timeSinceStart = 0f;
            }
            return;
        }

       
        float cycleTime = totalDistance / speed;
        float offset = Mathf.PingPong(timeSinceStart * speed, totalDistance);

        transform.position = startPosition + Vector3.down * offset;

        if (timeSinceStart > 1000f)
            timeSinceStart = 0f;
    }
}
