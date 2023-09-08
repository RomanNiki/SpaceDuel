using System;
using JetBrains.Annotations;

namespace Modules.Container.Scripts
{
    [MeansImplicitUse(ImplicitUseKindFlags.Access)]
    [AttributeUsage(AttributeTargets.Method)]
    public class ContextEvent : Attribute
    {
    }
}