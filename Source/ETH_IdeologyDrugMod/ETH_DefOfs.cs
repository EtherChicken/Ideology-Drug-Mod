using RimWorld;

namespace IdeologyDrugMod;

[DefOf]
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