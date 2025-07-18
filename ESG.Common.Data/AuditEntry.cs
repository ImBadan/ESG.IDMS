using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace ESG.Common.Data;

/// <summary>
/// Helper class for creating audit log entries from a tracked entity.
/// </summary>
/// <remarks>
/// Creates an instance of <see cref="AuditEntry"/> with the provided tracked entity.
/// </remarks>
/// <param name="entry"></param>
public class AuditEntry(EntityEntry entry)
{

    /// <summary>
    /// The tracked entity
    /// </summary>
    public EntityEntry Entry { get; } = entry;

    /// <summary>
    /// Id of the user doing the transaction
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Unique identifier for this request
    /// </summary>
    public string? TraceId { get; set; }

    /// <summary>
    /// The table affected
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// The primary key values
    /// </summary>
    public Dictionary<string, object?> KeyValues { get; } = [];

    /// <summary>
    /// The values of the fields before the transaction
    /// </summary>
    public Dictionary<string, object?> OldValues { get; } = [];

    /// <summary>
    /// The values of the fields after the transaction
    /// </summary>
    public Dictionary<string, object?> NewValues { get; } = [];

    /// <summary>
    /// Collection of temporary properties
    /// </summary>
    public List<PropertyEntry> TemporaryProperties { get; } = [];

    /// <summary>
    /// The transaction type, e.g. Add, Edit, Delete, etc.
    /// </summary>
    public AuditType? AuditType { get; set; }

    /// <summary>
    /// The columns affected
    /// </summary>
    public List<string> ChangedColumns { get; } = [];

    /// <summary>
    /// Inidicator if the tracked entity has temporary properties.
    /// Cannot create audit entries from entities that have
    /// temporary properties.
    /// </summary>
    public bool HasTemporaryProperties => TemporaryProperties.Count != 0;

    /// <summary>
    /// Creates and <see cref="Audit"/> instance
    /// </summary>
    /// <returns></returns>
    public Audit ToAudit()
    {
        var audit = new Audit
        {
            UserId = UserId,
            TraceId = TraceId,
            Type = AuditType?.ToString(),
            TableName = TableName,
            DateTime = DateTime.UtcNow,
            PrimaryKey = KeyValues.Count == 1 ? KeyValues.First().Value!.ToString() : JsonSerializer.Serialize(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
            AffectedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns)
        };
        return audit;
    }
}
