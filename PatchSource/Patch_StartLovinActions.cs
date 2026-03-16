using HarmonyLib;
using LoveyDoveySexWithEuterpe;

namespace IntimacyPerfFix
{
    /// <summary>
    /// Patches JobDriver_Sex.StartLovinActions() which is called every tick
    /// during the lovin animation toil. The original calls SetAllGraphicsDirty()
    /// unconditionally every tick, causing a full pawn graphics rebuild each frame.
    /// Any mod with postfixes on WornGraphicPath (like THIGAPPE/APPUtilities)
    /// then pays its full uncached lookup cost thousands of times.
    ///
    /// Fix: only dirty graphics once when IsCurrentlyLovin first flips to true.
    /// </summary>
    [HarmonyPatch(typeof(JobDriver_Sex), "StartLovinActions")]
    public static class Patch_JobDriver_Sex_StartLovinActions
    {
        [HarmonyPrefix]
        public static bool Prefix(JobDriver_Sex __instance)
        {
            if (!__instance.IsCurrentlyLovin)
            {
                __instance.IsCurrentlyLovin = true;
                __instance.pawn?.Drawer?.renderer?.SetAllGraphicsDirty();
            }
            return false; // skip original
        }
    }

    /// <summary>
    /// Same fix for JobDriver_Sex_Mechanitor which is a separate class
    /// (does NOT inherit from JobDriver_Sex) but has the identical bug.
    /// </summary>
    [HarmonyPatch(typeof(JobDriver_Sex_Mechanitor), "StartLovinActions")]
    public static class Patch_JobDriver_Sex_Mechanitor_StartLovinActions
    {
        [HarmonyPrefix]
        public static bool Prefix(JobDriver_Sex_Mechanitor __instance)
        {
            if (!__instance.IsCurrentlyLovin)
            {
                __instance.IsCurrentlyLovin = true;
                __instance.pawn?.Drawer?.renderer?.SetAllGraphicsDirty();
            }
            return false; // skip original
        }
    }
}
