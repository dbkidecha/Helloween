using UnityEngine;
using UnityEngine.EventSystems;

public class NetBucket : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 offset;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject hand;
    public GameObject netImage;
    public GameObject burnNet;
    public GameObject baseObj;

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);

        if (hand != null)
            hand.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = mousePosition + offset;
        transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector2.zero;
    }
}