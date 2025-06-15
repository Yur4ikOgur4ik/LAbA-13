using Arguments;
using Collections;
using MusicalInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class Lab13Tests
    {
        private MyObservableCollection<MusicalInstrument> collection;
        private Journal journal;

        [TestInitialize]
        public void Setup()
        {
            // Инициализируем коллекцию и журнал перед каждым тестом
            collection = new MyObservableCollection<MusicalInstrument>();
            collection.Name = "Моя коллекция";

            journal = new Journal();

            // Подписываем журнал на события
            collection.CollectionCountChanged += journal.CollectionCount;
            collection.CollectionReferenceChanged += journal.CollectionChanged;
        }

        [TestMethod]
        public void Test_Add_Item_Should_Generate_Event()
        {
            // Arrange
            var guitar = new Guitar { Name = "Fender" };

            // Act
            collection.Add(guitar);

            // Assert
            Assert.AreEqual(1, journal.GetEntries().Count);
            var entry = journal.GetEntries()[0];
            Assert.AreEqual("Моя коллекция", entry.CollectionName);
            Assert.AreEqual("Добавлен элемент", entry.ChangeType);
            Assert.AreEqual("Id: 0, name - Fender, number of strings: 6", entry.ObjectData);
        }

        [TestMethod]
        public void Test_Remove_Item_Should_Generate_Event()
        {
            // Arrange
            var guitar = new Guitar { Name = "Fender" };
            collection.Add(guitar);

            // Act
            collection.Remove(guitar);

            // Assert
            Assert.AreEqual(2, journal.GetEntries().Count); // Add + Remove
            var removeEntry = journal.GetEntries()[1];
            Assert.AreEqual("Моя коллекция", removeEntry.CollectionName);
            Assert.AreEqual("Удалён элемент", removeEntry.ChangeType);
            Assert.AreEqual("Id: 0, name - Fender, number of strings: 6", removeEntry.ObjectData);
        }

        [TestMethod]
        public void Test_Change_Item_Through_Indexer_Should_Generate_Event()
        {
            // Arrange
            var guitar1 = new Guitar { Name = "Fender" };
            var guitar2 = new Guitar { Name = "Gibson" };

            collection.Add(guitar1);

            // Act
            collection[guitar1] = guitar2;

            // Assert
            Assert.AreEqual(4, journal.GetEntries().Count); // Add + Replace + из предыдущих тестов
            var replaceEntry = journal.GetEntries()[3]; //потому что редачили в конце самом
            Assert.AreEqual("Моя коллекция", replaceEntry.CollectionName);
            Assert.AreEqual("Элемент заменён", replaceEntry.ChangeType);
            Assert.AreEqual("Id: 0, name - Gibson, number of strings: 6", replaceEntry.ObjectData);
        }

    }
}
