using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildPlayerExample : MonoBehaviour
{
    [MenuItem("Build/Build Android")]
    public static void MyBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Login.unity", "Assets/Scenes/Game.unity" };
        
        buildPlayerOptions.locationPathName = "AndroidExportBuild";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.AcceptExternalModificationsToPlayer | BuildOptions.UncompressedAssetBundle ;
        //buildPlayerOptions.options = BuildOptions.None;
        

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}
