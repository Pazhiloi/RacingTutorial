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
  float carSpeed, carSpeedConverted, motorTorque, tireAngle, vertical = 0f, horizontal = 0f;

  bool handBrake = false;
  Rigidbody carRigidbody;

  private void Start()
  {
    carRigidbody = GetComponent<Rigidbody>();

    if (carRigidbody != null)
    {
      carRigidbody.centerOfMass = COM.localPosition;
    }
  }

  private void Update() {
    GetInputs();
    CalculateCarMovement();
    CalculateSteering();
  }

  private void GetInputs()
  {
    if (control == ControlMode.Keyboard)
    {
      horizontal = Input.GetAxis("Horizontal");
      vertical = Input.GetAxis("Vertical");
    }
  }

  private void CalculateCarMovement()
  {
    carSpeed = carRigidbody.velocity.magnitude;
    carSpeedConverted = Mathf.Round(carSpeed * 3.6f);

    // Apply Braking
    HandleBrake();

    ApplyMotorTorque();
  }

  private void CalculateSteering(){
    tireAngle = maximumSteeringAngle * horizontal;
    FrontWheelLeftCollider.steerAngle = tireAngle;
    FrontWheelRightCollider.steerAngle = tireAngle;
  }



  private void HandleBrake(){
    if (Input.GetKey(KeyCode.Space))
    {
      handBrake = true;
    }
    else
    {
      handBrake = false;
    }

    if (handBrake)
    {
      motorTorque = 0f;
      ApplyBrake();
    }
    else
    {
      ReleaseBrake();
      if (carSpeedConverted < maximumSpeed)
      {
        motorTorque = maximumMotorTorque * vertical;
      }
      else
      {
        motorTorque = 0f;
      }
    }
  }

  private void ApplyMotorTorque(){
    if (carType == CarType.FrontWheelDrive)
    {
      FrontWheelLeftCollider.motorTorque = motorTorque;
      FrontWheelRightCollider.motorTorque = motorTorque;
    } else if (carType == CarType.RearWheelDrive)
    {
      BackWheelLeftCollider.motorTorque = motorTorque;
      BackWheelRightCollider.motorTorque = motorTorque;
    } else if (carType == CarType.FourWheelDrive)
    {
      FrontWheelLeftCollider.motorTorque = motorTorque;
      FrontWheelRightCollider.motorTorque = motorTorque;
      BackWheelLeftCollider.motorTorque = motorTorque;
      BackWheelRightCollider.motorTorque = motorTorque;
    }
  }




  private void ApplyBrake()
  {
    FrontWheelLeftCollider.brakeTorque = brakePower;
    FrontWheelRightCollider.brakeTorque = brakePower;
    BackWheelLeftCollider.brakeTorque = brakePower;
    BackWheelRightCollider.brakeTorque = brakePower;
  }
  private void ReleaseBrake()
  {
    FrontWheelLeftCollider.brakeTorque = 0;
    FrontWheelRightCollider.brakeTorque = 0;
    BackWheelLeftCollider.brakeTorque = 0;
    BackWheelRightCollider.brakeTorque = 0;
  }

}
