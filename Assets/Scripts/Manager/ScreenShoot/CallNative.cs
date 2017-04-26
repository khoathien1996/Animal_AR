using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class CallNative
{
	[DllImport("__Internal")]
	private static extern int _SaveImage(string str);

	public static int SaveImageToCameraRoll(string path)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return CallNative._SaveImage(path);
		}
		return 0;
	}
}
