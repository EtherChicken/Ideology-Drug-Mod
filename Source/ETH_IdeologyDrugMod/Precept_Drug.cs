using System.Collections.Generic;
using RimWorld;
using Verse;

namespace IdeologyDrugMod;

// Literally just copied this from Precept_Animal and changed the name to mine
public class Precept_Drug : Precept_ThingDef
{
    
    public override string UIInfoFirstLine
    {
        get
        {
            if (base.ThingDef == null)
            {
                return base.UIInfoFirstLine;
            }
            return base.ThingDef.LabelCap;
        }
    }

    public override string TipLabel => def.issue.LabelCap + ": " + base.ThingDef.LabelCap;
    
    
    
    
    /* Whatever this is, trying to get a list of drugs but im p sure the worker does that
     public static List<ThingDef> Druglist;

    public static void GetDruglist(List<ThingDef> defList)
    {
        var defenums = defList.GetEnumerator();
        var index = 0;
        while (defenums.MoveNext())
        {
            index++;
            if (defList[index].thingCategories.Contains(ThingCategoryDefOf.Drugs))
            {
                Druglist.Add(defList[index]);
            }
        }
    }
}

[StaticConstructorOnStartup]
public static class GetPreceptDrugs
{
    static List<ThingDef> defList = DefDatabase<ThingDef>.AllDefsListForReading;

    static GetPreceptDrugs()
    {
        Precept_Drug.GetDruglist(defList);
    }*/
    
    
}