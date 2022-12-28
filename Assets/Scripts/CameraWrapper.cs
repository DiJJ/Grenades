using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWrapper
{
    private Camera _camera;
    private Transform _cameraTransform;
    private Vector3 _forward;
    private Vector3 _right;

    public CameraWrapper(Camera camera)
    {
        _camera = camera;
        _cameraTransform = camera.transform;
    }

    public Vector3 Position => _cameraTransform.position;

    public Vector3 PureForward => _cameraTransform.forward;
    public Vector3 PureRight => _cameraTransform.right;

    public Vector3 Forward
    {
        get
        {
            _forward = _cameraTransform.forward;
            _forward.y = 0;
            return _forward.normalized;
        }
    }

    public Vector3 Right
    {
        get
        {
            _right = _cameraTransform.right;
            _right.y = 0;
            return _right.normalized;
        }
    }

    public void SetRotation(Quaternion rotation)
    {
        _cameraTransform.rotation = rotation;
    }
}
