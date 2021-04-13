using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    [SerializeField] private Wall LeftWall;
    [SerializeField] private Wall RightWall;

    private float _defaultWidth;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _defaultWidth = _camera.orthographicSize * _camera.aspect;

        RightWall.transform.position = new Vector3(_defaultWidth, 0, 0);
        LeftWall.transform.position = new Vector3(-_defaultWidth, 0, 0);
    }

    private void Update()
    {
        _camera.orthographicSize = _defaultWidth / _camera.aspect;
    }
}
