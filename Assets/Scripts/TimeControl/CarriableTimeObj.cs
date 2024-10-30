using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarriableTimeObj : MonoBehaviour, TimeBody
{
    [SerializeField] private ControlManager controlManager;
    private TimeBodyStates currentState;
    private TimeBodyStates previousState;
    private List<PointInTime> pointsInTime;

    private Rigidbody rb;

    private bool isFocusedOn = false;

    public void Focus()
    {
        isFocusedOn = true;
    }

    public void UnFocus()
    {
        isFocusedOn = false;
    }

    public void StartRewinding()
    {
        SetState(TimeBodyStates.Rewinding);
        rb.isKinematic = true;
    }

    private void SetState(TimeBodyStates state)
    {
        if (state == currentState)
            return;

        previousState = currentState;
        currentState = state;
    }

    private void HandleTime()
    {
        switch (currentState)
        {
            case TimeBodyStates.Natural:
                Record();
                break;
            case TimeBodyStates.Rewinding:
                Rewind();
                break;
            default:
                break;
        }
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(5f / Time.deltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewiding();
        }
    }

    private void StopRewiding()
    {
        SetState(TimeBodyStates.Natural);
        rb.isKinematic = false;
    }

    private void StartFreezing()
    {
        SetState(TimeBodyStates.Stoped);
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    private void StopFreezing()
    {
        rb.constraints = 0;
        if (previousState == TimeBodyStates.Rewinding) {
            StartRewinding();
        } else {
            StopRewiding();
        }
    }
    
    private void Start()
    {
        currentState = TimeBodyStates.Natural;
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }
    
    // private void Update()
    // {
    // }

    // Update is called once per frame
    void Update()
    {
        HandleTime();

        if (isFocusedOn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartRewinding();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StopRewiding();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartFreezing();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                StopFreezing();
            }

            Debug.Log(currentState);
        }
        else
        {
            StopFreezing();
            StopRewiding();
        }
    }
}
