using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smootTime;
    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPos = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, _smootTime);
    }
}
