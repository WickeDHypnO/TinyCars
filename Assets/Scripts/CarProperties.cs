using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using System.IO;
using UnityEngine;
#endif

public class CarProperties : ScriptableObject {

    public Mesh model;
    public Material material;
    public Vector3 offset;
    public Vector3 colliderBounds;
    public float scale;
    public Mesh wheelModel;
    public Vector3 frontWheelOffset;
    public Vector3 backWheelOffset;
    public Vector3 wheelScale;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/CarProperties")]
    public static void CreatCar()
    {
        var asset = CreateInstance<CarProperties>();
        AssetDatabase.CreateAsset(asset, "Assets/CarProperties/New Car.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif
}
