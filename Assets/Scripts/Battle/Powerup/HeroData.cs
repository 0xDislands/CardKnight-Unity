using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroData
{
    public HeroId id;
    public List<PowerupId> powerUps;
    public string name;
    public string description;
    public Sprite sprite;
    public Sprite[] skins;
    public int selectSkin;
}