using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    public float smooth;

    private Vector3 offset;
    // Update is called once per frame
    void Update () {
    }

    void LateUpdate()
    {
        offset = Vector3.zero; //this should be adjusted depending on if player is moving right or left
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smooth * Time.smoothDeltaTime);
        smoothedPosition.z = transform.position.z;
        Debug.Log(smoothedPosition);
        transform.position = smoothedPosition;
    }
}
