using UnityEngine;
using System.Collections;

public class DestroyBlood : MonoBehaviour {
	public float Time;
	void Start () {
	Destroy(gameObject, Time);
	}
}
