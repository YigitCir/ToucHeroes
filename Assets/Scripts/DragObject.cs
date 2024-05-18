using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera _camera;
    private GameObject _draggedObject;
    private Vector3 _offset;
    private float _zCoord;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    _draggedObject = hit.collider.gameObject;
                    _zCoord = _camera.WorldToScreenPoint(_draggedObject.transform.position).z;
                    _offset = _draggedObject.transform.position - GetMouseWorldPos();
                }
            }
        }

        if (Input.GetMouseButton(0) && _draggedObject != null)
        {
            _draggedObject.transform.position = GetMouseWorldPos() + _offset;
        }

        if (Input.GetMouseButtonUp(0) && _draggedObject != null)
        {
            _draggedObject = null;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _zCoord;
        return _camera.ScreenToWorldPoint(mousePoint);
    }
}