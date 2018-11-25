using UnityEngine;

// taken from http://www.mediafire.com/file/cd6r6vpr7znfa6c/CarCamera.zip
// Credit: Why485 on https://www.youtube.com/watch?v=VyOGduSxyqk

public class CameraController : MonoBehaviour
{
    Transform rootNode;
    Transform car;
    Rigidbody carPhysics;

    //If car speed is below this value, then the camera will default to looking forwards.
    public float rotationThreshold = 1f;
    
    //How closely the camera follows the car's position. The lower the value, the more the camera will lag behind.
    public float cameraStickiness = 10.0f;
    
    //How closely the camera matches the car's velocity vector. The lower the value, the smoother the camera rotations, but too much results in not being able to see where you're going.
    public float cameraRotationSpeed = 5.0f;

    void Awake()
    {
        rootNode = GetComponent<Transform>();
        car = rootNode.parent.GetComponent<Transform>();
        carPhysics = car.GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Detach the camera so that it can move freely on its own.
        rootNode.parent = null;
    }

    void FixedUpdate()
    {
        Quaternion view_angle;

        // Moves the camera to match the car's position.
        rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);

        // If the car isn't moving, default to looking forwards. Prevents camera from freaking out with a zero velocity getting put into a Quaternion.LookRotation
        if (carPhysics.velocity.magnitude < rotationThreshold)
            view_angle = Quaternion.LookRotation(car.forward);
        else
            view_angle = Quaternion.LookRotation(carPhysics.velocity.normalized);

        // Rotate the camera towards the velocity vector.
        view_angle = Quaternion.Slerp(rootNode.rotation, view_angle, cameraRotationSpeed * Time.fixedDeltaTime);                
        rootNode.rotation = view_angle;
    }
}