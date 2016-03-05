﻿using MapperExpression.Core;
using MapperExpression.Exceptions;
using MapperExpression.Tests.Units.ClassTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MapperExpression.Tests.Units
{
    [TestClass]
    public class MapperConfigurationBaseTests
    {

        [TestMethod,TestCategory("Constructor")]
        public void NewMapperConfigurationBase_SetProperties()
        {
            MapperConfigurationBase actual = null;
            actual = new MapperConfiguration<ClassSource, ClassDest>("sourceTest");
            Assert.IsNotNull(actual.MemberToMapForNew);
            Assert.AreEqual(actual.TargetType, typeof(ClassDest));
            Assert.AreEqual(actual.SourceType, typeof(ClassSource));
        }

        [TestMethod, TestCategory("GetDestinationType")]
        public void GetDestinationType_WithoutServiceConstructor()
        {
            Type actual = null;
            var mapper = new MapperConfiguration<ClassSource, ClassDest>("sourceTest");
            actual = mapper.GetDestinationType();

            Assert.AreEqual(actual, typeof(ClassDest));
        }

        [TestMethod, TestCategory("GetDestinationType")]
        public void GetDestinationType_WithServiceConstructor()
        {
            Type actual = null;
            Mapper.ConstructServicesUsing((x) => { return new ClassDest2(); });
           
            var mapper = Mapper.CreateMap<ClassSource2, IClassDest2>().ConstructUsingServiceLocator();
            Mapper.Initialize();
            actual = mapper.GetDestinationType();

            Assert.AreEqual(actual, typeof(ClassDest2));
            Mapper.Reset();
        }


        [TestMethod, TestCategory("GetDelegate"), ExpectedException(typeof(MapperNotInitializedException))]
        public void GetDelegate_MapperNotInitialise_Exception()
        {
          
            MapperConfigurationBase mapper = new MapperConfiguration<ClassSource, ClassDest>("sourceTest");
            
            mapper.GetDelegate();
         
        }
        
        
        [TestMethod, TestCategory("CheckAndConfigureTuple")]
        public void CheckAndConfigureMappingTest_List_NotSameType_Success()
        {
            Mapper.Reset();
            Mapper.CreateMap<ClassSource2, ClassDest2>();
           
            
            MapperConfigurationTestContainer expected = new MapperConfigurationTestContainer();
            MapperConfigurationContainer.Instance.Add(expected);
            Mapper.Initialize();
            Expression<Func<ClassSource, object>> source = s => s.ListProp;
            Expression<Func<ClassDest, object>> target = d => d.ListProp;
            Tuple<LambdaExpression, LambdaExpression, bool> tuple = Tuple.Create<LambdaExpression, LambdaExpression, bool>(source, target, true);
            expected.CheckAndConfigureMappingTest(tuple);
            Assert.IsNotNull(expected.GetDelegate());


        }

        [TestMethod, TestCategory("CheckAndConfigureTuple")]
        public void CheckAndConfigureMappingTest_List_SameType_Success()
        {
            Mapper.Reset();
            Mapper.CreateMap<ClassSource2, ClassDest2>();


            MapperConfigurationTestContainer expected = new MapperConfigurationTestContainer();
            MapperConfigurationContainer.Instance.Add(expected);
            Mapper.Initialize();
            Expression<Func<ClassSource, object>> source = s => s.ListString;
            Expression<Func<ClassDest, object>> target = d => d.ListString;
            Tuple<LambdaExpression, LambdaExpression, bool> tuple = Tuple.Create<LambdaExpression, LambdaExpression, bool>(source, target, false);
            expected.CheckAndConfigureMappingTest(tuple);
            Assert.IsNotNull(expected.GetDelegate());


        }
    }
}
