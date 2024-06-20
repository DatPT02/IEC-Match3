using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [Header("SkinData")]
    [SerializeField] private NormalItemSkinData m_NormalItemSkinData;
    [SerializeField] private BonusItemSkinData m_BonusItemSkinData;

    [Space(5)][Header("Object Pool Data")]
    [SerializeField] private int m_Amount;
    [SerializeField] private GameObject[] m_NormalItems;
    [SerializeField] private GameObject[] m_BonusItems;

    private Dictionary<string, List<GameObject>> m_ObjectsOfType = new Dictionary<string, List<GameObject>>();
    [SerializeField] private Dictionary<string, int> m_InactiveObjectCount = new Dictionary<string, int>(0);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(Instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (GameObject obj in m_NormalItems)
        {
            for (int i = 0; i < m_Amount; i++)
            {
                SetNewObject(obj);
            }
        }        

        foreach (GameObject obj in m_BonusItems)
        {
            for (int i = 0; i < m_Amount; i++)
            {
                SetNewObject(obj);
            }
        }
    }

    void SetNewObject(GameObject obj)
    {
        GameObject newObj = Instantiate(obj, this.transform);

        Sprite sprite = m_NormalItemSkinData.GetSkinByPrefabName("prefabs/" + obj.name);
        if (sprite != null)
            newObj.GetComponent<SpriteRenderer>().sprite = sprite;
        else
        {
            sprite = m_BonusItemSkinData.GetSkinByPrefabName("prefabs/" + obj.name);
            if (sprite != null)
                newObj.GetComponent<SpriteRenderer>().sprite = sprite;
        }


        if (!m_ObjectsOfType.ContainsKey("prefabs/" + obj.name))
        {
            m_ObjectsOfType.Add("prefabs/" + obj.name, new List<GameObject>());
        }

        m_ObjectsOfType["prefabs/" + obj.name].Add(newObj);

        if(!m_InactiveObjectCount.ContainsKey("prefabs/" + obj.name))
        {
            m_InactiveObjectCount.Add("prefabs/" + obj.name, 0);
        }
        m_InactiveObjectCount["prefabs/" + obj.name]++;

        newObj.SetActive(false);
    }

    public GameObject GetObject(string prefabName)
    {
        foreach (GameObject obj in m_ObjectsOfType[prefabName])
        {
            if (!obj.activeInHierarchy)
            {
                m_InactiveObjectCount[prefabName]--;
                return obj;
            }
        }

        return null;
    }

    public NormalItem.eNormalType GetPriorityTypeExcept(HashSet<NormalItem.eNormalType> types)
    {
        HashSet<string> prefabNames = new HashSet<string>();
        int maxInactiveObject = 0;
        string maxInactiveObjectType = "";

        foreach(NormalItem.eNormalType type in types)
        {
            prefabNames.Add(m_NormalItemSkinData.GetPrefabName(type));
        }

        //Find the type with most inactive object for priority
        foreach(KeyValuePair<string, int> entry in m_InactiveObjectCount)
        {
            if (prefabNames.Contains(entry.Key))
                continue;

            if (entry.Value >= maxInactiveObject)
                maxInactiveObjectType = entry.Key;
        }

        //Get random type from list of types with exception types remove and additional priority type added
        List<NormalItem.eNormalType> values = Enum.GetValues(typeof(NormalItem.eNormalType)).OfType<NormalItem.eNormalType>().ToList();
        foreach(NormalItem.eNormalType item in types)
            values.Remove(item);

        values.Add(NormalItem.GetTypeFromPrefabName(maxInactiveObjectType));

        NormalItem.eNormalType result = (NormalItem.eNormalType)values[URandom.Range(0, values.Count)];

        return result;
    }

    public void ResetObject(GameObject obj)
    {
        foreach(KeyValuePair<string, int> entry in m_InactiveObjectCount)
        {
            if(obj.name.Contains(entry.Key.Remove(0, ("prefab/").Length)))
            {
                m_InactiveObjectCount[entry.Key]++;
                break;
            }
        }
        obj.transform.parent = this.transform;
        obj.transform.localScale = Vector3.one;
        obj.SetActive(false);
    }

    public void Reset()
    {
        foreach (KeyValuePair<string, List<GameObject>> entry in m_ObjectsOfType)
        {
            entry.Value.Clear();
        }
    }
}
