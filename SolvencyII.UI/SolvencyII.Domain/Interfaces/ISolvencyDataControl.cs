using System;
using System.Drawing;
using SolvencyII.Domain.ENumerators;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface interherited from ISolvencyControl defining a data related control
    /// </summary>
    public interface ISolvencyDataControl : ISolvencyControl
    {
        bool IsValid();
        SolvencyDataType ColumnType { get; set; }
        
        string ColName { get; set; } 
        string TableName { get; set; }
        string Name { get; set; } // Gets easy access to base Name without need to cast.
        
        Rectangle Bounds { get; set; }
        string Text { get; set; }
        string TrueValue { get; }
        object Result { get; set; }
        bool Enabled { get; set; }

        bool IsRowKey { get; set; }

        event EventHandler TextChanged;

        Size Size { get; set; }
        Point Location { get; set; }
        void Dispose();

        ISolvencyDataControl DeepCopy();



    }
}
