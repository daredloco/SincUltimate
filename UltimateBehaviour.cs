using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Globals.WriteLog("--Ultimate Difficulty activated--","info");
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
                    ChangeCustomizationTooltips();
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

        private void ChangeCustomizationTooltips()
        {
            var creativityLabelTooltip = GameObject.Find("MainPanel/SubSkill/Skill/CreativityPanel/Text").GetComponent<GUIToolTipper>();
            var creativitySliderTooltip = GameObject.Find("MainPanel/SubSkill/Skill/CreativityPanel/Slider").GetComponentInChildren<GUIToolTipper>();
            var yearDropdown = GameObject.Find("MainPanel/GameConf/Year");
            var yearDropdownTooltip = yearDropdown.AddComponent<GUIToolTipper>();
            var loanSlider = GameObject.Find("MainPanel/GameConf/MoneySlider/Handle Slide Area/Handle");
            var loanSliderTooltip = loanSlider.AddComponent<GUIToolTipper>();

            yearDropdownTooltip.TooltipDescription = "ULTIMATE_YEAR_TOOLTIP";
            loanSliderTooltip.TooltipDescription = "ULTIMATE_LOAN_TOOLTIP";
            creativityLabelTooltip.TooltipDescription = "ULTIMATE_CREATIVITY_TOOLTIP";
            creativitySliderTooltip.TooltipDescription = "ULTIMATE_CREATIVITY_TOOLTIP";
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
                Globals.WriteLog("Preparing MainScene for Ultimate difficulty, please wait!", "info");

                //FIRST START
                if (Globals.FirstStart)
                {
                    Globals.WriteLog("Preparing for first start...");
                    ChangeFounderAge();
                }

                //Remove OS
                Globals.WriteLog("Adding listener for software products combobox to deactivate OS for players");
                GUICombobox softwareProductsCb = GameObject.Find("DesignDocumentWindow/ContentPanel/PageContent/InfoPanel/MainInfo/Combobox").GetComponent<GUICombobox>();
                softwareProductsCb.OnSelectedChanged.AddListener(() => {
                    if(GameData.SelectedDifficulty.Name == "Ultimate" && softwareProductsCb.Selected == 0 && !Globals.CanOS())
                    {
                        softwareProductsCb.Selected = 1;
                    }
                });

                Globals.WriteLog("MainScene prepared, have fun =)", "info");
            }
        }

        private async void ChangeFounderAge()
        {
            Globals.WriteLog("Waiting for founders to be initialized...");
            await Task.Run(() => {
                while(GameSettings.Instance.Founders.Count < 1)
                {

                }
                Globals.WriteLog("Founders initialized...");
                foreach (var founder in GameSettings.Instance.Founders)
                {
                    if (founder.employee.GetAge() < 40)
                    {
                        Globals.WriteLog($"Changing age of founder \"{founder.employee.Name}\" to 40 years");
                        var diffAge = 40 - founder.employee.GetAgeFlat();
                        var founderAge = new SDateTime(0, 40);
                        founder.employee.BirthDate = founderAge;
                    }
                    else
                    {
                        Globals.WriteLog($"Founder \"{founder.employee.Name}\" is already over 40 years old. Age {founder.employee.GetAgeFlat()}", "warn");
                    }
                }
            });
        }

        public override void OnDeactivate()
        {
            Globals.WriteLog("--Ultimate Difficulty deactivated--", "info");

            if (Difficulties.Count >= 7)
            {
                Difficulties.Remove(UltimateTitle);
            }

            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }
    }
}
