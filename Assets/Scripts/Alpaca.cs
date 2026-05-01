using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Alpaca : MonoBehaviour
{
    private bool isPlaying = false;
    private Sequence sequence;
    [SerializeField] private AnimationCurve animationCurve;
    private void Squashed()
    {
        if (sequence != null && sequence.IsActive()) sequence.Kill();
        
        this.sequence = DOTween.Sequence();
    
        var tween = this.transform.DOScaleY(1.0f, 0.2f);
        tween.SetEase(animationCurve);
        sequence.Append(tween);

        this.sequence.AppendInterval(0.2f);
        
        var tween2 = this.transform.DOScaleY(5.0f, 0.2f);
        tween2.SetEase(Ease.OutBounce);
        sequence.Append(tween2);
    }
    
    IEnumerator PlaySequence()
    {
        isPlaying = true;
        this.Squashed();
        this.sequence.Play();
        yield return this.sequence.WaitForCompletion();
        isPlaying = false;
    }
    
    public void OnAlpacaStomped()
    {
        if (isPlaying){return;}
        StartCoroutine(PlaySequence());
    }
}
