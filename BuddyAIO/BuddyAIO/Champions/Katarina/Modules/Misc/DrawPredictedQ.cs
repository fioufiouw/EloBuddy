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
        public override void MenuCreate()
        {
            try
            {
                DrawSpells drawspells = new DrawSpells();
                var mMenu = drawspells.mMenu;
                mMenu.AddCheckBox("drawpredictedq", "Draw Predicted Q");
            }
            catch
            {
                Chat.Print("BuddyAIO:: An error has occured!", System.Drawing.Color.Red);
                Console.WriteLine("Exception caught - Code[KATARINA.DRAWPREDICTEDQ.MENUCREATE]");
                
            }

        }
        public override void OnLoad()
        {
            try
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
            catch
            {
                Chat.Print("BuddyAIO:: An error has occured!", System.Drawing.Color.Red);
                Console.WriteLine("Exception caught - Code[KATARINA.DRAWPREDICTEDQ.ONLOAD]");
                
            }
        }
        public override ModuleType GetModuleType()
        {
            return ModuleType.Other;
        }
    }
}
