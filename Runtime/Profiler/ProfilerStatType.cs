﻿// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

namespace DTTUnityCore.Profiler
{
    public enum ProfilerStatType
    {
        #region Custom
        /// <summary>
        /// Profile the main thead rate
        /// </summary>
        ProfilerMarker,
        #endregion

        #region Internal
        /// <summary>
        /// Profile the main thead rate
        /// </summary>
        MainThread,
        #endregion

        #region Memory
        /// <summary>
        /// System Used Memory (MB)
        /// </summary>
        SystemUsedMemory,

        /// <summary>
        /// Graphic Card Reserved Memory (MB)
        /// </summary>
        GCReservedMemory,
        #endregion

        #region Render
        SetPassCallsCount,
        DrawCallsCount,
        TotalBatchesCount,
        TrianglesCount,
        VerticesCount,
        ShadowCastersCount
        #endregion
    }
}




