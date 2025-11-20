using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragTool : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private float scaleFactor;
    private Vector2 startingPos;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public int gameNo = 1;

    private void Start()
    {
        scaleFactor = Game4.instance.canvas.scaleFactor;
        startingPos = transform.localPosition;

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = Game4.instance.canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        //image.color = new Color32(255, 255, 255, 200);
        //transform.localScale = Vector3.one * 2.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            if (results[0].gameObject.name.StartsWith("Target") && results[0].gameObject.CompareTag(gameObject.tag))
            {
                //SoundManager.instance.PlaySound(2);
                if (gameNo.Equals(1) || gameNo.Equals(4))
                {
                    Destroy(results[0].gameObject.transform.GetChild(0).gameObject);
                    transform.SetParent(results[0].gameObject.transform);
                    transform.localPosition = Vector3.zero;
                    image.raycastTarget = false;
                    Game4.instance.doneSteps++;
                    SoundManager.instance.PlaySound(6);

                    transform.GetComponent<MoveCharacter>().enabled = true;
                }
                else if (gameNo.Equals(3) || gameNo.Equals(6))
                {
                    results[0].gameObject.transform.parent.GetComponent<Image>().sprite = image.sprite;
                    Destroy(gameObject);
                    Game4.instance.doneSteps++;
                    SoundManager.instance.PlaySound(6);
                }
                else if (gameNo.Equals(2) || gameNo.Equals(5))
                {
                    results[0].gameObject.GetComponent<Image>().sprite = image.sprite;
                    Destroy(gameObject);
                    Game4.instance.doneSteps++;
                    SoundManager.instance.PlaySound(6);
                }                
            }
            else
            {
                ResetShape();
            }
        }
        else
        {
            ResetShape();
        }
    }

    private void ResetShape()
    {
        image.raycastTarget = true;
        //image.color = Color.white;
        //transform.localScale = Vector3.one;
        transform.DOLocalMove(startingPos, 0.3f);

        SoundManager.instance.PlaySound(7);
    }
}