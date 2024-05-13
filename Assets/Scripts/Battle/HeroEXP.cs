using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class HeroEXP : MonoBehaviour
{
    [SerializeField] TextLevelUp textLevelUp;
    private Hero hero;
    private static List<int> requireToLevelUp = new List<int>();
    private void Awake()
    {
        hero = GetComponent<Hero>();
        requireToLevelUp = GetLevelUpList();
    }
    public static List<int> GetLevelUpList()
    {
        return new List<int>()
        {
            3,
            4,
            5,
            5,
            6,
            6,
        };
    }
    public static int GetExpNeedForNextLevel(int level)
    {
        if (level >= requireToLevelUp.Count) level = requireToLevelUp.Count - 1;
        return requireToLevelUp[level];
    }
    public void AddEXP(float exp)
    {
        var heroData = hero.heroData;
        float neededExp = GetExpNeedForNextLevel(heroData.level);
        heroData.currentExp += exp;
        heroData.totalExp += exp;
        if (heroData.currentExp > neededExp)
        {
            int oldLevel = heroData.level;
            heroData.currentExp -= neededExp;
            heroData.level++;
            Gameplay.Instance.heroProgressBarExp.SetValue(0);
            SimpleObjectPool.Instance.GetObjectFromPool(textLevelUp, transform.position);

            if (Gameplay.Instance.state == GameplayState.Lose ||
                Gameplay.Instance.state == GameplayState.Win)
            {
                return;
            }

            DOVirtual.DelayedCall(0.5f, () =>
            {
                PopupManager.Instance.ShowInQueue(() =>
                {
                    Gameplay.Instance.popupLevelUp.ShowLevelUp(oldLevel);
                });
                UnlockSkills();
            });
        }
    }
    private void UnlockSkills()
    {
        var dataManager = DataManager.Instance;
        var powerUpDatas = DataManager.Instance.dicHero[CardManager.selectedHero].powerUps;
        for (int i = 0; i < powerUpDatas.Count; i++)
        {
            var id = powerUpDatas[i];
            var heroData = hero.heroData;
            if (heroData.level == dataManager.dicPowerUp[id].unlockLevel)
            {
                PopupManager.Instance.ShowInQueue(() =>
                {
                    Gameplay.Instance.popupPoweupUnlocked.ShowUnlock(id);
                });
            }
        }
    }
}