using System;
using System.Linq;
using Unity;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

[InitializeOnLoad]
public class DialogueGraphChecks : MonoBehaviour
{
    //this is for optional/required packages
    const string pkgGraphToolkit = "com.unity.graphtoolkit";
    const string pkgCinaMachine = "com.unity.cinemachine";

    static DialogueGraphChecks()
    {
        CheckPackages(pkgGraphToolkit);
        CheckPackages(pkgCinaMachine);
    }

    private static void CheckPackages(string pkgName)
    {
        var pack = Client.List();
        while (!pack.IsCompleted) ;
        var havePkgIDs = pack.Result.FirstOrDefault(q => q.name == pkgName);

        if (havePkgIDs == null)
        {
            Debug.LogError($"'{pkgName}' is not installed. {pkgName} may be required for effective usage of the Dialogue Graph");
        }
    }
}