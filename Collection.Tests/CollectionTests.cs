using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests
    {

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            //Arange
            var nums = new Collection<int>();
            //Act

            //Assert
            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }
        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            var nums = new Collection<int>(5);

            Assert.AreEqual(1, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[5]"));

        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            var nums = new Collection<int>(5, 10, 15);

            Assert.AreEqual(3, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15]"));

        }

        [Test]
        public void Test_Collection_Add()
        {
            var nums = new Collection<int>(5, 10, 15, 20);
            nums.Add(25);

            Assert.AreEqual(5, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15, 20, 25]"));
        }

        [Test]
        public void Test_Collection_AddWithGrow()
        {
            var nums = new Collection<int>(10, 20, 30);
            int oldCapasity = nums.Capacity;

            for (int i = 1; i <= 50; i++)
            {
                nums.Add(i);
            }

            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapasity));

            string expectedNums = "[10, 20, 30, "
                + string.Join(", ", Enumerable.Range(1, 50)) + "]";

            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));

        }

        [Test]
        public void Test_Collection_AddRange()
        {
            var nums = new Collection<int>(5, 10, 15, 20);
            nums.AddRange(25, 30, 35);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15, 20, 25, 30, 35]"));
        }

        [Test]

        public void Test_Collection_GetByIndex()
        {
            var names = new Collection<string>("Mike", "Taylor");

            string firstItem = names[0];
            string secondItem = names[1];

            Assert.That(firstItem, Is.EqualTo("Mike"));
            Assert.That(secondItem, Is.EqualTo("Taylor"));

        }

        [Test]

        public void Test_Collection_GetByInvalidIndex()
        {
            var names = new Collection<string>("Mike", "Taylor");

            Assert.That(() => { var name = names[-1]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { var name = names[2]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { var name = names[500]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(names.ToString(), Is.EqualTo("[Mike, Taylor]"));
        }

        [TestCase("Peter", 0, "Peter")]
        public void Test_Collection_GetByValidIndex(
            string data, int index, string expectedData)
        {
            var nums = new Collection<string>(data);

            var actual = nums[index];

            Assert.AreEqual(expectedData, actual);
        }

        [TestCase("Peter", 0, "Peter")]
        public void Test_Collection_SetByValidIndex(
            string data, int index, string expectedData)
        {
            var nums = new Collection<string>(data);

            var actual = "Peter";

            nums[index] = actual;

            Assert.AreEqual(expectedData, actual);
        }

        [Test]

        public void Test_Collection_SetByIndex()
        {
            var names = new Collection<string>("Mike", "Taylor");
            names[0] = "Jeni";
            names[1] = "Deya";

            Assert.That(names.ToString(), Is.EqualTo("[Jeni, Deya]"));

        }

        [Test]

        public void Test_Collection_SetByInvalidIndex()
        {
            var names = new Collection<string>("Mike", "Taylor");

            Assert.That(() => { names[-1] = "newName"; },
                 Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names[-2] = "newName"; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names[500] = "newName"; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Mike, Taylor]"));
        }

        [Test]

        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();

            nums.AddRange(newNums);

            string expectedNums = "[" + string.Join(", ", newNums) + "]";

            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

        }

        [Test]
        public void Test_Collections_InsertAt()
        {
            var nums = new Collection<int>(3, 5);

            nums.InsertAt(0, 3);

            Assert.That(nums.Count == 3, "Count property");

        }


        [Test]

        public void Test_Collection_InsertAtStart()
        {
            var nums = new Collection<int>(1, 2, 3);
            nums.InsertAt(0, 7);

            Assert.That(nums.Count, Is.EqualTo(4));
            Assert.That(nums.Capacity, Is.EqualTo(16));
            Assert.That(nums.ToString(), Is.EqualTo("[7, 1, 2, 3]"));

            var names = new Collection<string>("Mike", "Taylor");
            names.InsertAt(0, "Jeni");

            Assert.That(names.ToString(), Is.EqualTo("[Jeni, Mike, Taylor]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]

        public void Test_Collection_InsertAtEnd()
        {
            var nums = new Collection<int>(1, 2, 3);
            nums.InsertAt(3, 7);

            Assert.That(nums.Count, Is.EqualTo(4));
            Assert.That(nums.Capacity, Is.EqualTo(16));
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3, 7]"));

            var names = new Collection<string>("Mike", "Taylor");
            names.InsertAt(2, "Jeni");

            Assert.That(names.ToString(), Is.EqualTo("[Mike, Taylor, Jeni]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]

        public void Test_Collection_InsertAtMiddle()
        {
            var nums = new Collection<int>(1, 2, 3, 4);
            nums.InsertAt(2, 7);

            Assert.That(nums.Count, Is.EqualTo(5));
            Assert.That(nums.Capacity, Is.EqualTo(16));
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 7, 3, 4]"));

            var names = new Collection<string>("Mike", "Taylor");
            names.InsertAt(1, "Jeni");

            Assert.That(names.ToString(), Is.EqualTo("[Mike, Jeni, Taylor]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]

        public void Test_Collection_InsertAtWithGrow()
        {
            var names = new Collection<string>("Mike", "Taylor");
            int oldCapacity = names.Capacity;

            names.InsertAt(0, "Niki");
            names.InsertAt(3, "Maya");
            names.InsertAt(4, "Anna");

            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(names.ToString(), Is.EqualTo("[Niki, Mike, Taylor, Maya, Anna]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));

        }

        [Test]

        public void Test_Collection_InsertAtInvalidIndex()
        {
            var names = new Collection<string>("Mike", "Taylor");

            Assert.That(() => names.InsertAt(-1, "Jeni"),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.InsertAt(4, "Taylor"),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.InsertAt(120, "Anna"),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Mike, Taylor]"));
        }

        [Test]
        public void Test_Collections_Exchange()
        {
            var nums = new Collection<int>(3, 5);

            nums.Exchange(0, 1);

            Assert.That(nums[1], Is.EqualTo(3));
            Assert.That(nums[0], Is.EqualTo(5));
        }

        [Test]

        public void Test_Collection_ExchangeMiddle()
        {
            var nums = new Collection<int>(1, 2, 3, 4);
            nums.Exchange(1, 2);
            Assert.That(nums.Count, Is.EqualTo(4));
            Assert.That(nums.Capacity, Is.EqualTo(16));
            Assert.That(nums.ToString(), Is.EqualTo("[1, 3, 2, 4]"));
        }

        [Test]

        public void Test_Collection_ExchangeFirstLast()
        {
            var nums = new Collection<int>(1, 2, 3, 4);
            nums.Exchange(0, 3);

            Assert.That(nums.Count, Is.EqualTo(4));
            Assert.That(nums.Capacity, Is.EqualTo(16));
            Assert.That(nums.ToString(), Is.EqualTo("[4, 2, 3, 1]"));
        }

        [Test]

        public void Test_Collection_ExchangeInvalidIndexes()
        {
            var names = new Collection<string>("Jeni", "Mike");

            Assert.That(() => names.Exchange(-1, 1),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(1, -1),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(2, 1),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(2, 1),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(-120, 120),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Jeni, Mike]"));
        }

        [Test]
        public void Test_Collection_ExhangeWithOneInvalidIndex()
        {
            var items = new int[] { 3, 4, 5, 6, 7 };
            var nums = new Collection<int>();

            nums.AddRange(3, 4, 5, 6, 7);
            Assert.Throws<ArgumentOutOfRangeException>(() => nums.Exchange(2, 7));
        }

        [Test]
        public void Test_Collection_ExhangeWithTwoInvalidIndex()
        {
            var items = new int[] { 1, 2, 3, 4 };
            var nums = new Collection<int>();

            nums.AddRange(1, 2, 3, 4);
            Assert.Throws<ArgumentOutOfRangeException>(() => nums.Exchange(-2, 4));
        }

        [Test]
        public void Test_Collections_RemovedAt()
        {
            var names = new Collection<string>("Misho", "Pesho");

            var removedItem = names.RemoveAt(0);

            Assert.That(removedItem, Is.EqualTo("Misho"));
            Assert.That(names.Count == 1, "Count property");
        }

        [Test]

        public void Test_Collection_RemoveAtStart()
        {
            var names = new Collection<string>("Jeni", "Mike", "Anna", "Taylor");
            var removedName = names.RemoveAt(0);

            Assert.That(removedName, Is.EqualTo("Jeni"));
            Assert.That(names.ToString(), Is.EqualTo("[Mike, Anna, Taylor]"));

        }

        [Test]

        public void Test_Collection_RemoveAtEnd()
        {
            var names = new Collection<string>("Jeni", "Mike", "Anna", "Taylor");
            var removedName = names.RemoveAt(3);

            Assert.That(removedName, Is.EqualTo("Taylor"));
            Assert.That(names.ToString(), Is.EqualTo("[Jeni, Mike, Anna]"));
        }

        [Test]

        public void Test_Collection_RemoveAtMiddle()
        {
            var names = new Collection<string>("Jeni", "Mike", "Anna", "Taylor");
            var removedName = names.RemoveAt(2);

            Assert.That(removedName, Is.EqualTo("Anna"));
            Assert.That(names.ToString(), Is.EqualTo("[Jeni, Mike, Taylor]"));
        }

        [Test]

        public void Test_Collection_RemoveAtInvalidIndex()
        {
            var names = new Collection<string>("Jeni", "Mike");

            Assert.That(() => names.RemoveAt(-1),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.RemoveAt(2),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.RemoveAt(120),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Jeni, Mike]"));
        }

        [Test]

        public void Test_Collection_RemoveAll()
        {
            var nums = new Collection<int>();
            const int itemCount = 100;

            nums.AddRange(Enumerable.Range(1, itemCount).ToArray());

            for (int i = 1; i <= itemCount; i++)
            {
                var removedNums = nums.RemoveAt(0);
                Assert.That(removedNums, Is.EqualTo(i));
            }

            Assert.That(nums.Count, Is.EqualTo(0));
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }

        [Test]

        public void Test_Collection_Clear()
        {
            var names = new Collection<string>("Jeni", "Mike", "Anna", "Taylor");
            names.Clear();

            Assert.That(names.Count, Is.EqualTo(0));
            Assert.That(names.ToString(), Is.EqualTo("[]"));
        }

        [Test]

        public void Test_Collection_CountAndCapacity()
        {
            var nums = new Collection<int>();


            const int itemCount = 100;

            for (int i = 1; i <= itemCount; i++)
            {
                nums.Add(i);

                Assert.That(nums.Count, Is.EqualTo(i));
                Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            }

            for (int i = itemCount; i >= 1; i--)
            {
                nums.RemoveAt(i - 1);

                Assert.That(nums.Count, Is.EqualTo(i - 1));
                Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            }
            for (int i = 0; i < nums.Capacity; i++)
            {
                nums.Add(i);
            }
        }

        [Test]
        public void Test_Collection_EnsureCapacity()
        {
            var nums = new Collection<int>();

            for (int i = 0; i <= 14; i++)
            {
                nums.Add(1);
            }

            Assert.That(nums.Count, Is.EqualTo(15));
            Assert.That(nums.Capacity, Is.EqualTo(16));
            nums.Add(1);
            Assert.That(nums.Count, Is.EqualTo(16));
            Assert.That(nums.Capacity, Is.EqualTo(16));
        }

        [Test]

        public void Test_Collection_ToStringEmpty()
        {
            var names = new Collection<string>();

            Assert.That(names.ToString(), Is.EqualTo("[]"));
        }

        [Test]

        public void Test_Collection_ToStringSingle()
        {
            var names = new Collection<string>("Jeni");

            Assert.That(names.ToString(), Is.EqualTo("[Jeni]"));

        }

        [Test]

        public void Test_Collection_ToStringMultiple()
        {
            var names = new Collection<string>("Jeni", "Mike", "Taylor");
            Assert.That(names.ToString(), Is.EqualTo("[Jeni, Mike, Taylor]"));

            var newString = new Collection<object>("Jeni", "Mike", 1, 2);
            Assert.That(newString.ToString(), Is.EqualTo("[Jeni, Mike, 1, 2]"));

        }

        [Test]

        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Jeni", "Anna");
            var nums = new Collection<int>(1, 2);
            var date = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, date);

            Assert.That(nested.ToString(), Is.EqualTo("[[Jeni, Anna], [1, 2], []]"));
        }

        [Test]

        public void Test_Collection_1MillionItems()
        {

            const int itemCount = 1000000;
            var nums = new Collection<int>();

            nums.AddRange(Enumerable.Range(1, itemCount).ToArray());

            Assert.That(nums.Count, Is.EqualTo(itemCount));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

            for (int i = itemCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            Assert.That(nums.ToString(), Is.EqualTo("[]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }



    }
}