using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static DifficultyValues;

namespace SincUltimate
{
    public class UltimateBehaviour : ModBehaviour
    {
        private const string UltimateTitle = "<b><color=#990000>Ultimate</color></b>";

        public override void OnActivate()
        {
            DevConsole.Console.LogInfo("--Ultimate Difficulty activated--");
            if(Difficulties.Count < 7)
            {
                Difficulties.Add(UltimateTitle, UltimateSetting());
            }

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene newScene, LoadSceneMode arg1)
        {
            if (newScene.name == "Customization")
            {
                HandleCustomizationScene();
            }
            else if(newScene.name == "MainScene")
            {
                HandleMainScene();
            }
        }

        private void HandleCustomizationScene()
        {
            GUICombobox difficultyCb = GameObject.Find("MainPanel/GameConf/DiffCombobox").GetComponent<GUICombobox>();
            difficultyCb.OnSelectedChanged.AddListener(() =>
            {
                GameObject customDifficultyButtonGo = GameObject.Find("MainPanel/GameConf/-FCustomDiff");
                if (difficultyCb.Selected == 6)
                {
                    customDifficultyButtonGo.SetActive(false);
                    ActorCustomization.StartLoanMonths = 12 * 5;
                    ActorCustomization.StartLoans = new int[1] { 120000 };
                    ActorCustomization.Instance.StartMoney.value = 1;
                    ActorCustomization.Instance.StartMoney.enabled = false;
                    ActorCustomization.Instance.Year.UpdateSelection(0);
                    ActorCustomization.Instance.Year.OnSelectedChanged.AddListener(() =>
                    {
                        if(difficultyCb.Selected == 6 && ActorCustomization.Instance.Year.Selected != 0)
                        {
                            ActorCustomization.Instance.Year.UpdateSelection(0);
                        }
                    });
                    Globals.FirstStart = true;
                    ActorCustomization.Instance.CreativitySlider.value = 0;
                    ActorCustomization.Instance.CreativitySlider.onValueChanged.AddListener((float f) =>
                    {
                        if(difficultyCb.Selected == 6 && ActorCustomization.Instance.CreativitySlider.value != 0)
                        {
                            ActorCustomization.Instance.CreativitySlider.value = 0;
                        }
                    });
                   // ActorCustomization.Instance.PersonalityChosen[1].Selected = 1; Maybe select personality at some point
                }
                else
                {
                    customDifficultyButtonGo.SetActive(true);
                    Globals.FirstStart = false;
                }
            });

        }

        private DifficultySetting UltimateSetting()
        {
            var diff = new DifficultySetting("Ultimate")
            {
                DefaultStartMoney = -30000f,
                MaxSkillPoints = 0.35f,
                MaxSpecPoints = 0.25f,
                DesignDocumentSpeedBonus = .5f,
                AlphaSpeedBonus = .5f,
                EmployeeSkillGainBonus = .5f,
                ContractIncomeFactor = .5f,
                PlayerLicenseCostFactor = 2f,
                RentCostFactor = 2f,
                CreativityFactor = 3f,
                PressReleaseHypeDeadline = 8f,
                ProductReputationFactor = 0.00001f,
                RecognitionSalesFactor = 0.9f,
                AICompanyAverageSavy = 2f,
                Taxes = 0.75f,
                MarketingEndQualityEstimate = 1f,
                TakeoverMonths = 1f,
                FounderDividend = 0.95f,
                Contracts = 0f,
                Deals = 0f,
                Loans = 1f,
                Publisher = 0f
            };
            return diff;
        }

        private void HandleMainScene()
        {
            if(GameData.SelectedDifficulty.Name == "Ultimate")
            {
                DevConsole.Console.LogInfo("Preparing MainScene for Ultimate difficulty, please wait!");

                //FIRST START
                if (Globals.FirstStart)
                {
                    DevConsole.Console.Log("Preparing for first start...");
                    ChangeFounderAge();
                }

                //Remove OS
                DevConsole.Console.Log("Adding listener for software products combobox to deactivate OS for players");
                GUICombobox softwareProductsCb = GameObject.Find("DesignDocumentWindow/ContentPanel/PageContent/InfoPanel/MainInfo/Combobox").GetComponent<GUICombobox>();
                softwareProductsCb.OnSelectedChanged.AddListener(() => {
                    if(GameData.SelectedDifficulty.Name == "Ultimate" && softwareProductsCb.Selected == 0 && !Globals.CanOS())
                    {
                        softwareProductsCb.Selected = 1;
                    }
                });
                
                DevConsole.Console.LogInfo("MainScene prepared, have fun =)");
            }
        }

        private async void ChangeFounderAge()
        {
            DevConsole.Console.Log("Waiting for founders to be initialized...");
            await Task.Run(() => {
                while(GameSettings.Instance.Founders.Count < 1)
                {

                }
                DevConsole.Console.Log("Founders initialized...");
                foreach (var founder in GameSettings.Instance.Founders)
                {
                    if (founder.employee.GetAge() < 40)
                    {
                        DevConsole.Console.Log($"Changing age of founder \"{founder.employee.Name}\" to 40 years");
                        var diffAge = 40 - founder.employee.GetAgeFlat();
                        var founderAge = new SDateTime(0, 40);
                        founder.employee.BirthDate = founderAge;
                    }
                    else
                    {
                        DevConsole.Console.LogWarning($"Founder \"{founder.employee.Name}\" is already over 40 years old. Age {founder.employee.GetAgeFlat()}");
                    }
                }
            });
        }

        public override void OnDeactivate()
        {
            DevConsole.Console.LogInfo("--Ultimate Difficulty deactivated--");

            if (Difficulties.Count >= 7)
            {
                Difficulties.Remove(UltimateTitle);
            }

            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }
    }
}
