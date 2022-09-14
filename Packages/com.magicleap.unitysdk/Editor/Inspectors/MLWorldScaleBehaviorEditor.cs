// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
// Copyright (c) (2019-2022) Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Software License Agreement, located here: https://www.magicleap.com/software-license-agreement-ml2
// Terms and conditions applicable to third-party materials accompanying this distribution may also be found in the top-level NOTICE file appearing herein.
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicLeap.Core
{
    /// <summary>
    /// This class extends the inspector for the MLWorldScaleBehavior component, providing visual runtime information.
    /// </summary>
    [CustomEditor(typeof(MLWorldScaleBehavior))]
    public class MLWorldScaleBehaviorEditor : Editor
    {
        class Tooltips
        {
            public static readonly GUIContent Measurement = new GUIContent(
                "Measurement",
                "Measurement type to apply as worldscale.");

            public static readonly GUIContent CustomValue = new GUIContent(
                "Custom Value",
                "Custom value to apply as worlscale.");

            public static readonly GUIContent OnUpdateEvent = new GUIContent(
                "On Update Event",
                "Event that gets triggered when world scale is updated.");
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            LayoutGUI();

            this.serializedObject.ApplyModifiedProperties();
        }

        void LayoutGUI()
        {
            MLWorldScaleBehavior myTarget = (MLWorldScaleBehavior)target;

            EditorGUILayout.Space();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour(myTarget), typeof(MLWorldScaleBehavior), false);
            GUI.enabled = true;

            EditorGUILayout.Space();

            myTarget.Measurement = (MLWorldScaleBehavior.ScaleMeasurement)EditorGUILayout.EnumPopup(Tooltips.Measurement, myTarget.Measurement);

            if (myTarget.Measurement == MLWorldScaleBehavior.ScaleMeasurement.CustomUnits)
            {
                myTarget.CustomValue = EditorGUILayout.FloatField(Tooltips.CustomValue, myTarget.CustomValue);
            }
        }
    }
}
