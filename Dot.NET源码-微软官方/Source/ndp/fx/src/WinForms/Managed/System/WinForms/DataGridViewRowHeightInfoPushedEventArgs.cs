//------------------------------------------------------------------------------
// <copyright file="DataGridViewRowHeightInfoPushedEventArgs.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms
{
    using System;
    using System.Diagnostics;
    using System.ComponentModel;

    /// <include file='doc\DataGridViewRowHeightInfoPushedEventArgs.uex' path='docs/doc[@for="DataGridViewRowHeightInfoPushedEventArgs"]/*' />
    public class DataGridViewRowHeightInfoPushedEventArgs : HandledEventArgs
    {
        private int rowIndex;
        private int height;
        private int minimumHeight;

        internal DataGridViewRowHeightInfoPushedEventArgs(int rowIndex, int height, int minimumHeight) : base(false)
        {
            Debug.Assert(rowIndex >= -1);
            this.rowIndex = rowIndex;
            this.height = height;
            this.minimumHeight = minimumHeight;
        }

        /// <include file='doc\DataGridViewRowHeightInfoPushedEventArgs.uex' path='docs/doc[@for="DataGridViewRowHeightInfoPushedEventArgs.Height"]/*' />
        public int Height
        {
            get
            {
                return this.height;
            }
        }

        /// <include file='doc\DataGridViewRowHeightInfoPushedEventArgs.uex' path='docs/doc[@for="DataGridViewRowHeightInfoPushedEventArgs.MinimumHeight"]/*' />
        public int MinimumHeight
        {
            get
            {
                return this.minimumHeight;
            }
        }

        /// <include file='doc\DataGridViewRowHeightInfoPushedEventArgs.uex' path='docs/doc[@for="DataGridViewRowHeightInfoPushedEventArgs.RowIndex"]/*' />
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}
