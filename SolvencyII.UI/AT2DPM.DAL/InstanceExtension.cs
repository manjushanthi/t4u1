using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class dInstance
    {
        /// <summary>
        /// To create or get default instance                                                                                                                                                                                                          
        /// </summary>
        /// <param name="contex"></param>
        /// <returns></returns>
        public static dInstance GetOrCreateXBRTDefaultIntance(DPMdb contex, string identifier, string entityName)
        {
            dInstance inst = (from i in contex.dInstances 
                              where i.EntityIdentifier == identifier &&
                              i.EntityScheme == "MSEXCEL"
                              select i).FirstOrDefault();

            if (inst == null)
            {
                long maxInstanceID;

                try
                {
                    maxInstanceID = contex.dInstances.Select(t => t.InstanceID).Max();
                }
                catch (Exception) { maxInstanceID = 1000; }

                inst = new dInstance();
                inst.InstanceID = ++maxInstanceID;
                inst.EntityName = entityName;
                inst.FileName = entityName;
                inst.EntityScheme = "MSEXCEL";
                inst.EntityIdentifier = identifier;

                contex.dInstances.Add(inst);
                contex.SaveChanges();
            }

            return inst;
        }

        /// <summary>
        /// Get default instance
        /// </summary>
        /// <param name="contex"></param>
        /// <returns></returns>

        public static dInstance GetXBRTDefaultIntance(DPMdb contex, string identifier)
        {
            dInstance inst = (from i in contex.dInstances
                              where i.EntityIdentifier == identifier &&
                                  i.EntityScheme == "MSEXCEL"
                              select i).FirstOrDefault();

            return inst;
        }

        public static IEnumerable<dInstance> GetAllTemplates(DPMdb contex)
        {
            IEnumerable<dInstance> inst = (from i in contex.dInstances
                                           where i.EntityScheme == "MSEXCEL" && i.EntityIdentifier == "ANNOTATED_TEMPLATE"
                              select i);

            return inst;
        }
    }
}
