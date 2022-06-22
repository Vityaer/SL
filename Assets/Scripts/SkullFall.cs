using UnityEngine;
using System.Collections;
using HelpFuction;
public class SkullFall : MonoBehaviour {

	public GameObject Skull;
	private Transform tr;
	public float timeCreate = 4.0f;
	public Vector2 startDir = new Vector2(0, 0);
	private TimerScript Timer;
	private GameTimer timerSpawn;
	private GameObject currentObject;
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform>();
        Timer = TimerScript.Timer;
        CreatePrefab();
	}
	void CreatePrefab(){
		if(currentObject == null){
			currentObject = Instantiate(Skull,tr.position,Quaternion.identity);
			currentObject.GetComponent<Rigidbody2D>().velocity = startDir;
		}
        timerSpawn = Timer.StartTimer(timeCreate, CreatePrefab);
	}
}
