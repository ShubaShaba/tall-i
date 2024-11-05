using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformBase : MonoBehaviour
{
    [SerializeField] private List<float> speed;
    [SerializeField] private List<Transform> path;
    [SerializeField] protected Vector3 carryDetectionOffset;
    [SerializeField] protected Vector3 carryDetectionSize;
    protected Rigidbody rb;
    private int currentTarget = 0;
    private bool isForwardDirection = true;
    protected bool isMoving = true;
    private Vector3 lastFramePos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lastFramePos = rb.position;
        carryDetectionOffset = new Vector3(0, 0.5f, 0);
        carryDetectionSize = new Vector3(2.5f, 0.25f, 2.5f);
    }

    public void StartCycling()
    {
        isMoving = true;
    }

    public void StopCycling()
    {
        isMoving = false;
    }

    protected void MoveInFixedUpdate()
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

    protected void UpdateTarget()
    {
        if (Vector3.Distance(rb.position, path[currentTarget].position) < speed[currentTarget] * Time.fixedDeltaTime)
        {
            currentTarget = currentTarget + (isForwardDirection ? 1 : -1);
            if (currentTarget >= path.Count || currentTarget < 0)
            {
                isForwardDirection = !isForwardDirection;
                currentTarget = currentTarget + (isForwardDirection ? 1 : -1);
            }
        }
    }

    private void CarryObjects()
    {
        Collider[] hitColliders = Physics.OverlapBox(rb.position + carryDetectionOffset, carryDetectionSize, Quaternion.identity);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i] == GetComponent<Collider>()) continue;

            if (hitColliders[i].TryGetComponent<Transform>(out Transform childNodeTr) && !hitColliders[i].TryGetComponent<Rigidbody>(out Rigidbody childNodeRb))
                childNodeTr.position = childNodeTr.position + (rb.position - lastFramePos);
        }
    }
}