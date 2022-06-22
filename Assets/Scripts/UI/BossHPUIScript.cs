using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHPUIScript : MonoBehaviour{
	private Canvas canvas;
	public Slider sliderHP;
	public Image imageBackgroundSlider, imageSlider;
	private float maxHP = 0f;
	private static BossHPUIScript instance;
	public static BossHPUIScript Instance{get => instance;}
	void Awake(){
		instance = this; 
		canvas = GetComponent<Canvas>();
		sliderHP.value = 0f;
	}
	private EnemyHP enemyHP;
	public void OpenSlider(EnemyHP enemyHP){
		this.enemyHP = enemyHP;
		enemyHP.RegisterOnChangeHP(ChangeHP);
		this.maxHP = enemyHP.HP;
		sliderHP.DOValue(1f, 0.5f);
		canvas.enabled = true;
		imageBackgroundSlider.DOFade(0.4f, 0.5f);
		imageSlider.DOFade(1f, 0.5f);
	}

	public void ChangeHP(float currentHP){
		sliderHP.DOValue(currentHP/maxHP, 0.1f);
		if(currentHP < 1f){
			imageBackgroundSlider.DOFade(0f, 1f);
			imageSlider.DOFade(0f, 1f);
			enemyHP.UnRegisterOnChangeHP(ChangeHP);
		}
	}
}
