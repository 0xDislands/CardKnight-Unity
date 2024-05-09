using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLifeSteal : MonoBehaviour
{
    public Vector2Int pos;
    public Card card;
    public void OnClick()
    {
        var monster = card.GetComponent<Monster>();
        if (monster)
        {
            card.gameObject.SetActive(false);
            monster.TakeDamage(new DamageData(monster.monsterData.maxHp), out var dead);
            CardManager.Instance.hero.AddHP(new DamageData((int)(monster.monsterData.maxHp * 0.25f)));
        }
        var powers = FindObjectsOfType<PowerUpLifeSteal>();
        for (int i = 0; i < powers.Length; i++)
        {
            powers[i].gameObject.SetActive(false);
        }
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}
