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
using TM.BLL.Services;
using TM.Domain.ViewModel;

namespace TM.WinForm
{
    public partial class EventForm : Telerik.WinControls.UI.RadForm
    {
        private TodoService todoService;
        private TomatoService tomatoService;
        private int label_fontsize = 10;
        private string label_fontfamily = "Comic Sans MS, Verdana";
        private string titleOfCurrentEvent;
        private string tomatoIdOfCurrentEvent;
        private string todoIdOfCurrentEvent;

        public EventForm()
        {
            InitializeComponent();
            //事件初始化
            EventInit();
            todoService = new TodoService();
            tomatoService = new TomatoService();

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

            //this.ActiveControl = null; 
        } 

        private void EventForm_Load(object sender, EventArgs e)
        {
            //加入title bar 按鈕
            RadButtonElement buttonElement = new RadButtonElement();
            buttonElement.Text = "TitleBar Button";
            this.FormElement.TitleBar.Children[2].Children[0].Children.Insert(0, buttonElement);

            //cancel focus
            //this.ActiveControl = null; 
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

        #region todo事件
        private void txt_todo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                if (string.IsNullOrEmpty(txt_todo.Text) || txt_todo.Text == "事件描述") return;

                //變數設定
                var lb_count = 0;
                foreach (var c in splitPanel2.Controls)
                {
                    if (c is Label) lb_count += 1;
                }
                var location_heigh = 25 * lb_count / 2;
                var location_width = 35;
                var value = txt_todo.Text;
                var tag = txt_tag.Text;
                var needToamtoCount = (int)ddl_needTomato.Value;
                //var panel = new Panel();
                var label = new Label();
                var tomatoLabel = new Label();
                var checkbox = new RadCheckBox();
                var startButton = new RadButton();
                var deleteButton = new RadButton();
                var id = Guid.NewGuid().ToString();

                //UI新增待辦事件
                label.Name = "lb_" + id;
                label.Text = string.Format("{0}", value);
                label.AutoSize = true;
                label.Font = new Font(label_fontfamily, label_fontsize);
                label.Location = new Point(location_width+35, location_heigh);

                tomatoLabel.Name = "tomato_" + id;
                tomatoLabel.Text = string.Format("(0/{0})", needToamtoCount);
                tomatoLabel.AutoSize = true;
                tomatoLabel.Font = new Font(label_fontfamily, label_fontsize);
                tomatoLabel.Location = new Point(location_width, location_heigh);

                checkbox.Name = id;
                checkbox.Location = new Point(location_width - 22, location_heigh+4);
                checkbox.CheckStateChanged += checkbox_CheckStateChanged;

                startButton.Name = "btn_" + id;
                startButton.Location = new Point(location_width + 222, location_heigh+4);
                startButton.Text = "啟";
                startButton.Width = 22;
                startButton.Height = 18;
                startButton.Click += startButton_Click;

                deleteButton.Name = "del_btn_" + id;
                deleteButton.Location = new Point(location_width + 200, location_heigh+4);
                deleteButton.Text = "刪";
                deleteButton.Width = 22;
                deleteButton.Height = 18;
                deleteButton.Click += deleteButton_Click; ;

                splitPanel2.Controls.Add(label);
                splitPanel2.Controls.Add(tomatoLabel);
                splitPanel2.Controls.Add(checkbox);
                splitPanel2.Controls.Add(startButton);
                splitPanel2.Controls.Add(deleteButton);

                //資料存到資料庫
                TodoViewModel data = new TodoViewModel() { TodoID = id, Title = value, Tag = tag, NeedTomato = needToamtoCount };
                todoService.AddTodo(data);

                //完成後狀態
                txt_todo.Text = "";
                //this.ActiveControl = null;  
            }
        }



        void checkbox_CheckStateChanged(object sender, EventArgs e)
        {
            // 完成事件流程 + 劃掉該事件
            RadCheckBox ck = sender as RadCheckBox;
            var id = ck.Name;
            var lableID = "lb_" + id;
            var btnID = "btn_" + id;
            var delBtnID = "del_btn_" + id;
            Control currentLabel = ck.Parent.Controls[lableID];
            Control currentBtn = ck.Parent.Controls[btnID];
            Control currentDelBtn = ck.Parent.Controls[delBtnID];

            if (ck.Checked)
            {
                currentLabel.Font = new Font(label_fontfamily, label_fontsize, FontStyle.Strikeout);
                currentBtn.Visible = false;
                currentDelBtn.Visible = false;
                // 儲存完成事件
                todoService.FinishTodo(id);
            }
            else
            {
                currentLabel.Font = new Font(label_fontfamily, label_fontsize);
                currentBtn.Visible = true;
                currentDelBtn.Visible = true;
                // 取消事見
                todoService.FinishTodo(id, false);
            }

          
        }

        void deleteButton_Click(object sender, EventArgs e)
        {
            RadButton btn = sender as RadButton;
            var id = btn.Name.Replace("del_btn_", "");
            todoService.Delete(id);

            //清除UI
            var lableID = "lb_" + id;
            var tomatoLableID = "tomato_" + id;
            var btnID = "btn_" + id;
            var delBtnID = "del_btn_" + id;
            Control currentLabel = btn.Parent.Controls[lableID];
            Control currentTomatoLabel = btn.Parent.Controls[tomatoLableID];
            Control currentBtn = btn.Parent.Controls[btnID];
            Control currentDelBtn = btn.Parent.Controls[delBtnID];
            Control currentCkbox = btn.Parent.Controls[id];
            currentLabel.Dispose();
            currentTomatoLabel.Dispose();
            currentBtn.Dispose();
            currentDelBtn.Dispose();
            currentCkbox.Dispose();
        }
        #endregion
       

        #region 計時器相關
        //起用計時
        void startButton_Click(object sender, EventArgs e)
        {
            RadButton btn = sender as RadButton;
            var id = btn.Name.Replace("btn_","");
            var lableID = "lb_" + id;
            var title = btn.Parent.Controls[lableID].Text;

            // 新增番茄
            tomatoIdOfCurrentEvent = tomatoService.AddTomato(new TomatoViewModel(){TodoID = id});

            // 啟用記時器
            timer1.Enabled = true;
            btn_cancel.Enabled = true;
            btn_pause.Enabled = true;
            titleOfCurrentEvent = title;
            todoIdOfCurrentEvent = id;
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
            if (ticks == 2)
            {
                //timer 結束
                timer1.Enabled = false;
                ticks = 0;
                radProgressBar1.Value1 = 0;
                radProgressBar1.Text = "番茄計時器";
                btn_cancel.Enabled = false;
                btn_pause.Enabled = false;

                // 完成番茄紀錄
                tomatoService.FinishTomato(tomatoIdOfCurrentEvent);
                todoService.FinishOneTomato(todoIdOfCurrentEvent);

                //TODO UI 事件番茄數+1
                var todoEvent = "tomato_" + todoIdOfCurrentEvent;
                Control currentLabel = splitPanel2.Controls[todoEvent];
                currentLabel.Text = todoService.GetEventState(todoIdOfCurrentEvent);

                //TODO UI 實作番茄紀錄list
                var lb_count = 0;
                foreach (var c in splitPanel5.Controls)
                {
                    if (c is Label || c is RadLabel) lb_count += 1;
                }
                var location_heigh = 25 * lb_count;
                var location_width = 35;
                var today = DateTime.Now.ToString("M月d日");

                //當日第一次新增:加入日期
                var dateLabelList = GetAllControlsRecusrvive<RadLabel>(splitPanel5);
               
                bool hasToday = false;
                foreach (var dateLabel in dateLabelList)
                {
                    // validate 當日
                    if (dateLabel.Text.IndexOf(today) != -1)
                    {
                        hasToday = true;
                    }
                }

                if (dateLabelList.Count == 0 || hasToday == false)
                {
                    var dateLabel = new RadLabel();
                    dateLabel.Text = today;
                    dateLabel.ForeColor = System.Drawing.Color.Gray;
                    dateLabel.Font = new Font(label_fontfamily, label_fontsize);
                    dateLabel.Location = new Point(location_width, location_heigh);
                    splitPanel5.Controls.Add(dateLabel);

                    //更新
                    lb_count++;
                    location_heigh = 25 * lb_count;
                }
               
                var value = titleOfCurrentEvent;
                var startTime = DateTime.Now.ToString("HH:mm");
                var endTime = DateTime.Now.AddMinutes(-25).ToString("HH:mm");

                var label = new Label();
                label.Font = new Font(label_fontfamily, label_fontsize);
                label.AutoSize = true;
                label.Text = string.Format("{0}-{1}   {2}",startTime, endTime, value);
                label.Font = new Font(label_fontfamily, label_fontsize);
                label.Location = new Point(location_width, location_heigh);
                label.BackColor = Color.Transparent;
               
                splitPanel5.Controls.Add(label);

                //提醒休息
                RadMessageBox.SetThemeName("Desert");
                RadMessageBox.Instance.MinimumSize = new System.Drawing.Size(100, 100);
                DialogResult result = Telerik.WinControls.RadMessageBox.Show("辛苦了，請讓眼睛休息5分唷^_^", "", MessageBoxButtons.OK, RadMessageIcon.Info);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

            RadMessageBox.SetThemeName("Desert");
            RadMessageBox.Instance.MinimumSize = new System.Drawing.Size(100, 100);
            DialogResult result = Telerik.WinControls.RadMessageBox.Show("確定取消計時?", "", MessageBoxButtons.YesNo, RadMessageIcon.Info);

            if (result == DialogResult.Yes)
            {
                //TODO 事件取消
                timer1.Enabled = false;
                ticks = 0;
                radProgressBar1.Value1 = 0;
                radProgressBar1.Text = "番茄計時器";
                btn_pause.Enabled = false;
                btn_cancel.Enabled = false;
            }
            else if (result == DialogResult.No)
            {
                //...
            }
            else
            {
                //...
            } 

            

        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                //TODO 事件暫停
                timer1.Stop();
                btn_pause.Text = "繼續";
            }
            else
            {
                //事件恢復
                timer1.Start();
                btn_pause.Text = "暫停";
            }
            

            

        }
        #endregion
    



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

        #endregion

        public static IList<T> GetAllControlsRecusrvive<T>(Control control) where T : Control
        {
            var rtn = new List<T>();
            foreach (Control item in control.Controls)
            {
                var ctr = item as T;
                if (ctr != null)
                {
                    rtn.Add(ctr);
                }
                else
                {
                    rtn.AddRange(GetAllControlsRecusrvive<T>(item));
                }

            }
            return rtn;
        }


      

        



    }
}
