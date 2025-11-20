using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Round2 : MonoBehaviour
{
    public static Round2 instance;

    public Transform options;
    public Transform shapes;
    public Image dough;
    public Transform plate;
    public GameObject smokeParticle;
    public GameObject oven;
    public Sprite[] doughShapes;
    public Sprite[] wrappedDough;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ShowOptions()
    {
        StartCoroutine(_ShowOptions());
    }

    private IEnumerator _ShowOptions()
    {
        yield return new WaitForSeconds(1f);
        options.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        options.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        options.GetChild(2).gameObject.SetActive(true);
    }

    public void SelectShape(int index)
    {
        shapes.GetChild(index).gameObject.SetActive(true);
        for (int i = 0; i < options.childCount; i++)
        {
            options.GetChild(i).DOLocalMoveX(0f, 0.5f);
        }
        Game6.instance.shapeIndex = index;
    }

    public void ShapeCut(int index)
    {
        SoundManager.instance.PlaySound(6);
        dough.sprite = doughShapes[index];
        shapes.GetChild(index).gameObject.SetActive(false);
        StartCoroutine(ComePlate(index));
    }

    private IEnumerator ComePlate(int index)
    {
        yield return new WaitForSeconds(1f);

        plate.DOLocalMoveY(-125f, 1.5f).OnComplete(() =>
        {
            dough.transform.SetParent(plate);
            plate.DOLocalMoveY(600f, 1.5f).SetEase(Ease.InFlash).OnComplete(() =>
            {
                StartCoroutine(BackPlate(index));
                smokeParticle.SetActive(true);
            });
        });

        oven.transform.DOLocalMoveY(240f, 1f);
    }

    private IEnumerator BackPlate(int index)
    {
        SoundManager.instance.PlaySound(18);
        yield return new WaitForSeconds(3f);
        SoundManager.instance.PlaySound(17);
        yield return new WaitForSeconds(0.5f);
        dough.sprite = wrappedDough[index];
        plate.DOLocalMoveY(-90f, 1.5f).OnComplete(() =>
        {
            oven.transform.DOLocalMoveY(700f, 1f);
        });
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Game6.instance.ShowParticle(1));
    }
}