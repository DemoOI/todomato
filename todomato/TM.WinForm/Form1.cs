using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TM.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            WebClient client = new WebClient();
            client.Headers["Accept"] = "application/json";
            string rvl = client.DownloadString(new Uri("http://localhost:1535/api/Todo"));

        }
    }
}
