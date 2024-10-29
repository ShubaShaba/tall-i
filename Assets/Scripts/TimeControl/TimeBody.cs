using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    public bool isRewinding = false;
    public bool isFreezing = false;
    List<PointInTime> pointsInTime;

    Rigidbody rb;

    private bool isFocusedOn = false;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFocusedOn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartRewiding();
                isFreezing = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StopRewiding();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartFreezing();
                isRewinding = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                StopFreezing();
            }
        }
        else
        {
            StopFreezing();
            isFreezing = false;
            isRewinding = false;
            StopRewiding();
        }
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
        if (isFreezing)
        {
            Freeze();
        }
        else
        {
            DeFreeze();
        }

        void Freeze()
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        void DeFreeze()
        {
            rb.constraints = 0;
        }

    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    void Rewind()
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

    public void StartRewiding()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewiding()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }

    public void StartFreezing()
    {
        isFreezing = true;
        rb.isKinematic = true;
    }

    public void StopFreezing()
    {
        isFreezing = false;
        rb.isKinematic = false;
    }

    public void Focus() {
        isFocusedOn = true;
    }

    public void UnFocus() {
        isFocusedOn = false;
    }
}
