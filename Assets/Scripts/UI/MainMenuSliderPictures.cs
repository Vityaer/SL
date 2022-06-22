using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using HelpFuction;

public class MainMenuSliderPictures : MonoBehaviour{

	public List<Sprite> listSprites = new List<Sprite>();
	public List<Image> listPlaceForImage = new List<Image>();
	int numSprite = 0, currentPlace = 0;
	public float timeShow;
	void Start(){
		NextSlide();
	}
	private GameTimer SlideTimer;
	private void NextSlide(){
		SlideTimer = TimerScript.Timer.StartTimer(2 * timeShow + 1.5f, NextSlide);
		listPlaceForImage[currentPlace].DOFade(0f, timeShow);
		int newNum = Random.Range(0, listSprites.Count);
		while(numSprite == newNum) newNum = Random.Range(0, listSprites.Count);
		numSprite = newNum;

		newNum = Random.Range(0, listPlaceForImage.Count);
		while(currentPlace == newNum) newNum = Random.Range(0, listPlaceForImage.Count);
		currentPlace = newNum;
		
		listPlaceForImage[currentPlace].sprite = listSprites[numSprite];
		listPlaceForImage[currentPlace].DOFade(1f, timeShow);
	}
	void OnDestroy(){
		TimerScript.Timer.StopTimer(SlideTimer);
	}
}
