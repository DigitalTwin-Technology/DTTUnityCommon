// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEngine;

namespace DTTUnityCore.Atributes
{
    /// <summary>
    /// Specify a required interface type for the field
    /// </summary>
    public sealed class RequireInterfaceAttribute : PropertyAttribute
    {
        // Interface type.
        public System.Type requiredType { get; private set; }
        /// <summary>
        /// Requiring implementation of the <see cref="T:RequireInterfaceAttribute"/> interface.
        /// </summary>
        /// <param name="type">Interface type.</param>
        public RequireInterfaceAttribute(System.Type type)
        {
            requiredType = type;
        }
    }
}

