using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio.Interfaces
{
    public interface ISoundService : IDisposable
    {
        public void Load(List<VolumeData> volumeDatas);

        public void Save(List<VolumeData> volumeDatas);
    }
}