using RimWorld;
using Verse;
using HarmonyLib;
using System;
using System.Reflection;


namespace IdeologyDrugMod
{
    [StaticConstructorOnStartup]
    internal static class DoPatching
    {
        static DoPatching()
        {
            new Harmony("Ether.IdeoDrugMod.").PatchAll();
        }
    }
    
    [HarmonyPatch(typeof(IdeoUIUtility), "DoPrecepts")]
    public static class PreceptPatch
    {
        public static void Postfix(ref float curY, float width, Ideo ideo, IdeoEditMode editMode)
        {
            Func<PreceptDef, bool> myFunc = (PreceptDef p) => p.preceptClass == typeof(Precept_Drug);

            object[] newargs = new object[9];

            string categoryLabel = "ETH_VeneratedDrugs".Translate();
            string addPreceptLabel = "ETH_Drugs".Translate();

            newargs[0] = categoryLabel;
            newargs[1] = addPreceptLabel;
            newargs[2] = false;
            newargs[3] = ideo;
            newargs[4] = editMode;
            newargs[5] = curY; 
            newargs[6] = width;
            newargs[7] = myFunc;
            newargs[8] = false;
            
            /*Log.Message("_______________________");
            for (int i = 0; i < newargs.Length; i++)
            {
                Log.Message(newargs[i].ToString());
            }
            Log.Message("_______________________");*/

            typeof(IdeoUIUtility).GetMethod
                ("DoPreceptsInt", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null,newargs);


        }
    }
    [HarmonyPatch(typeof(IdeoUIUtility), "DoIdeoDetails")]
    public static class IdeoWindowSize
    {
        public static void Prefix(ref float viewHeight)
        {
            viewHeight += 1300f;
        }
    }

    [HarmonyPatch(typeof(IdeoUIUtility), "DoAppearanceItems")]
    public static class IdeoAppearanceLocation
    {
        public static void Prefix(ref float curY)
        {
            curY += 230f;
        }
    }


}