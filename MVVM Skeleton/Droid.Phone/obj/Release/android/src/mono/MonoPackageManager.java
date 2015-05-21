package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	public static void LoadApplication (Context context, String runtimeDataDir, String[] apks)
	{
		synchronized (lock) {
			if (!initialized) {
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = context.getApplicationInfo ().dataDir + "/lib";
				ClassLoader loader  = context.getClassLoader ();

				Runtime.init (
						language,
						apks,
						runtimeDataDir,
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new java.io.File (
							android.os.Environment.getExternalStorageDirectory (),
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath (),
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				initialized = true;
			}
		}
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		"CompassMobile.Droid.Phone.dll",
		"AutoMapper.Android.dll",
		"AutoMapper.dll",
		"CompassMobile.Droid.dll",
		"CompassMobile.Shared.BL.dll",
		"CompassMobile.Shared.Bootstrapper.dll",
		"CompassMobile.Shared.Common.dll",
		"CompassMobile.Shared.DAL.dll",
		"CompassMobile.Shared.VM.dll",
		"ExifLib.dll",
		"GalaSoft.MvvmLight.dll",
		"GalaSoft.MvvmLight.Extras.dll",
		"GalaSoft.MvvmLight.Platform.dll",
		"GCM.Client.dll",
		"Microsoft.Practices.ServiceLocation.dll",
		"Microsoft.Practices.Unity.dll",
		"ModernHttpClient.dll",
		"Mono.Data.Sqlcipher.dll",
		"Newtonsoft.Json.dll",
		"OkHttp.dll",
		"Refractored.PagerSlidingTabStrip.dll",
		"Telerik.Xamarin.Android.Common.dll",
		"Telerik.Xamarin.Android.Input.dll",
		"Xamarin.Android.Support.v13.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"Xamarin.Insights.dll",
		"System.Diagnostics.Tracing.dll",
		"System.Reflection.Emit.dll",
		"System.Reflection.Emit.ILGeneration.dll",
		"System.Reflection.Emit.Lightweight.dll",
		"System.ServiceModel.Security.dll",
		"System.Threading.Timer.dll",
		"Auth0Client.Android.dll",
		"Xamarin.Auth.Android.dll",
		"CompassMobile.Shared.SAL.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = null;
}
