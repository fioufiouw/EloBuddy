using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyAIO.Utility.Config;
using Menu = BuddyAIO.Utility.Config.MenuExtensions;
using EloBuddy.SDK;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using System.Drawing;

namespace BuddyAIO.Champions.Katarina.Modules.Misc
{
    class DrawPredictedQ : IModule
    {
        void IModule.MenuCreate()
        {
            DrawSpells drawspells = new DrawSpells();
            var mMenu = drawspells.mMenu;
            mMenu.AddCheckBox("drawpredictedq", "Draw Predicted Q");

        }
        void IModule.OnLoad()
        {
            MenuIndex.Drawings drawings = new MenuIndex.Drawings();
            if (!drawings.DrawPredictedQ)
                return;
                Drawing.OnDraw += delegate
            {
                var cursorpos = Game.CursorPos;
                var obj = ObjectManager.Get<Obj_AI_Base>().Where(o => o.IsInRange(cursorpos, 400));

                Drawing.DrawLine(obj.OrderBy(o => o.Distance(cursorpos)).ElementAtOrDefault(1).Position.WorldToScreen(), obj.OrderBy(o => o.Distance(cursorpos)).ElementAtOrDefault(2).Position.WorldToScreen(), 1, Color.White);
                Drawing.DrawLine(obj.OrderBy(o => o.Distance(cursorpos)).ElementAtOrDefault(2).Position.WorldToScreen(), obj.OrderBy(o => o.Distance(cursorpos)).ElementAtOrDefault(3).Position.WorldToScreen(), 1, Color.White);
                Drawing.DrawLine(obj.OrderBy(o => o.Distance(cursorpos)).ElementAtOrDefault(3).Position.WorldToScreen(), obj.OrderBy(o => o.Distance(cursorpos)).ElementAtOrDefault(4).Position.WorldToScreen(), 1, Color.White);
            };
        }
        ModuleType IModule.GetModuleType()
        {
            return ModuleType.Other;
        }
        bool IModule.ShouldDo()
        {
            return false;
        }

        void IModule.Do()
        {
            
        }
    }
}
