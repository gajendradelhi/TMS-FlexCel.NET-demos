﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using FlexCel.Core;
using FlexCel.XlsAdapter;
using FlexCel.Report;
using FlexCel.Demo.SharedData;


namespace JoinandUnion
{
    /// <summary>
    /// Form-Like reports
    /// </summary>
    public partial class mainForm: System.Windows.Forms.Form
    {

        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            AutoRun();
        }

        public void AutoRun()
        {
            using (FlexCelReport report = SharedData.CreateReport())
            {
                string DataPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ".."), "..") + Path.DirectorySeparatorChar;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    report.Run(DataPath + "Join and Union.template.xls", saveFileDialog1.FileName);

                    if (MessageBox.Show("Do you want to open the generated file?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Process.Start(saveFileDialog1.FileName);
                    }
                }
            }
        }


        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }

}
