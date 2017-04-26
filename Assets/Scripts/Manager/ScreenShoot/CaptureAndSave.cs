using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class CaptureAndSave : MonoBehaviour
{
	private string FILENAME_PREFIX = "Pokemon";

	public string folderName = "Pictures";

	public GameObject refreshGalleryPrefab;

	private string androidPath = "";

    private string StrImgID = "imageID";
    private int imgID = 0;
	private void Start()
	{
		if (Application.platform == RuntimePlatform.Android && Directory.Exists("/storage/sdcard0"))
		{
			this.androidPath = Path.Combine("/storage/sdcard0", this.folderName);
			if (!Directory.Exists(this.androidPath))
			{
				Directory.CreateDirectory(this.androidPath);
			}
		}
        imgID = PlayerPrefs.GetInt(StrImgID);
	}

	public void SaveTextureAtPath(Texture2D tex2D, string path)
	{
		this.SaveTexture(tex2D, path);
	}

	public void SaveTextureToGallery(Texture2D tex2D)
	{
		this.SaveTexture(tex2D, "");
	}
	private void SaveTexture(Texture2D tex2D, string path)
	{
		try
		{
			byte[] bytes = tex2D.EncodeToPNG();
			string fullPath = this.GetFullPath(path);
			File.WriteAllBytes(fullPath, bytes);
			GC.Collect();
			Resources.UnloadUnusedAssets();
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				this.TransferToCameraRoll(fullPath);
			}
			else if (Application.platform == RuntimePlatform.Android)
			{
				if (this.refreshGalleryPrefab != null)
				{
					this.refreshGalleryPrefab.SendMessage("RefreshGallery", fullPath, SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					
				}
			}
			else if (CaptureAndSaveEventListener.onSuccessInvoker != null)
			{
				CaptureAndSaveEventListener.onSuccessInvoker(fullPath);
			}
		}
		catch (Exception ex)
		{
			if (CaptureAndSaveEventListener.onErrorInvoker != null)
			{
				CaptureAndSaveEventListener.onErrorInvoker(ex.Message);
			}
		}
	}

    //[DebuggerHidden]
    //private IEnumerator SaveToAlbum(int x, int y, int width, int height, string path)
    //{
    //    CaptureAndSave.<SaveToAlbum>c__Iterator0 <SaveToAlbum>c__Iterator = new CaptureAndSave.<SaveToAlbum>c__Iterator0();
    //    <SaveToAlbum>c__Iterator.x = x;
    //    <SaveToAlbum>c__Iterator.y = y;
    //    <SaveToAlbum>c__Iterator.width = width;
    //    <SaveToAlbum>c__Iterator.height = height;
    //    <SaveToAlbum>c__Iterator.path = path;
    //    <SaveToAlbum>c__Iterator.<$>x = x;
    //    <SaveToAlbum>c__Iterator.<$>y = y;
    //    <SaveToAlbum>c__Iterator.<$>width = width;
    //    <SaveToAlbum>c__Iterator.<$>height = height;
    //    <SaveToAlbum>c__Iterator.<$>path = path;
    //    <SaveToAlbum>c__Iterator.<>f__this = this;
    //    return <SaveToAlbum>c__Iterator;
    //}

    //[DebuggerHidden]
    //private IEnumerator CaptureAndUpload(int x, int y, int width, int height, string phpURL)
    //{
    //    CaptureAndSave.<CaptureAndUpload>c__Iterator1 <CaptureAndUpload>c__Iterator = new CaptureAndSave.<CaptureAndUpload>c__Iterator1();
    //    <CaptureAndUpload>c__Iterator.x = x;
    //    <CaptureAndUpload>c__Iterator.y = y;
    //    <CaptureAndUpload>c__Iterator.phpURL = phpURL;
    //    <CaptureAndUpload>c__Iterator.<$>x = x;
    //    <CaptureAndUpload>c__Iterator.<$>y = y;
    //    <CaptureAndUpload>c__Iterator.<$>phpURL = phpURL;
    //    <CaptureAndUpload>c__Iterator.<>f__this = this;
    //    return <CaptureAndUpload>c__Iterator;
    //}

    //[DebuggerHidden]
    //private IEnumerator UploadToServer(Texture2D tex, string phpURL)
    //{
    //    CaptureAndSave.<UploadToServer>c__Iterator2 <UploadToServer>c__Iterator = new CaptureAndSave.<UploadToServer>c__Iterator2();
    //    <UploadToServer>c__Iterator.phpURL = phpURL;
    //    <UploadToServer>c__Iterator.tex = tex;
    //    <UploadToServer>c__Iterator.<$>phpURL = phpURL;
    //    <UploadToServer>c__Iterator.<$>tex = tex;
    //    <UploadToServer>c__Iterator.<>f__this = this;
    //    return <UploadToServer>c__Iterator;
    //}

	private string GetFileName()
	{
        imgID++;
        PlayerPrefs.SetInt(StrImgID,imgID);
		return string.Concat(new object[]
		{
			this.FILENAME_PREFIX,
			"_",
			imgID.ToString(),
			".png"
		});
	}

	private string GetFullPath(string path)
	{
		string fileName = this.GetFileName();
		string text = path;
		if ((Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor) && string.IsNullOrEmpty(text))
		{
			text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), fileName);
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			text = Path.Combine(Application.persistentDataPath, fileName);
		}
		if (Application.platform == RuntimePlatform.Android)
		{
			if (this.androidPath != "")
			{
				text = Path.Combine(Path.Combine("/storage/sdcard0", this.folderName), fileName);
			}
			else
			{
				text = Path.Combine(Application.persistentDataPath, fileName);
			}
		}
		return text;
	}

	private Texture2D GetScreenShot(int x, int y, int width, int height)
	{
		if (width == 0 || height == 0)
		{
			return null;
		}
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGB24, false);
		texture2D.ReadPixels(new Rect((float)x, (float)(Screen.height - height), (float)width, (float)height), 0, 0);
		texture2D.Apply();
		return texture2D;
	}

	public void TransferToCameraRoll(string path)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer && CallNative.SaveImageToCameraRoll(path) == 0 && CaptureAndSaveEventListener.onErrorInvoker != null)
		{
			CaptureAndSaveEventListener.onErrorInvoker("Image not saved!");
		}
	}

	private void OnError(string err)
	{
		if (CaptureAndSaveEventListener.onErrorInvoker != null)
		{
			CaptureAndSaveEventListener.onErrorInvoker(err);
		}
	}

	private void OnComplete(string msg)
	{
		if (CaptureAndSaveEventListener.onSuccessInvoker != null)
		{
			CaptureAndSaveEventListener.onSuccessInvoker(msg);
		}
	}
}
