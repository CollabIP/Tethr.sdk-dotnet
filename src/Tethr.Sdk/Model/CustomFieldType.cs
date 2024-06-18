using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

#if !NET8_0_OR_GREATER
[JsonConverter(typeof(JsonStringEnumConverter))]
#endif
public enum CustomFieldType
{
    /// <summary>
    /// Fields that have a fixed/limited number of values (e.g. account numbers, disposition codes). Supports exact matches in filters/graphing as well as aggregations.
    /// </summary>
    String,

    /// <summary>
    /// Numbers without decimals that have a fixed / limited number of values. Supports ranges and exact matches in filters/graphing.
    /// </summary>
    Long,

    /// <summary>
    /// Represents a floating-point number for values requiring precision,
    /// such as measurements or fractional quantities.
    /// It distinguishes from whole numbers by allowing decimal points.
    /// Supports ranges in filters/graphing
    /// </summary>
    Double,

    /// <summary>
    /// Represents a date and time field.
    /// </summary>
    /// <remarks>
    /// Used for timestamping events or entities.
    /// This type facilitates time-based queries and sorting.
    /// <para>(in beta - not fully supported)</para>
    /// </remarks>
    DateTime,

    /// <summary>
    /// Represents hierarchical data structured through a specified delimiter.
    /// </summary>
    /// <remarks>
    /// Fields that contain a single char delimiter between parts, that should be represented as a hierarchical/tree structure.
    /// </remarks>
    Hierarchy,

    /// <summary>
    /// Represents free-form text fields.
    /// </summary>
    /// <remarks>
    /// These fields are not used for aggregations but allow for keyword searches.
    /// Tethr can analyze these fields to extract entities and insights,
    /// useful in scenarios like survey verbatim or agent notes.
    /// </remarks>
    Text
}
