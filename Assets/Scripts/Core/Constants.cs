using Core.Structs;

namespace Core
{
    public static class Constants
    {
        internal static readonly ColorData GroundColor = new ColorData(.25f, .2f, .2f, 1);
        internal static readonly ColorData WaterColor = new ColorData(.96f, .45f, .18f, 1);

        internal static readonly ColorData[] PlayerColors =
        {
            new(1, 1, 1, 1),
            new(0f, .2f, 1f, 1),
            new(1f, .1f, .2f, 1)
        };
        internal static readonly ColorData HighlightedColor = new ColorData(1, .1f, .1f, 1);
    }
}