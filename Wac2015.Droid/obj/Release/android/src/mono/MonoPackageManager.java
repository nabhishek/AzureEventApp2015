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
		"Azure4Sure.dll",
		"ByteSmith.WindowsAzure.Messaging.Android.dll",
		"ExifLib.dll",
		"FormsViewGroup.dll",
		"GCM.Client.dll",
		"Microsoft.WindowsAzure.Messaging.Android.dll",
		"Microsoft.WindowsAzure.Mobile.dll",
		"Microsoft.WindowsAzure.Mobile.Ext.dll",
		"Newtonsoft.Json.dll",
		"SQLite.Net.dll",
		"SQLite.Net.Platform.XamarinAndroid.dll",
		"System.Net.Http.Extensions.dll",
		"System.Net.Http.Primitives.dll",
		"Wac2015.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Forms.Core.dll",
		"Xamarin.Forms.Platform.Android.dll",
		"Xamarin.Forms.Xaml.dll",
		"Xamarin.Insights.dll",
		"System.Diagnostics.Tracing.dll",
		"System.Reflection.Emit.dll",
		"System.Reflection.Emit.ILGeneration.dll",
		"System.Reflection.Emit.Lightweight.dll",
		"System.ServiceModel.Security.dll",
		"System.Threading.Timer.dll",
		"LinqToTwitterPcl.dll",
		"System.Reactive.Linq.dll",
		"System.Reactive.Core.dll",
		"System.Reactive.Interfaces.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = null;
}
