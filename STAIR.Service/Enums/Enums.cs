using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAIR.Service.Enums
{
    public enum EntryStatusEnum : int
    {
        COMPONENT_CREATE = 11,
        COMPONENT_UPDATE = 12,
        COMPONENT_DELETE = 13,
        COMPONENT_UPDATECREATE = 14,
        COMPONENT_UPDATEDELETE = 15,
        OPEN_CREATE = 21,
        OPEN_UPDATE = 22,
        OPEN_DELETE = 23,
        OPEN_UPDATECREATE = 24,
        OPEN_UPDATEDELETE = 25,
        ISSUE_CREATE = 31,
        ISSUE_UPDATE = 32,
        ISSUE_DELETE = 33,
        ISSUE_UPDATECREATE = 34,
        ISSUE_UPDATEDELETE = 35,
        SCRAP_CREATE = 41,
        SCRAP_UPDATE = 42,
        SCRAP_DELETE = 43,
        SCRAP_UPDATECREATE = 44,
        SCRAP_UPDATEDELETE = 45,
        UNUSABLERETURN_CREATE = 51,
        UNUSABLERETURN_UPDATE = 52,
        UNUSABLERETURN_DELETE = 53,
        UNUSABLERETURN_UPDATECREATE = 54,
        UNUSABLERETURN_UPDATEDELETE = 55,
        LOAN_CREATE = 61,
        LOAN_UPDATE = 62,
        LOAN_DELETE = 63,
        LOAN_UPDATECREATE = 64,
        LOAN_UPDATEDELETE = 65,
        LOANRETURN_CREATE = 71,
        LOANRETURN_UPDATE = 72,
        LOANRETURN_DELETE = 73,
        LOANRETURN_UPDATECREATE = 74,
        LOANRETURN_UPDATEDELETE = 75,
        GOODRECIEVE_CREATE = 81,
        GOODRECIEVE_UPDATE = 82,
        GOODRECIEVE_DELETE = 83,
        GOODRECIEVE_UPDATECREATE = 84,
        GOODRECIEVE_UPDATEDELETE = 85,
    }

    public enum UseInEnum : int
    {
        SCRAP =-1,
        AIRCRAFT = 0,
        INVENTORY = 999    
    }

    public enum IssueStatusEnum : int
    {
        OPEN,
        PENDING,
        APPROVED,
        ISSUED,
        RECEIVED,
        REJECTED
    }

    public enum DemandStatusEnum : int
    {
        OPEN,
        DEMANDED,
        APPROVED
    }

    public enum OpeningStatusEnum : int
    {
        OPEN, 
        PREPARED,
        APPROVED
    }

    public enum ScrapStatusEnum : int
    {
        OPEN, 
        PREPARED,
        APPROVED
    }

    //public enum UnusableReturnStatusEnum : int
    //{
    //    APPROVED = 1,
    //    ACCEPT
    //}

    public enum PriorityEnum : int
    {
        CRITICAL = 1,
        NORMAL,
        AOG,
        IOR
    }
    public enum ComparativeStatementEnum : int
    {
        OPEN = 0,
        PENDING ,
        REVIEWED,
        APPROVED,
        SELECTED,
        WITHHELD
    }
    public enum RackLifeTypeEnum : int
    {
        EXPIRY_DATE = 1,
        ON_CONDITION,
        UNLIMITED
    }

    public enum UnserviceableItemEnum : int
    {
        Unserviceable = 1,
        Exchange,
        Servicing,
        WarrentyClaim
    }

    public enum WorkFlowStatusEnum : int
    {
        OPEN = 0,
        PENDING,
        REVIEWED,
        APPROVED,
        APPROVEREJECTED,
        ISSUED,
        ACCEPTED,
        RECEIVED,
        RECEIVEREJECTED,
        DEMANDED,
        GRNOSubmit
    }

    public enum WorkFlowActionEnum : int
    {
        APPROVAL = 1,
        REVIEW,
        GRNOSubmit,
        ACCEPTANCE
    }

    public enum RequisitionTypeEnum : int
    {
        General = 1,
        Exchange,
    }

    public enum QuoteItemTypeEnum : int
    {
        Exchange = 2,
        New,
        Overhaul
    }

    public enum POTypeEnum : int
    {
        General = 1,
        Servicing,
        WarrentyClaim
    }

}
