using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Jh.Abp.Application.Contracts.Permissions
{
    public static class CrudPermissions
    {
        public const string GroupName = "CrudBase";

        public static class Cruds
        {
            public const string Default = GroupName + ".Cruds";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CrudPermissions));
        }
    }
}
