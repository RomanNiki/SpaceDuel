using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using UnityEngine.AddressableAssets;

namespace _Project.Develop.Runtime.Engine.Common
{
    [Serializable]
    public struct AssetPair
    {
        public ObjectId Id;
        public AssetReference AssetReference;
        public int InitializeSize;

        public AssetPair(ObjectId id, AssetReference assetReference, int initializeSize)
        {
            Id = id;
            AssetReference = assetReference;
            InitializeSize = initializeSize;
        }
    }
}