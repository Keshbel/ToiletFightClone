using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform lookTarget;

    private void Awake()
    {
        if (!lookTarget) lookTarget = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(lookTarget);
    }
}
