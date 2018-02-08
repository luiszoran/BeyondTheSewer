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
    public class GeneratorEventController : MonoBehaviour
    {
        private Vector3 startingPosition;
        private Renderer renderer;
        private float currentGazingTime;
        private bool gazedAtObject;
        public float gazeTime;

        public Material inactiveMaterial;
        public Material gazedAtMaterial;

        void Start()
        {
            currentGazingTime = 0f;
            gazeTime = gazeTime != 0f ? gazeTime : 10f;
            gazedAtObject = false;
            startingPosition = transform.localPosition;
            renderer = GetComponent<Renderer>();
            SetGazedAt(false);
        }

        void Update()
        {
            if (currentGazingTime >= gazeTime)
            {
                currentGazingTime = 0;
				SceneManager.LoadScene("menu");
                Debug.Log("done");
            }

            if (gazedAtObject)
            {
                currentGazingTime = currentGazingTime + (1 * Time.deltaTime);
                Debug.Log(currentGazingTime);
            }

        }

        public void SetGazedAt(bool gazedAt)
        {
            if (inactiveMaterial != null && gazedAtMaterial != null)
            {
                renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;

            }
            gazedAtObject = gazedAt ? true : false;
        }

        public void Reset()
        {
            int sibIdx = transform.GetSiblingIndex();
            int numSibs = transform.parent.childCount;
            for (int i = 0; i < numSibs; i++)
            {
                GameObject sib = transform.parent.GetChild(i).gameObject;
                sib.transform.localPosition = startingPosition;
                sib.SetActive(i == sibIdx);
            }
        }

    }
}
