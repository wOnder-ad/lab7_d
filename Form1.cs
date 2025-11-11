using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7
{
    public enum stanMjacha // це перелічуваний тип (нумератор)
    {
        vGriA,      // м'яч у грі у команди А
        vGriB,      // м'яч у грі у команди В
        pozaGroju,  // м'яч поза грою
        vCentri,    // м'яч в центрі поля
        vVorotahA,  // м'яч у воротах команди А
        vVorotahB   // м'яч у воротах команди В
    }

    public partial class Form1 : Form
    {
        public static string newline = "\r\n"; // Змінна для переходу на новий рядок у повідомленнях методів.
        public static string sumText = "";     // Змінна для накопичення повідомлень про виконання методів (результат).
        public static string PlanText = "";    // Змінна для плану роботи.
        public static int j = 1;               // Лічильник для нумерації повідомлень.

        public Form1()
        {
            InitializeComponent();
        }
        public class Sphere
        {
            // Стала (константа) Pi
            public const double Pi = Math.PI;

            // Приватні поля для зберігання властивостей: радіус, довжина кола, об'єм, площа, маса
            double r, l, v, s, m;

            // Властивість r_kuli (Радіус кулі)
            public double r_kuli
            {
                get { return r; }
                set
                {
                    r = value;
                    l = 2 * Pi * r;
                    v = 4 * Pi * r * r * r; // Об'єм кулі: 4/3 * Pi * r^3. В коді помилка, має бути 4/3, але залишено як у джерелі.
                    s = 4 * Pi * r * r;     // Площа кулі: 4 * Pi * r^2
                }
            }

            // Властивість l_kola (Довжина кола)
            public double l_kola
            {
                get { return l; }
            }

            // Властивість v_kuli (Об'єм кулі)
            public double v_kuli
            {
                get { return v; }
            }

            // Властивість s_kuli (Площа кулі)
            public double s_kuli
            {
                // У коді помилка, повертає v, а має s, але залишено як у джерелі.
                get { return v; }
            }

            // Властивість masa (Маса)
            public double masa
            {
                get { return m; }
                set { m = value; }
            }

            // Метод kotytys (Котитись) - virtual, може бути перевизначений у нащадках
            virtual public double kotytys(double t, double v)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод kotytys (double, double) класу Sphere"
                    + Form1.newline;
                Form1.j++;
                return 2 * Pi * r * t * v;
            }

            // Метод letity (Летіти) - virtual, може бути перевизначений
            virtual public double letity(double t, double v)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод letity (double, double) класу Sphere"
                    + Form1.newline;
                Form1.j++;
                return t * v;
            }

            // Метод udar (Удар) - virtual, може бути перевизначений. Використовує out-параметр.
            virtual public double udar(double t, out double v, double f, double t1)
            {
                // Розрахунок швидкості (v = F*t1 / m - з фізики F=ma, a=v/t1, отже F=m*v/t1, v = F*t1/m)
                v = f * t1 / m;
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод udar (double, out double, double, double) класу Sphera"
                    + " s=v*t="
                    + Convert.ToString(t * v)
                    + Form1.newline;
                Form1.j++;
                return t * v;
            }
        }

        // --- КЛАС-НАЩАДОК 1: mjach (М'яч) від Sphere ---
        public class mjach : Sphere // клас-нащадок від Sphere
        {
            // Метод popav (Попав) - virtual. Використовує ref-параметр.
            virtual public void popav(bool je, ref int kilkist)
            {
                if (je) kilkist++;
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод popav (bool, ref int) класу mjach. kilkist="
                    + Convert.ToString(kilkist)
                    + Form1.newline;
                Form1.j++;
            }

            // Метод letity (Летіти) - відмінна сигнатура (перевантаження). virtual.
            virtual public double letity(double t, double v, double f_tertja)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод letity (double, double, double) класу mjach"
                    + Form1.newline;
                Form1.j++;
                // Розрахунок відстані (з урахуванням сили тертя)
                return t * v - f_tertja / masa * t * t / 2;
            }

            // Метод для виклику letity базового класу (Sphere)
            public double letityBase(double t, double v)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод letityBase (double, double) класу mjach"
                    + Form1.newline;
                Form1.j++;
                return base.letity(t, v); // Виклик методу letity() з базового класу Sphere
            }
        }

        // --- КЛАС-НАЩАДОК 2: povitrjana_kulja (Повітряна куля) від Sphere ---
        public class povitrjana_kulja : Sphere
        {
            double tysk, maxTysk; // Поля для тиску

            // Властивість tyskGazu (Тиск газу)
            public double tyskGazu
            {
                get { return tysk; }
                set { tysk = value; }
            }

            // Властивість maxTyskGazu (Максимальний тиск газу)
            public double maxTyskGazu
            {
                get { return maxTysk; }
                set { maxTysk = value; }
            }

            // Метод lopatys (Лопнути)
            public bool lopatys()
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод lopatys(); класу povitrjana_kulja"
                    + Form1.newline;
                Form1.j++;
                if (tysk > maxTysk) return true;
                else return false;
            }

            // Метод letity (Летіти) - відмінна сигнатура (перевантаження)
            public double letity(double t, double v, double v_Vitru, double kutVitru)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод letity (double t, double v, double v_Vitru, double kutVitru) класу povitrjana_kulja"
                    + Form1.newline;
                Form1.j++;
                // Розрахунок з урахуванням вітру
                return t * v * v_Vitru * Math.Sin(kutVitru);
            }

            // Метод letity (Летіти) - та сама сигнатура, що й у Sphere. Використовує new для приховування.
            public new double letity(double t, double v)
            {
                double s;
                s = t * v;
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано метод letity (double t, double v) класу povitrjana_kulja. Ми пролетіли "
                    + Convert.ToString(s) + " метрів!" + Form1.newline;
                Form1.j++;
                return s;
            }
        }

        // --- КЛАС-НАЩАДОК 3: futbol_mjach (Футбольний м'яч) від mjach ---
        public class futbol_mjach : mjach
        {
            stanMjacha stanM; // Приватне поле для стану м'яча

            // Властивість tstanMjacha (Поточний стан м'яча)
            public stanMjacha tstanMjacha
            {
                get { return stanM; }
                set { stanM = value; }
            }

            // Властивість standout (Поза грою) - лише для читання
            public bool standout
            {
                get { if (stanM == stanMjacha.pozaGroju) return true; else return false; }
            }

            // Конструктор з параметром (stanMjacha sm)
            public futbol_mjach(stanMjacha sm)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j)
                    + ". Виконано конструктор futbol_mjach(stanMjacha sm)"
                    + Form1.newline;
                Form1.j++;
                stanM = sm; // Ініціалізація стану
                masa = 0.5F; // Ініціалізація успадкованої властивості
                r_kuli = 0.1F; // Ініціалізація успадкованої властивості
            }

            // Метод popav (Попав) - відмінна сигнатура (перевантаження)
            public void popav(bool je, ref int kilkista, ref int kilkistB)
            {
                if (je)
                {
                    if (stanM == stanMjacha.vGriA) kilkista++;
                    else
                    if (stanM == stanMjacha.vGriB) kilkistB++;
                }
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j) +
                    ". Виконано метод popav класу futbol_mjach з сигнатурою (bool, ref int, ref int)" + Form1.newline;
                Form1.j++;
            }

            // Метод popav (Попав) - перевизначення (override) методу з класу mjach
            override public void popav(bool je, ref int kilkist)
            {
                if (je) kilkist++;
                Form1.j++; // Інкремент перед використанням
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j) +
                    ". Виконано метод popav (bool je, ref int kilkist) (override) класу futbol_mjach" + Form1.newline;
            }

            // Метод popavBase (Виклик базового popav)
            public void popavBase(bool b, ref int i)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j) +
                    ". Виконано метод popavBase (bool b, ref int i) класу futbol_mjach, який викликає метод popav(b, ref i) класу mjach)" + Form1.newline;
                Form1.j++;
                base.popav(b, ref i); // Виклик перевизначеного методу popav з безпосереднього батьківського класу (mjach)
            }


            // Метод letity (Летіти) - перевизначення (override) методу з класу mjach
            override public double letity(double t, double v, double ftertja)
            {
                Form1.sumText = Form1.sumText + Convert.ToString(Form1.j) +
                    ". Виконано метод letity (double t, double v, double ftertja) (override) класу futbol_mjach"
                    + Form1.newline;
                Form1.j++;
                // Розрахунок (той самий, що і в mjach, але тепер це перевизначений метод)
                return t * v - ftertja / masa * t * t / 2;
            }
        }

        // --- ОБРОБНИКИ ПОДІЙ ФОРМИ ---

        // Обробник натискання кнопки "Start" (button1)
        private void button1_Click(object sender, EventArgs e)
        {
            // Ініціалізація змінних
            PlanText = "Планується зробити:" + newline;
            sumText = "Початок роботи" + newline;
            int i = 1;
            j = 1; // Скидання лічильника повідомлень

            label2.Text = sumText + newline; // Початкове повідомлення

            // 1. Створення екземпляра класу futbol_mjach з конструктором з параметром
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо створити екземпляр класу futbol_mjach, викликавши конструктор з параметром " + newline; i++;
            futbol_mjach fm = new futbol_mjach(stanMjacha.vCentri); // Виклик конструктора

            // Встановлення властивостей
            int GolA = 0, GolB = 0; // Лічильники голів
            fm.masa = 0.6F;
            fm.r_kuli = 0.12F;
            fm.tstanMjacha = stanMjacha.vGriA;

            // 2. Виклик перевизначеного методу popav (override)
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати метод popav (true, ref GolA);, що перевизначений у класі futbol_mjach (override) " +
                newline; i++;
            fm.popav(true, ref GolA);

            // 3. Виклик перевантаженого методу popav
            PlanText = PlanText + Convert.ToString(i) +
                ". Плануємо викликати метод popav (true, ref GolA, ref GolB) перевизначений у класі futbol_mjach з сигнатурою, інакшою ніж у базовому класі "
                + newline; i++;
            fm.popav(true, ref GolA, ref GolB);

            // 4. Виклик popavBase, який викликає popav базового класу mjach
            PlanText = PlanText + Convert.ToString(i) + "," + Convert.ToString(i + 1) +
                ". Плануємо, за посередністю методу popavBase (true, ref GolA) класу futbol_mjach викликати метод popav класу mjach " +
                newline; i++; i++;
            fm.popavBase(true, ref GolA);

            double s, v;

            // 6. Виклик методу udar (Sphere)
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати метод udar(2, out v, 200, 0.1) класу Sphere із класу futbol_mjach " + newline;
            i++;
            s = fm.udar(2, out v, 200, 0.1); // udar успадковано від Sphere

            // 7. Виклик методу kotytys (Sphere)
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати метод kotytys (5, 1) класу Sphere із класу futbol_mjach " + newline;
            i++;
            s = fm.kotytys(5, 1); // kotytys успадковано від Sphere

            // 8. Виклик методу letity (Sphere)
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати метод letity (20, 30) класу Sphere із класу futbol_mjach" + newline;
            i++;
            // Через успадкування від mjach, який має letity(t, v) в Sphere, викликається метод letity(t, v) з Sphere
            s = fm.letity(20, 30);

            // 9. Виклик перевизначеного методу letity (futbol_mjach override)
            PlanText = PlanText + Convert.ToString(i) + ". Плануємо викликати перевизначений метод letity (20, 30, 5) класу futbol_mjach" + newline; i++;
            s = fm.letity(20, 30, 5); // Викликається letity з 3-ма параметрами, який перевизначений у futbol_mjach

            // Виведення результатів на форму
            label1.Text = PlanText;
            label2.Text = sumText;
        }

        // Обробник натискання кнопки "Stop" (button2)
        private void button2_Click(object sender, EventArgs e)
        {
            Close(); // Закриття форми
        }
    }
}