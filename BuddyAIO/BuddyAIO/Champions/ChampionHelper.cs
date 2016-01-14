using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;

namespace BuddyAIO.Champions
{
    public abstract class IChampion
    {
        public abstract void OnLoad();
        public abstract void OnUpdate();
        public virtual void AfterAA() { }
        public abstract EloBuddy.Champion Hero();
    }

    public abstract class IModule
    {
        public virtual void OnLoad() { }
        public virtual bool ShouldDo { get; }
        public virtual void Do() { }
        public abstract ModuleType GetModuleType();
        public virtual void MenuCreate() { }
    }

    public enum ModuleType
    {
        OnUpdate, AfterAA, Other
    }
}
