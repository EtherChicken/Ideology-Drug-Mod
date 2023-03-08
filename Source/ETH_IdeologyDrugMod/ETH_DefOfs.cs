using RimWorld;

namespace IdeologyDrugMod;

[DefOf]
//My Venerated Drug class that allows for the precepts in the IdeoUIUtility window and the issue for in game
public static class ETH_VeneratedDrug
{
    
    static ETH_VeneratedDrug()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(ETH_VeneratedDrug));
    }
    [MayRequireIdeology]
    public static PreceptDef DrugVenerated;
}

[DefOf]
public static class ETH_IssueDefOf
{
    [MayRequireIdeology]
    public static IssueDef DrugsVenerated;
}