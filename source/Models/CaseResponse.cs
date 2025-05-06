namespace Clientspace.Models;

/// <summary>
/// Represents the response from the ClientSpace API for a case.
/// </summary>
/// <remarks>
/// The fields included in this are dependent on ClientSpace's API having been setup for the full details (as indicated below).  For setups that do not have the full details, the fields will be null (or may result in assumed errors).
/// </remarks>
public class CaseGetResponse
{
    public int ID { get; set; }
    public Guid? RowGUID { get; set; }
    public string? TableName { get; set; }
    public int ProjectTypeID { get; set; }
    public int WorkspaceID { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public bool IsActive { get; set; }
    public CaseResponse Fields { get; set; } = new ();
}

public class CaseResponse
{
    public bool Archive { get; set; }
    public int? fkUserIDAssignedTo { get; set; }
    public DateTime? AssignedToChangeDate { get; set; }
    public string? CaseAssignment { get; set; }
    public string? CaseAudit { get; set; }
    public string? CaseDetails { get; set; }
    public string? CaseNotes { get; set; }
    public int CaseNumber { get; set; }
    public string? crCategory { get; set; }
    public int? fkContactIDClientContact { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? CreateTime { get; set; }
    public int? fkUserIDCreatedBy { get; set; }
    public string? CaseInfo { get; set; }
    public bool hasNotificationsDisabled { get; set; }
    public DateTime? DueDate { get; set; }
    public int? fkEmployeeID { get; set; }
    public DateTime? EscalationDate { get; set; }
    public string? fsCaseEscalation { get; set; }
    public DateTime? EscalationTime { get; set; }
    public string? ExternalReportedByID { get; set; }
    public DateTime? FirstResponseDate { get; set; }
    public string? HiddenFields { get; set; }
    public decimal HoursToComplete { get; set; }
    public bool IncludeinNotification { get; set; }
    public string? InternalNotes { get; set; }
    public DateTime? Escalation2Date { get; set; }
    public DateTime? Escalation2Time { get; set; }
    public string? fsNotUsed { get; set; }
    public int? BrokerContact { get; set; }
    public int? fkOwnerUserID { get; set; }
    public string? luPriority { get; set; }
    public string? CallerName { get; set; }
    public int? fkReportedByEmployeeID { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Resolution { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public DateTime? ResolutionTime { get; set; }
    public string? CommunicationMethod { get; set; }
    public string? luStatus { get; set; }
    public DateTime? StatusChangeDate { get; set; }
    public string? Subject { get; set; }
    public int? TimeSpent { get; set; }
    public int? fkCaseTypeID { get; set; }
    public string? UpdatedFieldsExternal { get; set; }
    public string? UpdatedFieldsInternal { get; set; }
    public string? fsWorkflowHistory { get; set; }
}

public class CaseAddRequest
{
    public CaseResponse Values { get; set; } = new();
}

public class CaseAddResponse
{
    public Guid? RowGUID { get; set; }
    public string? TableName { get; set; }
    public int ProjectTypeID { get; set; }
    public int WorkspaceID { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public bool IsActive { get; set; }
    public int ID { get; set; }
    public CaseAddFieldResponse Fields { get; set; } = new();
    public List<CaseAddResponseError> Errors { get; set; } = [];
}

public class CaseAddFieldResponse
{
    public bool Archive { get; set; }
    public DateTime? AssignedToChangeDate { get; set; }
    public int? BrokerContact { get; set; }
    public string? CallerName { get; set; }
    public string? CaseAudit { get; set; }
    public string? CaseInfo { get; set; }
    public string? CaseNotes { get; set; }
    public int CaseNumber { get; set; }
    public string? CommunicationMethod { get; set; }
    public string? crCategory { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? CreateTime { get; set; }
    public DateTime? DueDate { get; set; }
    public string? EmailAddress { get; set; }
    public DateTime? Escalation2Date { get; set; }
    public DateTime? Escalation2Time { get; set; }
    public DateTime? EscalationDate { get; set; }
    public DateTime? EscalationTime { get; set; }
    public string? ExternalReportedByID { get; set; }
    public DateTime? FirstResponseDate { get; set; }
    public int? fkCaseTypeID { get; set; }
    public int? fkContactIDClientContact { get; set; }
    public int? fkEmployeeID { get; set; }
    public int? fkEmployeeID1 { get; set; }
    public int? fkReportedByEmployeeID { get; set; }
    public int? fkReportedByEmployeeID1 { get; set; }
    public int? fkUserIDAssignedTo { get; set; }
    public int? fkUserIDCreatedBy { get; set; }
    public bool hasNotificationsDisabled { get; set; }
    public decimal HoursToComplete { get; set; }
    public bool IncludeinNotification { get; set; }
    public string? InternalNotes { get; set; }
    public string? luPriority { get; set; }
    public string? luStatus { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Resolution { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public DateTime? ResolutionTime { get; set; }
    public DateTime? StatusChangeDate { get; set; }
    public string? Subject { get; set; }
    public int? TimeSpent { get; set; }
    public string? UpdatedFieldsExternal { get; set; }
    public string? UpdatedFieldsInternal { get; set; }
    public string? BrokerContact_D { get; set; }
    public string? CommunicationMethod_D { get; set; }
    public string? crCategory_D { get; set; }
    public string? fkCaseTypeID_D { get; set; }
    public string? fkUserIDAssignedTo_D { get; set; }
    public string? fkUserIDCreatedBy_D { get; set; }
    public string? luPriority_D { get; set; }
    public string? luStatus_D { get; set; }

    // Example of the response from ClientSpace API
    //         "Archive": false,
    //         "AssignedToChangeDate": "2025-04-28T13:56:07Z",
    //         "BrokerContact": 500,
    //         "CallerName": "JB",
    //         "CaseAudit": "",
    //         "CaseInfo": "Testing 123",
    //         "CaseNotes": "",
    //         "CaseNumber": 2362,
    //         "CommunicationMethod": "ClientSpaceAPI",
    //         "crCategory": "IT",
    //         "CreateDate": "2025-04-28T13:56:07Z",
    //         "CreateTime": "2025-04-28T13:56:07Z",
    //         "DueDate": "2025-04-18T04:00:00Z",
    //         "EmailAddress": "",
    //         "Escalation2Date": null,
    //         "Escalation2Time": null,
    //         "EscalationDate": null,
    //         "EscalationTime": null,
    //         "ExternalReportedByID": "",
    //         "FirstResponseDate": null,
    //         "fkCaseTypeID": 317,
    //         "fkContactIDClientContact": null,
    //         "fkEmployeeID": null,
    //         "fkEmployeeID1": null,
    //         "fkReportedByEmployeeID": null,
    //         "fkReportedByEmployeeID1": null,
    //         "fkUserIDAssignedTo": 500,
    //         "fkUserIDCreatedBy": 500,
    //         "hasNotificationsDisabled": false,
    //         "HoursToComplete": 0.00,
    //         "IncludeinNotification": true,
    //         "InternalNotes": "",
    //         "luPriority": "Medium",
    //         "luStatus": "New",
    //         "PhoneNumber": "",
    //         "Resolution": "",
    //         "ResolutionDate": null,
    //         "ResolutionTime": null,
    //         "StatusChangeDate": "2025-04-28T13:56:07Z",
    //         "Subject": "Development Testing From Postman",
    //         "TimeSpent": null,
    //         "UpdatedFieldsExternal": "Status",
    //         "UpdatedFieldsInternal": "Status",
    //         "BrokerContact_D": "Bush, John [IT]",
    //         "CommunicationMethod_D": "ClientSpaceAPI",
    //         "crCategory_D": "Information Technology",
    //         "fkCaseTypeID_D": "Development",
    //         "fkUserIDAssignedTo_D": "Bush, John [IT]",
    //         "fkUserIDCreatedBy_D": "Bush, John [IT]",
    //         "luPriority_D": "Medium",
    //         "luStatus_D": "New"
}

public class CaseAddResponseError
{
    public string? Message { get; set; }
    public int Code { get; set; }
    public bool CanSaveAnyway { get; set; }

    // Example of the response from ClientSpace API
    //   "Message": "string",
    //   "Code": 0,
    //   "CanSaveAnyway": true
}