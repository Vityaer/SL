using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashSprite : MonoBehaviour
{
	public string startMenu = "scene0";
	public float fadeSpeed = 0.5f;
	private bool start;
	private Image rend;
	private AudioSource _audio;
	private RectTransform _rect;

	void Start()
	{
		start = true;
		Cursor.visible = false;

		_rect = GetComponent<RectTransform>();
		_audio = GetComponent<AudioSource>();
		rend = GetComponent<Image>();

		_rect.sizeDelta = new Vector2 (0, (Screen.width/16)*9);

		rend.color = Color.clear;
		_audio.Play ();
	}

	void Update()
	{
		if (start) 
		{
			rend.color = Color.Lerp(rend.color, Color.white, fadeSpeed * Time.deltaTime);
		}
		else
		{
			rend.color = Color.Lerp(rend.color, Color.clear, fadeSpeed * Time.deltaTime);
		}
		if (rend.color.a >= 0.95f)
		{
			if(!_audio.isPlaying) start = false;
		}
		if (rend.color.a <= 0.1f && !start || Input.anyKeyDown) 
		{
			_audio.Stop ();
			Cursor.visible = true;
			Application.LoadLevel(startMenu);
		}
	}
}