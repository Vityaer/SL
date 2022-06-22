using UnityEngine;
using System.Collections;

public class GoToLevel : MonoBehaviour {

	 void OnTriggerStay2D(Collider2D col)
    {
       if (col.gameObject.name == "Level_2")
        {
           FadeInOut.nextLevel = "Level_2"; 
           FadeInOut.sceneEnd = true;
           Destroy(col.gameObject);
        }
    }

}
