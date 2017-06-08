﻿using MapperExpression.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MapperExpression.Tests.Units.ClassTests
{
    /// <summary>
    ///Need to create a derived class for test the protected properties/Methods
    /// </summary>
    public class MapperConfigurationTestContainer : MapperConfiguration<ClassSource, ClassDest>
    {
        public MapperConfigurationTestContainer()
          :  base("sTest")
        {

        }

        public int GetIgnoreCount()
        {
            return PropertiesToIgnore.Count;
        }

        public int GetAfterMapActionCount()
        {
            return ActionsAfterMap.Count;
        }

        public MapperConfigurationBase GetMapperTest(Type tSource, Type tDest, bool throwExceptionOnNoFound)
        {
            return GetMapper(tSource, tDest, throwExceptionOnNoFound);
        }
        public void CheckAndConfigureMappingTest(Tuple<Expression, Expression, bool,string> configExpression)
        {
            CheckAndConfigureMapping( ref configExpression);
        }
        public PropertyInfo GetPropertyInfoTest(LambdaExpression expression)
        {
            return GetPropertyInfo(expression);
        }

        public void CreateCommonMemberTest()
        {
            CreateCommonMember();
        }
        public void CheckAndRemoveMemberDestTest(string propertyName)
        {
            CheckAndRemoveMemberDest(propertyName);
        }


        public void CreateMemberAssignementForExistingTargetTest()
        {
            CreateMemberAssignementForExistingTarget();
        }

        internal override void CreateMappingExpression(Func<Type, object> constructor)
        {
            base.CreateMappingExpression(constructor);
        }

        public Delegate GetDelegateForExistingTargetTest()
        {
            return GetDelegateForExistingTarget();
        }
    }
}
