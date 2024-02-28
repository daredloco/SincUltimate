using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SincUltimate
{
    public class MyModMeta : ModMeta
    {
        public override string Name => "Ultimate Difficulty";

        public override void ConstructOptionsScreen(RectTransform parent, bool inGame)
        {
            var title = WindowManager.SpawnLabel();
            title.text = "<b>Ultimate Difficulty</b>";
            title.fontSize = 28;
            var author = WindowManager.SpawnLabel();
            author.text = "by daRedLoCo";
            author.fontSize = 20;
            var thnks = WindowManager.SpawnLabel();
            thnks.fontSize = 14;
            thnks.text = "Playtester/Ideagiver/Description: CF";
            var description = WindowManager.SpawnLabel();
            description.fontSize = 16;
            description.text = "Brings a real challenge to Software Inc!";
            var debugToggle = WindowManager.SpawnCheckbox();
            debugToggle.GetComponentInChildren<Text>().text = "Debug Mode";
            debugToggle.isOn = Globals.DebugMode;
            debugToggle.onValueChanged.AddListener((bool state) => {
                DevConsole.Console.LogInfo("Sinc Ultimate debugmode is now set to " + state.ToString());
                Globals.DebugMode = debugToggle.isOn;
            });
            WindowManager.AddElementToElement(title.gameObject, parent.gameObject, new Rect(15, 15, 320, 28), Rect.zero);
            WindowManager.AddElementToElement(author.gameObject, parent.gameObject, new Rect(15, 50, 320, 20), Rect.zero);
            WindowManager.AddElementToElement(thnks.gameObject, parent.gameObject, new Rect(15, 75, 260, 14), Rect.zero);
            WindowManager.AddElementToElement(description.gameObject, parent.gameObject, new Rect(15, 95, 320, 32), Rect.zero);
            WindowManager.AddElementToElement(debugToggle.gameObject, parent.gameObject, new Rect(15, 135, 260, 32), Rect.zero);
        }
    }
}
