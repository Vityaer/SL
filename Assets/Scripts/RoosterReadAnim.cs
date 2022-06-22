using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoosterReadAnim : MonoBehaviour {

	public Sprite img1;
	public Sprite img2;
	public Sprite img3;
	Image Rooster;
	float Timer;
	int idImg = 1;
	public float RestTime;
	// Use this for initialization
	void Start () {
		Rooster = GetComponent<Image>(); 
		Timer = RestTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(Timer > 0){
			Timer -= Time.deltaTime; 
		}else{
			switch (idImg){
				case 1:
					Rooster.sprite = img1;
					idImg = 2;
					break;			 
				case 2:
					Rooster.sprite = img2;
					idImg = 3;
					break;
				case 3:
					Rooster.sprite = img3;
					idImg = 1;
					break;
			}
			Timer = RestTime;
		}
	}
}
