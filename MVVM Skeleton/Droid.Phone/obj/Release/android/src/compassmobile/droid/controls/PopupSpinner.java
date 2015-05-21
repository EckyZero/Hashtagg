package compassmobile.droid.controls;


public class PopupSpinner
	extends android.app.AlertDialog.Builder
	implements
		mono.android.IGCUserPeer,
		android.content.DialogInterface.OnCancelListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCancel:(Landroid/content/DialogInterface;)V:GetOnCancel_Landroid_content_DialogInterface_Handler:Android.Content.IDialogInterfaceOnCancelListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Controls.PopupSpinner, CompassMobile.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", PopupSpinner.class, __md_methods);
	}


	public PopupSpinner (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == PopupSpinner.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Controls.PopupSpinner, CompassMobile.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onCancel (android.content.DialogInterface p0)
	{
		n_onCancel (p0);
	}

	private native void n_onCancel (android.content.DialogInterface p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
