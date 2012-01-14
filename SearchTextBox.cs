//-----------------------------------------------------------------------
// <copyright file="SearchTextBox.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012  Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        public delegate void DoSearchHandler(string keywords);

        [Description("A auto search occur.")]
        public event DoSearchHandler DoSearch;

        /// <summary>
        /// 
        /// </summary>
        public SearchTextBox()
        {
            InitializeComponent();
            this.tableLayoutPnlSearchbox.ColumnCount = 2;
            if (Settings.NotesTooltipsEnabled)
            {
                this.toolTips.Active = true;
            }

            this.btnKeywordClear.ForeColor = Color.Black;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            //this.timerStartAutoSearch.Stop();
            this.tbKeywords.Clear();
            this.tbKeywords.BackColor = SystemColors.Window;
            this.timerStartAutoSearch.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartSearch()
        {
            this.timerStartAutoSearch.Stop();
            this.tbKeywords.BackColor = Color.LightYellow;

            if (DoSearch != null)
            {
                DoSearch(this.tbKeywords.Text);
            }

            if (this.tbKeywords.Text == string.Empty)
            {
                this.tbKeywords.BackColor = SystemColors.Window;
                this.tableLayoutPnlSearchbox.ColumnCount = 2;
            }
            else
            {
                this.tableLayoutPnlSearchbox.ColumnCount = 3;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetAutoSearchDelay()
        {            
            this.timerStartAutoSearch.Stop();
            this.tbKeywords.BackColor = SystemColors.Window;
            this.timerStartAutoSearch.Enabled = true;
            this.timerStartAutoSearch.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerStartSearch_Tick(object sender, EventArgs e)
        {
            this.StartSearch();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbKeywords_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.StartSearch();
            }
            else
            {
                this.ResetAutoSearchDelay();
            }

            if (this.tbKeywords.TextLength > 0)
            {
                this.btnKeywordClear.Visible = true;
            }
            else
            {
                this.btnKeywordClear.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKeywordClear_Click(object sender, EventArgs e)
        {
            this.btnKeywordClear.Visible = false;
            this.Clear();            
        }
    }
}
