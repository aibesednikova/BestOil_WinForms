using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestOil
{
    /// <summary>
    /// Класс с методом получения выбранного пункта меню в форме 2. 
    /// Возвращает в форму 1 номер списка, который редактировался в форме 2.
    /// </summary>
    static class SelectedItem
    {
        public static int Value { get; set; }
    }
    /// <summary>
    /// Класс с методом определения внесения изменений в списки товаров
    /// (для определения необходимости сохранить изменённые списки в файл).
    /// </summary>
    static class IsChanged
    {
        public static bool Value { get; set; }
    }

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SelectedItem.Value = -1;
            IsChanged.Value = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
