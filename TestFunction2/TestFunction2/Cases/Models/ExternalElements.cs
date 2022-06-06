using System;
using System.ComponentModel.DataAnnotations;

namespace TestFunction2.Cases.Models;
public class ExternalElements
{
    public Guid OperationsNodeID { get; set; }
    [Required]
    public Guid ClientNodeID { get; set; }
    [Required]
    public Guid CreatorNodeID { get; set; }
    [Required]
    public virtual Customer Customer { get; set; }
    public virtual Vehicle Vehicle { get; set; }
    public virtual Account Account { get; set; }
    public string ExternalClaimReference { get; set; }
    public string ExternalCaseId { get; set; }
    public string ExternalSid { get; set; }
    public DateTime DateOfIncident { get; set; }
    public bool ReuseExistingcaseIfFound { get; set; }

    
}
