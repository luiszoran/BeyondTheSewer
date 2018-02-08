// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace GoogleVR.HelloVR
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [RequireComponent(typeof(Collider))]
    public class StartGameEventController : MonoBehaviour
    {
        private Vector3 startingPosition;
        private float gazing;
        private bool gazedAtObject;
        public float gazeTime;

        public Material inactiveMaterial;
        public Material gazedAtMaterial;

        void Start()
        {
            gazing = 0f;
            gazeTime = gazeTime != 0f ? gazeTime : 3f;
            gazedAtObject = false;
            startingPosition = transform.localPosition;
            SetGazedAt(false);
        }

        void Update()
        {
            if (gazing >= gazeTime)
            {
                SceneManager.LoadScene("GameJam");
            }

            if (gazedAtObject)
            {
                gazing = gazing + (1 * Time.deltaTime);
                Debug.Log(gazing);
            }

        }

        public void SetGazedAt(bool gazedAt)
        {
            gazedAtObject = gazedAt ? true : false;
        }

    }
}
