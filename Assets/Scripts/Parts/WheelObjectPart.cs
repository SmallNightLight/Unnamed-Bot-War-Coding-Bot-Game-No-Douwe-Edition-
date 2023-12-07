using ScriptableArchitecture.Data;
using UnityEngine;

public class WheelObjectPart : ObjectPart
{
    [Header("Components")]
    [SerializeField] private WheelCollider _wheelCollider;
    [SerializeField] private Transform _meshTransform;

    private Vector2 _input;
    private float _turnAngle;

    void Update()
    {
        //Replace this with other movement in the future
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        if (GetComponentInParent<Bot>().IsInFight)
        {
            Steer(_input.x);
            Accelerate(_input.y);
            UpdateMeshPosition();
        }
    }

    private void Steer(float horizontalDirection)
    {
        _turnAngle = horizontalDirection * PartData.GetFloat("MaxAngle");
        _wheelCollider.steerAngle = _turnAngle;
    }

    public void Accelerate(float powerInput)
    {
        if (powerInput == 0)
            _wheelCollider.brakeTorque = 100;
        else
            _wheelCollider.brakeTorque = 0;

        _wheelCollider.motorTorque = powerInput * 300 * (PartData.GetBool("Inverted") ? -1 : 1);
    }

    private void UpdateMeshPosition()
    {
        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        _meshTransform.position = pos;
        _meshTransform.rotation = rot;
    }
}