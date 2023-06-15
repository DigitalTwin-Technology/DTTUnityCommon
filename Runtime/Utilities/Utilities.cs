// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEngine;

namespace DTTUnityCommon
{
    public class Utilities
    {
        public static void SafeDestroy(GameObject gameObject)
        { 
            if(Application.isEditor) 
            {
                Object.DestroyImmediate(gameObject);
            }
            else
            {
                Object.Destroy(gameObject);
            }
        }
    }
}