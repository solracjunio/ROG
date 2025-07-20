using UnityEngine;
using Obvious.Soap;
using System.Linq;

[CreateAssetMenu(fileName = "scriptable_list_" + nameof(Enemy), menuName = "Soap/ScriptableLists/" + nameof(Enemy))]
public class ScriptableListEnemy : ScriptableList<Enemy>
{
    public Enemy GetClosest(Vector3 position)
    {
        if (IsEmpty)
            return null;

        var closest = _list.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).First();
        return closest;
    }
}
