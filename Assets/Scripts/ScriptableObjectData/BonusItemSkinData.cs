using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObject/Skin/BonusItemSkin")]
public class BonusItemSkinData : ScriptableObject
{
    [SerializeField] public BonusSkinData[] Skins;

    public Sprite GetSkinByPrefabName(string prefabName)
    {
        for (int i = 0; i < Skins.Length; i++)
        {
            if (GetPrefabName(Skins[i].BonusType).Equals(prefabName))
                return Skins[i].ItemSprite;
        }

        return null;
    }

    private string GetPrefabName(BonusItem.eBonusType bonusType)
    {
        string prefabname = string.Empty;
        switch (bonusType)
        {
            case BonusItem.eBonusType.NONE:
                break;
            case BonusItem.eBonusType.HORIZONTAL:
                prefabname = Constants.PREFAB_BONUS_HORIZONTAL;
                break;
            case BonusItem.eBonusType.VERTICAL:
                prefabname = Constants.PREFAB_BONUS_VERTICAL;
                break;
            case BonusItem.eBonusType.ALL:
                prefabname = Constants.PREFAB_BONUS_BOMB;
                break;
        }

        return prefabname;
    }
}

[System.Serializable]
public class BonusSkinData
{
    public BonusItem.eBonusType BonusType;
    public Sprite ItemSprite;
}