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
            title.fontSize = 24;
            var description = WindowManager.SpawnLabel();
            description.text = "Get the ultimate difficulty for your game of SINC!";
            WindowManager.AddElementToElement(title.gameObject, parent.gameObject, new Rect(15, 15, 192, 32), Rect.zero);
            WindowManager.AddElementToElement(description.gameObject, parent.gameObject, new Rect(15, 60, 192, 32), Rect.zero);
        }
    }
}
