using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatformBase : MonoBehaviour, IPluggedTo
{
    [SerializeField] protected List<float> speed;
    [SerializeField] protected List<Transform> path;
    [SerializeField] protected Vector3 carryDetectionExtra = new Vector3(0f, 0.5f, 0f);
    [SerializeField] protected bool isLooping = false;
    protected BoxCollider carryDetectionZone;
    protected Rigidbody rb;
    protected int currentTarget = 0;
    protected bool isForwardDirection = true;
    [SerializeField] protected bool isMoving = true;
    protected Vector3 lastFramePos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        carryDetectionZone = GetComponent<BoxCollider>();
        lastFramePos = rb.position;
    }

    public void StartCycling()
    {
        isMoving = true;
    }

    public void StopCycling()
    {
        isMoving = false;
    }

    protected virtual void MoveInFixedUpdate()
    {
        if (isMoving)
        {
            Vector3 direction = (path[currentTarget].position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed[currentTarget] * Time.fixedDeltaTime);
        }

        UpdateTarget();
        CarryObjects();
        lastFramePos = rb.position;
    }

    protected virtual void UpdateTarget()
    {
        if (Vector3.Distance(rb.position, path[currentTarget].position) < speed[currentTarget] * Time.fixedDeltaTime)
        {
            currentTarget = currentTarget + (isForwardDirection ? 1 : -1);
            if (!isLooping && currentTarget >= path.Count || currentTarget < 0)
            {
                isForwardDirection = !isForwardDirection;
                currentTarget = currentTarget + (isForwardDirection ? 1 : -1);
            }
            else if (isLooping && currentTarget >= path.Count || currentTarget < 0)
            {
                currentTarget = isForwardDirection ? 0 : path.Count - 1;
            }
        }
    }

    protected virtual void CarryObjects()
    {
        Vector3 detectionZone = (carryDetectionZone.size / 2) + carryDetectionExtra;

        Collider[] hitColliders = Physics.OverlapBox(transform.position, detectionZone, rb.rotation);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].TryGetComponent<Transform>(out Transform childNodeTr) && !hitColliders[i].TryGetComponent<Rigidbody>(out Rigidbody childNodeRb))
                childNodeTr.position = childNodeTr.position + (rb.position - lastFramePos);
        }
    }

    public void Trigger()
    {
        Debug.Log("AAAAAAAAAAAA");
        isMoving = !isMoving;
    }
}