using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween fadeTween;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(TestFade());
    }

    // public void FadeEnable()
    // {
    //     StartCoroutine(TestFade());
    // }

    // public void FadeIn(float duration)
    // {
    //     Debug.Log("Fade In");
    //     LevelManager.Instance.FadingPanelObj.transform.GetChild(0).DOLocalMoveY(620, 1f);
    //     Fade(1f, duration, () =>
    //     {
    //         
    //         canvasGroup.interactable = true;
    //         canvasGroup.blocksRaycasts = true;
    //     });
    // }

    // public void FadeOut(float duration)
    // {
    //     Debug.Log("Fade Out");
    //     LevelManager.Instance.FadingPanelObj.transform.GetChild(0).DOLocalMoveY(456, 1f);
    //     
    //     Fade(0f, duration, () =>
    //     {
    //         canvasGroup.interactable = false;
    //         canvasGroup.blocksRaycasts = false;
    //     });
    // }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }

    // private IEnumerator TestFade()
    // {
    //     LevelManager.Instance.boostFadingDone = true;
    //     FadeIn(1f);
    //     yield return new WaitForSeconds(2f);
    //     FadeOut(1f);
    // }
}