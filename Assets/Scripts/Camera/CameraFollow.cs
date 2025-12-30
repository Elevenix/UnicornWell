using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float heightFollow = 2;
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private bool followBothVertically = false;

    private Vector3 _refVelocity;
    private Vector3 _pointToGo;

    private void Awake()
    {
        _pointToGo = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target.position.y - transform.position.y > heightFollow)
        {
            _pointToGo = new Vector3(0, target.position.y, transform.position.z);
        }

        if (followBothVertically)
        {
            if (target.position.y - transform.position.y < -heightFollow)
            {
                _pointToGo = new Vector3(0, target.position.y, transform.position.z);
            }
        }

        transform.position = Vector3.SmoothDamp(transform.position, _pointToGo, ref _refVelocity, smoothTime);
    }
}
