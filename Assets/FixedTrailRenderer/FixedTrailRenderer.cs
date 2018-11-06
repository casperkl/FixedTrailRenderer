using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FixedTrailRenderer : MonoBehaviour
{
    public float time;
    
    public LineRenderer lr;
    Vector3[] positions;
    Vector3[] activePositions = new Vector3[0];

    int amountOfPositions;
    int currentSize = 0;
    Vector3 oldPos;
    // Use this for initialization
    void Start()
    {
        amountOfPositions = (int)(time / Time.fixedDeltaTime);
        positions = new Vector3[amountOfPositions];
        lr.positionCount = amountOfPositions;
    }
    void FixedUpdate()
    {
        if (lr.gameObject.activeInHierarchy && currentSize < amountOfPositions)
            currentSize++;
        else if (!lr.gameObject.activeInHierarchy)
            currentSize = 1;
        ShiftPositions();
        positions[0] = transform.position;
    }
    private void LateUpdate()
    {
        //Make sure the first position is placed on the source of the trail renderer.
        //But save the position for the next frame, so the point will be on the correct position for the trail. 
        oldPos = positions[0];
        positions[0] = transform.position;
        if (currentSize != activePositions.Length)
        {
            activePositions = new Vector3[currentSize];
            lr.positionCount = currentSize;
        }
        Array.Copy(positions, activePositions, currentSize);

        lr.SetPositions(activePositions);
        //Set the old position back for the next frame.
        positions[0] = oldPos;
    }
    void ShiftPositions()
    {
        //Shift all the values down 1 position.
        for (int i = amountOfPositions - 1; i > 0; i--)
        {
            positions[i] = positions[i - 1];
        }
    }
}