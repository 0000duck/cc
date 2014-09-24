//------------------------------------------------------------------------------
// <copyright file="SqlException.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <owner current="true" primary="true">[....]</owner>
// <owner current="true" primary="false">[....]</owner>
//------------------------------------------------------------------------------

namespace System.Data.SqlClient {

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data.Common;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Text; // StringBuilder

    [Serializable]
    public sealed class SqlException : System.Data.Common.DbException {
        private SqlErrorCollection _errors;
        [System.Runtime.Serialization.OptionalFieldAttribute(VersionAdded = 4)]
        private Guid _clientConnectionId = Guid.Empty;

        private SqlException(string message, SqlErrorCollection errorCollection, Exception innerException, Guid conId) : base(message, innerException) {
            HResult = HResults.SqlException;
            _errors = errorCollection;
            _clientConnectionId = conId;
        }

        // runtime will call even if private...
        private SqlException(SerializationInfo si, StreamingContext sc) : base(si, sc) {
            _errors = (SqlErrorCollection) si.GetValue("Errors", typeof(SqlErrorCollection));
            HResult = HResults.SqlException;
            foreach (SerializationEntry siEntry in si) {
                if ("ClientConnectionId" == siEntry.Name) {
                    _clientConnectionId = (Guid)siEntry.Value;
                    break;
                }
            }
                
        }

        [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Flags=System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter)]
        override public void GetObjectData(SerializationInfo si, StreamingContext context) {
            if (null == si) {
                throw new ArgumentNullException("si");
            }
            si.AddValue("Errors", _errors, typeof(SqlErrorCollection));
            si.AddValue("ClientConnectionId", _clientConnectionId, typeof(Guid));
            base.GetObjectData(si, context);
        }

        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
        ]
        public SqlErrorCollection Errors {
            get {
                if (_errors == null) {
                    _errors = new SqlErrorCollection();
                }
                return _errors;
            }
        }

        public Guid ClientConnectionId {
            get {
                return this._clientConnectionId;
            }
        }

        /*virtual protected*/private bool ShouldSerializeErrors() { // MDAC 65548
            return ((null != _errors) && (0 < _errors.Count));
        }

        public byte Class {
            get { return this.Errors[0].Class;}
        }

        public int LineNumber {
            get { return this.Errors[0].LineNumber;}
        }

        public int Number {
            get { return this.Errors[0].Number;}
        }

        public string Procedure {
            get { return this.Errors[0].Procedure;}
        }

        public string Server {
            get { return this.Errors[0].Server;}
        }

        public byte State {
            get { return this.Errors[0].State;}
        }

        override public string Source {
            get { return this.Errors[0].Source;}
        }

        public override string ToString() {
                StringBuilder sb = new StringBuilder(base.ToString());
                sb.Append("\r\n");
                sb.Append("ClientConnectionId:");
                sb.Append(this._clientConnectionId.ToString());
                return sb.ToString();

        }

        static internal SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion) {
            return CreateException(errorCollection, serverVersion, Guid.Empty);
        }

        static internal SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion, Guid conId, Exception innerException = null) {
            Debug.Assert(null != errorCollection && errorCollection.Count > 0, "no errorCollection?");
            
            // concat all messages together MDAC 65533
            StringBuilder message = new StringBuilder();
            for (int i = 0; i < errorCollection.Count; i++) {
                if (i > 0) {
                    message.Append(Environment.NewLine);
                }
                message.Append(errorCollection[i].Message);
            }

            if (innerException == null && errorCollection[0].Win32ErrorCode != 0 && errorCollection[0].Win32ErrorCode != -1) {
                innerException = new Win32Exception(errorCollection[0].Win32ErrorCode);
            }

            SqlException exception = new SqlException(message.ToString(), errorCollection, innerException, conId);

            exception.Data.Add("HelpLink.ProdName",    "Microsoft SQL Server");

            if (!ADP.IsEmpty(serverVersion)) {
                exception.Data.Add("HelpLink.ProdVer", serverVersion);
            }
            exception.Data.Add("HelpLink.EvtSrc",      "MSSQLServer");
            exception.Data.Add("HelpLink.EvtID",       errorCollection[0].Number.ToString(CultureInfo.InvariantCulture));
            exception.Data.Add("HelpLink.BaseHelpUrl", "http://go.microsoft.com/fwlink");
            exception.Data.Add("HelpLink.LinkId",      "20476");

            return exception;
        }

        internal SqlException InternalClone() {
            SqlException exception = new SqlException(Message, _errors, InnerException, _clientConnectionId);
            if (this.Data != null)
                foreach (DictionaryEntry entry in this.Data)
                    exception.Data.Add(entry.Key, entry.Value);
            exception._doNotReconnect = this._doNotReconnect;
            return exception;
        }

        // Do not serialize this field! It is used to indicate that no reconnection attempts are required
        internal bool _doNotReconnect = false;        
    }
}
