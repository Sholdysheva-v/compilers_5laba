using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Лабораторная_1_компиляторы
{
    public partial class Form1 : Form
    {
        private string currentFilePath = string.Empty; 
        private bool isTextChanged = false;
        //private Panel lineNumberPanel; // Панель для отображения номеров строк
        public Form1()
        {
            InitializeComponent();
            UpdateTitle();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeFontSizeComboBox();
            //InitializeLineNumberPanel(); 
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFile();
        }

        
        private void UpdateTitle()
        {
            this.Text = string.IsNullOrEmpty(currentFilePath) ? "Компилятор" : Path.GetFileName(currentFilePath) ;
        }
        
        private bool ConfirmSaveChanges()
        {
            if (isTextChanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Подтверждение", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                return result != DialogResult.Cancel;
            }
            return true;
        }

        
        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
                SaveFileAs(); // Если путь пуст, вызываем "Сохранить как"
            else
            {
                File.WriteAllText(currentFilePath, richTextBox1.Text);
                isTextChanged = false;
            }
        }

        
        private void SaveFileAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                    currentFilePath = saveFileDialog.FileName;
                    isTextChanged = false;
                    UpdateTitle();
                }
            }
        }

        
        private void OpenFile()
        {
            if (ConfirmSaveChanges())
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                        currentFilePath = openFileDialog.FileName;
                        isTextChanged = false;
                        UpdateTitle();
                    }
                }
            }
        }
        private void NewFile()
        {
            if (ConfirmSaveChanges())
            {
                richTextBox1.Clear();
                currentFilePath = string.Empty;
                isTextChanged = false;
                UpdateTitle();
            }
        }

        
        private void ExitApplication()
        {
            if (ConfirmSaveChanges())
                Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = string.Empty;
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void правкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void текстToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void сохранитьКакToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void InitializeFontSizeComboBox()
        {
            // Добавляем размеры шрифта в комбобокс
            comboBoxFontSize.Items.AddRange(new object[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 });

            // Устанавливаем начальный размер шрифта
            comboBoxFontSize.SelectedIndex = 4; // Например, 12

            // Подписываемся на событие изменения выбора
            comboBoxFontSize.SelectedIndexChanged += ComboBoxFontSize_SelectedIndexChanged;
        }

        private void ComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем выбранный размер шрифта
            if (comboBoxFontSize.SelectedItem != null)
            {
                int fontSize = (int)comboBoxFontSize.SelectedItem;

                // Применяем размер шрифта к richTextBox1 и richTextBox2
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, fontSize);
                //richTextBox2.Font = new Font(richTextBox2.Font.FontFamily, fontSize);
            }
        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //AnalyzeCode();
            //CheckSyntax();
            RunAnalyzer();


        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

            //AnalyzeCode();
            //CheckSyntax();
            RunAnalyzer();
        }

        //private void AnalyzeCode()
        //{
        //    LexicalAnalyzer_2 analyzer = new LexicalAnalyzer_2();
        //    var tokens = analyzer.Analyze(richTextBox1.Text);

        //    StringBuilder result = new StringBuilder();
        //    foreach (var token in tokens)
        //    {
        //        result.AppendLine(token.ToString());
        //    }

        //    richTextBox2.Text = result.ToString();
        //}

        private void RunAnalyzer()
        {
            try
            {
                // Очищаем предыдущие результаты
                richTextBox2.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // Сбрасываем подсветку в исходном коде
                richTextBox1.SelectAll();
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox1.SelectionColor = Color.Black;

                // Получаем текст для анализа
                string inputText = richTextBox1.Text;

                // Проверяем, есть ли текст для анализа
                if (string.IsNullOrWhiteSpace(inputText))
                {
                    MessageBox.Show("Нет кода для анализа", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Настраиваем DataGridView для тетрад
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;

                // Добавляем колонки
                dataGridView1.Columns.Add("op", "Операция");
                dataGridView1.Columns.Add("arg1", "Аргумент 1");
                dataGridView1.Columns.Add("arg2", "Аргумент 2");
                dataGridView1.Columns.Add("result", "Результат");

                // Вызываем анализ
                Tetrada.AnalyzeExpression(
                    inputText,
                    richTextBox1,    // Для подсветки ошибок
                    dataGridView1,   // Для вывода тетрад
                    richTextBox2     // Для текста ошибок
                );

                // Настраиваем внешний вид richTextBox2 с ошибками
                if (!string.IsNullOrWhiteSpace(richTextBox2.Text))
                {
                    richTextBox2.BackColor = Color.LavenderBlush;
                    richTextBox2.ForeColor = Color.DarkRed;
                    richTextBox2.Font = new Font("Consolas", 10);
                    richTextBox2.SelectionStart = 0;
                    richTextBox2.ScrollToCaret();
                }
                else
                {
                    richTextBox2.BackColor = SystemColors.Window;
                    richTextBox2.Text = "Ошибок не обнаружено";
                    richTextBox2.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при анализе кода: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckSyntax()
        {
            // Получаем текст из редактора
            string code = richTextBox1.Text;

            // Лексический анализ
            var lexer = new LexicalAnalyzer();
            var tokens = lexer.Analyze(code);

            // Синтаксический анализ
            var parser = new SyntaxAnalyzer();
            var errors = parser.AnalyzeWithIrons(tokens);

            // Вывод результатов в richTextBox2
            parser.PrintResultsToRichTextBox(errors, richTextBox2);

            //// Дополнительная информация о структуре
            //if (parser.IsStructCorrect(tokens))
            //{
            //    richTextBox2.AppendText("\n\nДополнительная проверка: структура объявлена корректно.");
            //}
            //else if (errors.Count == 0)
            //{
            //    richTextBox2.AppendText("\n\nДополнительная проверка: найдены проблемы в структуре (возможно, не хватает полей или скобки не сбалансированы).");
            //}
        }

        private void постановкаЗадачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @"Структуры в языке PHP -- это составные типы данных, которые позволяют
группировать переменные различных типов под одним именем. Для объявления
структуры и её инициализации в PHP используется синтаксис вида:

struct ИмяСтруктуры { тип $поле1; тип $поле2; ... тип $полеN; };

Примеры:

1. Структура Point -- описывает точку в двумерном пространстве с
   координатами x и y:, например: struct Point { float \$x; float \$y; };

2. Пустая структура -- может использоваться как заглушка для
   дальнейшего расширения: struct EmptyStruct {};

В связи с разработанной автоматной грамматикой G[<Struct>],
синтаксический анализатор (парсер) будет считать верными следующие
записи объявления структур:

1. struct User {string $name; int $age; };

2. struct Product { string $title; float $price; };

3. struct Config {};";

            Form4 form4 = new Form4("Постановка задачи", content);
            form4.Show();
        }



        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @"Грамматика G[‹Def›] является автоматной.

Правила (1) -- (18) для G[‹Def›] реализованы на графе (см. рисунок 1).

Сплошные стрелки на графе характеризуют синтаксически верный разбор;
пунктирные символизируют состояние ошибки (ERROR);

Состояние 11 символизирует успешное завершение разбора.";

            Form4 form4 = new Form4("Метод анализа", content);
            form4.Show();
        }



        private void исходныйКодПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @"Листинг программной части разработанного синтаксического анализатора
объявления и определения структуры на языке PHP представлен в приложении В.";

            Form4 form4 = new Form4("Исходный код программы", content);
            form4.Show();
        }

        private void грамматикаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"Определим грамматику структур на языке PHP G[‹Def›] в нотации Хомского
с продукциями P:

1. <START> → 'STRUCT' <SPACE>
2. <SPACE> → ' '→ <Name>
3. <Name> → 'letter' → <NameRem>
4. <NameRem> → 'letter' → <NameRem>
5. <NameRem> → <Digit> <NameRem>
6. <NameRem> → '{' <TYPE>
7. <TYPE> → 'string' → 'SPACE'
8. <TYPE> → 'int' → <SPACE>
9. <TYPE> → 'bool' → <SPACE>
10. <TYPE> → 'float' → <SPACE>
11. <TYPE> -> 'double' → <SPACE>
12. <SPACE> → ' '→ <SPACE>
13. <SPACE> → '\$' → <StrRem>
14. <StrRem> → symbol <StrRem>
15. <StrRem> → ';' <TYPE>
16. <StrRem> → ';' <END>
17. <END> → '}' FINAL
18. FINAL → ';'‹E› → ‹;›

- ‹Digit› → '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9'
- ‹Letter› → 'a' | 'b' | 'c' | ... | 'z' | 'A' | 'B' | 'C' | ... | 'Z'";

            Form4 form4 = new Form4("Грамматика", content);
            form4.Show();
        }

        private void классификацияГрамматикиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"Согласно классификации Хомского, грамматика G[‹Def›] является
автоматной.

Поскольку все правила продукции имеют форму либо A → aB, либо A → a,
грамматика является праворекурсивной и, следовательно, соответствует
автоматной грамматике. Это удовлетворяет требованию о том, что все
правила должны быть либо леворекурсивными, либо праворекурсивными -- в
нашем случае они однородно праворекурсивные.";

            Form4 form4 = new Form4("Классификация грамматики", content);
            form4.Show();
        }

        private void метToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            //// Загрузка изображения из ресурсов проекта
            ////Image image = Properties.Resources.R4.png; // Ваше изображение из ресурсов

            //// Копирование в буфер обмена
            //Clipboard.SetImage(image);

            //// Вставка из буфера обмена в RichTextBox
            //richTextBox1.Paste();
        }

        private void тестовыйПримерToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"Примеры корректных структур:

    Ввод:
struct User {string $name; int $age; };
    Вывод:
Синтаксический анализ завершен успешно!
Ошибок не обнаружено.

    Ввод:
struct Product { string \$title; float \$price; };
    Вывод:
Синтаксический анализ завершен успешно!
Ошибок не обнаружено.

Примеры некорректных структур:

    Ввод
struct { float \$price; }; 
    Вывод
Найдена 1 ошибка:

• Ошибка: Ожидалось идентификатор, но получено '{' (открывающая скобка)
  Позиция: 8
  Фрагмент: '{'

    Ввод:
struct Product string \$title; float \$price; }; 
    Вывод
Найдена 1 ошибка:

• Ошибка: Ожидалось '{', но получено 'string' (тип данных)
  Позиция: 16
  Фрагмент: 'string'";

            Form4 form4 = new Form4("Тестовый пример", content);
            form4.Show();
        }

        private void списокЛитературыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"1. Шорников Ю.В. Теория и практика языковых процессоров : учеб. пособие
   / Ю.В. Шорников. -- Новосибирск: Изд-во НГТУ, 2022.

2. Gries D. Designing Compilers for Digital Computers. New York, Jhon
   Wiley, 1971. 493 p.

3. Теория формальных языков и компиляторов [Электронный ресурс] /
   Электрон. дан. URL:
   https://dispace.edu.nstu.ru/didesk/course/show/8594, свободный.
   Яз.рус. (дата обращения 25.03.2025).";

            Form4 form4 = new Form4("Список литературы", content);
            form4.Show();
        }

        private void исходныйКодПрограммыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"

";
            Form4 form4 = new Form4("Исходный код", content);
            form4.Show();
        }
    }
}
