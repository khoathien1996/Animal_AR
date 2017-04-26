using System;
using System.Runtime.CompilerServices;

public class CaptureAndSaveEventListener
{
	public delegate void OnError(string err);

	public delegate void OnSuccess(string msg);

	public static CaptureAndSaveEventListener.OnError onErrorInvoker;

	public static CaptureAndSaveEventListener.OnSuccess onSuccessInvoker;

	public static event CaptureAndSaveEventListener.OnError onError
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			CaptureAndSaveEventListener.AddHandler_onError(value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			CaptureAndSaveEventListener.RemoveHandler_onError(value);
		}
	}

	public static event CaptureAndSaveEventListener.OnSuccess onSuccess
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			CaptureAndSaveEventListener.AddHandler_onSuccess(value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			CaptureAndSaveEventListener.RemoveHandler_onSuccess(value);
		}
	}

	[MethodImpl(MethodImplOptions.Synchronized)]
	private static void AddHandler_onError(CaptureAndSaveEventListener.OnError value)
	{
		CaptureAndSaveEventListener.onErrorInvoker = (CaptureAndSaveEventListener.OnError)Delegate.Combine(CaptureAndSaveEventListener.onErrorInvoker, value);
	}

	[MethodImpl(MethodImplOptions.Synchronized)]
	private static void RemoveHandler_onError(CaptureAndSaveEventListener.OnError value)
	{
		CaptureAndSaveEventListener.onErrorInvoker = (CaptureAndSaveEventListener.OnError)Delegate.Remove(CaptureAndSaveEventListener.onErrorInvoker, value);
	}

	[MethodImpl(MethodImplOptions.Synchronized)]
	private static void AddHandler_onSuccess(CaptureAndSaveEventListener.OnSuccess value)
	{
		CaptureAndSaveEventListener.onSuccessInvoker = (CaptureAndSaveEventListener.OnSuccess)Delegate.Combine(CaptureAndSaveEventListener.onSuccessInvoker, value);
	}

	[MethodImpl(MethodImplOptions.Synchronized)]
	private static void RemoveHandler_onSuccess(CaptureAndSaveEventListener.OnSuccess value)
	{
		CaptureAndSaveEventListener.onSuccessInvoker = (CaptureAndSaveEventListener.OnSuccess)Delegate.Remove(CaptureAndSaveEventListener.onSuccessInvoker, value);
	}
}
