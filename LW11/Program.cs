using MusicalInstruments;
using Arguments;
using System.Collections;
using System;
using System.Dynamic;
using System.Transactions;
using System.Collections.Generic;
using System.Reflection;
using Collections;
using System.Xml.Linq;
using System.Diagnostics;
using System.ComponentModel.Design.Serialization;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;


namespace LW11
{
    public class Program
    {
        static void Main()
        {
            var collection1 = new MyObservableCollection<MusicalInstrument>();
            var collection2 = new MyObservableCollection<MusicalInstrument>();

            var journal1 = new Journal();
            var journal2 = new Journal();

            // Подписываемся на события
            collection1.CollectionCountChanged += journal1.CollectionCount;//рассказть про подписчика и издателя
            collection1.CollectionReferenceChanged += journal1.CollectionChanged;

            collection1.CollectionReferenceChanged += journal2.CollectionChanged;
            collection2.CollectionReferenceChanged += journal2.CollectionChanged;
            collection1.Name = "Коллекция 1";
            collection2.Name = "Коллекция 2";

            var guitar1 = new Guitar();
            var guitar2 = new Guitar();
            var piano1 = new Piano();
            var piano2 = new Piano();
            var eGuirar1 = new ElectroGuitar();
            guitar1.RandomInit();
            guitar2.RandomInit();
            piano1.RandomInit();
            piano2.RandomInit();
            eGuirar1.RandomInit();

            // Добавляем элементы в коллекции
            collection1.Add(guitar1);
            collection1.Add(piano1);

            collection2.Add(eGuirar1);

            // Удаляем элемент
            collection1.Remove(piano1);

            // Заменяем элемент через индексатор
            collection1[guitar1] = guitar2;

            collection2.Add(piano2);
            collection2[piano2] = guitar1;

            // Выводим журналы
            Console.WriteLine("\n--- Журнал 1 (события коллекции 1) ---");
            Console.WriteLine(journal1);

            Console.WriteLine("\n--- Журнал 2 (изменения ссылок всех коллекций) ---");
            Console.WriteLine(journal2);

            // Выводим таблицы (состояние коллекций)
            Console.WriteLine("\n--- Состояние коллекции 1 ---");
            //Console.WriteLine(collection1.PrintTable());

            //Console.WriteLine("\n--- Состояние коллекции 2 ---");
            //Console.WriteLine(collection2.PrintTable());

        }



    }
}
