using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            m_ObjectsOfType["prefabs/" + obj.name] = new List<GameObject>();
        }

        m_ObjectsOfType["prefabs/" + obj.name].Add(newObj);
        newObj.SetActive(false);
    }

    public GameObject GetObject(string prefabName)
    {
        foreach (GameObject obj in m_ObjectsOfType[prefabName])
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        return null;
    }

    public void ResetObject(GameObject obj)
    {
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
