using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using System.Diagnostics.Metrics;
using System;
using System.Linq;

namespace DelightMyFire
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "YourPatcher.esp")
                .Run(args);
        }

        private static List<String> stringmatch = new()
        {
            "cookingspit",
            "fxfirewithember",
            "fxsmokesmoke",
            "elfxcandlesmoke",
            "fireplacewood01burning",
            "fireplacewood02burning",
            "fireplacewood03burning",
            "fireplacewood04burning",
            "fxsmokewisps",
            "craftingcooking",
            "fireplace",
            "torch",
            "candle"
        };
        private static List<String> exclusionmatch = new()
        {
            "_Camp_CookingPot"
        };

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            foreach (var placedObject in state.LoadOrder.PriorityOrder.PlacedObject().WinningContextOverrides(state.LinkCache))
            {
                //If already disabled, skip
                if (placedObject.Record.MajorRecordFlagsRaw == 0x0000_0800) continue;
                var placedObjectRec = placedObject.Record;
                if (placedObjectRec.EditorID == null)
                {
                    if (placedObjectRec.Base.TryResolve<IStaticGetter>(state.LinkCache, out var placedObjectBase))
                    {
                        if (placedObjectBase.EditorID != null)
                        {
                            var eid = placedObjectBase.EditorID.ToString();
                            bool b = stringmatch.Any(s => eid.Contains(s, StringComparison.CurrentCultureIgnoreCase));
                            if (b && !exclusionmatch.Any(s => eid.Contains(s, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                IPlacedObject modifiedObject = placedObject.GetOrAddAsOverride(state.PatchMod);
                                modifiedObject.MajorRecordFlagsRaw |= 0x0000_0800;
                            }

                        }
                    }
                    if (placedObjectRec.Base.TryResolve<IMoveableStaticGetter>(state.LinkCache, out var placedObjectBasemove))
                    {
                        if (placedObjectBasemove.EditorID != null)
                        {
                            var eid = placedObjectBasemove.EditorID.ToString();
                            bool b = stringmatch.Any(s => eid.Contains(s, StringComparison.CurrentCultureIgnoreCase));
                            if (b && !exclusionmatch.Any(s => eid.Contains(s, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                IPlacedObject modifiedObject = placedObject.GetOrAddAsOverride(state.PatchMod);
                                modifiedObject.MajorRecordFlagsRaw |= 0x0000_0800;
                            }

                        }
                    }
                    if (placedObjectRec.Base.TryResolve<IFurnitureGetter>(state.LinkCache, out var placedObjectBasefurn))
                    {
                        if (placedObjectBasefurn.EditorID != null)
                        {
                            var eid = placedObjectBasefurn.EditorID.ToString();
                            bool b = stringmatch.Any(s => eid.Contains(s, StringComparison.CurrentCultureIgnoreCase));
                            if (b && !exclusionmatch.Any(s => eid.Contains(s, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                IPlacedObject modifiedObject = placedObject.GetOrAddAsOverride(state.PatchMod);
                                modifiedObject.MajorRecordFlagsRaw |= 0x0000_0800;
                            }

                        }
                    }
                    if (placedObjectRec.Base.TryResolve<ILightGetter>(state.LinkCache, out var placedObjectBaselight))
                    {
                        if (placedObjectBaselight.EditorID != null)
                        {
                            var eid = placedObjectBaselight.EditorID.ToString();
                            bool b = stringmatch.Any(s => eid.Contains(s, StringComparison.CurrentCultureIgnoreCase));
                            if (b && !exclusionmatch.Any(s => eid.Contains(s, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                IPlacedObject modifiedObject = placedObject.GetOrAddAsOverride(state.PatchMod);
                                modifiedObject.MajorRecordFlagsRaw |= 0x0000_0800;
                            }

                        }
                    }
                }
            }
        }
    }
}
