using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPQR;
using MySPQR;

namespace SPQR.Engine
{
    class Warrior : Engine.FightModule
    {
        public override string DisplayName
        {
            get { return " ~Arm Warrior"; }
        }

        internal enum Spells : int                      //This is a convenient list of all spells used by our combat routine
        {                                               //you can have search on wowhead.com for spell name, and get the id in url
            Devastate = 20243,
            Revenge = 6572,
            ShieldSlam = 23922,
            ShieldBlock = 2565,
            RagingBlow = 85288,
            WildStrike = 100130,
            Bloodthirst = 23881,
            MortalStrike = 12294,
            Slam = 1464,
            Overpower = 7384,
            ColossusSmash = 86346,
            HeroicStrike = 78,
            BerserkerRage = 18499,
			Execute = 5308,
			StormBolt = 107570,
			BattleShout = 6673,
			VictoryRush = 34428,
			Bloodbath = 12292
        }
        internal enum Auras : int                       //This is another convenient list of Auras used in our combat routine
        {												//you can have those in wowhead.com (again) and get the id in url
            Ultimatum = 122509,
			Recklessness = 1719,
			DeathSentence = 144442,
			SuddenDeath = 29725,
			suddenexecute = 139958,
			ColossusSmash = 108126,
        }

        public override void CombatLogic()              //This is the DPS / healing coutine, called in loop by SPQR all code here is executed
        {
            var TARGET = MySPQR.Internals.ObjectManager.Target;
            var ME = MySPQR.Internals.ObjectManager.WoWLocalPlayer;

            MySPQR.Internals.ActionBar.CastSpellById((int)Spells.BattleShout);
			if(TARGET.HealthPercent < 99)
                MySPQR.Internals.ActionBar.CastSpellById((int)Spells.Bloodbath);
			if(TARGET.HealthPercent < 99)
                MySPQR.Internals.ActionBar.CastSpellById((int)Spells.BerserkerRage);
			if(ME.HealthPercent < 70)
				MySPQR.Internals.ActionBar.CastSpellById((int)Spells.VictoryRush);
			if(TARGET.HealthPercent < 100 || ME.HasAurabyId((int)Auras.DeathSentence))
				MySPQR.Internals.ActionBar.CastSpellById((int)Spells.Execute);
			
			if(TARGET.HealthPercent < 20 || ME.Rage > 30)
				MySPQR.Internals.ActionBar.CastSpellById((int)Spells.Execute);
				
            MySPQR.Internals.ActionBar.CastSpellById((int)Spells.ColossusSmash);
			
			if(TARGET.HealthPercent < 99)
                MySPQR.Internals.ActionBar.CastSpellById((int)Spells.StormBolt);
				
			if(TARGET.HasAurabyId((int)Auras.ColossusSmash))
				MySPQR.Internals.ActionBar.CastSpellById((int)Spells.Slam);
				
			if(ME.HasAurabyId((int)Auras.Recklessness))
				MySPQR.Internals.ActionBar.CastSpellById((int)Spells.Slam);

			if(ME.HasAurabyId((int)Auras.suddenexecute))
				MySPQR.Internals.ActionBar.CastSpellById((int)Spells.Overpower);
				
			if(ME.Rage > 40)
                 MySPQR.Internals.ActionBar.CastSpellById((int)Spells.Slam);
			if(ME.Rage > 80)
                 MySPQR.Internals.ActionBar.CastSpellById((int)Spells.HeroicStrike);

            MySPQR.Internals.ActionBar.CastSpellById((int)Spells.Overpower);

            MySPQR.Internals.ActionBar.CastSpellById((int)Spells.MortalStrike);
        }

        public override void OnLoad()   //This is called when the Customclass is loaded in SPQR
        {
            
        }

        public override void OnClose() //This is called when the Customclass is unloaded in SPQR
        {
            
        }

        public override void OnStart() //This is called once, when you hit CTRL+X to start SPQR combat routine
        {

        }

        public override void OnStop() //This is called once, when you hit CTRL+X to stop SPQR combat routine
        {

        }
    }
}