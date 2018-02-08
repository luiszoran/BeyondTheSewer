using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour {


	public Texture2D text;
	public RawImage image;

	// Update is called once per frame
	void Start () {
		StartCoroutine("FreezeRadar");
	}

	IEnumerator FreezeRadar()
	{

		do{
			//Camera.main.clearFlags = CameraClearFlags.Nothing;

			//Camera.main.cullingMask = 0;
			Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture(1);
			Debug.Log(texture.name);
			text = texture;
			image.texture = texture;
			Object.Destroy(texture);
			yield return new WaitForSeconds(1f);

			//Camera.main.clearFlags = CameraClearFlags.Skybox;

			//Camera.main.cullingMask = 1;
		}while(true);
	//	yield return null;
	}
}
