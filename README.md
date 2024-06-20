# Project's design review
The project's design allows easy expansion, for example adding new levels, new difficulties, custom level editors, ... The project's organization is already clear and easy to navigate, though there can be changes, such as using namespace to separate different parts of the game.

Personally, I feel like using constant string names to get prefabs can cause problems while making changes to the project. My idea is to have prefabs handled similar to the item skins, that is to have an object holding an Enum refering to the prefab and the prefab object itself. Adding and getting these objects from the object pool would be easier than the way its currently handled.

```
[System.Serializable]
public class PrefabData
{
    public MyEnum.MyPrefabType myPrefabType;
    public GameObject prefab;
}
```

Then in the object pool we would hold arrays of PrefabDatas instead of GameObjects.

Another way of doing this is to have separate scripts on the prefabs themselves, which hold necessary data ( in this case only the type but as the project expands more info can be added).

These changes aim to reduce the complexity of getting Prefabs based on their types. If we separate the object pool to smaller sets based on their types, we can easily get the needed object.
