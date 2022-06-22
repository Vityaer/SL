
using UnityEngine;
using System.Collections;

internal class EnemyFight2D : MonoBehaviour{
    // point - точка контакта
    // radius - радиус поражения
    // layerMask - номер слоя, с которым будет взаимодействие
    // damage - наносимый урон
    // allTargets - должны-ли получить урон все цели, попавшие в зону поражения
    public static void Action(Vector2 point, float radius, int layerMask, int damage, bool isFacingLeft,string Message){
        GameObject obj = GameObject.FindWithTag("Player");
        if (Physics2D.OverlapCircle(point, radius, 1 << layerMask)){
            if (obj.GetComponent<PlayerHP>()){
                if(obj.GetComponent<PlayerHP>().HP > 0) Information.Instance.CauseOfDeath = Message;
                CameraShake.Shake(0.25f, 0.25f, CameraShake.ShakeMode.XY);
                obj.GetComponent<PlayerHP>().GetDamage(damage, isFacingLeft);
            }
            return;
        }
    }
    public static void Action(Vector2 point, float radius, int layerMask, int damage, bool isFacingLeft, bool stunning,string Message){
        GameObject obj = GameObject.FindWithTag("Player");
        if (Physics2D.OverlapCircle(point, radius, 1 << layerMask)){
            if (obj.GetComponent<PlayerHP>()){
                    if(obj.GetComponent<PlayerHP>().HP > 0) Information.Instance.CauseOfDeath = Message;
                    obj.GetComponent<PlayerHP>().GetDamage(damage, isFacingLeft);
                    if (stunning){stun();}
            }
            return;
        }
    }
    public static void stun() {
        GameObject obj = GameObject.FindWithTag("Player");
        if(obj.GetComponent<RoosterScriptWithShield>() != null){
            if(!obj.GetComponent<RoosterScriptWithShield>().Block){
                CameraShake.Shake(1, 1, CameraShake.ShakeMode.XY);
                obj.GetComponent<RoosterScriptWithShield>().Stun = true;
                obj.GetComponent<RoosterScriptWithShield>().StunTime = 1f;
            }else{
                CameraShake.Shake(0.8f, 0.8f, CameraShake.ShakeMode.XY);
                obj.GetComponent<RoosterScriptWithShield>().Stun = true;
                obj.GetComponent<RoosterScriptWithShield>().StunTime = 0.3f;
            }
        }
        if(obj.GetComponent<RoosterScript>() != null){
            CameraShake.Shake(1, 1, CameraShake.ShakeMode.XY);
            obj.GetComponent<RoosterScript>().Stun = true;
            obj.GetComponent<RoosterScript>().StunTime = 1f;
        }
        if(obj.GetComponent<RoosterScriptWizard>() != null){
            CameraShake.Shake(1, 1, CameraShake.ShakeMode.XY);
            obj.GetComponent<RoosterScriptWizard>().Stun = true;
            obj.GetComponent<RoosterScriptWizard>().StunTime = 1f;
        }
    }
}