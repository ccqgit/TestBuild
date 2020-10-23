using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class PrefabsBuilder : MonoBehaviour {

	[MenuItem("Build/Build/BuildAllBundles")]
	public static void BuildAllPrefabs(){
		string outputPath=Application.dataPath+"/Output/Bundles/"+current;
		TouchFolder(outputPath);
		BuildPipeline.BuildAssetBundles(outputPath,BuildAssetBundleOptions.DeterministicAssetBundle,current);
		AssetDatabase.Refresh();
	}

	[MenuItem("Assets/Look/Bundle Content")]
	public static void LookBundleContent(){
		string path=AssetDatabase.GetAssetPath(Selection.activeObject);
		path=path.TrimStart("Assets".ToCharArray());
		path=Application.dataPath+path;
		WWW fileLoader = new WWW ("file://" + path);
		if(fileLoader.assetBundle!=null){
			string[] assetNames=fileLoader.assetBundle.GetAllAssetNames();
			for(int i=0;i<assetNames.Length;i++){
				Debug.Log(assetNames[i]);
				//Object obj=fileLoader.assetBundle.LoadAsset(assetNames[i]);
			}
			fileLoader.assetBundle.Unload(true);
		}
	}

	private static void TouchFolder(string dir){
		if(!Directory.Exists(dir)){
			Directory.CreateDirectory(dir);
		}
	}
	private static void RenameFile(string from,string to){
		FileUtil.DeleteFileOrDirectory(to);
		FileUtil.MoveFileOrDirectory(from,to);
	}

	public static BuildTarget current{
		get{
			#if UNITY_ANDROID
			return BuildTarget.Android;
			#elif UNITY_IOS
			return BuildTarget.iOS;
			#else
			return BuildTarget.StandaloneWindows;
			#endif
		}
	}
}
