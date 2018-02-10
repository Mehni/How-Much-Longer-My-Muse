using Verse;
using Harmony;
using RimWorld;
using System;

namespace PostArmorDamage
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {

        static HarmonyPatches()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("mehni.rimworld.insepidinspirationinspector.main");

            harmony.Patch(AccessTools.Property(typeof(Inspiration), nameof(Inspiration.InspectLine)).GetGetMethod(false),  
                new HarmonyMethod(typeof(HarmonyPatches), nameof(InspectLine_Prefix)), null);
        }

        private static bool InspectLine_Prefix(ref string __result, Inspiration __instance)
        {
            //I've been told to put comments in code, so here goes
            //This changes the inspector string to show the expiration string on inspirations.
            //Just like the mod description says it does.
            int num = Convert.ToInt32( (__instance.def.baseDurationDays * 60000) - (__instance.Age));
            __result = __instance.def.baseInspectLine + " (" + "LifespanExpiry".Translate() + " " + num.ToStringTicksToPeriod(true, true, false) + ")";
            return false;
        }
    }
}
