package compassmobile.droid.phone.os_services.gcm;


public class GcmOfflineBroadcastReceiver
	extends gcm.client.GcmBroadcastReceiverBase_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.OS_Services.GCM.GcmOfflineBroadcastReceiver, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GcmOfflineBroadcastReceiver.class, __md_methods);
	}


	public GcmOfflineBroadcastReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GcmOfflineBroadcastReceiver.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.OS_Services.GCM.GcmOfflineBroadcastReceiver, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
