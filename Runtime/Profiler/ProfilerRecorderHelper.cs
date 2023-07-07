// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System;
using System.Collections.Generic;
using System.Text;
using Unity.Profiling;

namespace DTTUnityCore.Profiler
{
    public sealed partial class ProfilerRecorderHelper : IDisposable
    {

        public ProfilerRecorder ProfilerRecorder { get; set; }
        public string Name { get; private set; }
        public ProfilerStatType ProfilerStatType { get; private set; }

        public ProfilerRecorderHelper(ProfilerMarker profilerMarker, string name, int capacity = 1, ProfilerRecorderOptions profilerRecorderOptions = ProfilerRecorderOptions.Default)
        {
            ProfilerStatType = ProfilerStatType.ProfilerMarker;
            Name = name;

            ProfilerRecorder = ProfilerRecorder.StartNew(profilerMarker, capacity, profilerRecorderOptions);
        }

        public ProfilerRecorderHelper(ProfilerStatType profilerStatType, int capacity = 1)
        {
            ProfilerStatType = profilerStatType;
            Name = ProfilerStatNames.GetStatName(profilerStatType);

            ProfilerRecorder = ProfilerStatNames.BuildProfilerRecorder(profilerStatType, capacity);
        }

        public void Dispose()
        {
            ProfilerRecorder.Dispose();
        }

        public override string ToString()
        {
            if (!ProfilerRecorder.Valid)
            {
                return new StringBuilder($"{Name} is invalid stat").ToString();
            }

            if (ProfilerStatType == ProfilerStatType.MainThread)
            {
                double frameTimeMilliSeconds = GetRecorderFrameAverage(ProfilerRecorder) * (1e-6f);
                double fps = 1000.0 / frameTimeMilliSeconds;
                return new StringBuilder($"{Name}: {frameTimeMilliSeconds:F1} ms, FPS: {fps:F0}").ToString();
            }
            else if (ProfilerStatType == ProfilerStatType.SystemUsedMemory || ProfilerStatType == ProfilerStatType.GCReservedMemory)
            {
                return new StringBuilder($"{Name}: {ProfilerRecorder.LastValue / (1048576)} MB").ToString();
            }
            else
            {
                return new StringBuilder($"{Name}: {ProfilerRecorder.LastValue}").ToString();
            }
        }

        private static double GetRecorderFrameAverage(ProfilerRecorder recorder)
        {
            var samplesCount = recorder.Capacity;
            if (samplesCount == 0)
            {
                return 0;
            }

            List<ProfilerRecorderSample> samples = new List<ProfilerRecorderSample>(samplesCount);
            recorder.CopyTo(samples);

            double r = 0;
            for (var i = 0; i < samples.Count; ++i)
            {
                r += samples[i].Value;
            }
            r /= samplesCount;

            return r;
        }
    }
}

    



