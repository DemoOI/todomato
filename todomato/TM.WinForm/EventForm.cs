using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;

namespace TM.WinForm
{
    public partial class EventForm : Telerik.WinControls.UI.RadForm
    {
        private int label_fontsize = 11;
        private string label_fontfamily = "Segoe UI";

        public EventForm()
        {
            InitializeComponent();
            //事件初始化
            EventInit();


            this.ActiveControl = null; 
        }

        private void EventInit()
        {
            //txt_todo.Enter += txt_todo_Enter;
            txt_todo.LostFocus += txt_todo_LostFocus;
            txt_todo.GotFocus += txt_todo_GotFocus;

            this.docTabAlignCombo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(docTabAlignCombo_SelectedIndexChanged);
            this.toolTabAlignCombo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.toolTabAlignCombo_SelectedIndexChanged);
            this.docTabsVisibleCheck.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.docTabsVisibleCheck_ToggleStateChanged);
            this.toolTabsVisibleCheck.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.toolTabsVisibleCheck_ToggleStateChanged);
            this.docCloseButtonCheck.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.docCloseButtonCheck_ToggleStateChanged);
            this.docTextOrientationCombo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.docTextOrientationCombo_SelectedIndexChanged);
            this.toolCloseButtonCheck.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.toolCloseButtonCheck_ToggleStateChanged);
            this.toolTextOrientationCombo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.toolTextOrientationCombo_SelectedIndexChanged);

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
            //this.IsMdiContainer = true;

            //RadForm form = new RadForm();
            //form.Text = "MDI Child 1";
            //form.MdiParent = this;
            //form.ThemeName = "Desert";
            //form.Show();

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

            this.ActiveControl = null; 
        } 

        private void EventForm_Load(object sender, EventArgs e)
        {
            //加入title bar 按鈕
            RadButtonElement buttonElement = new RadButtonElement();
            buttonElement.Text = "TitleBar Button";
            this.FormElement.TitleBar.Children[2].Children[0].Children.Insert(0, buttonElement);

            this.ActiveControl = null; 
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

        private void txt_todo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                if (string.IsNullOrEmpty(txt_todo.Text) || txt_todo.Text == "事件描述"  ) return;
                
                //變數設定
                var lb_count = 0;
                foreach (var c in splitPanel2.Controls)
                {
                    if (c is Label) lb_count += 1;
                }
                var location_heigh = 25 * lb_count;
                var location_width = 35;
                var value = txt_todo.Text;
                //var panel = new Panel();
                var label = new Label();
                var checkbox = new RadCheckBox();
                var startButton = new RadButton();
                var id = Guid.NewGuid().ToString();

                //執行新增待辦事件
                label.Name = "lb_" + id;
                label.Text = value;
                label.Font = new Font(label_fontfamily, label_fontsize);
                label.Location = new Point(location_width, location_heigh);
                checkbox.Name = id;
                checkbox.Location = new Point(location_width - 22, location_heigh + 4 );
                checkbox.CheckStateChanged += checkbox_CheckStateChanged;
                startButton.Name = "btn_" + id;
                startButton.Location = new Point(location_width + 240, location_heigh + 4);
                startButton.Text = "啟用";
                startButton.Width = 43;
                startButton.Height = 18;
                startButton.Click += startButton_Click;

                splitPanel2.Controls.Add(label);
                splitPanel2.Controls.Add(checkbox);
                splitPanel2.Controls.Add(startButton);
                //TODO 資料存到資料庫

                //完成後狀態
                txt_todo.Text = "事件描述";
                this.ActiveControl = null;  
            }
        }

        void startButton_Click(object sender, EventArgs e)
        {
            RadButton btn = sender as RadButton;
            var lableID = btn.Name.Replace("btn_", "lb_");
            var title = btn.Parent.Controls[lableID].Text;

            //TODO 啟用記時器
            timer1.Enabled = true;
        }

        void checkbox_CheckStateChanged(object sender, EventArgs e)
        {
            // 完成事件流程 + 劃掉該事件
            RadCheckBox ck = sender as RadCheckBox;
            var lableID = "lb_" + ck.Name;
            Control currentLabel = ck.Parent.Controls[lableID];

            if (ck.Checked)
	        {
                currentLabel.Font = new Font(label_fontfamily, label_fontsize, FontStyle.Strikeout);
                //TODO 儲存完成事件
	        }
            else
            {
                currentLabel.Font = new Font(label_fontfamily, label_fontsize);
                //TODO 取消事見

            }

            





        }

        private void toolTabsVisibleCheck_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            this.radDock1.ToolTabsVisible = this.toolTabsVisibleCheck.Checked;
        }

        void txt_todo_LostFocus(object sender, EventArgs e)
        {
            if (txt_todo.Text == "") txt_todo.Text = "事件描述";
        }

        void txt_todo_GotFocus(object sender, EventArgs e)
        {
            RadTextBox currentTextbox = sender as RadTextBox;
            if (currentTextbox.Text == "事件描述")
            {
                currentTextbox.Text = "";
            }
            //this.ActiveControl = null; 
        }

        private void txt_todo_Enter(object sender, EventArgs e)
        {
            RadTextBox currentTextbox = sender as RadTextBox;
            if (currentTextbox.Text == "事件描述")
            {
                currentTextbox.Text = "";
            }
        }

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


        //計時器 每格時間點做什麼事情
        int ticks = 0;
        double countTime = 1501.0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks++;
            radProgressBar1.Value1 = ticks;

            //設置倒數時間
            countTime--;
            TimeSpan timeSpan = TimeSpan.FromSeconds(countTime); 
            var timerText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds); 

            radProgressBar1.Text = timerText;
            if (ticks == 1501)
            {
                //timer 結束
                timer1.Enabled = false;
                ticks = 0;

                //TODO 實作番茄紀錄list
            }
        }
        #endregion

      

        



    }
}
