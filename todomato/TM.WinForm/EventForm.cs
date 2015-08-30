using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace TM.WinForm
{
    public partial class EventForm : Telerik.WinControls.UI.RadForm
    {
        public EventForm()
        {
            InitializeComponent();
        }


        private void EventForm_Shown_1(object sender, EventArgs e)
        {
            //邊框顏色
            this.FormElement.Border.ForeColor = System.Drawing.Color.Black;
            //內粗框的顏色
            //this.FormElement.ImageBorder.BackColor = Color.Green;

            //允許放大縮小功能
            //this.FormElement.TitleBar.MaximizeButton.Enabled = false;
            //this.FormElement.TitleBar.MinimizeButton.Enabled = false;

            //新增子視窗
            this.IsMdiContainer = true;

            RadForm form = new RadForm();
            form.Text = "MDI Child 1";
            form.MdiParent = this;
            form.ThemeName = "Desert";
            form.Show();

            //form = new RadForm();
            //form.Text = "MDI Child 2";
            //form.MdiParent = this;
            //form.ThemeName = "Office2007Black";
            //form.Show();

            //form = new RadForm();
            //form.Text = "MDI Child 3";
            //form.MdiParent = this;
            //form.ThemeName = "TelerikMetro";
            //form.Show();

            this.docTabsVisibleCheck.Checked = this.radDock1.DocumentTabsVisible; this.toolTabsVisibleCheck.Checked = this.radDock1.ToolTabsVisible; this.docCloseButtonCheck.Checked = this.radDock1.ShowDocumentCloseButton; this.toolCloseButtonCheck.Checked = this.radDock1.ShowToolCloseButton; this.FillTabStripAlignment(this.docTabAlignCombo, this.radDock1.DocumentTabsAlignment); this.FillTabStripAlignment(this.toolTabAlignCombo, this.radDock1.ToolTabsAlignment); this.FillTabStripTextOrientation(this.docTextOrientationCombo, this.radDock1.DocumentTabsTextOrientation); this.FillTabStripTextOrientation(this.toolTextOrientationCombo, this.radDock1.ToolTabsTextOrientation);

            this.docTabAlignCombo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(docTabAlignCombo_SelectedIndexChanged);
            this.toolTabAlignCombo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.toolTabAlignCombo_SelectedIndexChanged);
            this.docTabsVisibleCheck.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.docTabsVisibleCheck_ToggleStateChanged);
            this.toolTabsVisibleCheck.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.toolTabsVisibleCheck_ToggleStateChanged);
            this.docCloseButtonCheck.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.docCloseButtonCheck_ToggleStateChanged);
            this.docTextOrientationCombo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.docTextOrientationCombo_SelectedIndexChanged);
            this.toolCloseButtonCheck.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.toolCloseButtonCheck_ToggleStateChanged);
            this.toolTextOrientationCombo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.toolTextOrientationCombo_SelectedIndexChanged);
        } 

        private void EventForm_Load(object sender, EventArgs e)
        {
            //加入title bar 按鈕
            RadButtonElement buttonElement = new RadButtonElement();
            buttonElement.Text = "TitleBar Button";
            this.FormElement.TitleBar.Children[2].Children[0].Children.Insert(0, buttonElement);
        }

        #region Implementation

        //protected override void OnLoad(EventArgs e)
        //{
        //    ThemeResolutionService.ApplyThemeToControlTree(this.settingsPanel, MainForm.DefaultTheme);
        //}

        private void FillTabStripTextOrientation(RadDropDownList combo, TabStripTextOrientation selected)
        {
            combo.BeginUpdate();
            combo.Items.Clear();

            int counter = 0;
            int selectedIndex = -1;
            foreach (TabStripTextOrientation orientation in Enum.GetValues(typeof(TabStripTextOrientation)))
            {
                RadListDataItem item = new RadListDataItem(orientation.ToString());
                item.Value = orientation;
                combo.Items.Add(item);

                if (orientation == selected)
                {
                    selectedIndex = counter;
                }

                counter++;
            }

            combo.SelectedIndex = selectedIndex;
            combo.EndUpdate();
        }

        private void FillTabStripAlignment(RadDropDownList combo, TabStripAlignment selected)
        {
            combo.BeginUpdate();
            combo.Items.Clear();

            int counter = 0;
            int selectedIndex = -1;
            foreach (TabStripAlignment align in Enum.GetValues(typeof(TabStripAlignment)))
            {
                RadListDataItem item = new RadListDataItem(align.ToString());
                item.Value = align;
                combo.Items.Add(item);

                if (align == selected)
                {
                    selectedIndex = counter;
                }

                counter++;
            }

            combo.SelectedIndex = selectedIndex;
            combo.EndUpdate();
        }

        private TabStripAlignment? GetTabStripAlignment(RadDropDownList combo)
        {
            RadListDataItem item = combo.SelectedItem as RadListDataItem;
            if (item == null)
            {
                return null;
            }

            return (TabStripAlignment)item.Value;
        }

        private TabStripTextOrientation? GetTabStripTextOrientation(RadDropDownList combo)
        {
            RadListDataItem item = combo.SelectedItem as RadListDataItem;
            if (item == null)
            {
                return null;
            }

            return (TabStripTextOrientation)item.Value;
        }

        #endregion

        #region Event Handlers

        private void docTextOrientationCombo_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            TabStripTextOrientation? orientation = this.GetTabStripTextOrientation(this.docTextOrientationCombo);
            if (orientation != null)
            {
                this.radDock1.DocumentTabsTextOrientation = orientation.Value;
            }
        }

        private void toolTextOrientationCombo_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            TabStripTextOrientation? orientation = this.GetTabStripTextOrientation(this.toolTextOrientationCombo);
            if (orientation != null)
            {
                this.radDock1.ToolTabsTextOrientation = orientation.Value;
            }
        }

        private void docCloseButtonCheck_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            this.radDock1.ShowDocumentCloseButton = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
        }

        private void toolCloseButtonCheck_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            this.radDock1.ShowToolCloseButton = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
        }

        private void docTabAlignCombo_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            TabStripAlignment? align = this.GetTabStripAlignment(this.docTabAlignCombo);
            if (align != null)
            {
                this.radDock1.DocumentTabsAlignment = align.Value;
            }
        }

        private void toolTabAlignCombo_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            TabStripAlignment? align = this.GetTabStripAlignment(this.toolTabAlignCombo);
            if (align != null)
            {
                this.radDock1.ToolTabsAlignment = align.Value;
            }
        }

        private void docTabsVisibleCheck_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            this.radDock1.DocumentTabsVisible = this.docTabsVisibleCheck.Checked;
        }

        private void toolTabsVisibleCheck_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            this.radDock1.ToolTabsVisible = this.toolTabsVisibleCheck.Checked;
        }

        #endregion

    }
}
