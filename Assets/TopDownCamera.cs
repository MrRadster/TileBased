using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;          // drag the Player here
    public float height = 10f;
    public float smoothSpeed = 8f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = new Vector3(target.position.x, height, target.position.z);
        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
    }
}