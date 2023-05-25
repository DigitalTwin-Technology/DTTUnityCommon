using System;

namespace DTTUnityCommon.DataStructs
{
    public interface IMetaDataNodeBase : IEquatable<IMetaDataNodeBase>
    {
        string Id { get; }
    }
}

