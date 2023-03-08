using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace IdeologyDrugMod;

// PreceptWorker is how the list of drugs gets created
public class PreceptWorker_Drug : PreceptWorker
{
    // This method enumerates all the ThingDefs put in the list by the getter
    public override IEnumerable<PreceptThingChance> ThingDefs
    {
        get
        {
            // foreach def with the category of drug gets added to the IEnumerable with yield return
            foreach (ThingDef item in DefDatabase<ThingDef>.AllDefs.Where((ThingDef x) => x.thingCategories != null &&
                         x.thingCategories.Contains(ThingCategoryDefOf.Drugs)))
            {
                PreceptThingChance preceptThingChance = default(PreceptThingChance);
                preceptThingChance.def = item;
                preceptThingChance.chance = 1f;
                yield return preceptThingChance;
            }
        }
    }
    
    public override AcceptanceReport CanUse(ThingDef def, Ideo ideo, FactionDef generatingFor)
    {

            /* DNSpy code
             using (List<Precept>.Enumerator enumerator = ideo.PreceptsListForReading.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Precept_Drug precept_Drug;
                    if ((precept_Drug = (enumerator.Current as Precept_Drug)) != null && precept_Drug.ThingDef == def)
                    {
                        return false;
                    }
                }
            }
    
            return true;*/
        //ILSpy code
        foreach (Precept item in ideo.PreceptsListForReading)
        {
            if (item is Precept_Drug precept_Drug && precept_Drug.ThingDef == def)
            {
                return false;
            }
        }
        return true;
    }
}