using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;

namespace BuddyAIO.Champions
{
    public interface IChampion
    {
        void OnLoad();
        void OnUpdate();
        void AfterAA();
        EloBuddy.Champion Hero();
    }

    public interface IModule
    {
        void OnLoad();
        bool ShouldDo();
        void Do();
        ModuleType GetModuleType();
        void MenuCreate();
    }

    public enum ModuleType
    {
        OnUpdate, AfterAA, Other
    }
}
