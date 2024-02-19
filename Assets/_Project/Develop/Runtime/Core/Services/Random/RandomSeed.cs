using System;
using System.Security.Cryptography;

namespace _Project.Develop.Runtime.Core.Services.Random
{
    //Source https://github.com/vangogih/Dont-Use-UnityEngine.Random/tree/master
    public static class RandomSeed
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

        /// <summary>Create seed based on DateTime structure</summary>
        public static int Time()
        {
            return DateTime.UtcNow.GetHashCode();
        }

        /// <summary>Create seed based on <see cref="Environment.TickCount"/> and <see cref="System.Guid"/></summary>
        public static int Guid()
        {
            return Environment.TickCount ^ System.Guid.NewGuid().GetHashCode();
        }

        /// <summary>Create seed based on <see cref="System.Security.Cryptography.RandomNumberGenerator"/></summary>
        public static int Crypto()
        {
            var bytes = new byte[4];
            RandomNumberGenerator.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}