namespace SolvencyII.Domain
{
		public class TreeViewData
		{

			public int ModuleID { get; set; }
			public int FrameworkID { get; set; }
			public int TemplateOrTableID { get; set; }
            public int FilingTemplateOrTableID { get; set; }
			public int CalcTemplateOrTableID { get; set; }
			public int TableID { get; set; }
			public string FrameworkCode { get; set; }
			public string TaxonomyCode { get; set; }
			public string ModuleCode { get; set; }
			public string ModuleLabel { get; set; }
			public string TemplateVariant { get; set; }
			public string TemplateVariantLabel { get; set; }
			public string BusinessTable { get; set; }
			public string BusinessTableLocationRange { get; set; }
			public string BusinessTableLabel { get; set; }
			public string AnnotatedTable { get; set; }
			public string AnnotatedTableLocationRange { get; set; }
			public string AnnotatedTableLabel { get; set; }
			public string TableCode { get; set; }
			public string CalcTableLabel { get; set; }
			public string CalcTableCode { get; set; }
			public bool IsTyped { get; set; }
			public string WhatToShow { get; set; }
            public string Version { get; set; }

	}
}

