using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FadeInOut : MonoBehaviour {
	
	public static bool sceneEnd;
	public float fadeSpeed = 0.75f;
	public static string nextLevel;
	private Image _image;
	public static bool sceneStarting;

	void Awake (){
		_image = GetComponent<Image>();
		_image.enabled = true;
		sceneStarting = true;
		sceneEnd = false;
		Cursor.visible = true;
	}

	void Update (){
		if(sceneStarting) StartScene();
		if(sceneEnd) EndScene();
	}

	public void StartScene (){
		_image.color = Color.Lerp(_image.color, Color.clear, fadeSpeed * Time.deltaTime);
		if(_image.color.a <= 0.01f){
			_image.color = Color.clear;
			_image.enabled = false;
			sceneStarting = false;
		}
	}

	void EndScene (){
		_image.enabled = true;
		_image.color = Color.Lerp(_image.color, Color.black, fadeSpeed * Time.deltaTime);

		if(_image.color.a >= 0.95f){
			Cursor.visible = false;
			_image.color = Color.black;
			Application.LoadLevel(nextLevel);
		}
	}
}