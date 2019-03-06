using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace _08_UIResponsive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DoSomethingVoid();
            label1.Text = @"DoSomethingVoid terminated"; 
            Cursor.Current = Cursors.Default;

        }

        private void DoSomethingVoid()
        {
            Thread.Sleep(2000);
            MessageBox.Show($@"DoSomethingVoid terminated {Thread.CurrentThread.Name}");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            label2.Text = DoSomethingString();
            Cursor.Current = Cursors.Default;
        }

        private string DoSomethingString()
        {
            Thread.Sleep(2000);
            MessageBox.Show(@"DoSomethingString terminated");
            return @"DoSomethingString terminated"; ;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            await DoSomethingVoidAsync();
            label3.Text = @"DoSomethingVoidAsync terminated";
            Cursor.Current = Cursors.Default;
        }

        

        private Task DoSomethingVoidAsync()
        {
            var task = Task.Run(() =>
            {
                var x = 0;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    x = i;
                }
                //MessageBox.Show(@"DoSomethingVoidAsync Terminated");
            });
            return task;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            label4.Text = await DoSomethingStringAsync();
            Cursor.Current = Cursors.Default;
        }

        private Task<string> DoSomethingStringAsync()
        {
            var task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                //MessageBox.Show(@"DoSomethingStringAsync Terminated");
                return @"DoSomethingStringAsync terminated";
            });
            return task;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            label5.Text = @"Hello World";
            await DoComplicatedTaskAsync();
            label5.Text = @"Bye World";
            Cursor.Current = Cursors.Default;
        }

        private Task DoComplicatedTaskAsync()
        {
            Task task = Task.Run(() =>
            {
                Thread.Sleep(2000);
            });
            return task;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label1.Text = string.Empty;
            label2.Text = string.Empty;
            label3.Text = string.Empty;
            label4.Text = string.Empty;
            label5.Text = string.Empty;
        }
    }
}
