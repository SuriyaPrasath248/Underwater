using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class swimmer : MonoBehaviour
{
    [Header("values")]
    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 2f;
    [SerializeField] float minForce = 2f;
    [SerializeField] float minTimeBetweenStorkes = 2f;

    [Header("Refrences")]
    [SerializeField] InputActionReference leftControlSwimReference;
    [SerializeField] InputActionReference rightControlSwimReference;
    [SerializeField] InputActionReference leftControlVelocity;
    [SerializeField] InputActionReference rightControlVelocity;
    [SerializeField] Transform trackingReference;
    public Text debugConsol;
    Rigidbody _rigidbody;
    float _coolDownTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

    }

    private void FixedUpdate()
    {

        _coolDownTimer += Time.fixedDeltaTime;
        if (_coolDownTimer > minTimeBetweenStorkes && leftControlSwimReference.action.IsPressed() && rightControlSwimReference.action.IsPressed())
        {
            var leftHandVelocity = leftControlVelocity.action.ReadValue<Vector3>();
            var rightHandVelocity = rightControlVelocity.action.ReadValue<Vector3>();
            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
            localVelocity *= -1;
            debugConsol.text = "Velocity is :" + localVelocity;
            if (localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _coolDownTimer = 0f;
            }
        }

        if (_rigidbody.velocity.sqrMagnitude > 0.01f)
        {
            _rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
        }

    }



}
