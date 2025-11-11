using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7
{
    public enum stanMjacha
    {
        vGriA,
        vGriB,
        pozaGroju,
        vCentri,
        vVorotahA,
        vVorotahB
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string newline = "\r\n";
        public static string sumText = "";
        public static string PlanText = "";
        public static int j = 1;

        public class Animal
        {
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private int _age;
            public int Age
            {
                get { return _age; }
                set { if (value > 0) _age = value; }
            }

            virtual public string Speak()
            {
                string logMsg = ". Виконано 'virtual' метод Speak() класу Animal";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
                return "(Якийсь загальний звук тварини)";
            }

            public void Eat(string food)
            {
                string logMsg = $". Виконано метод Eat(string) класу Animal. {this.Name} їсть {food}.";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
            }
        }

        public class Mammal : Animal
        {
            public string FurColor { get; set; }

            virtual public void Walk()
            {
                string logMsg = $". Виконано 'virtual' метод Walk() класу Mammal. {Name} йде.";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
            }
        }

        public class Dog : Mammal
        {
            public Dog(string name)
            {
                this.Name = name;
                string logMsg = $". Виконано конструктор Dog(). Створено собаку: {Name}.";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
            }

            override public string Speak()
            {
                string logMsg = ". Виконано 'override' метод Speak() класу Dog";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
                return "Гав! Гав!";
            }

            public string Speak(int times)
            {
                string sound = "";
                for (int i = 0; i < times; i++) sound += "Гав! ";

                string logMsg = $". Виконано 'overload' метод Speak(int) класу Dog. ({sound.Trim()})";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
                return sound;
            }

            public void Fetch()
            {
                string logMsg = $". Виконано метод Fetch() класу Dog. {Name} приніс палицю.";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
            }
        }

        public class Cat : Mammal
        {
            public Cat(string name, string color)
            {
                this.Name = name;
                this.FurColor = color;
                string logMsg = $". Виконано конструктор Cat(). Створено кота: {Name}, колір: {FurColor}.";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
            }

            override public string Speak()
            {
                string logMsg = ". Виконано 'override' метод Speak() класу Cat";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
                return "Мяу!";
            }

            new public void Walk()
            {
                string logMsg = $". Виконано 'new' метод Walk() класу Cat. {Name} йде безшумно.";
                Form1.sumText += Form1.j + logMsg + Form1.newline;
                Form1.j++;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            PlanText = "Планується зробити:" + newline;
            sumText = "Початок демонстрації" + newline;
            int i = 1;
            j = 1;
            label2.Text = sumText;

            PlanText += $"{i++}. Створити екземпляр 'Dog' (виклик конструктора)" + newline;
            Dog myDog = new Dog("Рекс");

            PlanText += $"{i++}. Встановити успадковану властивість 'Age'" + newline;
            myDog.Age = 5;

            PlanText += $"{i++}. Викликати 'override' метод Speak()" + newline;
            myDog.Speak();

            PlanText += $"{i++}. Викликати 'overload' метод Speak(3)" + newline;
            myDog.Speak(3);

            PlanText += $"{i++}. Викликати успадкований метод Eat() (з Animal)" + newline;
            myDog.Eat("кістку");

            PlanText += $"{i++}. Викликати успадкований метод Walk() (з Mammal)" + newline;
            myDog.Walk();

            PlanText += $"{i++}. Викликати власний метод Fetch()" + newline;
            myDog.Fetch();

            sumText += "--- Кінець демонстрації Dog ---" + newline;

            PlanText += $"{i++}. Створити екземпляр 'Cat' (виклик конструктора)" + newline;
            Cat myCat = new Cat("Мурчик", "Рудий");

            PlanText += $"{i++}. Викликати 'override' метод Speak()" + newline;
            myCat.Speak();

            PlanText += $"{i++}. Викликати 'new' (прихований) метод Walk()" + newline;
            myCat.Walk();

            sumText += "--- Демонстрація поліморфізму 'new' vs 'override' ---" + newline;

            PlanText += $"{i++}. Створити змінну типу 'Mammal' з екземпляром 'Cat'" + newline;
            Mammal genericMammal = myCat;

            PlanText += $"{i++}. Викликати 'Speak()' через змінну 'Mammal'" + newline;
            genericMammal.Speak();

            PlanText += $"{i++}. Викликати 'Walk()' через змінну 'Mammal'" + newline;
            genericMammal.Walk();

            label1.Text = PlanText;
            label2.Text = sumText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}