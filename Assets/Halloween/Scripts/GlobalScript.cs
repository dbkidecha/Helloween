using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript instance;

    [SerializeField] private GameObject loadScreen;
    [SerializeField] private AudioSource sound;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScreen(int screenNo)
    {
        loadScreen.SetActive(true);
        loadScreen.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
        StartCoroutine(ChangeScene(screenNo));
    }

    public IEnumerator ChangeScene(int screenNo)
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(screenNo);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void CloseLoading()
    {
        StartCoroutine(IE_CloseLoading());
    }

    private IEnumerator IE_CloseLoading()
    {
        yield return new WaitForSeconds(0.5f);
        sound.Stop();
        loadScreen.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(() =>
        {
            loadScreen.SetActive(false);
        });
    }

    public void PlaySound()
    {
        if (sound != null)
            sound.Play();
    }
}