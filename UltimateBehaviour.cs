using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DifficultyValues;

namespace SincUltimate
{
    public class UltimateBehaviour : ModBehaviour
    {

        public override void OnActivate()
        {
            DevConsole.Console.LogInfo("--Ultimate Difficulty activated--");
            if(Difficulties.Count < 7)
            {
                Difficulties.Add("Ultimate", UltimateSetting());
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
                    ActorCustomization.StartLoans = new int[1] { 60000 };
                    ActorCustomization.Instance.StartMoney.value = 1;
                    ActorCustomization.Instance.StartMoney.enabled = false;
                   // ActorCustomization.Instance.PersonalityChosen[1].Selected = 1; Maybe select personality at some point
                }
                else
                {
                    customDifficultyButtonGo.SetActive(true);
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
                Taxes = 0.5f,
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
                DevConsole.Console.Log("Preparing MainScene for Ultimate difficulty, please wait!");
                DevConsole.Console.Log("MainScene prepared, have fun =)");
            }
        }

        public override void OnDeactivate()
        {
            DevConsole.Console.LogInfo("--Ultimate Difficulty deactivated--");

            if (Difficulties.Count >= 7)
            {
                Difficulties.Remove("Ultimate");
            }

            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }
    }
}
