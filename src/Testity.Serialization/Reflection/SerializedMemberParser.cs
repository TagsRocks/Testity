﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Fasterflect;

namespace Testity.Serialization
{
	public static class SerializedMemberParser<TMemberInfoType, TTypeToParse>
		where TMemberInfoType : MemberInfo where TTypeToParse : class
    {
		/// <summary>
		/// Caches the functional 1 to 1 relationship between a Type and its <see cref="MemberTypes"/>.
		/// </summary>
		private static readonly IDictionary<Type, MemberTypes> cacheTypeMemberMap
			= new Dictionary<Type, MemberTypes>(Enum.GetValues(typeof(MemberTypes)).Length)
			{
				{typeof(FieldInfo), MemberTypes.Field},
				{typeof(PropertyInfo), MemberTypes.Property}
			};

		/// <summary>
		/// Creates a collection of <see cref="IEnumerable{TMemberInfoType}"/> that reference the <see cref="TTypeToParse"/> members.
		/// These members will also be decorated by the <see cref="ExposeDataMemeberAttribute"/>.
		/// </summary>
		/// <returns>A collection of <typeparamref name="MemberInfoType"/></returns>
		public static IEnumerable<TMemberInfoType> Parse()
		{
			return typeof(TTypeToParse).MembersWith<ExposeDataMemeberAttribute>(cacheTypeMemberMap[typeof(TMemberInfoType)], Flags.InstanceAnyVisibility) //get all members from current and base classes
				.Cast<TMemberInfoType>();
		}
	}
}