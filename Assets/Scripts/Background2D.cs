using UnityEngine;
using System.Collections;

public class Background2D : MonoBehaviour {

	public Transform BG1;
	public float speedBG1 = 0.001f;

	public Transform BG2;
	public float speedBG2 = 0.003f;

	public Transform BG3;
	public float speedBG3 = 0.005f;

	public Transform BG4;
	public float speedBG4 = 0.007f;

	public Transform BG5;
	public float speedBG5 = 0.009f;

	public Transform BG6;
	public float speedBG6 = 0.011f;
	private Camera cam;
	public float speedY;

	void Awake (){
		cam = GetComponent<Camera>();
	}

	Vector3 GetVector(Vector3 position, float speed){
		float posX = position.x;
		posX += cam.velocity.x * speed * Time.deltaTime;
		float posY = position.y;
		posY += cam.velocity.y * speedY  * Time.deltaTime;
		return new Vector3(posX, posY, position.z);
	}

	void Update () {
		if(BG1) BG1.position = GetVector(BG1.position, speedBG1);
		if(BG2) BG2.position = GetVector(BG2.position, speedBG2);
		if(BG3) BG3.position = GetVector(BG3.position, speedBG3);
		if(BG4) BG4.position = GetVector(BG4.position, speedBG4);
		if(BG5) BG5.position = GetVector(BG5.position, speedBG5);
		if(BG6) BG6.position = GetVector(BG6.position, speedBG6);
	}
}