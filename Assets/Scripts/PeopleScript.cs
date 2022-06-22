using UnityEngine;
using System.Collections;

public class PeopleScript : MonoBehaviour {
	private bool isFacingLeft = true;
	public Transform tr;

    public Vector2 RPosition;
    private Information information;
	// Use this for initialization
	void Start () {
    	tr = GetComponent<Transform>();
        information = Information.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        InformationAboutRooster();
        if((RPosition.x < tr.position.x)&&!(isFacingLeft)){
        	isFacingLeft = !isFacingLeft;
        	Flip();
        }
        if((RPosition.x > tr.position.x)&&(isFacingLeft)){
        	Flip();
        	isFacingLeft = !isFacingLeft;
        }
	}

    private void Flip(){
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
    void InformationAboutRooster(){
        RPosition = information.position;
    }
}
