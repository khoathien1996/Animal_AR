using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenShootManager : MonoSingleton<ScreenShootManager>
{
    public CaptureAndSave snapShot;
    public GameObject m_ui;
    public RawImage imgCapture;

    private int scrWidth = 0;
    private int scrHeight = 0;

    private Texture2D currTexture;

	// Use this for initialization
	void Awake () {
        InitGame();
	}

    public void InitGame()
    {
        scrWidth = Screen.width;
        scrHeight = Screen.height;
        imgCapture.transform.parent.gameObject.SetActive(false);
        
    }

    IEnumerator TakeTemporaryScreenshot()
    {
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        // create a texture to pass to encoding
        currTexture = new Texture2D(scrWidth, scrHeight, TextureFormat.RGB24, false);

        // assign new texture to variable
        // put buffer into texture
        currTexture.ReadPixels(new Rect(0, 0, scrWidth, scrHeight), 0, 0);
        currTexture.Apply();
        if (currTexture != null)
        {
            //imgCapture.transform.parent.gameObject.SetActive(true);
            ScreenManager.Instance.ShowPopupScreen(ePopupType.SNAP_SHOOT);
            imgCapture.texture = currTexture;
        }
        m_ui.SetActive(true);
    }

    // when click capture
    public void OnClickCapture()
    {
        m_ui.SetActive(false);
        StartCoroutine("TakeTemporaryScreenshot");
    }

    public void SaveImage()
    {
        snapShot.SaveTextureToGallery(currTexture);
        Reset();
    }

    public void AgainCapture()
    {
        Reset();
    }

    public void Reset()
    {
        ScreenManager.Instance.HideCurrentPopup();
        StopCoroutine("TakeTemporaryScreenshot");
    }
}
