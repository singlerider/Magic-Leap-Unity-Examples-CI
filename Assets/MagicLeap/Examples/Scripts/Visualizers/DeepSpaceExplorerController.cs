// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
// Copyright (c) (2019-2022) Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Software License Agreement, located here: https://www.magicleap.com/software-license-agreement-ml2
// Terms and conditions applicable to third-party materials accompanying this distribution may also be found in the top-level NOTICE file appearing herein.
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using UnityEngine;

namespace MagicLeap.Examples
{
    /// <summary>
    /// This class makes it easier to set the radius of the orbit of the Deep Space Explorer.
    /// </summary>
    public class DeepSpaceExplorerController : MonoBehaviour
    {
        [SerializeField, Tooltip("Radius of the orbit of the rockets")]
        private Transform _xOffset = null;

        public float OrbitRadius
        {
            set
            {
                _xOffset.localPosition = new Vector3(value, 0, 0);
            }
        }

        /// <summary>
        /// Validate input variables.
        /// </summary>
        void Start ()
        {
            if (null == _xOffset)
            {
                Debug.LogError("Error: DeepSpaceExplorerController._xOffset is not set, disabling script");
                enabled = false;
                return;
            }
        }
    }
}
