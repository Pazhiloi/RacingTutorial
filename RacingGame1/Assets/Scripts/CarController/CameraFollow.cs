using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public float moveSmoothness, rotationSmoothness;

   public Vector3 moveOffset, rotationOffset;

   public Transform carTarget;

   private void LateUpdate() {
    FollowTarget();
   }

   private void FollowTarget(){
    HandleMovement();
    HandleRotation();
   }

   private void HandleMovement(){
    Vector3 targetPos = new Vector3();

    targetPos  = carTarget.TransformPoint(moveOffset);

    transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
   }
   private void HandleRotation(){
    var direction = carTarget.position - transform.position;
    var rotation = new Quaternion();

    rotation = Quaternion.LookRotation(direction + rotationOffset, Vector3.up);

    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSmoothness * Time.deltaTime);
   }
}
