using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string filePath = "students.txt"; 
        List<Student> students = new List<Student>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents();
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
         
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gender = radioButtonMale.Checked ? "male" : radioButtonFmale.Checked ? "Female" : "NOT Specified";

            Student student = new Student()
            {
                Name = textBoxName.Text,
                Level = textBoxLevel.Text,
                Email = textBoxEmail.Text,
                PassWord = textBoxPassword.Text,
                Gender = gender,
                Age = textBoxAge.Text
            };

            
            SaveStudent(student);

           
            LoadStudents();
        }

        private void LoadStudents()
        {
            students.Clear();
            listBox1.Items.Clear();

            if (!File.Exists(filePath))
            {
                return;
            }

            try
            {
                var lines = File.ReadAllLines(filePath, Encoding.UTF8);
                foreach (var line in lines)
                {
                    // التنسيق: Name|Age|Level|Email|PassWord|Gender
                    var parts = line.Split('|');
                    if (parts.Length >= 6)
                    {
                        var s = new Student
                        {
                            Name = parts[0],
                            Age = parts[1],
                            Level = parts[2],
                            Email = parts[3],
                            PassWord = parts[4],
                            Gender = parts[5]
                        };
                        students.Add(s);
                    }
                }

                RefreshListBox();
            }
             catch (Exception ex)
              {
                  MessageBox.Show("خطأ أثناء قراءة الملف: " + ex.Message);
              }
        }

        private void SaveStudent(Student s)
        {
            try
            {
                // التنسيق: Name|Age|Level|Email|PassWord|Gender
                var line = string.Join("|", new[] { s.Name, s.Age, s.Level, s.Email, s.PassWord, s.Gender });
                File.AppendAllText(filePath, line + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الحفظ: " + ex.Message);
            }
        }

        private void RefreshListBox()
        {
            listBox1.Items.Clear();
            foreach (var s in students)
            {
                string display = string.Format("Name: {0}, Age: {1}, Level: {2}, Email: {3}, Pass: {4}, Gender: {5}",s.Name, s.Age, s.Level, s.Email, s.PassWord, s.Gender);
                listBox1.Items.Add(display);
            }
        }
    }
}
