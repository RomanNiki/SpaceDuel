using System;
using Core.Common.Enums;
using UnityEngine.AddressableAssets;

namespace Engine.Common
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