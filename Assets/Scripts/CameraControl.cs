using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float mouseSense = 1f;
    private float yRotate;
    private float xRotate;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float disFromTarget = 3f;

    private Vector3 currentRotate;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField]
    private float smoothTime = 0.2f;

    [SerializeField]
    private Vector2 rotateMinMax = new Vector2(-40, 40);

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*mouseSense ;
        float mouseY = Input.GetAxis("Mouse Y")*mouseSense ;

        yRotate += mouseX;
        xRotate += mouseY;

        xRotate = Mathf.Clamp(xRotate,rotateMinMax.x,rotateMinMax.y);

        Vector3 nextRotation = new Vector3(xRotate, yRotate);

        currentRotate = Vector3.SmoothDamp(currentRotate, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotate;
        transform.position = target.position - transform.forward * disFromTarget;
    }

}
