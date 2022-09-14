// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
// Copyright (c) (2019-2022) Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Software License Agreement, located here: https://www.magicleap.com/software-license-agreement-ml2
// Terms and conditions applicable to third-party materials accompanying this distribution may also be found in the top-level NOTICE file appearing herein.
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using MagicLeap.Core;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap.Examples
{
    /// <summary>
    /// This class helps show visibility on image tracking.
    /// </summary>
    public class ImageTrackerVisualizer : MonoBehaviour
    {

        [SerializeField, Tooltip("The MLImageTrackerBehavior that will be subscribed to.")]
        private MLImageTrackerBehavior _imageTracker = null;

        [SerializeField, Tooltip("The GameObject used to visualize the axes of the image target.")]
        private GameObject _axis = null;
        [SerializeField, Tooltip("The GameObject used to visualize the tracking cube of the image target.")]
        private GameObject _trackingCube = null;

        /// <summary>
        /// Validates fields and registers for _imageTracker callbacks.
        /// </summary>
        void Start()
        {
            if (_imageTracker == null)
            {
                Debug.LogError("Error: ImageTrackingVisualizer._trackerBehavior is not set, disabling script.");
                enabled = false;
                return;
            }

            if (_axis == null)
            {
                Debug.LogError("Error: ImageTrackingVisualizer._axis is not set, disabling script.");
                enabled = false;
                return;
            }

            if (_trackingCube == null)
            {
                Debug.LogError("Error: ImageTrackingVisualizer._trackingCube is not set, disabling script.");
                enabled = false;
                return;
            }

            #if UNITY_MAGICLEAP || UNITY_ANDROID
            _imageTracker.OnTargetUpdated += OnTargetUpdated;
            _imageTracker.OnTargetLost += OnTargetLost;
            _imageTracker.OnTargetFound += OnTargetFound;
            #endif

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Unregisters from the _imageTracker callbacks.
        /// </summary>
        void OnDestroy()
        {
            #if UNITY_MAGICLEAP || UNITY_ANDROID
            _imageTracker.OnTargetUpdated -= OnTargetUpdated;
            _imageTracker.OnTargetLost -= OnTargetLost;
            _imageTracker.OnTargetFound -= OnTargetFound;
            #endif
        }

        #if UNITY_MAGICLEAP || UNITY_ANDROID
        /// <summary>
        /// Callback for when image tracked is updated.
        /// </summary>
        private void OnTargetUpdated(MLImageTracker.Target target, MLImageTracker.Target.Result result)
        {
            transform.position = result.Position;
            transform.rotation = result.Rotation;
        }

        /// <summary>
        /// Callback for when image tracked is lost.
        /// </summary>
        private void OnTargetLost(MLImageTracker.Target target, MLImageTracker.Target.Result result)
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Callback for when image tracked is found.
        /// </summary>
        private void OnTargetFound(MLImageTracker.Target target, MLImageTracker.Target.Result result)
        {
            gameObject.SetActive(true);
        }
        #endif
    }
}
