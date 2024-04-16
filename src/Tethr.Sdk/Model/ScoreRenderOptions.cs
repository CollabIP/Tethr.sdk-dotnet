namespace Tethr.Sdk.Model;

public class ScoreRenderOptions 
{
    public double? Min { get; set; }
		
    public double? Max { get; set; }
		
    public double? Scale { get; set; }
		
    public int? Precision { get; set; }
		
    public RoundingType? Rounding { get; set; }

    /// <summary>
    /// When true, the score value should never be shown in the UI, only the label of
    /// the range it falls into.
    /// </summary>
    public bool LabelsOnly { get; set; }

    public List<ScoreRange> Ranges { get; set; } = new();
		
    public GradientConfig? Gradient { get; set; }

    public class GradientConfig
    {
        public List<GradientValue> Ranges { get; set; } = new();

        public GradientValue? Lower { get; set; }

        public GradientValue? Upper { get; set; }
    }

    public class GradientValue
    {
        public double? Value { get; set; }

        public string? Color { get; set; }
    }
}
