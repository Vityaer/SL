using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletScript : MonoBehaviour, IDeath{
	[SerializeField] private EnemyHP enemyHPComponent;
	[SerializeField] private Transform tr;
	[Header("Speech")]
	[SerializeField] private List<Dialogue> listObjectSpeech = new List<Dialogue>();
	[SerializeField] private List<Dialogue> listLastSpeech = new List<Dialogue>();
	[SerializeField] private GameObject objectSpeech;
	[SerializeField] private GameObject warrior;
	[SerializeField] private bool isCreatedWarritor = false;
	[SerializeField] private int countDamage = 2;
	[SerializeField] private int countSpeech = 5;
	void Start(){
		enemyHPComponent.RegisterOnChangeHP(OnHeat);
		countSpeech = Random.Range(3, 9);
		countDamage = Random.Range(2, 4);
	} 
	Coroutine coroutineOnHeat;
	private void OnHeat(float HP){
		if(isCreatedWarritor == false){
			countDamage -= 1;
			if(countDamage == 0){
				CreateSpeech();
			}
		}
		if(coroutineOnHeat != null){
			StopCoroutine(coroutineOnHeat);
			coroutineOnHeat = null;
		}
		coroutineOnHeat = StartCoroutine(IShake());
	}
	[SerializeField] float power = 1f, duration = 0.3f;
	private float percentComplete = 1f;
	Vector3 originalPos = new Vector3();
	IEnumerator IShake(){
		if(percentComplete == 1) originalPos = tr.position;
		float elapsed = 0;
		while(elapsed < duration){
			elapsed += Time.deltaTime;
			percentComplete = elapsed / duration;
			percentComplete = Mathf.Clamp01(percentComplete);
			Vector3 rnd = Random.insideUnitSphere * power * (1f - percentComplete)/10;
			tr.position = originalPos + new Vector3(rnd.x, rnd.y, 0);
			yield return null;
		}
	}
	void CreateSpeech(){
		countSpeech -= 1;
		countDamage = Random.Range(2, 6); 
        DialogueScript objectDialog = (Instantiate(objectSpeech,Information.Instance.position,Quaternion.Euler(0,0,0))).GetComponent<DialogueScript>();
		if(countSpeech > 0){
			objectDialog.SetDialogue(listObjectSpeech[Random.Range(0, listObjectSpeech.Count)]);
		}else{
			isCreatedWarritor = true;
			objectDialog.SetDialogue(listLastSpeech[Random.Range(0, listLastSpeech.Count)]);
	        Instantiate(warrior, tr.position,Quaternion.Euler(0,0,0));
		}
	}
	void OnDestroy(){
		enemyHPComponent.UnRegisterOnChangeHP(OnHeat);
	}
	public void Death(){
		this.enabled = false;
	} 
}