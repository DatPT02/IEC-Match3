using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObject/Skin/NormalItemSkin")]
public class NormalItemSkinData : ScriptableObject
{
    [SerializeField] public NormalSkinData[] Skins;

    public Sprite GetSkinByPrefabName(string prefabName)
    {
        for (int i = 0; i < Skins.Length; i++)
        {
            if (GetPrefabName(Skins[i].NormalType).Equals(prefabName))
                return Skins[i].ItemSprite;
        }

        return null;
    }

    public string GetPrefabName(NormalItem.eNormalType normalType)
    {
        string prefabName = null;
        switch (normalType)
        {
            case NormalItem.eNormalType.TYPE_ONE:
                prefabName = Constants.PREFAB_NORMAL_TYPE_ONE;
                break;
            case NormalItem.eNormalType.TYPE_TWO:
                prefabName = Constants.PREFAB_NORMAL_TYPE_TWO;
                break;
            case NormalItem.eNormalType.TYPE_THREE:
                prefabName = Constants.PREFAB_NORMAL_TYPE_THREE;
                break;
            case NormalItem.eNormalType.TYPE_FOUR:
                prefabName = Constants.PREFAB_NORMAL_TYPE_FOUR;
                break;
            case NormalItem.eNormalType.TYPE_FIVE:
                prefabName = Constants.PREFAB_NORMAL_TYPE_FIVE;
                break;
            case NormalItem.eNormalType.TYPE_SIX:
                prefabName = Constants.PREFAB_NORMAL_TYPE_SIX;
                break;
            case NormalItem.eNormalType.TYPE_SEVEN:
                prefabName = Constants.PREFAB_NORMAL_TYPE_SEVEN;
                break;
        }

        return prefabName;
    }
}

[System.Serializable]
public class NormalSkinData
{
    public NormalItem.eNormalType NormalType;
    public Sprite ItemSprite;
}