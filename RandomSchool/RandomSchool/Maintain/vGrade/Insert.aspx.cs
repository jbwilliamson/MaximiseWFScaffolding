﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool.Extenders;
using RandomSchool.Repositories;
using RandomSchool.FieldTemplates;
using ScaffoldFilter;

namespace RandomSchool.Maintain.vGrade
{
    public partial class Insert : System.Web.UI.Page
    {
		private GradeRepository<RandomSchool.Models.Grade, int> _repository = new GradeRepository<RandomSchool.Models.Grade, int>();

		protected void Page_Init()
        {
            fvGrade.SetDataMethodsObject(_repository);
            fvGrade.RedirectToRouteOnItemInserted("~/Maintain/vGrade/Default");
            fvGrade.RedirectToRouteOnItemCommand("~/Maintain/vGrade/Default");
        }
        
		protected void Page_Load(object sender, EventArgs e)
        {

        }

		public void ForeignKeyEventHandler_LoadForeignKey(ForeignModelEventArgs e)
        {
            e.returnResults = _repository.GetForeignList(e.foreignKeyModel, e.keyType);
        }

        protected void dcForeignKey_Load(object sender, EventArgs e)
        {
            System.Web.DynamicData.DynamicControl dc = (System.Web.DynamicData.DynamicControl)sender;
            ForeignKey_EditField fkef = (ForeignKey_EditField)dc.FieldTemplate;
			
			if (fkef != null)
			{
				fkef.ForeignKey += new ForeignKeyEventHandler(ForeignKeyEventHandler_LoadForeignKey);
			}
        }
    }
}
