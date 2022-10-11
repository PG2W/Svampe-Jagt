using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask groundLayer;
    public bool IsGrounded => Physics.CheckSphere(transform.position, 0.3f, groundLayer);

 


    
}
