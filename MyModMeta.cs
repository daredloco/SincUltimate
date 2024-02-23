using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SincUltimate
{
    public class MyModMeta : ModMeta
    {
        public override string Name => "Ultimate Difficulty";

        public override void ConstructOptionsScreen(RectTransform parent, bool inGame)
        {
            var title = WindowManager.SpawnLabel();
            title.text = "Ultimate Difficulty - by daRedLoCo";
            title.fontSize = 32;
            var description = WindowManager.SpawnLabel();
            description.fontSize = 24;
            description.text = "Get the ultimate difficulty for your game of SINC!";
            var thnks = WindowManager.SpawnLabel();
            thnks.fontSize = 12;
            thnks.text = "Special thanks goes to CF as he gave me the idea to make the mod and he did all the playtesting because I'm bad...";
            WindowManager.AddElementToElement(title.gameObject, parent.gameObject, new Rect(15, 15, 192, 32), Rect.zero);
            WindowManager.AddElementToElement(description.gameObject, parent.gameObject, new Rect(15, 60, 192, 32), Rect.zero);
            WindowManager.AddElementToElement(thnks.gameObject, parent.gameObject, new Rect(15, 100, 260, 96), Rect.zero);
        }
    }
}
