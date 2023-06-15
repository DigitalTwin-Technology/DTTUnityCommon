// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using Unity.Profiling;

namespace DTTUnityCommon.Profiler
{
    public sealed partial class ProfilerRecorderHelper
    {
        private static class ProfilerStatNames
        {
            #region Internal
            private static readonly string MainThread = "Main Thread";
            #endregion

            #region Memory
            private static readonly string SystemUsedMemory = "System Used Memory";
            private static readonly string GCReservedMemory = "GC Reserved Memory";
            #endregion

            #region Render

            private static readonly string SetPassCallsCountName = "SetPass Calls Count";
            private static readonly string DrawCallsCount = "Draw Calls Count";
            private static readonly string TotalBatchesCount = "Total Batches Count";
            private static readonly string TrianglesCount = "Triangles Count";
            private static readonly string VerticesCount = "Vertices Count";
            private static readonly string ShadowCastersCount = "Shadow Casters Count";

            #endregion

            public static ProfilerRecorder BuildProfilerRecorder(ProfilerStatType profilerStatType, int capacity = 1)
            {
                return ProfilerRecorder.StartNew(GetProfilerCategory(profilerStatType), GetStatName(profilerStatType), capacity);
            }

            public static string GetStatName(ProfilerStatType profilerStatType)
            {
                switch (profilerStatType)
                {
                    default:
                        return string.Empty;

                    #region Internal
                    case ProfilerStatType.MainThread:
                        return MainThread;
                    #endregion

                    #region Memory
                    case ProfilerStatType.SystemUsedMemory:
                        return SystemUsedMemory;
                    case ProfilerStatType.GCReservedMemory:
                        return GCReservedMemory;
                    #endregion

                    #region Render
                    case ProfilerStatType.SetPassCallsCount:
                        return SetPassCallsCountName;
                    case ProfilerStatType.DrawCallsCount:
                        return DrawCallsCount;
                    case ProfilerStatType.TotalBatchesCount:
                        return TotalBatchesCount;
                    case ProfilerStatType.TrianglesCount:
                        return TrianglesCount;
                    case ProfilerStatType.VerticesCount:
                        return VerticesCount;
                    case ProfilerStatType.ShadowCastersCount:
                        return ShadowCastersCount;
                        #endregion
                }
            }

            public static ProfilerCategory GetProfilerCategory(ProfilerStatType profilerStatType)
            {
                switch (profilerStatType)
                {
                    default:
                        return ProfilerCategory.Internal;

                    #region Internal
                    case ProfilerStatType.MainThread:
                        return ProfilerCategory.Internal;
                    #endregion

                    #region Memory
                    case ProfilerStatType.SystemUsedMemory:
                        return ProfilerCategory.Memory;
                    case ProfilerStatType.GCReservedMemory:
                        return ProfilerCategory.Memory; ;
                    #endregion

                    #region Render
                    case ProfilerStatType.SetPassCallsCount:
                        return ProfilerCategory.Render;
                    case ProfilerStatType.DrawCallsCount:
                        return ProfilerCategory.Render;
                    case ProfilerStatType.TotalBatchesCount:
                        return ProfilerCategory.Render;
                    case ProfilerStatType.TrianglesCount:
                        return ProfilerCategory.Render;
                    case ProfilerStatType.VerticesCount:
                        return ProfilerCategory.Render;
                    case ProfilerStatType.ShadowCastersCount:
                        return ProfilerCategory.Render;
                        #endregion
                }
            }
        }
    }
}

    



