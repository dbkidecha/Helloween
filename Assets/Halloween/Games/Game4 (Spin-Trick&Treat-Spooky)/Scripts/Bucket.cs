using UnityEngine;
using UnityEngine.EventSystems;

public class Bucket : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Camera _camera;
    private Vector2 offset;
    [SerializeField] private GameObject hand;

    // Start is called before the first frame update
    void Start()
    {

    }

    //private void OnMouseDown()
    //{
    //    offset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
    //}

    //private void OnMouseDrag()
    //{
    //    Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 pos = mousePosition + offset;
    //    transform.position = new Vector3(Mathf.Clamp(pos.x, 0f, 7f), pos.y, 0f);
    //}

    //private void OnMouseUp()
    //{
    //    offset = Vector2.zero;
    //}

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
        hand.SetActive(false);        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = mousePosition + offset;
        transform.position = new Vector3(Mathf.Clamp(pos.x, -5f, 5f), pos.y, 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector2.zero;
    }
}