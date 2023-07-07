// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEngine;

namespace DTTUnityCore
{
    public static class Utilities
    {
        public static void SafeDestroy(Object gameObject)
        { 
            if(gameObject == null) { return; }

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