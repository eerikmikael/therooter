using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    [SerializeField]
    private Color fadeColor;
    [SerializeField]
    private Image blackFade;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void FadeFromBlack()
    {
        blackFade.gameObject.SetActive(true);
        blackFade.DOColor(Color.black, 0f);
        blackFade.DOColor(Color.clear, 2f);
    }

    public void FadeToWhite(float time)
    {
        blackFade.DOColor(Color.clear, 0f);
        blackFade.DOColor(fadeColor, time);
    }

    public void FadeFromWhite()
    {
        blackFade.gameObject.SetActive(true);
        blackFade.DOColor(fadeColor, 0f);
        blackFade.DOColor(Color.clear, 2f);
    }
}
