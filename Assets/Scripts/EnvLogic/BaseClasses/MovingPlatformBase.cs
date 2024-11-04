using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformBase : MonoBehaviour
{
    [SerializeField] private List<float> speed;
    [SerializeField] private List<Transform> path;
    [SerializeField] private Vector3 carryDetectionOffset;
    [SerializeField] private Vector3 carryDetectionSize;
    private int currentTarget = 0;
    private bool isForwardDirection = true;
    private bool isMoving = true;
    private Vector3 lastFramePos;

    void Awake()
    {
        lastFramePos = transform.position;
        carryDetectionOffset = new Vector3(0, 0.5f, 0);
        carryDetectionSize = new Vector3(2.5f, 0.25f, 2.5f);
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float step = speed[currentTarget] * Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, path[currentTarget].position, step);
            UpdateTarget();
        }

        CarryObjects();
        lastFramePos = transform.position;
    }

    public void StartCycling()
    {
        isMoving = true;
    }

    public void StopCycling()
    {
        isMoving = false;
    }

    private void UpdateTarget()
    {
        if (Vector3.Distance(transform.position, path[currentTarget].position) < 0.001f)
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
        Collider[] hitColliders = Physics.OverlapBox(transform.position + carryDetectionOffset, carryDetectionSize, Quaternion.identity);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].TryGetComponent<Rigidbody>(out Rigidbody childNodeRb))
            {
                childNodeRb.MovePosition(childNodeRb.position + (transform.position - lastFramePos));
            }
            else if (hitColliders[i].TryGetComponent<Transform>(out Transform childNodeTr))
            {
                childNodeTr.position = childNodeTr.position + (transform.position - lastFramePos);
            }
        }
    }
}