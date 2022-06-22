
    using UnityEngine;
using System.Collections;

internal class Fight2D : MonoBehaviour{
    // функция возвращает ближайший объект из массива, относительно указанной позиции
    static GameObject NearTarget(Vector3 position, Collider2D[] array){
        Collider2D current = null;
        GameObject result = null;
        float dist = Mathf.Infinity;
        foreach (Collider2D coll in array){
            if(coll.GetComponent<EnemyHP>()){
                float curDist = Vector3.Distance(position, coll.transform.position);
                if (curDist < dist){
                    current = coll;
                    dist = curDist;
                }
            }
        }
        if(current != null) result = current.gameObject;
        return result;
    }

    // point - точка контакта
    // radius - радиус поражения
    // layerMask - номер слоя, с которым будет взаимодействие
    // damage - наносимый урон
    // allTargets - должны-ли получить урон все цели, попавшие в зону поражения
    public static void Action(Vector2 point, float radius, int layerMask, float damage, bool isFasingLeft){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);
            GameObject obj = NearTarget(point, colliders);
            if (obj != null) 
                obj.GetComponent<EnemyHP>().GetDamage(damage, isFasingLeft);
    }
}