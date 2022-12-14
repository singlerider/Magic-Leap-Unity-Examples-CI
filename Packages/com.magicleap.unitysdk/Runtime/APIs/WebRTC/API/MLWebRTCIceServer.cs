// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
// Copyright (c) (2018-2022) Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Software License Agreement, located here: https://www.magicleap.com/software-license-agreement-ml2
// Terms and conditions applicable to third-party materials accompanying this distribution may also be found in the top-level NOTICE file appearing herein.
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

namespace UnityEngine.XR.MagicLeap
{
    /// <summary>
    /// MLWebRTC class contains the API to interface with the
    /// WebRTC C API.
    /// </summary>
    public partial class MLWebRTC
    {
        /// <summary>
        /// Class that represents an ice server used by the MLWebRTC API.
        /// </summary>
        public partial struct IceServer
        {
            /// <summary>
            /// Gets the uri of the ice server.
            /// </summary>
            public string Uri { get; private set; }

            /// <summary>
            /// Gets the username to log into the ice server.
            /// </summary>
            public string UserName { get; private set; }

            /// <summary>
            /// Gets the password to log into the ice server.
            /// </summary>
            public string Password { get; private set; }

            /// <summary>
            /// Factory method used to create a new IceServer object.
            /// </summary>
            /// <param name="uri">The uri of the ice server.</param>
            /// <param name="userName">The username to log into the ice server.</param>
            /// <param name="password">The password to log into the ice server.</param>
            /// <returns>An ice candidate object with the given handle.</returns>
            public static IceServer Create(string uri, string userName = null, string password = null)
            {
                IceServer iceServer = new IceServer()
                {
                    Uri = uri,
                    UserName = userName,
                    Password = password,
                };

                return iceServer;
            }

            public override string ToString()
            {
                return $"Uri : {Uri} ; Username : {UserName} ; Password : {Password}";
            }
        }
    }
}
