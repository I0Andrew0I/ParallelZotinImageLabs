using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
namespace Lab2
{
    public partial class Lab2Form : Form
    {
        string filePath;
        byte[] input;
        byte[] buffer;
        byte[] result;
        int size;
        string curFilter;
        string curChannel = "r";
        int height;
        int width;
        public Lab2Form()
        {
            InitializeComponent();
        }
    }
}
