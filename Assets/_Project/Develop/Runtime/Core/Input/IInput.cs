using System;
using _Project.Develop.Runtime.Core.Common.Enums;

namespace _Project.Develop.Runtime.Core.Input
{
    public interface IInput
    {
        event Action<TeamEnum> StartPrimaryShot;
        event Action<TeamEnum> CancelPrimaryShot;  
        event Action<TeamEnum> StartSecondaryShot;
        event Action<TeamEnum> CancelSecondaryShot;
        event Action<TeamEnum> StartAccelerate;
        event Action<TeamEnum> CancelAccelerate;
        event Action<TeamEnum, float> StartRotate;
        event Action<TeamEnum> CancelRotate;
        event Action Menu;
    }
}