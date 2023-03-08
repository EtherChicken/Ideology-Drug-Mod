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
        // This postfix takes the args from the DoPrecepts method inside the IdeoUIUtility class
        // goes after the entire method has completed, giving me the highest currentY value so it properly adds
        public static void Postfix(ref float curY, float width, Ideo ideo, IdeoEditMode editMode)
        {
            // In the game's code this is a lambda but for my purpose I need to switch it to a Func<>
            // It uses my custom preceptClass called Precept_Drug which contains a list of all drugs
            // This list is what shows up when you click on the "add drugs" button
            Func<PreceptDef, bool> myFunc = (PreceptDef p) => p.preceptClass == typeof(Precept_Drug);

            object[] newargs = new object[9];

            // Connects my xml language to the words in game
            string categoryLabel = "ETH_VeneratedDrugs".Translate();
            string addPreceptLabel = "ETH_Drugs".Translate();

            // the 9 args needed to invoke DoPreceptsInt
            newargs[0] = categoryLabel;
            newargs[1] = addPreceptLabel;
            newargs[2] = false; // arg is mainPrecepts, setting to false makes it a window like venerated animals
            newargs[3] = ideo; // the current ideo, taken from the DoPrecepts method
            newargs[4] = editMode; // takes the edit mode from DoPrecepts method (I believe it's either devEdit mode or not)
            newargs[5] = curY; // The current Y value, taken as a reference and needed to make the window work
            // This is what caused me the most issues and in fact when going into DoPreceptsInt, it looks like
            // the value resets which is why I need to bring apparel down, look into it
            
            newargs[6] = width; // width taken from the DoPrecepts, most likely width of the Ideo window
            newargs[7] = myFunc; // The function we defined earlier
            newargs[8] = false; // sortFloatMenuOptionsByLabel, true sorts it alphabetically false is either random
            // Or sorted by when it is added into the Precept_Drug preceptClass, which would be by def location
            
            /*Log.Message("_______________________");
            for (int i = 0; i < newargs.Length; i++)
            {
                Log.Message(newargs[i].ToString());
            }
            Log.Message("_______________________");*/

            // Gets the method DoPreceptsInt, and since it is private and static we need the 
            // BindingFlags.NonPublic | BindingFlags.Static
            // After checking for a null value it invokes the method with my object[] newargs array
            // The first arg in Invoke is null because the method is static, if it was not static you would need
            // An object that invoked it
            typeof(IdeoUIUtility).GetMethod
                ("DoPreceptsInt", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null,newargs);


        }
    }
    [HarmonyPatch(typeof(IdeoUIUtility), "DoIdeoDetails")]
    
    // Adds 1300 px to the vertical height of the Ideo Window to allow for my extra line
    // Probably can do it dynamically but it works 
    public static class IdeoWindowSize
    {
        public static void Prefix(ref float viewHeight)
        {
            viewHeight += 1300f;
        }
    }

    [HarmonyPatch(typeof(IdeoUIUtility), "DoAppearanceItems")]
    
    // Because the CurrentY ref does not seem to stay as it was after my invoked method
    // This makes the Appearance Location in the Ideo Window be 230px lower (Arbitrary Number)
    // If I don't do this it confuses the game and the whole window messes up
    // If I can get the curY to be what it is after my invoked method this would not need to be done
    public static class IdeoAppearanceLocation
    {
        public static void Prefix(ref float curY)
        {
            curY += 230f;
        }
    }


}