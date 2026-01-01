using UnityEngine;
using UnityEditor;
using UnityEngine.Animations.Rigging;
using System.Collections.Generic;

public class AutoRigBuilder : EditorWindow
{
    private Animator animator;
    private GameObject rigPrefab;

    private void OnEnable()
    {
        rigPrefab = FindPrefabByName("AutoRig");
    }

    private GameObject FindPrefabByName(string prefabName)
    {
        string[] guids = AssetDatabase.FindAssets($"t:Prefab {prefabName}");
        if (guids.Length == 0)
        {
            Debug.LogError("Rig prefab not found.");
            return null;
        }

        return AssetDatabase.LoadAssetAtPath<GameObject>(
            AssetDatabase.GUIDToAssetPath(guids[0]));
    }


    [MenuItem("Tools/Auto Rig Builder")]
    static void Open()
    {
        GetWindow<AutoRigBuilder>("Auto Rig Builder");
    }

    private void OnGUI()
    {
        GUILayout.Label("Auto Rig Builder", EditorStyles.boldLabel);

        animator = (Animator)EditorGUILayout.ObjectField(
            "Animator", animator, typeof(Animator), true);

        //rigPrefab = (GameObject)EditorGUILayout.ObjectField(
        //    "Rig Prefab", rigPrefab, typeof(GameObject), false);

        GUI.enabled = animator;

        if (GUILayout.Button("Create Rig"))
            CreateRig();

        //GUI.enabled = true;
    }

    private void CreateRig()
    {
        GameObject character = animator.gameObject;

        // ---------------- RIG BUILDER ----------------
        RigBuilder rigBuilder = character.GetComponent<RigBuilder>() ??
                                character.AddComponent<RigBuilder>();

        // ---------------- RIG PREFAB ----------------
        GameObject rigGO = (GameObject)PrefabUtility.InstantiatePrefab(
            rigPrefab, character.scene);

        rigGO.name = "Rig";
        rigGO.transform.SetParent(character.transform, false);

        Rig rig = rigGO.GetComponent<Rig>();
        if (!Validate(rig, "Rig component missing on prefab"))
        {
            DestroyImmediate(rigGO);
            return;
        }

        AddRigLayer(rigBuilder, rig);

        // ---------------- HEAD ----------------
        SetupMultiAim(
            animator.GetBoneTransform(HumanBodyBones.Head),
            rigGO,
            "Head_Control");

        // ---------------- HIPS ----------------
        SetupMultiPosition(
            animator.GetBoneTransform(HumanBodyBones.Hips),
            rigGO,
            "Hip_Constraint",
            "Hip_Control");

        SetupMultiRotation(
            animator.GetBoneTransform(HumanBodyBones.Hips),
            rigGO,
            "Hip_Constraint",
            "Hip_Control");

        // ---------------- LEFT LEG IK ----------------
        SetupTwoBoneIK(
            rigGO,
            "LeftLeg_IK",
            animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg),
            animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg),
            animator.GetBoneTransform(HumanBodyBones.LeftFoot),
            "LeftLeg_Control",
            "LeftLeg_Hint");

        // ---------------- RIGHT LEG IK ----------------
        SetupTwoBoneIK(
            rigGO,
            "RightLeg_IK",
            animator.GetBoneTransform(HumanBodyBones.RightUpperLeg),
            animator.GetBoneTransform(HumanBodyBones.RightLowerLeg),
            animator.GetBoneTransform(HumanBodyBones.RightFoot),
            "RightLeg_Control",
            "RightLeg_Hint");

        // ---------------- RIGHT ARM IK ----------------
        SetupTwoBoneIK(
            rigGO,
            "RightArm_IK",
            animator.GetBoneTransform(HumanBodyBones.RightUpperArm),
            animator.GetBoneTransform(HumanBodyBones.RightLowerArm),
            animator.GetBoneTransform(HumanBodyBones.RightHand),
            "RightArm_Control",
            "RightArm_Hint");

        // ---------------- LEFT ARM IK ----------------
        SetupTwoBoneIK(
            rigGO,
            "LeftArm_IK",
            animator.GetBoneTransform(HumanBodyBones.LeftUpperArm),
            animator.GetBoneTransform(HumanBodyBones.LeftLowerArm),
            animator.GetBoneTransform(HumanBodyBones.LeftHand),
            "LeftArm_Control",
            "LeftArm_Hint");

        EditorUtility.SetDirty(character);
        Debug.Log("Auto rig successfully created.");
    }

    // =========================================================
    // HELPERS
    // =========================================================

    private void AddRigLayer(RigBuilder rigBuilder, Rig rig)
    {
        var layers = new List<RigLayer>(rigBuilder.layers)
        {
            new RigLayer(rig)
        };
        rigBuilder.layers = layers;
    }

    private void SetupMultiAim(Transform bone, GameObject rigGO, string controlName)
    {
        if (!Validate(bone, "Head bone not found")) return;

        MultiAimConstraint constraint =
            rigGO.GetComponentInChildren<MultiAimConstraint>();
        Transform control = rigGO.transform.Find(controlName);

        if (!Validate(constraint, "MultiAimConstraint not found") ||
            !Validate(control, $"{controlName} not found"))
            return;

        constraint.data.constrainedObject = bone;
        constraint.data.sourceObjects = CreateSources(control);
        constraint.weight = 1f;
    }

    private void SetupMultiPosition(Transform bone,GameObject rigGO,string constraintName,string controlName)
    {
        if (!Validate(bone, "Hips bone not found")) return;

        MultiPositionConstraint constraint =
            rigGO.transform.Find(constraintName)
            ?.GetComponent<MultiPositionConstraint>();

        Transform control = rigGO.transform.Find(controlName);

        if (!Validate(constraint, $"{constraintName} not found") ||
            !Validate(control, $"{controlName} not found"))
            return;

        constraint.data.constrainedObject = bone;
        constraint.data.sourceObjects = CreateSources(control);
        constraint.weight = 1f;
    }

    private void SetupMultiRotation(Transform bone,GameObject rigGO,string constraintName,string controlName)
    {
        if (!Validate(bone, "Hips bone not found")) return;

        MultiRotationConstraint constraint =
            rigGO.transform.Find(constraintName)
            ?.GetComponent<MultiRotationConstraint>();

        Transform control = rigGO.transform.Find(controlName);

        if (!Validate(constraint, $"{constraintName} not found") ||
            !Validate(control, $"{controlName} not found"))
            return;

        constraint.data.constrainedObject = bone;
        constraint.data.sourceObjects = CreateSources(control);
        constraint.weight = 1f;
    }

    private void SetupTwoBoneIK(GameObject rigGO,string constraintName,Transform root,Transform mid,Transform tip,string controlName,string hintName)
    {
        if (!Validate(root, "Upper leg bone missing") ||
            !Validate(mid, "Lower leg bone missing") ||
            !Validate(tip, "Foot bone missing"))
            return;

        TwoBoneIKConstraint ik =
            rigGO.transform.Find(constraintName)
            ?.GetComponent<TwoBoneIKConstraint>();

        Transform control = rigGO.transform.Find(controlName);
        Transform hint = control ? control.Find(hintName) : null;

        if (!Validate(ik, $"{constraintName} not found") ||
            !Validate(control, $"{controlName} not found") ||
            !Validate(hint, $"{hintName} not found"))
            return;

        ik.data.root = root;
        ik.data.mid = mid;
        ik.data.tip = tip;
        ik.data.target = control;
        ik.data.hint = hint;
        ik.weight = 1f;
    }

    private WeightedTransformArray CreateSources(Transform source)
    {
        var array = new WeightedTransformArray();
        array.Add(new WeightedTransform(source, 1f));
        return array;
    }

    private bool Validate(Object obj, string error)
    {
        if (obj) return true;
        Debug.LogError(error);
        return false;
    }
}
