namespace Dance.UnitTest
{
    /// <summary>
    /// Ioc≤‚ ‘
    /// </summary>
    [TestClass]
    public class IocTest
    {
        [TestMethod]
        public void SingletonTest()
        {
            DanceIocBuilder builder = new();

            builder.AddSingleton<IStudent, Student>();

            DanceIocLifeScope lifeScope1 = builder.CreateLifeScope();
            IStudent student1 = lifeScope1.Resolve<IStudent>();
            student1.Name = "zhangsan";

            IStudent student2 = lifeScope1.Resolve<IStudent>();

            Assert.AreEqual(student1, student2);

            DanceIocLifeScope lifeScope2 = builder.CreateLifeScope();
            IStudent student3 = lifeScope2.Resolve<IStudent>();

            Assert.AreEqual(student1, student3);
        }

        [TestMethod]
        public void LifeScopeTest()
        {
            DanceIocBuilder builder = new();

            builder.AddLifeScope<IStudent, Student>();

            DanceIocLifeScope lifeScope1 = builder.CreateLifeScope();
            IStudent student1 = lifeScope1.Resolve<IStudent>();
            student1.Name = "zhangsan";

            IStudent student2 = lifeScope1.Resolve<IStudent>();

            Assert.AreEqual(student1, student2);

            DanceIocLifeScope lifeScope2 = builder.CreateLifeScope();
            IStudent student3 = lifeScope2.Resolve<IStudent>();

            Assert.AreNotEqual(student1, student3);
        }

        [TestMethod]
        public void AssemblyTest()
        {
            DanceIocBuilder builder = new();

            builder.AddAssemblys("Dance.");

            DanceIocLifeScope lifeScope1 = builder.CreateLifeScope();
            IStudent student1 = lifeScope1.Resolve<IStudent>();
            student1.Name = "zhangsan";

            IStudent student2 = lifeScope1.Resolve<IStudent>();

            Assert.AreEqual(student1, student2);
        }
    }
}