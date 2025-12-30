using UnityEngine;

public class LineAsteroid : MonoBehaviour
{
    [SerializeField] private float lineDistance = 20;
    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private LineRenderer line;
    [SerializeField] private float smoothTime = 1;

    private Vector3 _actualPosEndLine;
    private Vector3 _targetPosEndLine;
    private Vector3 _refVelocity;

    private void Awake()
    {
        _actualPosEndLine = Vector3.zero;
    }

    private void FixedUpdate()
    {
        _actualPosEndLine = Vector3.SmoothDamp(_actualPosEndLine, _targetPosEndLine, ref _refVelocity,smoothTime);
        line.SetPosition(1, _actualPosEndLine);
    }

    public void CreateLine(Vector2 _dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir,lineDistance, layerMaskGround);
        line.SetPosition(0, Vector3.zero);
        if (hit)
        {
            _targetPosEndLine = hit.point - (Vector2)transform.position;
        }
        else
        {
            _targetPosEndLine = Vector3.one * _dir * lineDistance;
        }
    }
}
