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
    /// Updates the transform an color on the Hit Position and Normal from the assigned object.
    /// </summary>
    public class MLRaycastVisualizer : MonoBehaviour
    {
        [Tooltip("The reference to the class to handle results from.")]
        public MLRaycastBehavior raycast = null;

        [SerializeField, Tooltip("When enabled the cursor will scale down once a certain minimum distance is hit.")]
        private bool _scaleWhenClose = true;

        // Stores default color
        [SerializeField]
        private Color _color = Color.green;

        // Stores Renderer component
        [SerializeField]
        private Renderer _render = null;

        /// <summary>
        /// Initializes variables and makes sure needed components exist.
        /// </summary>
        void Awake()
        {
            // Check if the Layer is set to Default and disable any child colliders.
            if (gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                Collider[] colliders = GetComponentsInChildren<Collider>();

                // Disable any active colliders.
                foreach (Collider collider in colliders)
                {
                    collider.enabled = false;
                }

                // Warn user if any colliders had to be disabled.
                if (colliders.Length > 0)
                {
                    Debug.LogWarning("Colliders have been disabled on this RaycastVisualizer.\nIf this is undesirable, change this object's layer to something other than Default.");
                }
            }

            if (_render == null)
            {
                Debug.LogError("Error: RaycastVisualizer._render is not set, disabling script.");
                enabled = false;
                return;
            }

            if (raycast != null)
            {
                raycast.OnRaycastResult += OnRaycastHit;
            }
        }

        /// <summary>
        /// Cleans up the component.
        /// </summary>
        void OnDestroy()
        {
            raycast.OnRaycastResult -= OnRaycastHit;
        }

        /// <summary>
        /// Callback handler called when raycast has a result.
        /// Updates the transform an color on the Hit Position and Normal from the assigned object.
        /// </summary>
        /// <param name="state"> The state of the raycast result.</param>
        /// <param name="mode">The mode that the raycast was in (physical, virtual, or combination).</param>
        /// <param name="ray">A ray that contains the used direction and origin for this raycast.</param>
        /// <param name="result">The hit results (point, normal, distance).</param>
        /// <param name="confidence">Confidence value of hit. 0 no hit, 1 sure hit.</param>
        public void OnRaycastHit(MLRaycast.ResultState state, MLRaycastBehavior.Mode mode, Ray ray, RaycastHit result, float confidence)
        {
            if (state != MLRaycast.ResultState.RequestFailed && state != MLRaycast.ResultState.NoCollision)
            {
                gameObject.SetActive(true);
                // Update the cursor position and normal.
                transform.position = result.point;
                transform.LookAt(result.normal + result.point, ray.direction);
                transform.localScale = Vector3.one;

                // Set the color to yellow if the hit is unobserved.
                _render.material.color = (state == MLRaycast.ResultState.HitObserved) ? _color : Color.yellow;

                if (_scaleWhenClose)
                {
                    // Check the hit distance.
                    if (result.distance < 1.0f)
                    {
                        // Apply a downward scale to the cursor.
                        transform.localScale = new Vector3(result.distance, result.distance, result.distance);
                    }
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
