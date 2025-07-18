using UnityEngine;
using Obvious.Soap;

[CreateAssetMenu(fileName = "scriptable_list_" + nameof(Enemy), menuName = "Soap/ScriptableLists/"+ nameof(Enemy))]
public class ScriptableListEnemy : ScriptableList<Enemy>
{
    
}
