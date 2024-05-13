using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ProgressBarHeroEXP : MonoBehaviour
{
    public Image imgFillValue;
    private float currentValue;

    private void Start()
    {
        SetValue(0);
    }

    private void Update()
    {
        if (currentValue != CardManager.Instance.hero.heroData.currentExp)
        {
            currentValue = CardManager.Instance.hero.heroData.currentExp;
            float ratio = CardManager.Instance.hero.heroData.currentExp / CardManager.Instance.hero.exp.expRequire;
            DOVirtual.Float(imgFillValue.fillAmount, ratio, 1f, (float f)=> {
                imgFillValue.fillAmount = f;
            });
        }
    }

    public void SetValue(float currentValue)
    {
        this.currentValue = currentValue;
        float ratio = currentValue / CardManager.Instance.hero.exp.expRequire;
        imgFillValue.fillAmount = ratio;
    }
}
