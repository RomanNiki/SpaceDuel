using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio.Groups
{
    [CreateAssetMenu(menuName = "SpaceDuel/Audio/AudioGroup", fileName = "AudioGroup")]
    public class AudioGroup : ScriptableObject
    {
        [SerializeField] private AudioGroup _parent;
        [SerializeField] private List<AudioGroup> _child = new();
        [field: SerializeField] public string Name { get; protected set; }
        [field: SerializeField, Range(0f, 1f)] public float Volume { get; protected set; }
        [SerializeField]  private VolumeStorage _volumeStorage;
        
        public virtual void SetVolume(float value)
        {
            if (value is > 0f and <= 1f)
            {
                _volumeStorage.SetParameter(name, value);
            }
        }

        public virtual float GetVolume()
        {
            if (_parent != null)
            {
                return _volumeStorage.GetParameter(name) * _parent.GetVolume();
            }

            return  _volumeStorage.GetParameter(name);;
        }

        public void AddChild(AudioGroup child)
        {
            _child.Add(child);
            child.SetParent(this);
        }

        public void RemoveChild(AudioGroup child)
        {
            _child.Remove(child);
            child.SetParent(null);
        }

        public void SetParent(AudioGroup parent)
        {
            if (this != parent && _child.Contains(parent) == false)
            {
                _parent = parent;
            }
        }
    }
}