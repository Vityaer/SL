using UnityEngine;
using System.Collections;

public class Parallax2DBackground : MonoBehaviour {

	private enum Mode
	{
		Horizontal,
		Vertical,
		HorizontalAndVertical
	}

	[SerializeField] private Transform[] backgrounds;
	[SerializeField] private float smoothing = 0.5f;
	[SerializeField] private Mode parallaxMode;

	private float[] scales;
	private Transform cam;
	private Vector3 previousCamPos;
	private Vector3 position;

	void Awake()
	{
		cam = Camera.main.transform;
	}

	void Start()
	{
		previousCamPos = cam.position;

		scales = new float[backgrounds.Length];

		for(int i = 0; i < backgrounds.Length; i++)
		{
			if(backgrounds[i] != null) scales[i] = backgrounds[i].position.z * -1;
		}
	}

	void LateUpdate()
	{
		for(int i = 0; i < backgrounds.Length; i++)
		{
			if(backgrounds[i] != null)
			{
				Vector3 parallax = (previousCamPos - cam.position) * scales[i];

				switch(parallaxMode)
				{
				case Mode.Horizontal:
					position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y, backgrounds[i].position.z);
					break;
				case Mode.Vertical:
					position = new Vector3(backgrounds[i].position.x, backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
					break;
				case Mode.HorizontalAndVertical:
					position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
					break;
				}

				backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, position, smoothing * Time.deltaTime);
			}
		}

		previousCamPos = cam.position;
	}
}