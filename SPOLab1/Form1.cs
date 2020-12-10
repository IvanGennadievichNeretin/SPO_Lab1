using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SPOLab1
{
    public partial class Form1 : Form
    {
        //глобальные переменные
        String[] identifyers;
        String currentFile;
        binaryTree Tree;
        SimpleList list;

        public Form1()
        {
            InitializeComponent();
            currentFile = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //получение начальной директории
            String fileFolderDirectory = "";
            try
            {
                Path.GetFullPath(fileFolderDirectory);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                //выбор файла
                openFileDialog1.InitialDirectory = fileFolderDirectory;
                openFileDialog1.Filter = "Файлы txt|*.txt";
                String fileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
                label2.Text = fileName;
                currentFile = fileName;

                //чтение из файла
                if (fileName != "")
                {
                    StreamReader reader = new StreamReader(fileName);
                    int stringCount = 0;
                    String bufer;

                    //количество строк в файле
                    while ((bufer = reader.ReadLine()) != null)
                    {
                        stringCount++;
                    }
                    //создание новой коллекции строк
                    identifyers = new string[stringCount];

                    //обнуление значений для считывания
                    reader.BaseStream.Position = 0;
                    stringCount = 0;
                    while ((bufer = reader.ReadLine()) != null){
                        identifyers[stringCount] = bufer;
                        stringCount++;
                    }

                    //создание бинарного дерева и простого списка
                    int treeDupes = 0;
                    int listDupes = 0;
                    Tree = binaryTree.StrListToTree(identifyers, ref treeDupes);
                    list = new SimpleList(identifyers);
                    listDupes = list.dupes;
                    MessageBox.Show("Структуры данных успешно созданы. Число дубликатов: " + "binaryTree = " + treeDupes + ", list = " +
                        listDupes, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //вывод содержимого файла
                    for (int i = 0; i < Math.Min(stringCount, 10000); i++)
                    {
                        listBox1.Items.Add(identifyers[i]);
                    }

                    reader.Close();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Проверка ввода
            if (identifyers == null)
            {
                MessageBox.Show("Сперва необходимо выбрать файл идентификаторов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String soughtStr = textBox1.Text;
            if (soughtStr.Equals(""))
            {
                MessageBox.Show("Сперва необходимо ввести идентификатор.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Бинарное дерево
            int operationsTree = 0;
            long ticks = DateTime.Now.Ticks;
            if (Tree.isItExist(soughtStr, ref operationsTree))
            {
                ticks = DateTime.Now.Ticks - ticks;
                MessageBox.Show("Идентификатор существует (binary tree)", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ticks = DateTime.Now.Ticks - ticks;
                MessageBox.Show("Идентификатор не существует (binary tree)", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            label5.Text = Convert.ToString(ticks);
            label9.Text = Convert.ToString(operationsTree);


            //Простой список
            int operationsList = 0;
            ticks = DateTime.Now.Ticks;
            if (list.IsItExist(soughtStr, ref operationsList))  
            {
                ticks = DateTime.Now.Ticks - ticks;
                MessageBox.Show("Идентификатор существует (simple list)", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ticks = DateTime.Now.Ticks - ticks;
                MessageBox.Show("Идентификатор не существует (simple list)", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            label4.Text = Convert.ToString(ticks);
            label7.Text = Convert.ToString(operationsList);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String savedFile = saveFileDialog1.FileName;
                StreamWriter writer = new StreamWriter(savedFile);
                Random rng = new Random();
                int randomValue = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    randomValue = rng.Next(0, 1000000000);
                    writer.WriteLine("id" + randomValue);
                }

                writer.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //добавить идентификатор
            //Проверка ввода
            if (identifyers == null)
            {
                MessageBox.Show("Сперва необходимо выбрать файл идентификаторов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String newIdentifyer = textBox2.Text;
            if (newIdentifyer.Equals(""))
            {
                MessageBox.Show("Сперва необходимо ввести идентификатор.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            int OperationsAddTree = 0;
            if (Tree.isItExist(newIdentifyer, ref OperationsAddTree))
            {
                MessageBox.Show("Идентификатор '" + newIdentifyer + "' уже существует. (binary tree)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                OperationsAddTree = 0;
                Tree.put(newIdentifyer, ref OperationsAddTree);
                label13.Text = Convert.ToString(OperationsAddTree);
                MessageBox.Show("Идентификатор '" + newIdentifyer + "' успешно добавлен! (binary tree)", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            int OperationsAddList = 0;
            if (list.IsItExist(newIdentifyer, ref OperationsAddList))
            {
                MessageBox.Show("Идентификатор '" + newIdentifyer + "' уже существует. (simple list)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                OperationsAddList = 0;
                int dupesList = 0;
                list.put(newIdentifyer, ref OperationsAddList, ref dupesList);
                label11.Text = Convert.ToString(OperationsAddList);
                MessageBox.Show("Идентификатор '" + newIdentifyer + "' успешно добавлен! (simple list)", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
