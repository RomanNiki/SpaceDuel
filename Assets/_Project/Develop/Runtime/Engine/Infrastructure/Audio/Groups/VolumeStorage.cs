using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio.Groups
{
    [CreateAssetMenu(menuName = "SpaceDuel/Audio/VolumeStorage", fileName = "VolumeStorage")]
    public class VolumeStorage : ScriptableObject
    {
        private readonly Dictionary<string, float> _parameters = new();

        public void SetParameter(string volumeName, float value)
        {
            _parameters[volumeName] = value;
        }
        
        public float GetParameter(string volumeName)
        {
            return _parameters.GetValueOrDefault(volumeName, 0f);
        }
    }
}