using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StaminaSliderScript : MonoBehaviour{
	private Slider sliderStamina;
	private RectTransform rect;
    private static StaminaSliderScript instance;
    public  static StaminaSliderScript Instance{get => instance;}
	private Image imageBackgroundSlider, imageSlider;
    public float maxStamina = 30f;
    private float currentStamina;
    void Awake(){
    	if(instance == null) {instance = this;} else { Destroy(this); }
    	imageBackgroundSlider = transform.Find("Background").GetComponent<Image>();
    	imageSlider = transform.Find("Fill Area/Fill").GetComponent<Image>(); 
        sliderStamina = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();
    }

    Vector3 showSizeDelta = new Vector3(1f, 1f, 1f), hideSizeDelta = new Vector3(0f, 1f, 1f);
    public void ChangeStamina(float currentStamina){
    	this.currentStamina = currentStamina;
		sliderStamina.DOValue(currentStamina/maxStamina, 0.1f);
		if(isProcess == false){
			isProcess = true;
			if(currentStamina >= maxStamina){
				imageBackgroundSlider.DOFade(0f, 1f).OnComplete(FinishInvisible);
				imageSlider.DOFade(0f, 1f);
			}else{
				imageBackgroundSlider.DOFade(0.4f, 0.175f).OnComplete(FinishVisible);
				imageSlider.DOFade(1f, 0.175f);
				rect.DOScale(showSizeDelta, 0.175f);
			}
		}
	}

	private bool isVisible = true, isProcess = false;
	public void FinishInvisible(){
		isVisible = false;
		isProcess = false;
		rect.localScale = hideSizeDelta;
		ChangeStamina(currentStamina);
	}
	public void FinishVisible(){
		isVisible = true;
		isProcess = false;
		ChangeStamina(currentStamina);
	}
}
