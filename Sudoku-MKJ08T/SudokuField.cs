using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_MKJ08T
{
    public class SudokuField : Button
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set 
            { _value = value;
                if (_value < 0)
                    _value = 9;
                else if 
                    (_value > 9)
                    _value=0;
                if (_value == 0)
                    Text = "";
                else
                    Text = _value.ToString();
            }
        }

        private int _value1;

        public int Value1
        {
            get { return _value1; }
            set { _value1 = value; }
        }


        private bool _active;

        public bool Active
        {
            get { return _active; }
            set 
            {
                _active = value;
                if (_active)
                    Font = new Font(FontFamily.GenericSansSerif, 12);
                else
                    Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            }
        }

        public SudokuField()
        {
            Height = 30;
            Width = Height;
            BackColor = Color.White;
            Value = 0;
            MouseDown += SudokuField_MouseDown;

        }

        private void SudokuField_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Active) return;

            if (e.Button == MouseButtons.Left)
                Value++;
            if (e.Button == MouseButtons.Right)
                Value--;
        }
    }
}
