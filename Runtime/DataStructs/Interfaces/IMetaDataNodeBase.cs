// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System;

namespace DTTUnityCore.DataStructs
{
    public interface IMetaDataNodeBase : IEquatable<IMetaDataNodeBase>
    {
        string Id { get; }
    }
}

