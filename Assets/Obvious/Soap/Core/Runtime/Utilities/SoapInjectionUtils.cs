using System;
using System.Collections.Generic;
using System.Reflection;
using Obvious.Soap.Attributes;
using UnityEngine;

namespace Obvious.Soap
{
    // public static class SoapInjectionUtils
    // {
    //     private static readonly Dictionary<(Type, string), List<FieldInfo>> _cachedInjectableFields =
    //         new Dictionary<(Type, string), List<FieldInfo>>();
    //
    //     public static void InjectInChildren<TVariable>(GameObject gameObject,
    //         TVariable runtimeVariable, string id, bool debugLogEnabled)
    //         where TVariable : ScriptableObject
    //     {
    //         var components = gameObject.GetComponentsInChildren<MonoBehaviour>();
    //         foreach (var component in components)
    //         {
    //             if (component == null)
    //                 continue;
    //
    //             var type = component.GetType();
    //             var cacheKey = (type, variableName: id);
    //             if (!_cachedInjectableFields.TryGetValue(cacheKey, out var fields))
    //             {
    //                 fields = new List<FieldInfo>();
    //                 foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic |
    //                                                      BindingFlags.Public))
    //                 {
    //                     if (Attribute.IsDefined(field, typeof(RuntimeInjectableAttribute)) &&
    //                         field.FieldType == typeof(TVariable))
    //                     {
    //                         // Check if the field has the correct variable name
    //                         var attribute = field.GetCustomAttribute<RuntimeInjectableAttribute>();
    //                         if (attribute.Id != id || string.IsNullOrEmpty(attribute.Id))
    //                             continue;
    //
    //                         fields.Add(field);
    //                     }
    //                 }
    //                 
    //                 _cachedInjectableFields[cacheKey] = fields;
    //             }
    //             
    //             foreach (var field in fields)
    //             {
    //                 field.SetValue(component, runtimeVariable);
    //                 if (debugLogEnabled)
    //                 {
    //                     Debug.Log(
    //                         $"<color=#f75369>[Injected]</color> {runtimeVariable.name} into {component.GetType().Name}.{field.Name}");
    //                 }
    //             }
    //         }
    //     }
    // }
}