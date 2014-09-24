//------------------------------------------------------------------------------
// <copyright file="TriggerAction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <owner current="true" primary="true">[....]</owner>
// <owner current="true" primary="true">[....]</owner>
// <owner current="true" primary="true">daltudov</owner>
// <owner current="true" primary="true">[....]</owner>
// <owner current="true" primary="false">beysims</owner>
// <owner current="true" primary="false">[....]</owner>
// <owner current="true" primary="false">vadimt</owner>
//------------------------------------------------------------------------------

namespace Microsoft.SqlServer.Server {

    internal enum EMDEventType {
        x_eet_Invalid           = 0,    // Not actually a valid type: not persisted.
        x_eet_Insert            = 1,    // Insert (eg. on table/view)
        x_eet_Update            = 2,    // Update (eg. on table/view)
        x_eet_Delete            = 3,    // Delete (eg. on table/view)
        x_eet_Create_Table      = 21,
        x_eet_Alter_Table       = 22,
        x_eet_Drop_Table        = 23,
        x_eet_Create_Index      = 24,
        x_eet_Alter_Index       = 25,
        x_eet_Drop_Index        = 26,
        x_eet_Create_Stats  = 27,
        x_eet_Update_Stats  = 28,
        x_eet_Drop_Stats        = 29,
        x_eet_Create_Secexpr    = 31,
        x_eet_Drop_Secexpr      = 33,
        x_eet_Create_Synonym    = 34,
        x_eet_Drop_Synonym      = 36,
        x_eet_Create_View       = 41,
        x_eet_Alter_View        = 42,
        x_eet_Drop_View         = 43,
        x_eet_Create_Procedure  = 51,
        x_eet_Alter_Procedure   = 52,
        x_eet_Drop_Procedure    = 53,
        x_eet_Create_Function   = 61,
        x_eet_Alter_Function    = 62,
        x_eet_Drop_Function     = 63,
        x_eet_Create_Trigger    = 71,   // On database/server/table
        x_eet_Alter_Trigger     = 72,
        x_eet_Drop_Trigger      = 73,
        x_eet_Create_Event_Notification = 74,
        x_eet_Drop_Event_Notification   = 76,
        x_eet_Create_Type       = 91,
    //  x_eet_Alter_Type        = 92,
        x_eet_Drop_Type         = 93,
        x_eet_Create_Assembly   = 101,
        x_eet_Alter_Assembly    = 102,
        x_eet_Drop_Assembly     = 103,
        x_eet_Create_User       = 131,
        x_eet_Alter_User        = 132,
        x_eet_Drop_User         = 133,
        x_eet_Create_Role       = 134,
        x_eet_Alter_Role        = 135,
        x_eet_Drop_Role         = 136,
        x_eet_Create_AppRole    = 137,
        x_eet_Alter_AppRole     = 138,
        x_eet_Drop_AppRole      = 139,
        x_eet_Create_Schema     = 141,
        x_eet_Alter_Schema      = 142,
        x_eet_Drop_Schema       = 143,
        x_eet_Create_Login      = 144,
        x_eet_Alter_Login       = 145,
        x_eet_Drop_Login        = 146,
        x_eet_Create_MsgType    = 151,
        x_eet_Alter_MsgType     = 152,
        x_eet_Drop_MsgType      = 153,
        x_eet_Create_Contract   = 154,
        x_eet_Alter_Contract    = 155,
        x_eet_Drop_Contract     = 156,
        x_eet_Create_Queue      = 157,
        x_eet_Alter_Queue       = 158,
        x_eet_Drop_Queue        = 159,
        x_eet_Create_Service    = 161,
        x_eet_Alter_Service     = 162,
        x_eet_Drop_Service      = 163,
        x_eet_Create_Route      = 164,
        x_eet_Alter_Route       = 165,
        x_eet_Drop_Route        = 166,
        x_eet_Grant_Statement   = 167,
        x_eet_Deny_Statement    = 168,
        x_eet_Revoke_Statement  = 169,
        x_eet_Grant_Object      = 170,
        x_eet_Deny_Object       = 171,
        x_eet_Revoke_Object     = 172,
        x_eet_Activation        = 173,
        x_eet_Create_Binding    = 174,
        x_eet_Alter_Binding     = 175,
        x_eet_Drop_Binding      = 176,
        x_eet_Create_XmlSchema  = 177,
        x_eet_Alter_XmlSchema       = 178,
        x_eet_Drop_XmlSchema        = 179,
        x_eet_Create_HttpEndpoint   = 181,
        x_eet_Alter_HttpEndpoint    = 182,
        x_eet_Drop_HttpEndpoint = 183,
        x_eet_Create_Partition_Function = 191,
        x_eet_Alter_Partition_Function  = 192,
        x_eet_Drop_Partition_Function   = 193,
        x_eet_Create_Partition_Scheme   = 194,
        x_eet_Alter_Partition_Scheme    = 195,
        x_eet_Drop_Partition_Scheme = 196,
    
        x_eet_Create_Database   = 201,
        x_eet_Alter_Database    = 202,
        x_eet_Drop_Database     = 203,
    
        // 1000 - 1999 is reserved for SQLTrace.
        x_eet_Trace_Start       = 1000,
        x_eet_Trace_End     = 1999,
        // WHEN ADDING, PLEASE 
    };
    
    public enum TriggerAction {
        Invalid = EMDEventType.x_eet_Invalid,
        Insert = EMDEventType.x_eet_Insert,
        Update = EMDEventType.x_eet_Update,
        Delete = EMDEventType.x_eet_Delete,
        CreateTable = EMDEventType.x_eet_Create_Table,
        AlterTable = EMDEventType.x_eet_Alter_Table,
        DropTable = EMDEventType.x_eet_Drop_Table,
        CreateIndex = EMDEventType.x_eet_Create_Index,
        AlterIndex = EMDEventType.x_eet_Alter_Index,
        DropIndex = EMDEventType.x_eet_Drop_Index,
        CreateSynonym = EMDEventType.x_eet_Create_Synonym,
        DropSynonym = EMDEventType.x_eet_Drop_Synonym,
        CreateSecurityExpression = EMDEventType.x_eet_Create_Secexpr,
        DropSecurityExpression = EMDEventType.x_eet_Drop_Secexpr,
        CreateView = EMDEventType.x_eet_Create_View,
        AlterView = EMDEventType.x_eet_Alter_View,
        DropView = EMDEventType.x_eet_Drop_View,
        CreateProcedure = EMDEventType.x_eet_Create_Procedure,
        AlterProcedure = EMDEventType.x_eet_Alter_Procedure,
        DropProcedure = EMDEventType.x_eet_Drop_Procedure,
        CreateFunction = EMDEventType.x_eet_Create_Function,
        AlterFunction = EMDEventType.x_eet_Alter_Function,
        DropFunction = EMDEventType.x_eet_Drop_Function,
        CreateTrigger = EMDEventType.x_eet_Create_Trigger,
        AlterTrigger = EMDEventType.x_eet_Alter_Trigger,
        DropTrigger = EMDEventType.x_eet_Drop_Trigger,
        CreateEventNotification = EMDEventType.x_eet_Create_Event_Notification,
        DropEventNotification = EMDEventType.x_eet_Drop_Event_Notification,
        CreateType = EMDEventType.x_eet_Create_Type,
        //	Alter_Type = EMDEventType.x_eet_Alter_Type,
        DropType = EMDEventType.x_eet_Drop_Type,
        CreateAssembly = EMDEventType.x_eet_Create_Assembly,
        AlterAssembly = EMDEventType.x_eet_Alter_Assembly,
        DropAssembly = EMDEventType.x_eet_Drop_Assembly,
        CreateUser = EMDEventType.x_eet_Create_User,
        AlterUser = EMDEventType.x_eet_Alter_User,
        DropUser = EMDEventType.x_eet_Drop_User,
        CreateRole = EMDEventType.x_eet_Create_Role,
        AlterRole = EMDEventType.x_eet_Alter_Role,
        DropRole = EMDEventType.x_eet_Drop_Role,
        CreateAppRole = EMDEventType.x_eet_Create_AppRole,
        AlterAppRole = EMDEventType.x_eet_Alter_AppRole,
        DropAppRole = EMDEventType.x_eet_Drop_AppRole,
        CreateSchema = EMDEventType.x_eet_Create_Schema,
        AlterSchema = EMDEventType.x_eet_Alter_Schema,
        DropSchema = EMDEventType.x_eet_Drop_Schema,
        CreateLogin = EMDEventType.x_eet_Create_Login,
        AlterLogin = EMDEventType.x_eet_Alter_Login,
        DropLogin = EMDEventType.x_eet_Drop_Login,
        CreateMsgType = EMDEventType.x_eet_Create_MsgType,
        DropMsgType = EMDEventType.x_eet_Drop_MsgType,
        CreateContract = EMDEventType.x_eet_Create_Contract,
        DropContract = EMDEventType.x_eet_Drop_Contract,
        CreateQueue = EMDEventType.x_eet_Create_Queue,
        AlterQueue = EMDEventType.x_eet_Alter_Queue,
        DropQueue = EMDEventType.x_eet_Drop_Queue,
        CreateService = EMDEventType.x_eet_Create_Service,
        AlterService = EMDEventType.x_eet_Alter_Service,
        DropService = EMDEventType.x_eet_Drop_Service,
        CreateRoute = EMDEventType.x_eet_Create_Route,
        AlterRoute = EMDEventType.x_eet_Alter_Route,
        DropRoute = EMDEventType.x_eet_Drop_Route,
        GrantStatement = EMDEventType.x_eet_Grant_Statement,
        DenyStatement = EMDEventType.x_eet_Deny_Statement,
        RevokeStatement = EMDEventType.x_eet_Revoke_Statement,
        GrantObject = EMDEventType.x_eet_Grant_Object,
        DenyObject = EMDEventType.x_eet_Deny_Object,
        RevokeObject = EMDEventType.x_eet_Revoke_Object,
        CreateBinding = EMDEventType.x_eet_Create_Binding,
        AlterBinding = EMDEventType.x_eet_Alter_Binding,
        DropBinding = EMDEventType.x_eet_Drop_Binding,
        CreatePartitionFunction = EMDEventType.x_eet_Create_Partition_Function,
        AlterPartitionFunction = EMDEventType.x_eet_Alter_Partition_Function,
        DropPartitionFunction = EMDEventType.x_eet_Drop_Partition_Function,
        CreatePartitionScheme = EMDEventType.x_eet_Create_Partition_Scheme,
        AlterPartitionScheme = EMDEventType.x_eet_Alter_Partition_Scheme,
        DropPartitionScheme = EMDEventType.x_eet_Drop_Partition_Scheme,
    }
}
