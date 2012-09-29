using System;
using System.Linq;
using Graphics.GUI.Controls;

namespace Graphics.GUI
{
    public static class ReflectionHelper
    {
        #region Reflection Methods
        
        /// <summary>
        /// Gets a Property Value from a passed object based on name sent
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="name">Name of Property</param>
        /// <returns>Object</returns>
        public static object GetPropertyValue(object control, string name)
        {
            //Check if our Property exists if it doesn't throw exception
            if (control.GetType().GetProperties().Where(p => p.Name == name).Count() > 0)
                return control.GetType().GetProperty(name).GetValue(control, null); //Return our Value

            var c = (ControlBase)control;
            throw new Exception("Reflection failed. Could not find Property '" + name + "' in Control '" + c.Name + "'");
        }

        /// <summary>
        /// Sets a Property Value based on information passed
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="name">Name of Property</param>
        /// <param name="data">Data being set</param>
        public static void SetPropertyValue(object control, string name, object data)
        {
            //Check our property exists before setting 
            if (control.GetType().GetProperties().Where(p => p.Name == name).Count() > 0)
            {
                control.GetType().GetProperty(name).SetValue(control, data, null);
                return;
            }

            //Throw exception because Property doesn't exist
            var c = (ControlBase)control;
            throw new Exception("Reflection failed. Could not set Property '" + name + "' in Control '" + c.Name + "'");
        }
        
        #endregion
    }
}
