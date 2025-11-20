using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int index;

    private float scaleFactor;
    private Vector2 startingPos;
    private RectTransform rectTransform;
    private CanvasGroup image;
    private Animator animator;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<CanvasGroup>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        scaleFactor = Game6.instance.canvas.scaleFactor;
        startingPos = transform.localPosition;

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = Game6.instance.canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        image.blocksRaycasts = false;
        Round1.instance.round1Hand.SetActive(false);
        Round1.instance.bowlHand.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Round1.instance.bowlHand.SetActive(false);

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
            if (results[0].gameObject.name.StartsWith("Target") && results[0].gameObject.CompareTag(gameObject.name))
            {
                if (gameObject.name.StartsWith("Bowl"))
                {
                    transform.position = results[0].gameObject.transform.position;
                    SoundManager.instance.PlaySound(4);
                    Round1.instance.ShowPowerBtn();
                    return;
                }

                animator.enabled = true;
                image.blocksRaycasts = false;
                if (gameObject.name.Equals(Ingredients.Flour.ToString()))
                    transform.localPosition = new Vector3(-165f, 330f, 0f);
                else if (gameObject.name.Equals(Ingredients.Milk.ToString()))
                    transform.localPosition = new Vector3(209f, 226f, 0f);
                else if (gameObject.name.Equals(Ingredients.BakingPowder.ToString()))
                    transform.localPosition = new Vector3(-118f, 155f, 0f);
                else if (gameObject.name.Equals(Ingredients.Sugar.ToString()))
                    transform.localPosition = new Vector3(-110f, 205f, 0f);
                else if (gameObject.name.Equals(Ingredients.Butter.ToString()))
                    transform.localPosition = new Vector3(140f, 188f, 0f);
                else if (gameObject.name.Equals(Ingredients.Egg.ToString()))
                    transform.localPosition = new Vector3(0f, 234f, 0f);

                Round1.instance.targets[index].SetActive(false);
                Invoke(nameof(ShowIngredient), 0.8f);

                SoundManager.instance.PlaySound(index + 11);
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
        image.blocksRaycasts = true;
        transform.DOLocalMove(startingPos, 0.3f);

        SoundManager.instance.PlaySound(19);
    }

    private void ShowIngredient()
    {
        Round1.instance.inBowlObj[index].SetActive(true);
    }

    public void EndAnimation()
    {
        image.DOFade(0f, 0.25f).OnComplete(() =>
        {
            if (index >= 5)
            {
                Invoke(nameof(ShowMixer), 1f);
            }
            Round1.instance.targets[index + 1].SetActive(true);
            gameObject.SetActive(false);
        });
    }

    private void ShowMixer()
    {
        Round1.instance.ShowMixer();
    }
}