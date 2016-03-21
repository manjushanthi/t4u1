using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Validation.Domain;

namespace SolvencyII.Validation.Query
{
    public class SqliteValidationQuery : IValidationQuery
    {
        public IEnumerable<ValidationMessage> GetArelleValidationErrors(ISolvencyData _conn, long instanceID)
        {
            int maxLog = 100;
            StringBuilder sb = new StringBuilder();
            sb.Append("select m.MessageID, m.SequenceInReport, r.DataPointSignature, m.MessageCode, m.MessageLevel, m.Value ");
            //sb.Append("from dMessage m, dMessageReference r ");
            sb.Append(" from dMessage m left join dMessageReference r ");
            sb.Append(" on m.MessageID = r.MessageID ");
            sb.Append(string.Format("where m.InstanceID = {0} and m.MessageLevel in('error','critical', 'warning') ", instanceID));
            //sb.Append(string.Format("where m.MessageID = r.MessageID and m.InstanceID = {0} and MessageLevel in('error','critical') ", instanceID));
            sb.Append(string.Format(" limit {0}", maxLog));

            return _conn.Query<ValidationMessage>(sb.ToString());

        }

        public IEnumerable<ValidationMessage> GetETLErrors(ISolvencyData _conn, long instanceID)
        {

            int maxLog = 100;
            StringBuilder sb = new StringBuilder();
            sb.Append("select m.MessageID, m.SequenceInReport, r.DataPointSignature, m.MessageCode, m.MessageLevel, m.Value as 'Description' ");
            //sb.Append("from dMessage m, dMessageReference r ");
            sb.Append(" from dMessage m left join dMessageReference r ");
            sb.Append(" on m.MessageID = r.MessageID ");
            sb.Append(string.Format("where m.InstanceID = {0} and MessageCode='EtlError'  ", instanceID));
            //sb.Append(string.Format("where m.MessageID = r.MessageID and m.InstanceID = {0} and MessageLevel in('error','critical') ", instanceID));
            sb.Append(string.Format(" limit {0}", maxLog));

            return _conn.Query<ValidationMessage>(sb.ToString());

        }

        public IEnumerable<vValidationRuleSQL> CrossTableValidationScripts(ISolvencyData _conn, int[] tableIDs)
        {
            StringBuilder query = new StringBuilder();
            query.Append(@"select vrs.SQL, vrs.CELLS, vr.ValidationRuleID, vr.Severity ");
            query.Append("from vValidationRule vr ");
            query.Append("inner join (select distinct vs.ValidationRuleID ");
            query.Append("from vValidationScope vs ");
            query.Append("where vs.TableID in ( @TABLE_IDS ) ");
            query.Append("group by vs.ValidationRuleID ");
            query.Append("having count( vs.TableID ) > 1 ");
            query.Append("and count( vs.TableID ) = ");
            query.Append("(select count( vs.TableID ) from vValidationScope e where vs.ValidationRuleID = e.ValidationRuleID) ");
            query.Append(") ss on ss.ValidationRuleID = vr.ValidationRuleID ");
            query.Append("inner join vValidationRuleSQL vrs on vrs.ValidationRuleID = vr.ValidationRuleID");

            StringBuilder sb = new StringBuilder();
            foreach (int tbaId in tableIDs)
                sb.Append(tbaId).Append(" ,");

            string tabs = sb.ToString().TrimEnd(new char[] { ',' });
            query = query.Replace("@TABLE_IDS", tabs);

            return _conn.Query<vValidationRuleSQL>(query.ToString());
        }

        public IEnumerable<vIntraTableSQL> IntraTableValidationScripts(ISolvencyData _conn, int[] tableIDs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int tbaId in tableIDs)
                sb.Append(tbaId).Append(" ,");

            string tabs = sb.ToString().TrimEnd(new char[] { ',' });

            string query = string.Format(@"select * from vIntraTableSQL its where its.TableID in ( {0} )", tabs);

            return _conn.Query<vIntraTableSQL>(query);
        }
    }
}
