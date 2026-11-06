using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5.5f, -8f);
    public float smooth = 6f;

    void LateUpdate()
    {
        if (!target) return;
        Vector3 desired = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desired, smooth * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.2f + Vector3.forward * 5f);
    }
}
