using Modding;
using System;

namespace MillibelleTheBanker
{
    public class MillibelleTheBankerMod : Mod
    {
        private static MillibelleTheBankerMod? _instance;

        internal static MillibelleTheBankerMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(MillibelleTheBankerMod)} was never constructed");
                }
                return _instance;
            }
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public MillibelleTheBankerMod() : base("MillibelleTheBanker")
        {
            _instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");

            On.HutongGames.PlayMaker.Actions.IntCompare.OnEnter += BankLimits;
            On.HutongGames.PlayMaker.Actions.SetTextMeshProText.OnEnter += OnTextSet;

            Log("Initialized");
        }

        private void OnTextSet(On.HutongGames.PlayMaker.Actions.SetTextMeshProText.orig_OnEnter orig, HutongGames.PlayMaker.Actions.SetTextMeshProText self)
        {
            if (self.Fsm.GameObject.name == "Txt Max Balance Amount" && self.Fsm.Name == "get_game_text" && self.Fsm.GameObject.scene.name == "Fungus3_35")
            {
                self.Fsm.GetFsmString("Text").Value = "99999";
            }

            orig(self);
        }

        private void BankLimits(On.HutongGames.PlayMaker.Actions.IntCompare.orig_OnEnter orig, HutongGames.PlayMaker.Actions.IntCompare self)
        {
            if (self.Fsm.GameObject.name == "Deposit Menu" && self.Fsm.Name == "Menu Control" && self.Fsm.GameObject.scene.name == "Fungus3_35" && self.State.Name == "Check Balance Limit")
            {
                self.integer2.Value = 99999;
            }

            else if (self.Fsm.GameObject.name == "Banker" && self.Fsm.Name == "Conversation Control" && self.State.Name == "Check Theft" && self.integer2.Name == "Theft Balance")
            {
                self.integer2.Value = 99999;
            }

            else if (self.Fsm.GameObject.name == "Menu 1" && self.Fsm.Name == "Confirm Control" && self.State.Name == "Can Deposit?")
            {
                self.integer2.Value = 99999;
            }
            orig(self);
        }
    }
}
