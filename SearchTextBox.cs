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
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// A SearchTextBox control
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the SearchTextBox class.
        /// </summary>
        public SearchTextBox()
        {
            this.InitializeComponent();
            this.lblTextSearch.Text = Strings.T("search:");
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
        /// <param name="keywords"></param>
        public delegate void SearchStartHandler(string keywords);

        /// <summary>
        /// 
        /// </summary>
        public delegate void SearchStopHandler();

        /// <summary>
        /// A search occur event.
        /// </summary>
        [Description("A search occur.")]
        public event SearchStartHandler SearchStart;

        /// <summary>
        /// Keywords entered are cleared not searching anymore event.
        /// </summary>
        [Description("Keywords entered are cleared not searching anymore.")]
        public event SearchStopHandler SearchStop;

        /// <summary>
        /// Gets a valeu indicating wherhera keyword is entered.
        /// </summary>
        public bool IsKeywordEntered
        {
            get
            {
                if (this.tbKeywords.TextLength <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// The content of this searchbox is cleared and not highlighted.
        /// Display all items normal again.
        /// </summary>
        public void Clear()
        {
            this.timerStartAutoSearch.Stop();
            this.tbKeywords.Clear();
            this.tbKeywords.BackColor = SystemColors.Window;
            this.tableLayoutPnlSearchbox.ColumnCount = 2;
            if (this.SearchStop != null)
            {
                this.SearchStop();
            }
        }

        /// <summary>
        /// keyword is entered and wait time for search to start has expire 
        /// or enter is entered do a search now.
        /// </summary>
        private void DoSearch()
        {
            this.timerStartAutoSearch.Stop();

            if (this.tbKeywords.TextLength > 0)
            {
                this.tbKeywords.BackColor = Color.LightYellow;
                this.tableLayoutPnlSearchbox.ColumnCount = 3;

                if (this.SearchStart != null)
                {
                    this.SearchStart(this.tbKeywords.Text);
                }
            }
            else
            {
                this.tbKeywords.BackColor = SystemColors.Window;
                this.tableLayoutPnlSearchbox.ColumnCount = 2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetAutoSearchDelay()
        {            
            this.timerStartAutoSearch.Stop();
            this.tbKeywords.BackColor = SystemColors.Window;
            this.timerStartAutoSearch.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void timerStartSearch_Tick(object sender, EventArgs e)
        {
            this.DoSearch();
        }

        /// <summary>
        /// Key is released in this SearchTextBox
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Key event arguments</param>
        private void tbKeywords_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DoSearch();
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
                this.SearchStop();
            }
        }

        /// <summary>
        /// The button for stop searching is clicked, clear search keyword in textbox.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnKeywordClear_Click(object sender, EventArgs e)
        {
            this.btnKeywordClear.Visible = false;
            this.Clear();
        }
    }
}
