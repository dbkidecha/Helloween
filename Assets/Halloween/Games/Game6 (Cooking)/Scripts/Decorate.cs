using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Decorate : MonoBehaviour
{
    public Button[] options;
    public GameObject[] cones;
    public GameObject[] hints;
    public GameObject[] fills;

    private int _coneIndex = 0;
    public int coneIndex
    {
        get { return _coneIndex; }
        set
        {
            _coneIndex = value;

            if (value < options.Length)
                options[value].interactable = true;
            else
                StartCoroutine(Game6.instance.ShowParticle(2));

            SoundManager.instance.PlaySound(8);
        }
    }

    private int _hintIndex = 0;
    public int hintIndex
    {
        get { return _hintIndex; }
        set
        {
            _hintIndex = value;
            if (value < hints.Length)
                hints[value].SetActive(true);
            fills[value - 1].SetActive(true);
            hints[value - 1].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowOptions());
    }

    private IEnumerator ShowOptions()
    {
        yield return new WaitForSeconds(1.5f);
        options[coneIndex].interactable = true;
        for (int i = 0; i < options.Length; i++)
        {
            options[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        hints[hintIndex].SetActive(true);
    }

    public void SelectCone(int index)
    {
        cones[index].SetActive(true);
    }
}