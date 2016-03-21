using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DpmDB;
using AT2DPM.DAL;
using AT2DPM.DAL.ExtendedModel;
using AT2DPM.DAL.Model;

namespace T4U.CRT.Generation.Model
{
    /// <summary>
    /// Produces dimensional characteristics basing on 
    /// </summary>
    internal class DimCharacteristicsFactory
    {        
        internal DimCharacteristic produceDimCharacteristic(mOrdinateCategorisation ordCategorisation)
        {
            if (ordCategorisation == null)
                throw new ArgumentNullException();

            DimCharacteristic newDimChar = new DimCharacteristic
            {
                DimCode = ordCategorisation.mDimension.DimensionXBRLCode,
                DomCode = ordCategorisation.mDimension.mDomain.DomainXBRLCode
            };

            if(ordCategorisation.mMember != null && ordCategorisation.mMember.MemberID != ProcesorGlobals.TypedMemberId)         
                newDimChar.MemCodes.Add(ordCategorisation.mMember.MemberXBRLCode);            

            return newDimChar;
        }

        internal DimCharacteristic produceDimCharacteristic(mDimension dim, mMember mem)
        {
            if(dim == null || mem == null || string.IsNullOrEmpty(dim.DimensionXBRLCode)
                || string.IsNullOrEmpty(mem.MemberXBRLCode) || string.IsNullOrEmpty(dim.mDomain.DomainXBRLCode))
                throw new ArgumentException();

            DimCharacteristic newDc = new DimCharacteristic();
            newDc.DimCode = dim.DimensionXBRLCode;
            newDc.DomCode = dim.mDomain.DomainXBRLCode;
            newDc.MemCodes.Add(mem.MemberXBRLCode);

            return newDc;
        }
    }
}
