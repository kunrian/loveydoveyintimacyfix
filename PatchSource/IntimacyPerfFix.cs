using HarmonyLib;
using Verse;

namespace IntimacyPerfFix
{
    // This class auto-loads because RimWorld instantiates all Mod subclasses
    // found in any DLL inside a mod's Assemblies folder.
    // Since this DLL sits next to LoveyDoveySexWithEuterpe.dll in the same
    // mod's Assemblies folder, it loads as part of the Intimacy mod — no
    // separate mod entry needed.
    [StaticConstructorOnStartup]
    public static class IntimacyPerfFixInit
    {
        static IntimacyPerfFixInit()
        {
            var harmony = new Harmony("local.intimacy.perffix");
            harmony.PatchAll();
            Log.Message("[IntimacyPerfFix] Patched SetAllGraphicsDirty per-tick spam in lovin animations.");
        }
    }
}
