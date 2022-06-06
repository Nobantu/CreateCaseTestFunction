using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunction2.Services.Models;

public class AccientTow
{
    public virtual CompletedAtLocation CompletedAtLocation { get; set; }
    public virtual DestinationLocation DestinationLocation { get; set; }
    public virtual DestinationContactPerson DestinationContactPerson { get; set; }
    public Guid CaseId { get; set; }
    public Guid CaseServiceDefinitionId { get; set; }
    public Guid CreatorId { get; set; }
    public virtual RequestLocation RequestLocation { get; set; }
    public virtual RequestContactPerson RequestContactPerson { get; set; }
    public bool IsServiceProviderOnScene { get; set; }
    public string ServiceProviderFirstName { get; set; }
    public string ServiceProvideSurname { get; set; }
    public string ServiceproviderContactInformation { get; set; }
    public Guid ServiceProviderId { get; set; }
    public virtual PaymentSpecificationModel PaymentSpecification { get; set; }
    public virtual ScheduleModel Schedule { get; set; }
    public Guid CallId { get; set; }
    public string ClientorderNumber { get; set; }
    public string Instruction { get; set; }
    public string ExternalCaseServiceId { get; set; }
    public string RequiresInterventionReason { get; set; }
}