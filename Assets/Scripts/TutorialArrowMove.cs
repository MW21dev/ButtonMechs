using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrowMove : MonoBehaviour
{
    public Direction direction;

    private float x;
    private float y;
    private float z;

    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float frequency;

    private Vector3 startPosition;

    private void Start()
    {
        x = transform.localPosition.x;
        y = transform.localPosition.y;
        z = transform.localPosition.z;

        amplitude = 4;
        frequency = 5;

        startPosition = new Vector3(x, y, z);
    }

    public enum Direction
    {
        Vertical,
        Horizontal,
    }

    private void Update()
    {
        
        switch (direction)
        {
            case Direction.Vertical:
                y = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
                break;
            case Direction.Horizontal:
                x = startPosition.x + Mathf.Sin(Time.time * frequency) * amplitude; 
                break;
        }

        transform.localPosition = new Vector3(x, y, z);
    }
}
