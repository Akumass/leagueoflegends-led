﻿using Games.LeagueOfLegends.ChampionModules.Common;
using Games.LeagueOfLegends.Model;
using LedDashboardCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games.LeagueOfLegends.ChampionModules
{
    [Champion(CHAMPION_NAME)]
    public sealed class TwistedFateModule : ChampionModule
    {
        public const string CHAMPION_NAME = "TwistedFate";
        // Variables

        // Champion-specific Variables
        HSVColor RColor = new HSVColor(0.81f, 0.43f, 1);
        HSVColor RColor2 = new HSVColor(0.91f, 0.87f, 1);

        public TwistedFateModule(int ledCount, GameState gameState, LightingMode preferredLightMode, AbilityCastPreference preferredCastMode)
            : base(ledCount, CHAMPION_NAME, gameState, preferredLightMode, preferredCastMode, true)
        {
            // Initialization for the champion module occurs here.
        }

        protected override AbilityCastMode GetQCastMode() => AbilityCastMode.Normal();
        protected override AbilityCastMode GetWCastMode() => AbilityCastMode.Instant(6000, 1);
        protected override AbilityCastMode GetECastMode() => AbilityCastMode.UnCastable();
        protected override AbilityCastMode GetRCastMode() => AbilityCastMode.Instant(6000, 1, AbilityCastMode.Normal());

        protected override async Task OnCastQ()
        {
            if (LightingMode == LightingMode.Keyboard)
            {
                // only for keyboard, for line mode it's too distracting
                await Task.Delay(100);
                RunAnimationOnce("q_cast", timeScale: 0.4f);
            }
        }
        protected override async Task OnCastW()
        {
            RunAnimationInLoop("w_loop", 5500, 0.1f, 0.08f);
        }
        protected override async Task OnCastR()
        {
            await Animator.ColorBurst(RColor, 0.05f, RColor2);
        }

        protected override async Task OnRecastW()
        {
            Animator.StopCurrentAnimation();
            Animator.ColorBurst(new HSVColor(0, 0, 1));
        }
        protected override async Task OnRecastR()
        {
            if (LightingMode == LightingMode.Keyboard)
            {
                RunAnimationOnce("r_cast", fadeOutAfterRate: 0.1f, timeScale: 0.22f);
                await Task.Delay(300);
                Animator.ColorBurst(new HSVColor(0, 0, 1), 0.08f);
            }
            else
            {
                RunAnimationOnce("r_cast_line", true, timeScale: 0.3f);
                await Animator.ColorBurst(new HSVColor(0, 0, 1));
            }
        }
    }
}