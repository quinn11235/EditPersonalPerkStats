
using System;
using System.Linq;
using System.Reflection;
using Base.Defs;
using UnityEngine;

namespace EditPersonalPerkStats
{
    internal static class EPPS_RepoUtil
    {
        /// <summary>
        /// Tries multiple strategies to obtain the live DefRepository:
        /// 1) GameUtl.GameComponent&lt;DefRepository&gt;()
        /// 2) Resources.FindObjectsOfTypeAll&lt;DefRepository&gt;()
        /// 3) Any public static property/field of type DefRepository on loaded types (best-effort)
        /// Returns null if not available yet.
        /// </summary>
        public static DefRepository FindRepo(out string how)
        {
            how = null;

            // 1) GameUtl.GameComponent<DefRepository>()
            try
            {
                var gameUtl = Type.GetType("PhoenixPoint.Common.Core.GameUtl")
                           ?? AppDomain.CurrentDomain.GetAssemblies()
                                 .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
                                 .FirstOrDefault(t => t.FullName == "PhoenixPoint.Common.Core.GameUtl");
                if (gameUtl != null)
                {
                    var mi = gameUtl.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                    .FirstOrDefault(m => m.IsGenericMethodDefinition && m.Name == "GameComponent" && m.GetParameters().Length == 0);
                    if (mi != null)
                    {
                        var obj = mi.MakeGenericMethod(typeof(DefRepository)).Invoke(null, null) as DefRepository;
                        if (obj != null) { how = "GameUtl.GameComponent"; return obj; }
                    }
                }
            } catch { /* swallow */ }

            // 2) Resources.FindObjectsOfTypeAll<DefRepository>()
            try
            {
                var mi = typeof(Resources).GetMethods(BindingFlags.Public | BindingFlags.Static)
                                          .FirstOrDefault(m => m.IsGenericMethodDefinition && m.Name == "FindObjectsOfTypeAll" && m.GetParameters().Length == 0);
                if (mi != null)
                {
                    var g = mi.MakeGenericMethod(typeof(DefRepository));
                    var arr = g.Invoke(null, null) as Array;
                    if (arr != null && arr.Length > 0)
                    {
                        var repo = arr.GetValue(0) as DefRepository;
                        if (repo != null) { how = "Resources.FindObjectsOfTypeAll"; return repo; }
                    }
                }
            } catch { /* swallow */ }

            // 3) Any static property/field on loaded types
            try
            {
                foreach (var t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } }))
                {
                    // Public static property
                    var props = t.GetProperties(BindingFlags.Static | BindingFlags.Public);
                    for (int i = 0; i < props.Length; i++)
                    {
                        var p = props[i];
                        if (p.PropertyType == typeof(DefRepository) && p.CanRead)
                        {
                            try
                            {
                                var val = (DefRepository)p.GetValue(null, null);
                                if (val != null) { how = t.FullName + "." + p.Name; return val; }
                            } catch { }
                        }
                    }
                    // Public static field
                    var fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                    for (int i = 0; i < fields.Length; i++)
                    {
                        var f = fields[i];
                        if (f.FieldType == typeof(DefRepository))
                        {
                            try
                            {
                                var val = (DefRepository)f.GetValue(null);
                                if (val != null) { how = t.FullName + "." + f.Name; return val; }
                            } catch { }
                        }
                    }
                }
            } catch { /* swallow */ }

            return null;
        }
    }
}
