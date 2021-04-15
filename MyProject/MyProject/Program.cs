using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyProject
{
    class Program
    {
        static void Main(string[] args)
        {

            Random random = new Random();

            Dictionary<string, int[]> food = new Dictionary<string, int[]>();
            food.Add("помидор", new int[] { 25, 15 });
            food.Add("огурец", new int[] { 51, 15 });
            food.Add("бананы", new int[] { 10, 15 });
            food.Add("яблоки", new int[] { 114, 15 });
            food.Add("лук", new int[] { 28, 15 });
            food.Add("виноград", new int[] { 10, 15 });
            food.Add("гранат", new int[] { 37, 15 });
            food.Add("абрикос", new int[] { 150, 15 });
            food.Add("персик", new int[] { 150, 15 });
            food.Add("мандарин", new int[] { 300, 15 });

            int userMoney = random.Next(1, 15000 + 1);

            Console.WriteLine("Ваш баланс - " + userMoney);

            Dictionary<string, int> money = new Dictionary<string, int>();
            money.Add("fiveBucks", 100);
            money.Add("oneBucks", 500);
            money.Add("twoBucks", 250);
            money.Add("tenBucks", 50);
            money.Add("fiftyBucks", 10);
            money.Add("oneHudritBucks", 5);
            money.Add("fiveHudritBucks", 5);
            money.Add("oneThousandBucks", 5);
            money.Add("fiveThousandBucks", 5);

            SetOfMoneyAndProductsDefoult(food, ref money);

            Dictionary<string, int> сustomerOrder = new Dictionary<string, int>();

            Console.WriteLine("Ассортимент товаров и их стоимость: ");

            ShowingFoodAvailability(food);

            string exitStr = string.Empty;

            Console.WriteLine("Какие продукты вы желаете взять?: ");

            HumanChoiceOfProducts(сustomerOrder, exitStr, food);

            int totalAmount = 0;

            FoodSelection(сustomerOrder, ref totalAmount, ref food);

            Console.WriteLine("Общая сумма покупки составляет " + totalAmount);

            CustomerService(totalAmount, userMoney, money, ref food);

            Console.ReadKey();
            StreamWriter stFood = new StreamWriter("VihodnieFood.txt", false, Encoding.UTF8);
            foreach(var item in food)
            {
                stFood.WriteLine(food[item.Key][0]);
            }    
            stFood.Close();
            StreamWriter stMoney = new StreamWriter("VihodnieMoney.txt", false, Encoding.UTF8);
            foreach (var item in money)
            {
                stMoney.WriteLine(money[item.Key]);
            }
            stMoney.Close();
            SetOfMoneyAndProductsDefoult(food, ref money);
        }

        public static Dictionary<string, int> CalculatingOptimalDilivery(Dictionary<string, int> money, int dilivery, ref Dictionary<string, int[]> food)
        {
            bool cassHaveMoney = true;
            do
            {
                if (dilivery >= 5000)
                {
                    string bill = "fiveThousandBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery -= 5000;
                }
                else
                if (dilivery >= 1000)
                {
                    string bill = "oneThousandBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery -= 1000;
                }
                else
                if (dilivery >= 500)
                {
                    string bill = "fiveHudritBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery -= 500;

                }
                else
                if (dilivery >= 100)
                {
                    string bill = "oneHudritBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery -= 100;

                }
                else
                if (dilivery >= 50)
                {
                    string bill = "fiftyBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery -= 50;

                }
                else
                if (dilivery >= 10)
                {
                    string bill = "tenBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery -= 10;

                }
                else
                if (dilivery >= 5)
                {
                    string bill = "fiveBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery -= 5;
                }
                else
                if (dilivery >= 2)
                {
                    string bill = "twoBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery -= 2;
                }
                else
                if (dilivery >= 1)
                {
                    string bill = "oneBucks";
                    IssuingASingleBill(money, bill, food, ref cassHaveMoney);
                    dilivery = 0;
                }
                else
                {
                    Console.WriteLine("Извините. У меня нет сдачи. Зайдите в другой раз");
                    SetOfMoneyAndProductsDefoult(food, ref money);
                    cassHaveMoney = false;
                }

            } while (cassHaveMoney == true && dilivery > 0);

            return money;
        }

        public static Dictionary<string, int[]> SetOfMoneyAndProductsDefoult(Dictionary<string, int[]> food, ref Dictionary<string, int> money)
        {
            List<string> keys = new List<string>();

            StreamReader srFood = new StreamReader("VihodnieFood.txt", Encoding.UTF8);

            foreach (var item in food)
            {
                keys.Add(item.Key);
            }

            foreach (var item in keys)
            {
                string line;
                line = srFood.ReadLine();
                if (line != null)
                {
                    food[item][0] = int.Parse(line);
                }
            }

            srFood.Close();

            StreamReader srMoney = new StreamReader("VihodnieMoney.txt", Encoding.UTF8);

            keys.Clear();

            foreach (var item in money)
            {
                keys.Add(item.Key);
            }

            foreach (var item in keys)
            {
                string line;
                line = srMoney.ReadLine();
                if (line != null)
                {
                    money[item] = int.Parse(line);
                }
            }

            srMoney.Close();

            return food;
        }

        public static Dictionary<string, int> IssuingASingleBill(Dictionary<string, int> money, string bill, Dictionary<string, int[]> food, ref bool cassHaveMoney)
        {
            if (money[bill] != 0)
            {
                money[bill]--;
            }
            return money;
        }

        public static Dictionary<string, int> CustomerService(int totalAmount, int userMoney, Dictionary<string, int> money, ref Dictionary<string, int[]> food)
        {
            int dilivery = 0;
            if (totalAmount < userMoney)
            {
                Console.WriteLine("Сколько вы можете дать?: ");
                int userGivesMoney = int.Parse(Console.ReadLine());
                dilivery = userGivesMoney - totalAmount;
                CalculatingOptimalDilivery(money, dilivery, ref food);
                Console.Write("Ваша сдача составляет " + dilivery + ". Всего доброго!");
                dilivery = 0;
            }
            else if (totalAmount == userMoney)
            {
                dilivery = 0;
                Console.Write("Вы оплатили без сдачи. Всего доброго!");
            }
            else if (totalAmount > userMoney)
            {
                Console.Write("Извините, вам не хватает денег. Я не отдам вам заказ. :(");
            }
            return money;
        }

        public static Dictionary<string, int> FoodSelection(Dictionary<string, int> сustomerOrder, ref int totalAmount, ref Dictionary<string, int[]> food)
        {
            List<string> userFood = new List<string>();

            foreach (var item in сustomerOrder)
            {
                userFood.Add(item.Key);
            }

            foreach (var item in userFood)
            {
                Console.Write($"Сколько {item} вы хотите взять?: ");
                int valueFood = int.Parse(Console.ReadLine());
                if (food[item][0] >= valueFood)
                {
                    сustomerOrder[item] = valueFood;
                    totalAmount += food[item][1] * valueFood;
                    food[item][0] -= valueFood;
                }
                else
                {
                    Console.WriteLine("Извините, в наличии нет столько продуктов");
                }
            }

            return сustomerOrder;
        }

        public static void ShowingFoodAvailability(Dictionary<string, int[]> food)
        {
            foreach (var item in food)
            {
                Console.WriteLine($"{item.Key} - (Кол-во){food[item.Key][0]}, (Цена){food[item.Key][1]} ");
            }
        }

        public static void HumanChoiceOfProducts(Dictionary<string, int> сustomerOrder, string exitStr, Dictionary<string, int[]> food)
        {
            do
            {
                exitStr = Console.ReadLine();
                if (food.ContainsKey(exitStr) == true)
                {
                    сustomerOrder.Add(exitStr, 0);
                }
                else if (exitStr == "end")
                {
                    break;
                }
            } while (exitStr != "end");
        }
        
    }
}
