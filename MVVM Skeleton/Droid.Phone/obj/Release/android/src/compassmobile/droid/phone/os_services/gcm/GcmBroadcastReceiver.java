package compassmobile.droid.phone.os_services.gcm;


public class GcmBroadcastReceiver
	extends gcm.client.GcmBroadcastReceiverBase_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CompassMobile.Droid.Phone.OS_Services.GCM.GcmBroadcastReceiver, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GcmBroadcastReceiver.class, __md_methods);
	}


	public GcmBroadcastReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GcmBroadcastReceiver.class)
			mono.android.TypeManager.Activate ("CompassMobile.Droid.Phone.OS_Services.GCM.GcmBroadcastReceiver, CompassMobile.Droid.Phone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
