using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{
    public enum eNormalType
    {
        TYPE_ONE,
        TYPE_TWO,
        TYPE_THREE,
        TYPE_FOUR,
        TYPE_FIVE,
        TYPE_SIX,
        TYPE_SEVEN
    }

    public eNormalType ItemType;

    public static eNormalType GetTypeFromPrefabName(string prefabName)
    {
        eNormalType type = eNormalType.TYPE_ONE;
        switch (prefabName)
        {
            case Constants.PREFAB_NORMAL_TYPE_ONE:
                type = eNormalType.TYPE_ONE;
                break;
            case Constants.PREFAB_NORMAL_TYPE_TWO:
                type = eNormalType.TYPE_TWO;
                break;
            case Constants.PREFAB_NORMAL_TYPE_THREE:
                type = eNormalType.TYPE_THREE;
                break;
            case Constants.PREFAB_NORMAL_TYPE_FOUR:
                type = eNormalType.TYPE_FOUR;
                break;
            case Constants.PREFAB_NORMAL_TYPE_FIVE:
                type = eNormalType.TYPE_FIVE;
                break;
            case Constants.PREFAB_NORMAL_TYPE_SIX:
                type = eNormalType.TYPE_SIX;
                break;
            case Constants.PREFAB_NORMAL_TYPE_SEVEN:
                type = eNormalType.TYPE_SEVEN;
                break;
        }

        return type;
    }


    public void SetType(eNormalType type)
    {
        ItemType = type;
    }

    protected override string GetPrefabName()
    {
        string prefabname = string.Empty;
        switch (ItemType)
        {
            case eNormalType.TYPE_ONE:
                prefabname = Constants.PREFAB_NORMAL_TYPE_ONE;
                break;
            case eNormalType.TYPE_TWO:
                prefabname = Constants.PREFAB_NORMAL_TYPE_TWO;
                break;
            case eNormalType.TYPE_THREE:
                prefabname = Constants.PREFAB_NORMAL_TYPE_THREE;
                break;
            case eNormalType.TYPE_FOUR:
                prefabname = Constants.PREFAB_NORMAL_TYPE_FOUR;
                break;
            case eNormalType.TYPE_FIVE:
                prefabname = Constants.PREFAB_NORMAL_TYPE_FIVE;
                break;
            case eNormalType.TYPE_SIX:
                prefabname = Constants.PREFAB_NORMAL_TYPE_SIX;
                break;
            case eNormalType.TYPE_SEVEN:
                prefabname = Constants.PREFAB_NORMAL_TYPE_SEVEN;
                break;
        }

        return prefabname;
    }

    internal override bool IsSameType(Item other)
    {
        NormalItem it = other as NormalItem;

        return it != null && it.ItemType == this.ItemType;
    }
}
