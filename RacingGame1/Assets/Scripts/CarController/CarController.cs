using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarType carType = CarType.FourWheelDrive;
    public ControlMode control;

    [Header("Wheel GameObjects Meshes")]

    public GameObject FrontWheelLeft;
    public GameObject FrontWheelRight;
    public GameObject BackWheelLeft;
    public GameObject BackWheelRight;
    [Header("WheelColliders")]

    public WheelCollider FrontWheelLeftCollider;
    public WheelCollider FrontWheelRightCollider;
    public WheelCollider BackWheelLeftCollider;
    public WheelCollider BackWheelRightCollider;

  [Header("Movement, Steering and Braking")]
  private float currentSpeed;
  public float maximumMotorTorque;
  public float maximumSteeringAngle = 20f;
  public float maximumSpeed;
  public float brakePower;
  public Transform COM;
  float carSpeed, carSpeedConverted, MotorTorque, tireAngle, vertical = 0f, horizontal = 0f;

  bool handBrake = false;
  Rigidbody carRigidbody;

  private void Start() {
    carRigidbody = GetComponent<Rigidbody>();

    if (carRigidbody != null)
    {
      carRigidbody.centerOfMass = COM.localPosition;
    }
  }
}
 